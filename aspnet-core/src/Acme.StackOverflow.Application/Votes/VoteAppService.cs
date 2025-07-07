using System;
using System.Threading.Tasks;
using Acme.StackOverflow;
using Acme.StackOverflow.Votes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

public class VoteAppService : CrudAppService<Vote, VoteDto, Guid, ListRequestDto, CreateUpdateVoteDto, CreateUpdateVoteDto>, IVoteAppService
{
    public VoteAppService(IRepository<Vote, Guid> repository) : base(repository)
    {
    }

    public override async Task<VoteDto> CreateAsync(CreateUpdateVoteDto input)
    {
        var existingVote = await Repository.FirstOrDefaultAsync(v =>
            v.PostId == input.PostId && v.AppUserId == input.AppUserId);

        if (existingVote == null)
        {
            var vote = new Vote
            {
                PostId = input.PostId,
                AppUserId = input.AppUserId,
                VoteType = input.VoteType
            };

            await Repository.InsertAsync(vote);
            return ObjectMapper.Map<Vote, VoteDto>(vote);
        }
        else
        {
            if (existingVote.VoteType != input.VoteType)
            {
                existingVote.VoteType = input.VoteType;
                await Repository.UpdateAsync(existingVote);
            }

            return ObjectMapper.Map<Vote, VoteDto>(existingVote);
        }
    }

    public async Task<VoteCountDto> GetVoteCountByPostIdAsync(Guid postId)
    {
        var votes = await Repository.GetQueryableAsync(); // get IQueryable<Vote>


        var upvoteCount = await votes.CountAsync(v => v.PostId == postId && v.VoteType == VoteType.Upvote);
        var downvoteCount = await votes.CountAsync(v => v.PostId == postId && v.VoteType == VoteType.Downvote);

        return new VoteCountDto
        {
            UpvoteCount = upvoteCount,
            DownvoteCount = downvoteCount
        };
    }
    public async Task UnvoteAsync(Guid postId, Guid appUserId)
    {
        var vote = await Repository.FirstOrDefaultAsync(v => v.PostId == postId && v.AppUserId == appUserId);

        if (vote != null)
        {
            await Repository.DeleteAsync(vote, true); // true => hard delete

        }
    }

}



