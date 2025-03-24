using CookBook.Api.BL.Installers;
using CookBook.Api.DAL.Installers;
using CookBook.Api.Endpoints;
using Microsoft.AspNetCore.Http.Json;
using NSwag.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureOpenApiDocuments(builder.Services);
new ApiDALInstaller().Install(builder.Services);
new ApiBLInstaller().Install(builder.Services);
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var application = builder.Build();

if(builder.Environment.IsDevelopment())
{
    application.UseDeveloperExceptionPage();
}
else
{
    UseSecurityFeatures(application);
}

UseEndpoints(application);
UseOpenApi(application);

application.Run();


void ConfigureOpenApiDocuments(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddOpenApiDocument(document =>
    {
        document.Title = "CookBook API";
        document.DocumentName = "cookbook-api";
    });
}

void UseSecurityFeatures(IApplicationBuilder app)
{
    app.UseHttpsRedirection();
}

void UseEndpoints(WebApplication app)
{
    app.MapGet("/", http =>
    {
        http.Response.Redirect("/swagger", false);
        return Task.CompletedTask;
    });

    app.UseImageEndpoints();
    app.UseIngredientAmountEndpoints();
    app.UseIngredientEndpoints();
    app.UseRecipeEndpoints();
}

void UseOpenApi(IApplicationBuilder app)
{
    app.UseOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.DocumentTitle = "CookBook Swagger UI";
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("CookBook API", "/swagger/cookbook-api/swagger.json"));
        settings.ValidateSpecification = true;
    });
}