using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Service.Interface.IValidation;
using ToDoApp.DTO.Response;
using ToDoApp.Models;
using ToDoApp.Validation;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidationToDoApp _userValidation;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,
                               IUserValidationToDoApp userValidation,
                               IMapper mapper)
        {
            _userRepository = userRepository;
            _userValidation = userValidation;
            _mapper = mapper;
        }

        [HttpPost("users")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserAsync([FromBody] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                User user = await _userRepository.GetUserAsync(id);

                if (user == null)
                    return NotFound();

                UserDto userDTO = _mapper.Map<UserDto>(user);

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("users")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var users = await _userRepository.GetUsersAsync();

                var usersDto = _mapper.Map<ICollection<UserDto>>(users);

                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("users/new")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (userDTO == null)
                    return BadRequest("user cannot be null.");

                if (!await _userValidation.IsUserValidAsync(userDTO))
                    return BadRequest("Incorrect user object.");

                User user = _mapper.Map<User>(userDTO);
                user.Id = Guid.NewGuid();

                if (!await _userRepository.CreateUserAsync(user))
                    return BadRequest("User already exists.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("users")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteUserAsync([FromBody] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _userRepository.DeleteUserAsync(id))
                    return BadRequest("Something happen during remove user.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("users")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserDto userToUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _userValidation.IsUserValidAsync(userToUpdate))
                    return BadRequest("Incorrect user object.");

                User user = _mapper.Map<User>(userToUpdate);

                if (!await _userRepository.UpdateUserAsync(user))
                    return BadRequest("Something happen during updating user.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
