using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace JogosDeGuerraWebAPI.Controllers
{

    [Authorize]
    [RoutePrefix("api/Batalhas")]
    public class BatalhasController : ApiController
    {
        public ModelJogosDeGuerra ctx { get; set; } = new ModelJogosDeGuerra();

        // GET: api/Batalhas
        public IEnumerable<Batalha> Get(bool Finalizada = true)
        {
            IEnumerable<Batalha> batalhas;
            if (Finalizada)
            {
                batalhas = ctx.Batalhas.Where(b => b.Vencedor != null).ToList();
            }
            else
            {
                batalhas = ctx.Batalhas.ToList();
            }
            return batalhas;
        }

        // GET: api/Batalhas/5
        public Batalha Get(int id)
        {
            
            return ctx.Batalhas.Find(id);
        }

        [Route("CriarNovaBatalha")]
        [HttpGet]
        public Batalha CriarNovaBatalha(AbstractFactoryExercito.Nacao Nacao)
        {
            Exercito e;
            var usuarioLogado = ObterUsuarioLogado();
            var batalha = ctx.Batalhas
                .Include(x => x.ExercitoBranco.Usuario)
                .Where(b => b.ExercitoPreto == null && b.ExercitoBranco.Usuario.Email != usuarioLogado.Email)
                .FirstOrDefault();
            //Se não existir uma batalha cujo exercito preto seja vazio, criar uma nova batalha.
            if (batalha == null)
            {
                batalha = new Batalha();
                batalha.ExercitoBranco = new Exercito();
                e = batalha.ExercitoBranco;
            }
            //Caso exista, colocar-se como desafiante.
            else
            {
                batalha.ExercitoPreto = new Exercito();
                e = batalha.ExercitoPreto;
            }            
            e.Nacao = Nacao;
            e.Usuario = usuarioLogado;
            ctx.Batalhas.AddOrUpdate(batalha);
            ctx.SaveChanges();
            //Não iria conseguir os Ids Corretos;
            //ctx.SaveChangesAsync();
            return batalha;
        }

        // POST: api/Batalhas
        public void Post([FromBody]Batalha value)
        {

        }

        // PUT: api/Batalhas/5
        public void Put(int id, [FromBody]Batalha value)
        {
        }

        // DELETE: api/Batalhas/5
        public void Delete(int id)
        {
        }

        private Usuario ObterUsuarioLogado()
        {
            var ident = System.Web.HttpContext.Current.User.Identity;
            if (ident.IsAuthenticated)
            {
                var usuario = ctx.Usuarios.Where(u => u.Email == ident.Name).SingleOrDefault();
                if(usuario == null)
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
