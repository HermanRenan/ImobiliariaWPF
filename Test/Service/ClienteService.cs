using Application.Interfaces;
using Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewProjectNew.Service
{
    public class ClienteService
    {
        private readonly InterfaceAppProduct _InterfaceProductApp;
        public ClienteService(InterfaceAppProduct _InterfaceProductApp)
        {
            this._InterfaceProductApp = _InterfaceProductApp;
        }

        public string AddClient(ClientModel client)
        {
            try
            {
                if (client.Id > 0)
                    this._InterfaceProductApp.Update(client);
                else
                    this._InterfaceProductApp.Add(client);

                return "Sucesso";
            }
            catch (Exception ex)
            {

                return "Erro aplicação interno";

            }
        }

        public string DeleteClient(ClientModel client)
        {
            this._InterfaceProductApp.Delete(client);

            return "Excluido com sucesso";
        }

        public List<ClientModel> GetClients()
        {
            return this._InterfaceProductApp.ListaLL();
        }
    }
}
