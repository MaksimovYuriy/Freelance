using FreelanceDB.Abstractions.Repository;
using FreelanceDB.Abstractions.Services;
using FreelanceDB.Authentication;
using FreelanceDB.Authentication.Abstractions;
using FreelanceDB.Database.Context;
using FreelanceDB.Database.Repositories;
using FreelanceDB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IResumeRepository, ResumeRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IResponseRepository, ResponseRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddDbContext<FreelancedbContext>();

builder.Services.AddCors(option => option.AddPolicy(
    name: "Default",
    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Default");
app.UseAuthorization();
app.MapControllers();



app.Run();