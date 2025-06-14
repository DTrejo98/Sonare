using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/comments").WithTags(nameof(Comment));

            group.MapGet("/", async (ICommentService services) =>
                await services.GetAllComments())
                .WithName("GetAllComments");

            group.MapPost("/", async (Comment comment, ICommentService services) =>
            {
                var created = await services.CreateComment(comment);
                return created is not null ? Results.Created($"/api/comments/{created.Id}", created) : Results.BadRequest();
            })
            .WithName("CreateComment");

            group.MapPut("/{id}", async (int id, Comment comment, ICommentService services) =>
            {
                var updated = await services.UpdateComment(id, comment);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateComment");

            group.MapDelete("/{id}", async (int id, ICommentService services) =>
            {
                var deleted = await services.DeleteComment(id);
                return Results.Ok(deleted);
            })
            .WithName("DeleteComment");
        }
    }
}
