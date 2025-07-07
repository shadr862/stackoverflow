using System;
using System.Collections.Generic;
using Acme.StackOverflow.Comments;
using Acme.StackOverflow.Posts;
using Acme.StackOverflow.Answers;
using Acme.StackOverflow.PostTags;
using Acme.StackOverflow.Votes;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.StackOverflow.AppUsers
{
    public class AppUser : AggregateRoot<Guid>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
     

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }

}



