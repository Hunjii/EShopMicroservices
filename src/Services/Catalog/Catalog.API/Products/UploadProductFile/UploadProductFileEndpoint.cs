using BuildingBlocks.Storage.Enums;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.API.Products.UploadProductFile
{
    public class UploadProductFileEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products/upload-file", async (HttpRequest req, ISender sender) =>
            {
                var file = req.Form.Files.GetFile("file");
                if (file == null)
                {
                    return Results.BadRequest("No file uploaded");
                }

                if (!Enum.TryParse<FileType>(req.Form["fileType"], out var fileType))
                {
                    return Results.BadRequest("Invalid file type");
                }

                var command = new UploadProductFileCommand(file, fileType);
                var result = await sender.Send(command);

                if (result.Success)
                {
                    return Results.Ok(new { fileUrl = result.FileUrl });
                }
                else
                {
                    return Results.BadRequest("Failed to upload file");
                }
            })
            .DisableAntiforgery()
            .WithName("UploadProductFile")
            .WithTags("Products");
        }
    }
}