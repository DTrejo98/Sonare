using Microsoft.EntityFrameworkCore;
using Sonare.Data;
using Sonare.Endpoints;
using Sonare.Interfaces;
using Sonare.Repositories;
using Sonare.Services;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// PostgreSQL connection
builder.Services.AddNpgsql<AppDbContext>(builder.Configuration["SonareConnectionString"]);

// Handle cyclic references in JSON
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Register Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IClipRepository, ClipRepository>();
builder.Services.AddScoped<IClipService, ClipService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<ICollaborationRepository, CollaborationRepository>();
builder.Services.AddScoped<ICollaborationService, CollaborationService>();

builder.Services.AddScoped<IClipCollaboratorRepository, ClipCollaboratorRepository>();
builder.Services.AddScoped<IClipCollaboratorService, ClipCollaboratorService>();

// Swagger + Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

// Map endpoints (to be created later)
app.MapUserEndpoints();
app.MapClipEndpoints();
app.MapCommentEndpoints();
app.MapCollaborationEndpoints();
app.MapClipCollaboratorEndpoints();

app.Run();

