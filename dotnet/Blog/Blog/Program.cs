using Blog.Endpoints;
using Blog.Repository;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddValidation();

var app = builder.Build();

app.UseStaticFiles();

Routes.Configure(app);

app.Run();
