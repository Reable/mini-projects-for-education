using Blog.Endpoints;
using Blog.Repository;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseStaticFiles();

Routes.Configure(app);

app.Run();
