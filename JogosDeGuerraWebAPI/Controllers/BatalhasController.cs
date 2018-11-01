using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class BatalhasController : ApiController
    {
        public ModelJogosDeGuerra ctx { get; set; }
        // GET: api/Batalhas
        public IEnumerable<Batalha> Get()
        {
            return ctx.Batalhas.ToList();
        }

        // GET: api/Batalhas/5
        public Batalha Get(int id)
        {
            return null;
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
