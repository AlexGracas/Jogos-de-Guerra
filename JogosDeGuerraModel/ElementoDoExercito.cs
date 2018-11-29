using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    [DataContract(IsReference = true)]
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
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Saude { get; set; }
        [DataMember]
        public Posicao posicao { get; set; }
        [DataMember]
        public int TabuleiroId { get; set; }
        [ForeignKey("TabuleiroId")]
        [InverseProperty("ElementosDoExercito")]
        public Tabuleiro Tabuleiro { get; set; }
        [DataMember]
        public int ExercitoId { get; set; }
        [ForeignKey("ExercitoId")]
        [InverseProperty("Elementos")]
        public Exercito Exercito { get; set; }
        [DataMember]
        public abstract int AlcanceMovimento {get; protected set;}
        [DataMember]
        public abstract int AlcanceAtaque { get; protected set; }
        [DataMember]
        public abstract int Ataque { get; protected set; }
    }


}
