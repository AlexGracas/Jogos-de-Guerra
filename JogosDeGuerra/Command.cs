using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerra
{
    public interface Command
    {
        void Execute();
        void Undo();
    }

    public class CommandCriarGuerreiro
        : Command
    {
        AbstractFactoryExercito Factory { get; set; }
        
        List<ElementoDoExercito> Elementos { get; set;}
        public ElementoDoExercito Elemento { get; set;}

        public CommandCriarGuerreiro(
            AbstractFactoryExercito factory, 
            List<ElementoDoExercito> elementos)
        {
            this.Elementos = elementos;
            this.Factory = factory;
        }

        public void Execute()
        {
            this.Elemento = Factory.CriarGuerreiro();
            Elementos.Add(this.Elemento);
        }

        public void Undo()
        {
            Elementos.Remove(this.Elemento);
        }
    }

    public class CommandCriarCavalaria
       : Command
    {
        AbstractFactoryExercito Factory { get; set; }

        List<ElementoDoExercito> Elementos { get; set; }
        public ElementoDoExercito Elemento { get; set; }

        public CommandCriarCavalaria(
            AbstractFactoryExercito factory,
            List<ElementoDoExercito> elementos)
        {
            this.Elementos = elementos;
            this.Factory = factory;
        }

        public void Execute()
        {
            this.Elemento = Factory.CriarCavalaria();
            Elementos.Add(this.Elemento);
        }

        public void Undo()
        {
            Elementos.Remove(this.Elemento);
        }
    }
}
