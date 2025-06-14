using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Endpoints
{
    public static class ClipCollaboratorEndpoints
    {
        public static void MapClipCollaboratorEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/clip-collaborators").WithTags(nameof(ClipCollaborator));

            group.MapGet("/", async (IClipCollaboratorService services) =>
                await services.GetAllClipCollaborators())
                .WithName("GetAllClipCollaborators");

            group.MapGet("/{clipId}/{userId}", async (int clipId, int userId, IClipCollaboratorService services) =>
            {
                var result = await services.GetClipCollaboratorByIds(clipId, userId);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetClipCollaboratorByIds");

            group.MapPost("/", async (ClipCollaborator collaborator, IClipCollaboratorService services) =>
            {
                var created = await services.CreateClipCollaborator(collaborator);
                return created is not null ? Results.Created($"/api/clip-collaborators/{collaborator.ClipId}/{collaborator.UserId}", created) : Results.BadRequest();
            })
            .WithName("CreateClipCollaborator");

            group.MapDelete("/{clipId}/{userId}", async (int clipId, int userId, IClipCollaboratorService services) =>
            {
                var deleted = await services.DeleteClipCollaborator(clipId, userId);
                return Results.Ok(deleted);
            })
            .WithName("DeleteClipCollaborator");
        }
    }
}
