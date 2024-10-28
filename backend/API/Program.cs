using API;
using BAL.Services;
using DAL;
using DAL.Repositories;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

string myCors = "CORS";

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<TicketService>();

builder.Services.AddCors(options => {
    options.AddPolicy(myCors, policy => {
        policy.WithOrigins("https://nrppw-1-zadaca.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var auth0Settings = builder.Configuration.GetSection("Auth0").Get<Auth0Settings>();

builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.Authority = $"https://{auth0Settings.Domain}/";
    options.Audience = auth0Settings.Audience;
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = $"https://{auth0Settings.Domain}/",
        ValidAudience = auth0Settings.Audience
    };
});

builder.Services.AddDbContext<Zadaca1Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) => {
    if (!context.Request.Path.StartsWithSegments("/api") && !Path.HasExtension(context.Request.Path.Value)) {
        context.Request.Path = "/index.html";
    }
    await next();
});
app.UseStaticFiles(); 
app.UseCors(myCors);

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers(); 
    endpoints.MapFallbackToFile("index.html"); 
});

app.Run();
