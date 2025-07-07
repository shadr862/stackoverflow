using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.StackOverflow.Votes
{
    public class CreateUpdateVoteDto
    {
        public Guid PostId { get; set; }
        public Guid AppUserId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
