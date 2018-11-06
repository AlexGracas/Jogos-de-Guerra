using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    class FactoryExercitoPersa : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroPersa();
        }

        public override Cavaleiro CriarCavalaria()
        {
            return new CavaleiroPersa();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroPersa();
        }
    }
}
