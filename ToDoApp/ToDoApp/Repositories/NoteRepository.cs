using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.DTO;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Models;

namespace ToDoApp.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ToDoContext _context;

        public NoteRepository(ToDoContext context)
        {
            _context = context;
        }
        public bool CreateNote(Note note)
        {
            _context.Add(note);
            return Save();
        }

        public bool DeleteNote(Note note)
        {
            _context.Remove(note);
            return Save();
        }

        public Note GetNote(Guid id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public ICollection<Note> GetNotes(Guid id)
        {
            return _context.Notes.Where(n => n.UserId == id).ToList();
        }

        public bool NoteExists(Guid id)
        {
            return _context.Notes.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateNote(Note note)
        {
            _context.Update(note);
            return Save();
        }
    }
}
