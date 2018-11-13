using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public class Batalha
    {
        public override bool Equals(object obj)
        {
            if (obj is Batalha)
                return ((Batalha)obj).Id == this.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public int Id { get; set; }

        public int? TabuleiroId { get; set; }
        [ForeignKey("TabuleiroId")]
        public Tabuleiro Tabuleiro { get; set; }


        public int? ExercitoBrancoId {get;set;}
        [ForeignKey("ExercitoBrancoId")]
        public Exercito ExercitoBranco { get; set; }

        public int? ExercitoPretoId { get; set; }
        [ForeignKey("ExercitoPretoId")]
        public Exercito ExercitoPreto { get; set; }

        public int? VencedorId { get; set; }
        [ForeignKey("VencedorId")]
        public Exercito Vencedor { get; set; } = null;

        public int? TurnoId { get; set; }
        [ForeignKey("TurnoId")]
        public Exercito Turno { get; set; }

        public enum EstadoBatalhaEnum {
            NaoIniciado =0,
            Iniciado =1,
            Finalizado =10,
            Cancelado =99}

        public EstadoBatalhaEnum Estado { get; set; } = 0;

        public bool VerificarAlcanceMovimento(Movimento movimento)
        {
            
        //Posição Atual
        Posicao p = Tabuleiro.ObterPosicao(movimento.Elemento);
        //Diferença entre a posição Atual e a nova.
        int movLargura = Math.Abs(p.Largura - movimento.posicao.Largura);
        int movAltura = Math.Abs(p.Altura - movimento.posicao.Altura);

        //Se for um movimento de ataque comparar com o alcançe do elemento 
        //para ataque. Caso seja um movimento comparar alcance para movimentação 
        int alcance = (movimento.TipoMovimento == Movimento.EnumTipoMovimento.Atacar) ?
                movimento.Elemento.AlcanceAtaque : movimento.Elemento.AlcanceMovimento;
        if ((movAltura > 0 && movAltura <= alcance) ||
            (movLargura > 0 && movLargura <= alcance))
        {
            return true;
        }

            
            return false;
        }

        public bool VerificarAlcanceAtaque(Movimento movimento)
        {
            return this.VerificarAlcanceMovimento(movimento);
        }

        public void CriarBatalha(
          AbstractFactoryExercito.Nacao Nacao,
          Usuario usuarioLogado)
        {
            Exercito e;
            //Se não existir uma batalha cujo exercito preto seja vazio, criar uma nova batalha.
            
            if (this.ExercitoBranco == null)
            {
                this.ExercitoBranco = new Exercito();
                e = this.ExercitoBranco;
            }
            //Caso exista, colocar-se como desafiante.
            else
            {
                this.ExercitoPreto = new Exercito();
                e = this.ExercitoPreto;
            }
            e.Nacao = Nacao;
            e.BatalhaId = this.Id;
            e.UsuarioId = usuarioLogado.Id;
        }

        public bool Jogar (Movimento movimento)
        {
            if(movimento.TipoMovimento == Movimento.EnumTipoMovimento.Mover)
            {
                //O destino da movimentação da peça deve estar vazio.
                if (this.Tabuleiro.ObterElemento(movimento.posicao) == null)
                {
                    if (VerificarAlcanceMovimento(movimento) == true)
                    {
                        this.Tabuleiro.MoverElemento(movimento);
                        return true;
                    }
                }
            }
            else if(movimento.TipoMovimento == Movimento.EnumTipoMovimento.Atacar)
            {
                //O destino do ataque deve estar ocupado.
                if (this.Tabuleiro.ObterElemento(movimento.posicao) != null)
                {
                    //Verificar se é possível atacar
                    if (VerificarAlcanceAtaque(movimento) == true)
                    {
                        var oponente = this.Tabuleiro.ObterElemento(movimento.posicao);
                        oponente.Saude -= movimento.Elemento.Ataque;
                        if (oponente.Saude < 0) {
                            oponente.Saude = 0;
                            //Caso tenha matado um elemento verificar quantos
                            //elementos vivos o oponente ainda tem.
                            if (oponente.Exercito.ElementosVivos.Count == 0)
                            {
                                //Caso seja 0 a batalha terminou.
                                this.Estado = EstadoBatalhaEnum.Finalizado;
                                //O vencedor é o autor do movimento.
                                this.Vencedor = movimento.Autor;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class Movimento
    {
        public Posicao posicao { get; set; }

        public int AutorId { get; set; }
        [ForeignKey("AutorId")]
        public Exercito Autor { get; set; }

        public int BatalhaId { get; set; }

        [ForeignKey("BatalhaId")]
        public Batalha Batalha { get; set; }

        public int ElementoId { get; set; }
        [ForeignKey("ElementoId")]
        public ElementoDoExercito Elemento { get; set; }

        public enum EnumTipoMovimento { Mover, Atacar}
        public EnumTipoMovimento TipoMovimento { get; set; }
    }
}
