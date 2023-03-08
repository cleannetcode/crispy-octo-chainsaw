using CrispyOctoChainsaw.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for edit course.
    public class EditCourseRequest
    {
        [Required]
        [StringLength(Course.MaxTitleLength)]
        [FromForm(Name = "title")]
        public string Title { get; set; }

        [Required]
        [StringLength(Course.MaxDescriptionsLength)]
        [FromForm(Name = "description")]
        public string Description { get; set; }

        [Required]
        [StringLength(Course.MaxRepositoryNameLength)]
        [FromForm(Name = "repositoryName")]
        public string RepositoryName { get; set; }

        [FromForm(Name = "image")]
        public IFormFile ImageFile { get; set; }
    }
}
