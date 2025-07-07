using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.StackOverflow.Tags
{
    public class TagAppService :
        CrudAppService<Tag, TagDto, Guid, ListRequestDto, CreateUpdateTagDto>,
        ITagAppService
    {
        public TagAppService(IRepository<Tag, Guid> repository)
            : base(repository)
        {
        }
    }
}
