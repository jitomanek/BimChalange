using Bim.Core.Models.View;
using Bim.Core.Services.Interface;

using Custom.Lib.Models.Repository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        protected readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataTableReply<TaskResponse>), StatusCodes.Status200OK )]
        public async Task<IActionResult> Index([FromBody] TaskTableRequest model, CancellationToken token)
        {
            var data = await _taskService.Table(model, token);

            return Ok(data);
        }
    }
}
