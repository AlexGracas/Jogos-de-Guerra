using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public class Batalha
    {
        public int Id { get; set; }
        Tabuleiro Tabuleiro { get; set; }

        public Exercito ExercitoBranco { get; set; }

        public Exercito ExercitoPreto { get; set; }

        public Exercito Vencedor { get; set; } = null;

        public Exercito Turno { get; private set; }

        public bool VerificarAlcanceMovimento(Movimento movimento)
        {
            
                Posicao p = Tabuleiro.ObterPosicao(movimento.Elemento);
                if (p.Largura == movimento.Largura || p.Altura == movimento.Altura)
                {
                    int movLargura = Math.Abs(p.Largura - movimento.Largura);
                    int movAltura = Math.Abs(p.Altura - movimento.Altura);
                    //Se for um movimento de ataque comparar com o alcançe do elemento 
                    //para ataque. Caso seja um movimento comparar alcance para movimentação 
                    int alcance = (movimento.TipoMovimento == Movimento.EnumTipoMovimento.Atacar) ?
                            movimento.Elemento.AlcanceAtaque : movimento.Elemento.AlcanceMovimento;
                    if ((movAltura > 0 && movAltura < alcance) ||
                        (movLargura > 0 && movLargura < alcance))
                    {
                        return true;
                    }
                }
            
            return false;
        }

        public bool Jogar (Movimento movimento)
        {
            if(movimento.TipoMovimento == Movimento.EnumTipoMovimento.Mover)
            {
                //O destino da movimentação da peça deve estar vazio.
                if (this.Tabuleiro.Casas[movimento.Largura][movimento.Altura] == null)
                {
                    if (VerificarAlcanceMovimento(movimento) == true)
                    {
                        this.Tabuleiro.Casas[movimento.Largura][movimento.Altura] = movimento.Elemento;
                        return true;
                    }
                }
            }
            else if(movimento.TipoMovimento == Movimento.EnumTipoMovimento.Atacar)
            {
                //O destino do ataque deve estar ocupado.
                if (this.Tabuleiro.Casas[movimento.Largura][movimento.Altura] != null)
                {
                    //Verificar se é possível
                    if (VerificarAlcanceMovimento(movimento) == true)
                    {
                        var oponente = this.Tabuleiro.Casas[movimento.Largura][movimento.Altura];
                        oponente.Saude -= movimento.Elemento.Ataque;
                        if (oponente.Saude < 0)
                            oponente.Saude = 0;
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class Movimento
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public Exercito Autor { get; set; }
        public ElementoDoExercito Elemento { get; set; }

        public enum EnumTipoMovimento { Mover, Atacar}
        public EnumTipoMovimento TipoMovimento { get; set; }
    }
}
