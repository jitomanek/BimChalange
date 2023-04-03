using Bim.Core.Entity.Models;

using Custom.Lib.Models.Repository;

namespace Bim.Core.Models.View
{
    public class TaskTableRequest : DataTable
    {
        public TaskTableRequest()
        {
            var o = new TaskEntity();
            this.PropertyNames = new Dictionary<string, string>
            {
                { nameof(Name), nameof(o.Name)},
                { nameof(Description), nameof(o.Description)},
                { nameof(Status), nameof(o.Status)},
                { nameof(Priority), nameof(o.Priority)}
            };
        }


        public DataTableFilter? Name { get; set; }
        public DataTableFilter? Description { get; set; }
        public DataTableFilter? Status { get; set; }
        public DataTableFilter? Priority { get; set; }
    }
}
