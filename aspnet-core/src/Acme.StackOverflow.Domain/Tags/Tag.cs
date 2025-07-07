using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.PostTags;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.StackOverflow.Tags
{
    public class Tag : AggregateRoot<Guid>
    {
        public string TagName { get; set; }
        public string TagDescription { get; set; }

        public ICollection<PostTag> PostTags { get; set; }
    }


}
