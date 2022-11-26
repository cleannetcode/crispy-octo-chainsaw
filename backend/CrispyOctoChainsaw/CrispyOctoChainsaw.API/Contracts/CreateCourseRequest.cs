using CrispyOctoChainsaw.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for create course
    public class CreateCourseRequest
    {
        [Required]
        [StringLength(Course.MaxTitleLength)]
        [FromForm(Name = "title")]
        public string? Title { get; set; }

        [Required]
        [StringLength(Course.MaxDescriptionsLength)]
        [FromForm(Name = "description")]
        public string? Description { get; set; }

        [Required]
        [StringLength(Course.MaxRepositoryNameLength)]
        [FromForm(Name = "repositoryName")]
        public string? RepositoryName { get; set; }

        [FromForm(Name = "image")]
        public IFormFile ImageFile { get; set; }
    }
}
