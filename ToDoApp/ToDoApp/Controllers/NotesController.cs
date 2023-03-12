using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Interfaces.Validators;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;
        private readonly INoteValidationToDoApp _noteValidation;
        private readonly IMapper _mapper;

        public NotesController(INoteRepository noteRepository,
                               IUserRepository userRepository,
                               INoteValidationToDoApp noteValidation,
                               IMapper mapper)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
            _noteValidation = noteValidation;
            _mapper = mapper;
        }

        [HttpPost("notes")]
        [ProducesResponseType(200, Type = typeof(NoteDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetNotes(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(id))
                return NotFound("User not found.");

            var notes = _mapper.Map<IEnumerable<NoteDTO>>(_noteRepository.GetNotes(id));

            return Ok(notes);
        }

        [HttpPost("notes/{noteId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NoteDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetNote(Guid noteId, [FromBody] Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Note note = _noteRepository.GetNote(noteId);

            if (note == null)
                return NotFound();

            if (userId != note.UserId)
                return StatusCode(403);

            NoteDTO noteDTO = _mapper.Map<NoteDTO>(note);

            return Ok(noteDTO);
        }

        [HttpPost("notes/new")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNote([FromBody] NoteDTO noteDTO, Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_noteValidation.IsNoteValid(noteDTO))
                return BadRequest("Incorrect note object.");

            if (!_userRepository.UserExists(userId))
                return BadRequest("User doesnt exist.");

            Note note = _mapper.Map<Note>(noteDTO);
            note.UserId = userId;

            if (!_noteRepository.CreateNote(note))
                return BadRequest("Couldnt create task.");

            return NoContent();
        }

        [HttpDelete("notes/{noteId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteNote([FromBody] Guid userId, Guid noteId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return BadRequest("User doesnt exist.");

            Note note = _noteRepository.GetNote(noteId);

            _noteRepository.DeleteNote(note);

            return NoContent();
        }

        [HttpPut("notes/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateNote([FromBody] NoteDTO noteDTO, Guid userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_noteValidation.IsNoteValid(noteDTO))
                return BadRequest("Incorrect note object.");

            if (!_userRepository.UserExists(userId))
                return NotFound("User doesnt exist.");

            if (!_noteRepository.NoteExists(noteDTO.Id))
                return NotFound("Note doesnt exist.");

            Note note = _mapper.Map<Note>(noteDTO);
            note.UserId = userId;

            if (!_noteRepository.UpdateNote(note))
                return BadRequest();

            return NoContent();
        }
    }
}
