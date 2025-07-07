using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.StackOverflow.PostTags;
using Acme.StackOverflow.Tags;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Acme.StackOverflow.Posts
{
    public class PostAppService : CrudAppService<
        Post,
        PostDto,
        Guid,
        ListRequestDto,
        CreateUpdatePostDto>,
        IPostAppService
    {
        private readonly IRepository<PostTag, Guid> _postTagRepo;
        private readonly IRepository<Tag, Guid> _tagRepo;

        public PostAppService(
            IRepository<Post, Guid> repository,
            IRepository<PostTag, Guid> postTagRepo,
            IRepository<Tag, Guid> tagRepo)
            : base(repository)
        {
            _postTagRepo = postTagRepo;
            _tagRepo = tagRepo;
        }

        public override async Task<PostDto> CreateAsync(CreateUpdatePostDto input)
        {
            var post = ObjectMapper.Map<CreateUpdatePostDto, Post>(input);
        

            await Repository.InsertAsync(post, autoSave: true);

            foreach (var tagId in input.TagIds)
            {
                var postTag = new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                };

                await _postTagRepo.InsertAsync(postTag);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return await GetAsync(post.Id); // ✅ Reuse GetAsync
        }

        public override async Task<PostDto> UpdateAsync(Guid id, CreateUpdatePostDto input)
        {
            var post = await Repository.GetAsync(id);
            ObjectMapper.Map(input, post); // Use simple map since entity already loaded

            await Repository.UpdateAsync(post);

            var oldTags = await _postTagRepo.GetListAsync(pt => pt.PostId == id);
            foreach (var pt in oldTags)
            {
                await _postTagRepo.DeleteAsync(pt);
            }

            foreach (var tagId in input.TagIds)
            {
                await _postTagRepo.InsertAsync(new PostTag
                {
                    PostId = id,
                    TagId = tagId
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return await GetAsync(id); // ✅ Reuse GetAsync
        }

        
        public async Task<PostDto> SetAcceptedAnswerAsync(Guid postId, Guid? acceptedAnswerId)
        {
            var post = await Repository.GetAsync(postId);
            post.AcceptedAnswerId = acceptedAnswerId; // can be null to unaccept

            await Repository.UpdateAsync(post);
            await CurrentUnitOfWork.SaveChangesAsync();

            return await GetAsync(postId);
        }



        public override async Task<PostDto> GetAsync(Guid id)
        {
            var post = await Repository.GetAsync(id);

            var postDto = ObjectMapper.Map<Post, PostDto>(post);

            var postTags = await _postTagRepo.GetListAsync(pt => pt.PostId == id);
            var tagIds = postTags.Select(pt => pt.TagId).ToList();
            var tags = await _tagRepo.GetListAsync(t => tagIds.Contains(t.Id));

            postDto.Tags = ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);

            return postDto;
        }

        public override async Task<PagedResultDto<PostDto>> GetListAsync(ListRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();

            var totalCount = await queryable.CountAsync();

            var posts = await queryable
                .OrderByDescending(p => p.Id) // Add OrderBy to avoid split-query issue
                .Take(input.MaxResultCount)
                .ToListAsync();

            var dtoList = new List<PostDto>();

            foreach (var post in posts)
            {
                var postDto = ObjectMapper.Map<Post, PostDto>(post);

                var postTags = await _postTagRepo.GetListAsync(pt => pt.PostId == post.Id);
                var tagIds = postTags.Select(pt => pt.TagId).ToList();
                var tags = await _tagRepo.GetListAsync(t => tagIds.Contains(t.Id));

                postDto.Tags = ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);

                dtoList.Add(postDto);
            }

            return new PagedResultDto<PostDto>(totalCount, dtoList);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var postTags = await _postTagRepo.GetListAsync(pt => pt.PostId == id);
            foreach (var pt in postTags)
            {
                await _postTagRepo.DeleteAsync(pt);
            }

            await Repository.DeleteAsync(id);
        }

        private async Task<PostDto> MapPostWithTagsToDtoAsync(Post post)
        {
            var dto = ObjectMapper.Map<Post, PostDto>(post);

            var postTags = await _postTagRepo.GetListAsync(pt => pt.PostId == post.Id);
            var tagIds = postTags.Select(pt => pt.TagId).ToList();
            var tags = await _tagRepo.GetListAsync(t => tagIds.Contains(t.Id));

            dto.Tags = ObjectMapper.Map<List<Tag>, List<TagDto>>(tags);

            return dto;
        }


        public async Task<List<PostDto>> GetAnswersAsync(Guid questionId)
        {
            var queryable = await Repository.GetQueryableAsync();

            var answers = await queryable
                .Where(p => p.PostType == PostType.Answer && p.ParentId == questionId)
                .ToListAsync();

            var answerDtos = new List<PostDto>();
            foreach (var answer in answers)
            {
                var dto = await MapPostWithTagsToDtoAsync(answer); // reuse existing method
                answerDtos.Add(dto);
            }

            return answerDtos;
        }

        public async Task<List<PostDto>> GetPostsByUserIdAsync(Guid appUserId)
        {
            var queryable = await Repository.GetQueryableAsync();

            var userPosts = await queryable
                .Where(p => p.AppUserId == appUserId)
                .ToListAsync();

            var postDtos = new List<PostDto>();

            foreach (var post in userPosts)
            {
                var dto = await MapPostWithTagsToDtoAsync(post);
                postDtos.Add(dto);
            }

            return postDtos;
        }


        public async Task<List<PostDto>> SearchPostsAsync(string keyword)
        {
            keyword = keyword?.ToLower() ?? string.Empty;

            var postQueryable = await Repository.GetQueryableAsync();
            var postTagQueryable = await _postTagRepo.GetQueryableAsync();
            var tagQueryable = await _tagRepo.GetQueryableAsync();

            // Get matching tag IDs first
            var matchingTagIds = await tagQueryable
                .Where(t => EF.Functions.Like(t.TagName.ToLower(), $"%{keyword}%"))
                .Select(t => t.Id)
                .ToListAsync();

            // Get post IDs that have those tags
            var postIdsWithMatchingTags = await postTagQueryable
                .Where(pt => matchingTagIds.Contains(pt.TagId))
                .Select(pt => pt.PostId)
                .Distinct()
                .ToListAsync();

            // Filter posts by title or by tags
            var filteredPostsQuery = postQueryable
                .Where(p => EF.Functions.Like(p.Title.ToLower(), $"%{keyword}%")
                         || postIdsWithMatchingTags.Contains(p.Id));

            // Project posts to PostDto WITHOUT tags first
            var posts = await filteredPostsQuery
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    AppUserId = p.AppUserId,
                    PostType = p.PostType,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    AcceptedAnswerId = p.AcceptedAnswerId,
                    Title = p.Title,
                    Body = p.Body,
                    Created = p.Created,
                    Tags = new List<TagDto>() // will fill after
                })
                .ToListAsync();

            // Load tags for all filtered posts at once
            var postIds = posts.Select(p => p.Id).ToList();

            var postTags = await postTagQueryable
                .Where(pt => postIds.Contains(pt.PostId))
                .ToListAsync();

            var tagIds = postTags.Select(pt => pt.TagId).Distinct().ToList();

            var tags = await tagQueryable
                .Where(t => tagIds.Contains(t.Id))
                .Select(t => new TagDto { Id = t.Id, TagName = t.TagName })
                .ToListAsync();

            // Map tags to posts
            foreach (var postDto in posts)
            {
                var tagIdsForPost = postTags
                    .Where(pt => pt.PostId == postDto.Id)
                    .Select(pt => pt.TagId)
                    .ToList();

                postDto.Tags = tags
                    .Where(t => tagIdsForPost.Contains(t.Id))
                    .ToList();
            }

            return posts;
        }









    }
}

