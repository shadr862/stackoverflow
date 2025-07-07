using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.AppUsers;
using Acme.StackOverflow.Posts;
using Volo.Abp.Domain.Entities;

namespace Acme.StackOverflow.Answers
{
    public class Answer:AggregateRoot<Guid>
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Name { get; set; }
        public string AnswerText { get; set; }
        public DateTime Created { get; set; }
    }
}
