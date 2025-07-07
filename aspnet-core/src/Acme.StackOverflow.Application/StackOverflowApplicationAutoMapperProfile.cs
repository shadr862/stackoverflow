using Acme.StackOverflow.Answers;
using Acme.StackOverflow.AppUsers;
using Acme.StackOverflow.Comments;
using Acme.StackOverflow.Posts;
using Acme.StackOverflow.Tags;
using Acme.StackOverflow.Votes;
using AutoMapper;

namespace Acme.StackOverflow;

public class StackOverflowApplicationAutoMapperProfile : Profile
{
    public StackOverflowApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // AppUser <-> AppUserDto
        CreateMap<AppUser, AppUserDto>();
        CreateMap<CreateUpdateAppUserDto, AppUser>();

        CreateMap<Tag, TagDto>();
        CreateMap<CreateUpdateTagDto, Tag>();

        CreateMap<Post, PostDto>();
        CreateMap<CreateUpdatePostDto, Post>();

        CreateMap<Comment,CommentDto>();
        CreateMap<CreateUpdateCommentDto, Comment>();

        CreateMap<Vote,VoteDto>();
        CreateMap<CreateUpdateVoteDto, Vote>();

        CreateMap<Answer, AnswerDto>();
        CreateMap<CreateUpdateAnswerDto,Answer>();
    }
}
