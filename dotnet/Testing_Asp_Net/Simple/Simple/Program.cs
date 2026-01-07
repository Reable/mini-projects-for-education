using System.Text.RegularExpressions;
using Simple.Controllers;

UsersController usersController = new ();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.Run(async context =>
   {
      var response = context.Response;
      var request = context.Request;
      var path = request.Path;

      var expForNums = "^/api/users/([0-9])";

      if (path == "/")
      {
         response.ContentType = "text/html; charset=utf-8";
         await response.SendFileAsync("html/index.html");
      }

      if (path == "/api/users" && request.Method == "GET")
      {
         await usersController.GetAllAsync(response);
      }
      
      if (Regex.IsMatch(path, expForNums) && request.Method == "GET")
      {
         var id = int.Parse(path.Value?.Split('/')[3] ?? string.Empty);
         await usersController.GetByIdAsync(response, id);
      }
      
      if (path == "/api/users" && request.Method == "POST")
      {
         await usersController.CreateAsync(request, response);
      }
      
      if (path == "/api/users" && request.Method == "PUT")
      {
         await usersController.UpdateAsync(request, response);
      }
      
      if (Regex.IsMatch(path, expForNums) && request.Method == "DELETE")
      {
         var id = int.Parse(path.Value?.Split('/')[3] ?? string.Empty);
         await usersController.DeleteAsync(response, id);
      }
   } 
);


app.Run();