using System.ComponentModel.DataAnnotations;

namespace Bim.Core.Models.View
{
    public class TaskUpdateRequest : TaskCreateRequest
    {
        [Required]
        public int? Id { get; set; }
    }
}
