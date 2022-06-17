using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
        }
        //Get Accoundt
        public IActionResult Login (string returnUrl)
        {
            return View(
                new LoginViewModel
                {
                    ReturnUrl = returnUrl,
                }
                );
        }
        [HttpPost]
        public async Task<ActionResult> Login (LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = await _usermanager.FindByNameAsync(model.UserName);
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return Redirect(model.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Falha ao realizar login");
            return View(model);
        }
        public IActionResult Register ()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var result = await _usermanager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Acconunt");
                }else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }
            return View();
        }
    }
}
