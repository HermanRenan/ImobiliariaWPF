using Application.Interfaces;
using Application.OpenApp;
using Autofac;
using Domain.Interfaces.InterfaceProduct;
using infrastructure.Repository.Repositories;
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
using Test;
using ViewProject.Container;
using ViewProjectNew.Service;
using ViewProject.Cadastro;
using Application.Model;
using Newtonsoft.Json;

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
            InitializeComponent();
            LoadData();
        }

        public List<ClientModel> ListClient { get; set; }

        private void Cadastro_Click(object sender, RoutedEventArgs e)
        {
            var vend = new ViewProject.Cadastro.Cadastro();
            vend.Show();
            this.Close();
        }

        private void BtnNameSearch_Click(object sender, RoutedEventArgs e)
        {
            var app = StartInterface();

            using (var scope = app.container.BeginLifetimeScope())
            {                
                var name = NameSearch.Text.Trim();
                var dataClient = scope.Resolve<ClienteService>().GetClients().Where(b=> b.Name.Contains(name)).ToList();
                ListClient = dataClient;

                ConteudoCred.ItemsSource = CreateSureList(ListClient);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var app = StartInterface();

            using (var scope = app.container.BeginLifetimeScope())
            {
                var client = (((System.Windows.FrameworkElement)sender).DataContext);
                var convertDataClient = ToDynamic(client);

                var dataClient = scope.Resolve<ClienteService>().DeleteClient(convertDataClient);

                LoadData();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var client = (((System.Windows.FrameworkElement)sender).DataContext);

            var convertDataClient = ToDynamic(client);
            var vend = new ViewProject.Cadastro.Cadastro(convertDataClient);
            vend.Show();
            this.Close();
        }

        private void BtnDocSearch_Click(object sender, RoutedEventArgs e)
        {
            var app = StartInterface();

            using (var scope = app.container.BeginLifetimeScope())
            {
                var name = DocSearch.Text.Trim();
                var dataClient = scope.Resolve<ClienteService>().GetClients().Where(b => b.Documento.Contains(name)).ToList();
                ListClient = dataClient;

                ConteudoCred.ItemsSource = CreateSureList(ListClient);
            }
        }

        private void LoadData()
        {
            var app = StartInterface();

            using (var scope = app.container.BeginLifetimeScope())
            {                
                var dataClient = scope.Resolve<ClienteService>().GetClients();
                ListClient = dataClient;

                ConteudoCred.ItemsSource = CreateSureList(ListClient);
            }
        }


        private dynamic CreateSureList(List<ClientModel> list)
        {
            List<dynamic> listsure = new List<dynamic>();

            foreach (var item in list)
            {
                listsure.Add(new {
                    Id = item.Id,
                    Name = item.Name,
                    Telefone = item.Telefone,
                    Endereço = item.Endereço,
                    Tipo = item.Tipo,
                    Documento = item.Documento,
                    Email = item.Email
                });
            }

            return listsure;
        }

        public StartUp StartInterface()
        {
            StartUp app = new StartUp();
            app.OnStartup();
            return app; 
        }

        public ClientModel ToDynamic(dynamic obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject(json, typeof(ClientModel));
        }
    }
}
