using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    [ComplexType]
    public class Posicao
    {
        public Posicao() { }

        public Posicao(int Altura, int Largura)
        {
            this.Altura = Altura;
            this.Largura = Largura;
        }

        public override int GetHashCode()
        {
            return 13+this.Altura*(int)Math.Pow(10,1000)+Largura;
        }
        public override bool Equals(object obj)
        {
            if(obj is Posicao)
            {
                var pos2 = (Posicao)obj;
                return pos2.Largura == this.Largura && 
                    pos2.Altura == this.Altura;
            }
            return base.Equals(obj);
        }
        public int Largura { get; set; } = -1;

        public int Altura { get; set; } = -1;
    }
}
