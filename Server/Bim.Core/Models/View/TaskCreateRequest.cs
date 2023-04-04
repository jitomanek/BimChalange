using Bim.Core.Entity.Models;

using System.ComponentModel.DataAnnotations;

namespace Bim.Core.Models.View
{
    public class TaskCreateRequest
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        [Range(1,5)]
        public int? Priority { get; set; }
        [Required]
        public TaskStatusEnum? Status { get; set; }
    }
}
