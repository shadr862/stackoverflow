using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.StackOverflow.Comments;
using Acme.StackOverflow.PostTags;
using Acme.StackOverflow.Votes;
using Acme.StackOverflow.Answers;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.StackOverflow.AppUsers;
using Volo.Abp.Domain.Entities;

namespace Acme.StackOverflow.Posts
{
    public class Post : AggregateRoot<Guid>
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Name { get; set; }

        public PostType PostType { get; set; }

        // Self-referencing parent (e.g., question is the parent of answer)
        public Guid? ParentId { get; set; }
        public Post Parent { get; set; }
        public ICollection<Post> ChildPosts { get; set; }

        // Accepted answer navigation
        public Guid? AcceptedAnswerId { get; set; }
      
        public string? Title { get; set; }
        public string Body { get; set; }
        public DateTime Created {  get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }


}
