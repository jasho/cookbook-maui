using CookBook.Api.BL.Facades.Interfaces;
using CookBook.Common.Models;
using System.Net;

namespace CookBook.Api.Endpoints;

public static class ImageEndpoints
{
    public static void UseImageEndpoints(this WebApplication app)
    {
        const string ImageBasePath = "/api/images";
        const string ImageBaseName = "Image";
        var ImagesTag = $"{ImageBaseName}s";

        app.MapGet($"{ImageBasePath}/{{id:guid}}", (Guid id, IImageFacade imageFacade) => Results.File(imageFacade.GetById(id)?.Data ?? [], "image/**"))
            .WithTags(ImagesTag)
            .WithName($"Get{ImageBaseName}ById");

        app.MapPost($"{ImageBasePath}", (ImageModel image, HttpRequest request, IImageFacade imageFacade) =>
        {
            // TODO input validation maybe ;)
            var id = imageFacade.Create(new ImageModel { Id = image.Id, Data = image.Data });
            return Results.Created($"{request.Scheme}://{request.Host}{request.Path}/{id}", $"{request.Scheme}://{request.Host}{request.Path}/{id}");
        })
            .WithTags(ImagesTag)
            .WithName($"Create{ImageBaseName}")
            .Produces<string>((int)HttpStatusCode.Created);

        app.MapPut($"{ImageBasePath}", (ImageModel image, IImageFacade imageFacade) =>
        {
            var id = imageFacade.Update(image);
            return id is null
                ? Results.NotFound()
                : Results.NoContent();
        })
            .WithTags(ImagesTag)
            .WithName($"Update{ImageBaseName}");

        app.MapDelete($"{ImageBasePath}/{{id:guid}}", (Guid id, IImageFacade imageFacade) => imageFacade.Delete(id))
            .WithTags(ImagesTag)
            .WithName($"Delete{ImageBaseName}");
    }
}
