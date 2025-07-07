using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Acme.StackOverflow.Posts
{
    public class CreateUpdatePostDto
    {
        public Guid AppUserId { get; set; }
        public PostType PostType { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? AcceptedAnswerId { get; set; }
        public string? Title { get; set; }
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public List<Guid> TagIds { get; set; } = new();
    }
}
