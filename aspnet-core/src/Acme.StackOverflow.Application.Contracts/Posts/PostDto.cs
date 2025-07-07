using System;
using Acme.StackOverflow.Tags;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow.Posts
{
    public class PostDto : AuditedEntityDto<Guid>
    {
        public Guid AppUserId { get; set; }
        public PostType PostType { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? AcceptedAnswerId { get; set; }
        public string? Title { get; set; }
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public List<TagDto> Tags { get; set; } = new();
    }
}
