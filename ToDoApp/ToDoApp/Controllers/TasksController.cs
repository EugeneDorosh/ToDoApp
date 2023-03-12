using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Interfaces.Validators;
using ToDoApp.Models;
using ToDoApp.Models.Enums;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskValidationToDoApp _taskValidation;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository taskRepository,
                               IUserRepository userRepository,
                               ITaskValidationToDoApp taskValidation,
                               IMapper mapper)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _taskValidation = taskValidation;
            _mapper = mapper;
        }

        [HttpPost("tasks")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ToDoTaskDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetTasks(Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            IEnumerable<ToDoTaskDTO> tasks = _mapper.Map<IEnumerable<ToDoTaskDTO>>(_taskRepository.GetTasks(userId));

            return Ok(tasks);
        }

        [HttpPost("tasks/{taskId}")]
        [ProducesResponseType(200, Type = typeof(ToDoTaskDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(Guid taskId, [FromBody] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return BadRequest();

            if (!_taskRepository.TaskExists(taskId))
                return NotFound();

            ToDoTask taskDTO = _taskRepository.GetTask(taskId);

            if (taskDTO.UserId != userId)
                return StatusCode(403);

            ToDoTaskDTO task = _mapper.Map<ToDoTaskDTO>(taskDTO);

            return Ok(task);
        }

        [HttpPost("tasks/new")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] ToDoTaskDTO taskDTO,
                                        [FromQuery] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return BadRequest();

            if (!_taskValidation.IsTaskValid(taskDTO))
                return BadRequest("Invalid task object.");

            ToDoTask task = _mapper.Map<ToDoTask>(taskDTO);
            task.UserId = userId;

            if (!_taskRepository.CreateTask(task))
                return BadRequest("Couldnt create task.");

            return NoContent();
        }

        [HttpPut("tasks/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateTask([FromBody] ToDoTaskDTO taskDTO, Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_taskValidation.IsTaskValid(taskDTO))
                return BadRequest("Invalid task object.");

            if (!_userRepository.UserExists(userId))
                return BadRequest();

            if (!_taskRepository.TaskExists(taskDTO.Id))
                return NotFound();

            ToDoTask task = _mapper.Map<ToDoTask>(taskDTO);
            task.UserId = userId;

            if (!_taskRepository.UpdateTask(task))
                return BadRequest("Couldnt create task.");

            return NoContent();
        }

        [HttpDelete("tasks/{taskId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteTask(Guid taskId, [FromBody] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return BadRequest();

            if (!_taskRepository.TaskExists(taskId))
                return NotFound();

            var taskToDelete = _taskRepository.GetTask(taskId);

            if (taskToDelete.UserId != userId)
                return StatusCode(403);

            if (!_taskRepository.DeleteTask(taskId))
                return BadRequest("Couldnt delete task.");

            return NoContent();
        }
    }
}
