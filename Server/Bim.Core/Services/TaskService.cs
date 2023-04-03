using Bim.Core.Entity.Models;
using Bim.Core.Models.View;
using Bim.Core.Repositories;
using Bim.Core.Services.Interface;
using Bim.Core.Parser;

using Microsoft.Extensions.Logging;
using Custom.Lib.Models.Repository;

namespace Bim.Core.Services
{
    public class TaskService : ITaskService
    {
        protected readonly TaskRepository _taskRepository;
        protected readonly ILogger _logger;

        public TaskService(TaskRepository taskRepository, ILoggerFactory loggerFactory)
        {
            _taskRepository = taskRepository;
            _logger = loggerFactory.CreateLogger<TaskService>();
        }

        public async Task<TaskResponse> GetItem(int id)
        {
            var model = await _taskRepository.GetItem<TaskEntity>(id);

            return model?.Parse();
        }

        public async Task<TaskResponse> Create(TaskCreateRequest model)
        {
            var data = await _taskRepository.CreateItem<TaskEntity>(model.Parse());

            return data.Parse();
        }

        public async Task<TaskResponse> Update(TaskUpdateRequest model)
        {
            var data = await _taskRepository.GetItem<TaskEntity>(model.Id.Value);

            if (data != null && data.Status != TaskStatusEnum.Complete)
            {
                data.Update(model);

                await _taskRepository.SaveChanges();
            }

            return data?.Parse();
        }

        public async Task Delete(int id)
        {
            var orig = await _taskRepository.GetItem<TaskEntity>(id);

            if (orig != null)
            {
                var taskSet = _taskRepository.GetDbSet<TaskEntity>();

                taskSet.Remove(orig);
            }

        }

        public async Task<DataTableReply<TaskResponse>> Table(DataTable model, CancellationToken token)
        {          
            var data = await _taskRepository.GetTable<TaskEntity>(model, token);

            return new DataTableReply<TaskResponse>
            {
                Count = data.Count,
                Data = data.Data.Select(x => x.Parse())
            };
        }
    }
}
