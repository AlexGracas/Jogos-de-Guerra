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
        public TelaDeGuerra TelaDeGuerra { get; set; }
        
        public ElementosDoExercito Elemento { get; set;}
        public CommandCriarGuerreiro(TelaDeGuerra tg)
        {
            this.TelaDeGuerra = tg;
        }
        public void Execute()
        {
            this.Elemento = this.TelaDeGuerra
                .FactoryExercito.CriarGuerreiro();
            TelaDeGuerra.Elementos.Add(this.Elemento);
        }

        public void Undo()
        {
            TelaDeGuerra.Elementos
                .Remove(this.Elemento);

        }
    }
}
