using Microsoft.AspNetCore.Mvc;
using OnionDemo.Application.Interfaces;

namespace OnionDemo.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_userService.GetAll());
    }

    [HttpPost]
    public IActionResult Create(string name)
    {
        return Ok(_userService.Create(name));
    }
}
