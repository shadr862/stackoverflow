using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.Comments;
using Volo.Abp.Application.Services;

namespace Acme.StackOverflow.Answers
{
    public interface IAnswerAppService:ICrudAppService<AnswerDto, Guid, ListRequestDto, CreateUpdateAnswerDto, CreateUpdateAnswerDto>
    {
    }
    
}
