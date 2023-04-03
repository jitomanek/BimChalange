using Bim.Core.Entity.Models;

namespace Bim.Core.Models.View
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get;set; }
        public TaskStatusEnum Status { get; set; }
    }
}
