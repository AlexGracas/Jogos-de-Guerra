using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public JogosDeGuerraModel.ModelJogosDeGuerra ctx { get; set; } = new JogosDeGuerraModel.ModelJogosDeGuerra();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            bool usuarioAutenticado = 
                Utils.Utils.ObterUsuarioLogado(
                    ctx
                    ) != null;

            if (!usuarioAutenticado)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        
        public ActionResult Tabuleiro(int BatalhaId=-1)
        {
            ViewBag.Title = "Tabuleiro";
            var batalha = ctx.Batalhas
                   .Where(b => b.Id == BatalhaId).FirstOrDefault();
            if (batalha!=null)
                return View(batalha);
            return View();
        }

        public ActionResult Login(string usuario, string password, string rememberme, string returnurl)
        {           
            return View();
        }

    }
}
