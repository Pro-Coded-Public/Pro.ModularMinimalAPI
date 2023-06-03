var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterModules();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }