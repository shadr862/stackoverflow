using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow.Comments
{
    public class CommentDto:AuditedEntityDto<Guid>
    {
        public Guid PostId { get; set; }
        public Guid AppUserId { get; set; }
        public string Name { get; set; }
        public string CommentText { get; set; }
        public DateTime Created { get; set; }
    }
}
