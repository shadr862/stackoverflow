using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow.Tags
{
    public class TagDto : AuditedEntityDto<Guid>
    {
        public string TagName { get; set; }
        public string TagDescription { get; set; }
    }
}
