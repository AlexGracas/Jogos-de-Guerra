using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogosDeGuerraModel
{
    public class Tabuleiro
    {
        public int Id { get; set; }
        public int Largura { get; set; }

        public int Altura { get; set; }

        public ElementoDoExercito[][] Casas { get; set;}

        public Posicao ObterPosicao(ElementoDoExercito elemento)
        {
            Posicao posicao = new Posicao();
            posicao.Tabuleiro = this;
            for (int i = 0; i < this.Largura; i++){
                for(int j = 0; j ++ < this.Altura; j++)
                {
                    if (Casas[i][j] == elemento)
                    {
                        posicao.Largura = i;
                        posicao.Altura = j;
                        return posicao;
                    }
                }
            }
            return null;
        }

        public void IniciarJogo(Exercito exercito1, Exercito exercito2)
        {
            Casas = new ElementoDoExercito[Largura][];
            for(int i = 0; i < this.Largura; i++){
                ElementoDoExercito[] elementos = this.Casas[i];
                elementos = new ElementoDoExercito[Altura];
            }

            for(int i=0; i< this.Largura; i++)
            {
                for( int j=0; j< this.Altura; j++)
                {
                    //Ultima ou primeira fileira?
                    Exercito exercito = (j == 0) ? exercito1 : exercito2;
                    ElementoDoExercito elemento = null;
                    AbstractFactoryExercito factory = 
                        AbstractFactoryExercito.CriarFactoryExercito(exercito.Nacao);

                    if (j==0 || j == this.Altura - 1)
                    {             
                        //Cria arqueiro nas posições pares e Cavaleiros nas posições impáres.
                        elemento= 
                            (i%2==0)?
                            (ElementoDoExercito)factory.CriarArqueiro(): 
                            (ElementoDoExercito)factory.CriarCavalaria();                      
                    }else if(j==1 || j == this.Altura - 2)
                    {
                        //Cria guerreiros
                        elemento = (ElementoDoExercito)factory.CriarGuerreiro();
                    }
                    //Se o elemento tiver sido instanciado criará o elemento no tabuleiro.
                    if (elemento != null)
                    {
                        exercito.Elementos.Add(elemento);
                        this.Casas[i][j] = elemento;
                    }
                }
            }
        }

    }
}
