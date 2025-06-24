using System.Security.Claims;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddKeycloakJwtBearer("keycloak", realm: "stocks", opts =>
{
    opts.RequireHttpsMetadata = false;
    opts.Audience = "account";
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("users/me",
        (ClaimsPrincipal claimsPrincipal) => { return claimsPrincipal.Claims.ToDictionary(x => x.Type, x => x.Value); })
    .RequireAuthorization();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultEndpoints();

app.Run();