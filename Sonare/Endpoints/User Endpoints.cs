using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/users").WithTags(nameof(User));

            group.MapGet("/", async (IUserService services) =>
                await services.GetAllUsers())
                .WithName("GetAllUsers")
                .Produces<List<User>>(StatusCodes.Status200OK);

            group.MapGet("/{id}", async (int id, IUserService services) =>
            {
                var user = await services.GetUserById(id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            })
            .WithName("GetUserById");

            group.MapPost("/", async (User user, IUserService services) =>
            {
                var created = await services.CreateUser(user);
                return created is not null ? Results.Created($"/api/users/{created.Id}", created) : Results.BadRequest();
            })
            .WithName("CreateUser");

            group.MapPut("/{id}", async (int id, User user, IUserService services) =>
            {
                var updated = await services.UpdateUser(id, user);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateUser");

            group.MapDelete("/{id}", async (int id, IUserService services) =>
            {
                var deleted = await services.DeleteUser(id);
                return Results.Ok(deleted);
            })
            .WithName("DeleteUser");
        }
    }
}

