using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow.Votes
{
    public class VoteDto:AuditedEntityDto<Guid>
    {
        public Guid PostId { get; set; }
        public Guid AppUserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
