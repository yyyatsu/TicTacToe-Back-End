using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TTT.Data.Context;
using TTT.Data.Entities;
using TTT.Domain.Models;
using TTT.Domain.Services.Interfaces;

namespace TTT.WebAPI.Controllers
{
  [ApiController]
  [Route("Account")]
  public class AccountController : ControllerBase
  {
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
      this.accountService = accountService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
    {
      if (ModelState.IsValid)
      {
        await Authenticate(registrationModel.Name);
        return Ok(await accountService.RegisterAsync(registrationModel));
      }
      return BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
      if (ModelState.IsValid)
      {
        User? user = await accountService.LoginAsync(loginModel);
        if (user != null) 
        {
          await Authenticate(loginModel.Name);
          return Ok(user);
        }
      }
      return BadRequest();
    }

    [HttpGet("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return Ok();
    }

    private async Task Authenticate(string userName)
    {
      List<Claim> claims = new ()
      {
        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
      };

      ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
  }
}
