namespace ToDoApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<ToDoTask> Tasks { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
