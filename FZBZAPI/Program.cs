using FzBz.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFzBz();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", async (HttpContext httpContext) =>
{
    var page = "<!doctype html><html><head><title>FzBz API</title></head><body>API ready</body></html>";
    httpContext.Response.ContentType = MediaTypeNames.Text.Html;
    httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(page);
    await httpContext.Response.WriteAsync(page);
})
.ExcludeFromDescription();

app.MapGet("/single/{number}", ([FromRoute] int number, [FromServices] IFzBzService fzbzService) =>
{
    return fzbzService.GetResult([number])[number];
})
.WithName("GetSingleNumber")
.WithOpenApi();

app.MapGet("/array", ([FromQuery] int[] n, [FromServices] IFzBzService fzbzService) =>
{
    return fzbzService.GetResult(n);
})
.WithName("GetArray")
.WithOpenApi();

app.Run();
