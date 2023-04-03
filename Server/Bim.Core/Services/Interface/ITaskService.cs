using Bim.Core.Entity.Models;
using Bim.Core.Models.View;

using Custom.Lib.Models.Repository;

namespace Bim.Core.Services.Interface
{
    public interface ITaskService
    {
        Task<TaskResponse> GetItem(int id);
        Task<TaskResponse> Create(TaskCreateRequest model);
        Task<TaskResponse> Update(TaskUpdateRequest model);
        Task Delete(int id);

        Task<DataTableReply<TaskResponse>> Table(DataTable model, CancellationToken token);
    }
}
