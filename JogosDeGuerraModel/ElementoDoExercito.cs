using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public abstract class ElementoDoExercito
    {
        public override bool Equals(object obj)
        {
            if (obj is ElementoDoExercito && this.Id > 0)
            {
                return ((ElementoDoExercito)obj).Id == this.Id;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            if (this.Id > 0)
            {
                return this.Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public int Id { get; set; }
        public int Saude { get; set; }

        public Posicao posicao { get; set; }
        
        public int TabuleiroId { get; set; }
        [ForeignKey("TabuleiroId")]
        [InverseProperty("ElementosDoExercito")]
        public Tabuleiro Tabuleiro { get; set; }

        public int ExercitoId { get; set; }
        [ForeignKey("ExercitoId")]
        [InverseProperty("Elementos")]
        public Exercito Exercito { get; set; }

        public abstract int AlcanceMovimento {get; protected set;}
    
        public abstract int AlcanceAtaque { get; protected set; }

        public abstract int Ataque { get; protected set; }
    }


}
