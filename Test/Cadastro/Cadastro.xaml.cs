using Application.Model;
using Autofac;
using Entities.Enums;
using Entities.Notify;
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
using ViewProject.Container;
using ViewProjectNew.Service;

namespace ViewProject.Cadastro
{
    /// <summary>
    /// Interaction logic for Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        public ClientModel client { get; set; }
        public Cadastro(ClientModel client)
        {
            this.client = client;            
            InitializeComponent();
            loadData(client);
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            StartUp app = new StartUp();
            app.OnStartup();

            var checkedValue = panel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);

            var client = new ClientModel();
            client.Name = NomeTxt.Text;
            client.Documento = CPF.Text;
            client.Email = EmailTxt.Text.Trim();
            client.Endereço = EnderecoTxt.Text.Trim();
            client.Telefone = TelefoneTxt.Text.Trim();
            client.Id = Id.Text.Trim() != "" ? Convert.ToInt32(Id.Text.Trim()) : 0;
            client.Tipo = (bool)Juridica.IsChecked ? TipoConta.ContaPessoaJuridica : (bool)Fisica.IsChecked ? TipoConta.ContaPessoaFisica : TipoConta.Vazia;

            using (var scope = app.container.BeginLifetimeScope())
            {
                if (validate(client))
                {
                    scope.Resolve<ClienteService>().AddClient(client);
                    if (client.Notitycoes.Any())                    
                        ErrorSystem(client.Notitycoes);
                    else
                    {
                        CameBack();
                    }
                        
                }
            }
        }

        private bool validate(ClientModel client)
        {
            if(client.Tipo.ToString() == "Vazia")
            {
                MessageBoxResult resu = MessageBox.Show("Obrigatório marcar algum tipo de conta", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if(String.IsNullOrEmpty(client.Name))
            {
                MessageBoxResult resu = MessageBox.Show("Obrigatório preencher o Nome", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (String.IsNullOrEmpty(client.Email))
            {
                MessageBoxResult resu = MessageBox.Show("Obrigatório preencher o Email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (String.IsNullOrEmpty(client.Endereço))
            {
                MessageBoxResult resu = MessageBox.Show("Obrigatório preencher o Endereço", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (String.IsNullOrEmpty(client.Telefone))
            {
                MessageBoxResult resu = MessageBox.Show("Obrigatório preencher o Telefone", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void loadData(ClientModel client)
        {
            NomeTxt.Text = client.Name;
            EmailTxt.Text = client.Email;
            CPF.Text = client.Documento;
            EnderecoTxt.Text = client.Endereço;
            TelefoneTxt.Text = client.Telefone;
            Id.Text = client.Id.ToString();
            if (client.Documento == TipoConta.ContaPessoaJuridica.ToString())
                Juridica.IsChecked = true;
            else
                Fisica.IsChecked = true;

        }

        private void ErrorSystem(List<Notifies> notify)
        {
            foreach (var item in notify)
            {
                MessageBoxResult resu = MessageBox.Show(item.mensagem, "Erro ao cadastrar", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void VoltarTela_Click(object sender, RoutedEventArgs e)
        {
            CameBack();
        }


        private void CameBack()
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
