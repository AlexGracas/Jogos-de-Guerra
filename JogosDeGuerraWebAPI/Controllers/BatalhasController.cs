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

        [Route("Iniciar")]
        [HttpGet]
        public Batalha IniciarBatalha(int id)
        {
            var usuario = this.ObterUsuarioLogado();
            var batalha = ctx.Batalhas
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Where(b => 
                (b.ExercitoBranco.Usuario.Email == usuario.Email 
                || b.ExercitoPreto.Usuario.Email == usuario.Email)
                && ( b.ExercitoBranco != null && b.ExercitoPreto != null) 
                && b.Id == id ).FirstOrDefault();

                        
            if (batalha.Tabuleiro == null){
                batalha.Tabuleiro = new Tabuleiro();
                batalha.Tabuleiro.Altura = 8;
                batalha.Tabuleiro.Largura = 8;
            }

            if(batalha.Estado== Batalha.EstadoBatalhaEnum.NaoIniciado)
            {
                batalha.Tabuleiro.IniciarJogo(batalha.ExercitoBranco, batalha.ExercitoPreto);
                Random r = new Random();
                batalha.Turno = r.Next(100) < 50 
                    ? batalha.ExercitoPreto : 
                    batalha.ExercitoBranco;
            }
            ctx.SaveChanges();
            return batalha;
        }

        [Route("Jogar")]
        [HttpPost]
        public Batalha Jogar(Movimento movimento)
        {
            movimento.Elemento = ctx.ElementosDoExercitos.Find(movimento.ElementoId);
            movimento.Batalha = ctx.Batalhas.Find(movimento.BatalhaId);
            var usuario = ObterUsuarioLogado();
            if (usuario.Id == movimento.AutorId)
            {
                var batalha = ctx.Batalhas
                    .Include(b => b.ExercitoBranco)
                    .Include(b => b.ExercitoPreto)
                    .Include(b => b.Tabuleiro)
                    .Include(b => b.ExercitoBranco.Elementos)
                    .Include(b => b.ExercitoPreto.Elementos)
                    .Where(b => b.Id== movimento.BatalhaId).First();
                if (movimento.AutorId == batalha.TurnoId)
                {
                    batalha.Jogar(movimento);
                }
                return batalha;
            }
            return null;
        }


        [Route("CriarNovaBatalha")]
        [HttpGet]
        public Batalha CriarNovaBatalha(AbstractFactoryExercito.Nacao Nacao)
        {
            //Obter usuário LOgado
            var usuarioLogado = ObterUsuarioLogado();
            //Verificar se existe uma batalha cujo exercito branco esteja definido
            //E exercito Preto esteja em branco
            var batalha = ctx.Batalhas
                .Include(x => x.ExercitoBranco.Usuario)
                .Where(b => b.ExercitoPreto == null && 
                    b.ExercitoBranco != null &&
                    b.ExercitoBranco.Usuario.Email != usuarioLogado.Email)
                .FirstOrDefault();
            if (batalha == null)
            {
                batalha = new Batalha();
                ctx.Batalhas.AddOrUpdate(batalha);
                ctx.SaveChanges();
            }
            batalha.CriarBatalha(Nacao, usuarioLogado);
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
