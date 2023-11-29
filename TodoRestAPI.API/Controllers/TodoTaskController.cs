using Microsoft.AspNetCore.Mvc;
using TodoRestAPI.Application.InputModels;
using TodoRestAPI.Application.Services;
using TodoRestAPI.Domain.Exceptions;

namespace TodoRestAPI.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoTaskController : Controller
    {
        private readonly TodoTaskAppService _todoTaskAppService;

        public TodoTaskController(TodoTaskAppService todoTaskAppService) 
        {
            _todoTaskAppService = todoTaskAppService;
        }

        [HttpPost]
        [Route("task")]
        public async Task<IActionResult> PostAsync([FromBody] TodoTaskInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = Guid.NewGuid();

            await _todoTaskAppService.AddAsync(id, input);

            return Ok(id);
        }

        [HttpGet]
        [Route("tasks/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var todoTask = await _todoTaskAppService.GetByIdAsync(id);
                return Ok(todoTask);
            }
            catch (TodoTaskNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("tasks")]
        public async Task<IActionResult> GetAllAsync() 
        { 
            var todoTasks = await _todoTaskAppService.GetAllAsync();

            return Ok(todoTasks);
        }

        [HttpPut]
        [Route("task/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] TodoTaskInputModel input)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            { 
                await _todoTaskAppService.UpdateAsync(id, input);
                return Ok();
            }
            catch (TodoTaskNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("task/{id}/complete")]
        public async Task<IActionResult> CompleteAsync([FromRoute] Guid id)
        {
            try
            {
                await _todoTaskAppService.CompleteAsync(id);
                return Ok();
            }
            catch (TodoTaskNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("task/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                await _todoTaskAppService.RemoveAsync(id);
                return Ok();
            }
            catch (TodoTaskNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   
    }
}
