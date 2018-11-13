using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            bool usuarioAutenticado = 
                Utils.Utils.ObterUsuarioLogado(
                    new JogosDeGuerraModel.ModelJogosDeGuerra()
                    ) != null;

            if (!usuarioAutenticado)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Tabuleiro()
        {
            ViewBag.Title = "Tabuleiro";

            return View();
        }

        public ActionResult Login(string usuario, string password, string rememberme, string returnurl)
        {
            /*
            ViewBag.Title = "Login";
            var user = busUser.ValidateUserAndLoad(email, password);
            if (user == null)
            {
                ErrorDisplay.ShowError(busUser.ErrorMessage);
                return View(ViewModel);
            }

            AppUserState appUserState = new AppUserState()
            {
                Email = user.Email,
                Name = user.Name,
                UserId = user.Id,
                Theme = user.Theme,
                IsAdmin = user.IsAdmin
            };
            IdentitySignin(appUserState, user.OpenId, rememberMe);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("New", "Snippet", null);
            */
            return View();
        }

    }
}
