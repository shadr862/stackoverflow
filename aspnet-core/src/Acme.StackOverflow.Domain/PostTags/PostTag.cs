using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.Posts;
using Acme.StackOverflow.Tags;
using Volo.Abp.Domain.Entities;

namespace Acme.StackOverflow.PostTags
{
    public class PostTag : AggregateRoot<Guid>
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }


}
