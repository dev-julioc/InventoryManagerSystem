using Carter;
using FluentValidation;
using InventoryManagerSystem.Application.Dtos.Auth.Validation;
using InventoryManagerSystem.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidation>();

builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddInfraestructureJwt(builder.Configuration);
builder.Services.AddCarter();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdministrationPolicy", adp =>
    {
        adp.RequireAuthenticatedUser();
        adp.RequireRole("Admin", "Manager");
    });
    opt.AddPolicy("UserPolicy", adp =>
    {
        adp.RequireAuthenticatedUser();
        adp.RequireRole("User");
    });
});
    
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy
            .WithOrigins("http://localhost:5103") // ou a porta do Blazor
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapCarter();
app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
