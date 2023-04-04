using Bim.Core.Models.View;
using Bim.Core.Services.Interface;
using Bim.Util;

using Microsoft.AspNetCore.Mvc;

namespace Bim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        protected readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _taskService.GetItem(id);

            return Ok(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest model)
        {
            if (ModelState.IsValid)
            {
                var data = await _taskService.Create(model);

                return Ok(data);
            }

            return BadRequest(ModelState.GetStateErrors());
        }

        [HttpPut]
        [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] TaskUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                var data = await _taskService.Update(model);

                return Ok(data);
            }

            return BadRequest(ModelState.GetStateErrors());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);

            return Ok();
        }
    }
}
