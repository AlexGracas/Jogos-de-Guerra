using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class Posicao
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Tabuleiro Tabuleiro { get; set; }

        public ElementoDoExercito Elemento { get; set; }
    }
}
