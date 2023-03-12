using AutoMapper;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<ToDoTask, ToDoTaskDTO>().ReverseMap();
        }
    }
}
