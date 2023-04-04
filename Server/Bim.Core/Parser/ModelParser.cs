using Bim.Core.Entity.Models;
using Bim.Core.Models.View;

namespace Bim.Core.Parser
{
    public static class ModelParser
    {
        public static TaskResponse Parse(this TaskEntity model)
        {
            if (model == null)
                return null;

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
            if (model == null)
                return null;

            return new TaskEntity
            {
                Description = model.Description,
                Name = model.Name,
                Priority = model.Priority ?? 0,
                Status = model.Status ?? 0
            };
        }

        public static void Update(this TaskEntity model, TaskUpdateRequest modelUpdate)
        {
            if (modelUpdate != null)
            {
                model.Description = modelUpdate.Description;
                model.Name = modelUpdate.Name;
                model.Status = modelUpdate.Status ?? 0;
                model.Priority = modelUpdate.Priority ?? 0;
            }
        }
    }
}
