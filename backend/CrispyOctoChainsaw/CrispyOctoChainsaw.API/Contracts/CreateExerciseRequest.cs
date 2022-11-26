using CrispyOctoChainsaw.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace CrispyOctoChainsaw.API.Contracts
{
    // Contract for create exercise.
    public class CreateExerciseRequest
    {
        [Required]
        [StringLength(Exercise.MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(Exercise.MaxDescriptionsLength)]
        public string Description { get; set; }

        [Required]
        [StringLength(Exercise.MaxBranchNameLength)]
        public string BranchName { get; set; }

        public int CourseId { get; set; }
    }
}
