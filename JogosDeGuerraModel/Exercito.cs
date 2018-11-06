using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public class Exercito
    {
        public int Id { get; set; }
        public ICollection<ElementoDoExercito> Elementos { get; set; } =
            new HashSet<ElementoDoExercito>();

        public Batalha Batalha { get; set; }

        public Usuario Usuario { get; set; }

        public AbstractFactoryExercito.Nacao Nacao { get; set; }
    }
}
