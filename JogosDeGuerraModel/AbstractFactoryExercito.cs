using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public abstract class AbstractFactoryExercito
    {
        public abstract Arqueiro CriarArqueiro();

        public abstract Cavaleiro CriarCavalaria();

        public abstract Guerreiro CriarGuerreiro();

        public enum Nacao { India=1, Persia=2, Egito=3};

        /// <summary>
        /// Este método é uma factory para a Abstract Factory.
        /// Deste modo, não existirá dependência do sistema com as 
        /// Factories concretas.
        /// </summary>
        /// <param name="nacionalidade"></param>
        /// <returns></returns>
        public static AbstractFactoryExercito CriarFactoryExercito(Nacao nacionalidade)
        {
            AbstractFactoryExercito factory = null;
            if (nacionalidade == Nacao.Persia)
            {
                factory = null;
            }
            else if (Nacao.Egito == nacionalidade)
            {
                factory = new FactoryExercitoPersa();
            }
            else if (Nacao.India == nacionalidade)
            {
                factory = new FactoryExercitoIndiano();
            }
            return factory;
        }
    }
}
