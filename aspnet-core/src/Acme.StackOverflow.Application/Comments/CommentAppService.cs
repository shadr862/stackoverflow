using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Acme.StackOverflow.Comments
{
    public class CommentAppService :
        CrudAppService<
            Comment,
            CommentDto,
            Guid,
            ListRequestDto,
            CreateUpdateCommentDto,
            CreateUpdateCommentDto>,
        ICommentAppService
    {
        public CommentAppService(IRepository<Comment, Guid> repository)
            : base(repository)
        {
        }

        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(Guid PostId)
        {
            // Get IQueryable to apply LINQ
            var queryable = await Repository.GetQueryableAsync();

            var query = queryable
                .Where(c => c.PostId == PostId);

            var comments = await query.ToListAsync();

            return ObjectMapper.Map<List<Comment>, List<CommentDto>>(comments);
        }


    }


}

