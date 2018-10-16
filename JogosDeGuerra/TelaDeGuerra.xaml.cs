using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JogosDeGuerra
{
    /// <summary>
    /// Interaction logic for TelaDeGuerra.xaml
    /// </summary>
    public partial class TelaDeGuerra : Window
    {
        public TelaDeGuerra()
        {
            InitializeComponent();
        }

        public Stack<Command> Comandos { get; set; }

        public List<ElementosDoExercito> Elementos { get; set; }

        public AbstractFactoryExercito FactoryExercito {get;set;}

        private void ButtonCriarArqueiro_Click(object sender, RoutedEventArgs e)
        {
            Command cmd = new CommandCriarGuerreiro(this);

            cmd.Execute();
            this.Comandos.Push(cmd);
        }

        private void ButtonCriarGuerreiro_Click(object sender, RoutedEventArgs e)
        {
            Guerreiro guerreiro = FactoryExercito.CriarGuerreiro();
            this.Elementos.Add(guerreiro);
        }

        private void ButtonCriarCavalaria_Click(object sender, RoutedEventArgs e)
        {
            Cavalaria cavalaria = FactoryExercito.CriarCavalaria();
            this.Elementos.Add(cavalaria);
        }
    }
}
