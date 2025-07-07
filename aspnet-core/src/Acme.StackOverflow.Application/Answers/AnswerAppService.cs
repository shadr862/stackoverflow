using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.Comments;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.StackOverflow.Answers
{
    public class AnswerAppService:CrudAppService<Answer,AnswerDto,Guid,ListRequestDto,CreateUpdateAnswerDto,CreateUpdateAnswerDto>,IAnswerAppService

    {
        public AnswerAppService(IRepository<Answer,Guid> repository):base(repository)
        { 
        }

        public async Task<List<AnswerDto>> GetAnswersByPostIdAsync(Guid PostId)
        {
            // Get IQueryable to apply LINQ
            var queryable = await Repository.GetQueryableAsync();

            var query = queryable
                .Where(c => c.PostId == PostId);

            var comments = await query.ToListAsync();

            return ObjectMapper.Map<List<Answer>, List<AnswerDto>>(comments);
        }

    }
}
