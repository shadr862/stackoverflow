using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.StackOverflow.Comments
{
    public class CreateUpdateCommentDto
    {
        public Guid PostId { get; set; }
        public Guid AppUserId { get; set; }
        public string Name { get; set; }
        public string CommentText { get; set; }
        public DateTime Created { get; set; }
    }
}
