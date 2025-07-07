using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.StackOverflow.Comments
{
    public interface ICommentAppService :
        ICrudAppService<CommentDto, Guid, ListRequestDto,CreateUpdateCommentDto,CreateUpdateCommentDto>
    {
    }
}
