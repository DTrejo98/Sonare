using Sonare.Interfaces;
using Sonare.Models;
using Sonare.Services;

namespace Sonare.Endpoints
{
    public static class ClipEndpoints
    {
        public static void MapClipEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/clips").WithTags(nameof(Clip));

            group.MapGet("/", async (IClipService services) =>
                await services.GetAllClips())
                .WithName("GetAllClips");

            group.MapGet("/{id}", async (int id, IClipService services) =>
            {
                var clip = await services.GetClipById(id);
                return clip is not null ? Results.Ok(clip) : Results.NotFound();
            })
            .WithName("GetClipById");

            group.MapPost("/", async (Clip clip, IClipService services) =>
            {
                var created = await services.CreateClip(clip);
                return created is not null ? Results.Created($"/api/clips/{created.Id}", created) : Results.BadRequest();
            })
            .WithName("CreateClip");

            group.MapPut("/{id}", async (int id, Clip clip, IClipService services) =>
            {
                var updated = await services.UpdateClip(id, clip);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            })
            .WithName("UpdateClip");

            group.MapDelete("/{id}", async (int id, IClipService services) =>
            {
                var deleted = await services.DeleteClip(id);
                return Results.Ok(deleted);
            })
            .WithName("DeleteClip");
        }
    }
}
