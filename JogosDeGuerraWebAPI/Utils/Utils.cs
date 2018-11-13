using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JogosDeGuerraWebAPI.Utils
{
    public class Utils
    {
        public static Usuario ObterUsuarioLogado(JogosDeGuerraModel.ModelJogosDeGuerra ctx)
        {
            var ident = System.Web.HttpContext.Current.User.Identity;
            if (ident.IsAuthenticated)
            {
                var usuario = ctx.Usuarios.Where(u => u.Email == ident.Name).SingleOrDefault();
                if (usuario == null)
                {
                    Usuario u = new Usuario();
                    u.Email = ident.Name;
                    ctx.Usuarios.Add(u);
                    ctx.SaveChanges();
                    return u;
                }
                return usuario;
            }
            return null;
        }
    }
}