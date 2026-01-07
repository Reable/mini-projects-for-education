using Razor.Models;
using Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages;

public class UsersModel(
    UserService userService
) : PageModel
{
    private readonly UserService _userService = userService;

    [BindProperty]
    public User CurrentUser { get; set; } = new();
    public List<User> Users => _userService.GetAll();
    
    public void OnGet()
    {
        
    }

    public JsonResult OnGetUser(int id)
    {
        var user = _userService.GetById(id);
        return new JsonResult(user);
    }

    public IActionResult OnPostSave()
    {
        bool isResult;

        if (CurrentUser.Id == 0)
            isResult = (CurrentUser = _userService.Add(CurrentUser)).Id != 0 ? true : false;
        else
            isResult = _userService.Update(CurrentUser);

        return RedirectToPage();
    }

    public JsonResult OnPostDelete(int id)
    {
        bool isResult = _userService.DeleteById(id);
        return new JsonResult(new
        {
            success = isResult
        });
    }
    
}