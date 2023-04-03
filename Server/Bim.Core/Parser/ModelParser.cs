using Bim.Core.Entity.Models;
using Bim.Core.Models.View;

namespace Bim.Core.Parser
{
    public static class ModelParser
    {
        public static TaskResponse Parse(this TaskEntity model)
        {
            return new TaskResponse
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Priority = model.Priority,
                Status = model.Status
            };
        }

        public static TaskEntity Parse(this TaskCreateRequest model)
        {
            return new TaskEntity
            {
                Description = model.Description,
                Name = model.Name,
                Priority = model.Priority.Value,
                Status = model.Status.Value
            };
        }

        public static void Update(this TaskEntity model, TaskUpdateRequest modelUpdate)
        {
            model.Description = modelUpdate.Description;
            model.Name = modelUpdate.Name;
            model.Status = modelUpdate.Status.Value;
            model.Priority = modelUpdate.Priority.Value;
        }
    }
}
