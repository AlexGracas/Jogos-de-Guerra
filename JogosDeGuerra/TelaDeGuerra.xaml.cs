using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class TelaDeGuerra : Window, 
        INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler 
            PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            PropertyChanged?.
                Invoke(this, 
                new PropertyChangedEventArgs(propertyName));
        }

        public TelaDeGuerra()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public CommandManager CommandManager { get; set; }
            = new CommandManager();

        public List<ElementoDoExercito> 
            Elementos { get; set; }
            = new List<ElementoDoExercito>();

        public AbstractFactoryExercito FactoryExercito {get;set;}

        private void ButtonCriarArqueiro_Click(object sender, RoutedEventArgs e)
        {            
            
            Arqueiro arqueiro = FactoryExercito.
                CriarArqueiro();
            this.Elementos.Add(arqueiro);
            this.NotifyPropertyChanged("Elementos");
        }

        private void ButtonCriarGuerreiro_Click(object sender, RoutedEventArgs e)
        {
            Command cmd = new CommandCriarGuerreiro(
                this.FactoryExercito, 
                this.Elementos);
            this.CommandManager.Execute(cmd);
            this.NotifyPropertyChanged("Elementos");

        }
        private void ButtonCriarCavalaria_Click(object sender, RoutedEventArgs e)
        {

            Command cmd = new CommandCriarCavalaria(
                this.FactoryExercito,
                this.Elementos);
            this.CommandManager.Execute(cmd);
            this.NotifyPropertyChanged("Elementos");
        }

        private void ButtonDesfazer_Click(object sender, RoutedEventArgs e)
        {
            CommandManager.Undo();
            this.NotifyPropertyChanged("Elementos");
        }

        private void ButtonRefazer_Click(object sender, RoutedEventArgs e)
        {
            CommandManager.Redo();
            this.NotifyPropertyChanged("Elementos");
        }
    }
}
