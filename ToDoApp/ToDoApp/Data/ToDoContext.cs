﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId);

            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }
    }
}
