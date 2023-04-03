using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bim.Core.Entity.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(128) not null")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Description { get; set; }
        [Range(1, 5)]
        public int Priority { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
