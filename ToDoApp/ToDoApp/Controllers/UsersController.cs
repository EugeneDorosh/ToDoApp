using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Interfaces.Validators;
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
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetUser([FromBody] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _userRepository.GetUser(id);

            if (user == null)
                return NotFound();

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        [HttpGet("users")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _mapper.Map<ICollection<UserDTO>>(_userRepository.GetUsers());

            return Ok(users);
        }

        [HttpPost("users/new")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userDTO == null)
                return BadRequest("user cannot be null.");

            if (!_userValidation.IsUserValid(userDTO))
                return BadRequest("Incorrect user object.");

            User user = _mapper.Map<User>(userDTO);

            if (!_userRepository.CreateUser(user))
                return BadRequest("User already exists.");

            return NoContent();
        }

        [HttpDelete("users")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser([FromBody] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User user = _userRepository.GetUser(id);

            if (user == null)
                return BadRequest("No such user.");

            if (!_userRepository.DeleteUser(user))
                return BadRequest("Something happen during remove user.");

            return NoContent();
        }

        [HttpPut("users")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUser([FromBody] UserDTO userToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userValidation.IsUserValid(userToUpdate))
                return BadRequest("Incorrect user object.");

            User user = _mapper.Map<User>(userToUpdate);

            if (!_userRepository.UpdateUser(user))
                return BadRequest("Something happen during updating user.");

            return NoContent();
        }

        //TODO: think about dtos and how to connect front and back in future(user id etc) 
    }
}
