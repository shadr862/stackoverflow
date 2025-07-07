using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.Posts;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.StackOverflow.AppUsers;
using Volo.Abp.Domain.Entities;

namespace Acme.StackOverflow.Votes
{
    public class Vote : AggregateRoot<Guid>
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public VoteType VoteType { get; set; }
    }

}
