using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerra
{
    class FactoryExercitoPersa : AbstractFactoryExercito
    {
        public override Arqueiro CriarArqueiro()
        {
            return new ArqueiroPersa();
        }

        public override Cavalaria CriarCavalaria()
        {
            return new CavalariaPersa();
        }

        public override Guerreiro CriarGuerreiro()
        {
            return new GuerreiroPersa();
        }
    }
}
