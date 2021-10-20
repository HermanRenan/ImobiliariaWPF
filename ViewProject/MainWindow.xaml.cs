using Application.Interfaces;
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
using ViewProject.Container;

namespace ViewProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly InterfaceAppProduct _InterfaceProductApp;
        public MainWindow(InterfaceAppProduct _InterfaceProductApp)
        {
            this._InterfaceProductApp = _InterfaceProductApp;
        }

        public MainWindow()
        {
            StartUp.Main();
            InitializeComponent();
        }

        private void Cadastro_Click(object sender, RoutedEventArgs e)
        {
            this._InterfaceProductApp.Add(new Application.Model.ClientModel() { Documento = "109", Email = "renan.herman" });
        }
    }
}
