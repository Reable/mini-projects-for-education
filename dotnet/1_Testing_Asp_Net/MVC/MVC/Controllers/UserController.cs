using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;

namespace MVC.Controllers;

public class UserController : Controller
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        var users = _userService.GetAll();
        return View(users);
    }

    [HttpPost]
    public IActionResult Save(User user)
    {
        if(user.Id == 0) _userService.AddUser(user);
        else _userService.Update(user);
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Select(int id)
    {
        var user = _userService.GetById(id) ?? new User();
        ViewBag.SelectedUser = user;
        
        return View("Index", _userService.GetAll());
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
         _userService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Reset()
    {
        return RedirectToAction(nameof(Index));
    }
}