using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.StackOverflow.Tags
{
    public class CreateUpdateTagDto
    {
        [Required]
        public string TagName { get; set; }

        public string TagDescription { get; set; }
    }
}
