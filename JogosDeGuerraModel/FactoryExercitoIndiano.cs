using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class FactoryExercitoIndiano : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroIndiano();
        }

        public override Cavaleiro CriarCavalaria()
        {
            return new CavaleiroIndiana();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroIndiano();
        }
    }
}
