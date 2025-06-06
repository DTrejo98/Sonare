using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Endpoints
{
    public static class CollaborationEndpoints
    {
        public static void MapCollaborationEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/collaborations").WithTags(nameof(Collaboration));

            group.MapGet("/", async (ICollaborationService services) =>
                await services.GetAllCollaborations())
                .WithName("GetAllCollaborations");

            group.MapGet("/{id}", async (int id, ICollaborationService services) =>
            {
                var collab = await services.GetCollaborationById(id);
                return collab is not null ? Results.Ok(collab) : Results.NotFound();
            })
            .WithName("GetCollaborationById");

            group.MapPost("/", async (Collaboration collab, ICollaborationService services) =>
            {
                var created = await services.CreateCollaboration(collab);
                return created is not null ? Results.Created($"/api/collaborations/{created.Id}", created) : Results.BadRequest();
            })
            .WithName("CreateCollaboration");

            group.MapDelete("/{id}", async (int id, ICollaborationService services) =>
            {
                var deleted = await services.DeleteCollaboration(id);
                return Results.Ok(deleted);
            })
            .WithName("DeleteCollaboration");
        }
    }
}
