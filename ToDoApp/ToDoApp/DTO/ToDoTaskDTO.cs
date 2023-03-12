﻿using ToDoApp.Models.Enums;
using ToDoApp.Models;

namespace ToDoApp.DTO
{
    public class ToDoTaskDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DateTime { get; set; }
        public Status Status { get; set; }
    }
}
