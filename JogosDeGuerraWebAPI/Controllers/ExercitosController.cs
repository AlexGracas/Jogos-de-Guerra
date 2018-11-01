using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class ExercitosController : ApiController
    {

        public JogosDeGuerraModel.ModelJogosDeGuerra ctx { get; set; }

        // GET: api/Exercitos
        public IEnumerable<Exercito> Get()
        {
            return ctx.Exercitos.ToList();
        }

        // GET: api/Exercitos/5
        public Exercito Get(int id)
        {
            Exercito ex = ctx.Exercitos.Find(id);

            return ex;
        }

        // POST: api/Exercitos
        public void Post([FromBody]Exercito value)
        {
            ctx.Exercitos.AddOrUpdate(value);
            ctx.SaveChanges();
        }

        // PUT: api/Exercitos/5
        public void Put(int id, [FromBody]Exercito value)
        {
            ctx.Exercitos.AddOrUpdate(value);
            ctx.SaveChanges();
        }

        // DELETE: api/Exercitos/5
        public void Delete(int id)
        {
            var ident = System.Web.HttpContext.Current.User.Identity;
            if (ident.IsAuthenticated)
            {
                var usuario = ctx.Usuarios.Where(u => u.Name == ident.Name);
                if (usuario == null)
                {
                    //TODO: https://stackoverflow.com/questions/10732644/best-practice-to-return-errors-in-asp-net-web-api
                    return;
                }
                var exercito = ctx.Exercitos.Find(id);
                if (exercito.Usuario == usuario)
                {
                    ctx.Exercitos.Remove(exercito);
                    ctx.SaveChanges();
                }
            }            
        }
    }
}
