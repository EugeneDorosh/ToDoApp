using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Interfaces 
{
    public interface INoteRepository : IBaseRepository
    {
        public Note GetNote(Guid id);
        public ICollection<Note> GetNotes(Guid id);
        public bool CreateNote(Note note);
        public bool UpdateNote(Note note);
        public bool DeleteNote(Note note);
        public bool NoteExists(Guid id);
    }
}
