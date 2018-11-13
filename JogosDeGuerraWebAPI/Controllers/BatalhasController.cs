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
            var usuario = Utils.Utils.ObterUsuarioLogado(ctx);
            var batalha = ctx.Batalhas
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Where(b => 
                (b.ExercitoBranco.Usuario.Email == usuario.Email 
                || b.ExercitoPreto.Usuario.Email == usuario.Email)
                && ( b.ExercitoBranco != null && b.ExercitoPreto != null) 
                && b.Id == id ).FirstOrDefault();
            if (batalha == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(String.Format("Não foi possível carregar a Batalha.")),
                    ReasonPhrase = "Não foi possível carregar a batalha."
                };
                throw new HttpResponseException(resp);
            }
                        
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
            movimento.Elemento = 
                ctx.ElementosDoExercitos.Find(movimento.ElementoId);
            if (movimento.Elemento == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(String.Format("O Elemento não existe.")),
                    ReasonPhrase = "O elemento informado para movimento não existe."
                };
                throw new HttpResponseException(resp);
            }

            movimento.Batalha = 
                ctx.Batalhas.Find(movimento.BatalhaId);
            var usuario = Utils.Utils.ObterUsuarioLogado(ctx);

            if (usuario.Id == movimento.AutorId)
            {
                var batalha = ctx.Batalhas
                    .Include(b => b.ExercitoBranco)
                    .Include(b => b.ExercitoPreto)
                    .Include(b => b.Tabuleiro)
                    .Include(b => b.ExercitoBranco.Elementos)
                    .Include(b => b.ExercitoPreto.Elementos)
                    .Where(b => b.Id== movimento.BatalhaId).First();
                if(movimento.AutorId != movimento.Elemento.Exercito.UsuarioId)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(String.Format("A peça não pertence ao usuário.")),
                        ReasonPhrase = "Não foi possível executar o movimento."
                    };
                    throw new HttpResponseException(resp);
                }
                if (movimento.AutorId == batalha.Turno.UsuarioId)
                {
                    if (!batalha.Jogar(movimento))
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(String.Format("Não foi possível executar o movimento.")),
                            ReasonPhrase = "Não foi possível executar o movimento."
                        };
                        throw new HttpResponseException(resp);
                    }
                    return batalha;
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(
                            String
                            .Format("O turno atual é do adversário.")),
                        ReasonPhrase = "Você não tem permissão para executar esta ação."
                    };
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        String
                        .Format(
                            "O usuário tentou executar uma ação como se fosse outro usuário.")),
                    ReasonPhrase = 
                    "Você não tem permissão para executar esta ação."
                };
                throw new HttpResponseException(resp);
            }           
        }


        [Route("CriarNovaBatalha")]
        [HttpGet]
        public Batalha CriarNovaBatalha(AbstractFactoryExercito.Nacao Nacao)
        {

            //Obter usuário LOgado
            var usuarioLogado = Utils.Utils.ObterUsuarioLogado(ctx);
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

       
    }
}
