using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Interfaces;
using ToDoApp.Interfaces.Repositories;
using ToDoApp.Interfaces.Validators;
using ToDoApp.Repositories;
using ToDoApp.Validation;
using ToDoApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ToDoContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<IUserValidationToDoApp, UserValidation>();
builder.Services.AddScoped<INoteValidationToDoApp, NoteValidation>();
builder.Services.AddScoped<ITaskValidationToDoApp, TaskValidation>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
