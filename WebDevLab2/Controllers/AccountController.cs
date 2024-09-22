using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDevLab2.Model;
using WebDevLab2.Model.Context;

namespace WebDevLab2.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly MainContext _context;
            public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, MainContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _context = context;
            }
            [HttpPost]
            [Route("api/account/register")]
            [AllowAnonymous]
            public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
            {
                if (ModelState.IsValid)
                {
                    User user = new();
                    user.UserName = model.login;
                    // Добавление нового пользователя
                    var result = await _userManager.CreateAsync(user,model.password);
                    if (result.Succeeded)
                    {
                    //Установка роли User
                    if (model.isdeveloper == false)
                    {
                        Player newPlayer = new Player 
                        { 
                            Login = model.login,
                            Password = model.password,
                        };
                        _context.Add(newPlayer);
                        await _context.SaveChangesAsync();

                        await _userManager.AddToRoleAsync(user, "player");
                    }
                    else
                    {
                        Developer newDeveloper = new Developer
                        {
                            CompanyName = model.company_name,
                            Login = model.login,
                            Password = model.password,

                        };

                        _context.Add(newDeveloper);
                        await _context.SaveChangesAsync();

                        await _userManager.AddToRoleAsync(user, "developer");
                    }
                        // Установка куки
                        await _signInManager.SignInAsync(user, false);
                        return Ok(new { message = "Добавлен новый пользователь: " + user.UserName });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        var errorMsg = new
                        {
                            message = "Пользователь не добавлен",
                            error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        return Created("", errorMsg);
                    }
                }
                else
                {
                    var errorMsg = new
                    {
                        message = "Неверные входные данные",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            [HttpPost]
            [Route("api/account/login")]
            //[AllowAnonymous]
            public async Task<IActionResult> Login([FromBody] LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var result =
                    await _signInManager.PasswordSignInAsync(model.login, model.password, model.rememberMe, false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByNameAsync(model.login);
                        IList<string>? roles = await _userManager.GetRolesAsync(user);
                        string? userRole = roles.FirstOrDefault();
                        return Ok(new { message = "Выполнен вход", userName = model.login, userRole });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                        var errorMsg = new
                        {
                            message = "Вход не выполнен",
                            error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        return Created("", errorMsg);
                    }
                }
                else
                {
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            [HttpPost]
            [Route("api/account/logoff")]
            public async Task<IActionResult> LogOff()
            {
                User usr = await GetCurrentUserAsync();
                if (usr == null)
                {
                    return Unauthorized(new { message = "Сначала выполните вход" });
                }
                // Удаление куки
                await _signInManager.SignOutAsync();
                return Ok(new { message = "Выполнен выход", userName = usr.UserName });
            }
            [HttpGet]
            [Route("api/account/isauthenticated")]
            public async Task<IActionResult> IsAuthenticated()
            {
                User usr = await GetCurrentUserAsync();
                if (usr == null)
                {
                    return Unauthorized(new { message = "Вы Гость. Пожалуйста, выполните вход" });
                }
                IList<string> roles = await _userManager.GetRolesAsync(usr);
                string? userRole = roles.FirstOrDefault();
                return Ok(new { message = "Сессия активна", userName = usr.UserName, userRole });
            }
            private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
