using JogosDeGuerraModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JogosDeGuerra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEscolherExercito_Click(object sender, RoutedEventArgs e)
        {
            AbstractFactoryExercito factory = null;
            if(sender == BtnEgito)
            {
                factory =
                    AbstractFactoryExercito.CriarFactoryExercito(
                        AbstractFactoryExercito.Nacao.Egito);
            }else if(sender == BtnPersia)
            {
                factory =
                    AbstractFactoryExercito.CriarFactoryExercito(
                        AbstractFactoryExercito.Nacao.Persia);
            }
            else if(sender == BtnIndia)
            {
                factory =
                    AbstractFactoryExercito.CriarFactoryExercito(
                        AbstractFactoryExercito.Nacao.India);
            }
           
            TelaDeGuerra tg = new TelaDeGuerra();
            tg.FactoryExercito = factory;
            tg.ShowDialog();
        }
    }
}
