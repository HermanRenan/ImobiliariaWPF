using Application.Interfaces;
using Application.Model;
using AutoMapper;
using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class ServiceClients : InterfaceAppProduct
    {
        IProduct IProduct;
        protected readonly IMapper _mapper;
        public ServiceClients(IProduct IProduct, IMapper _mapper)
        {
            this.IProduct = IProduct;
            this._mapper = _mapper;
        }
        public async Task Add(ClientModel product)
        {
            try
            {
                var Document = true;
                var entidade = _mapper.Map<ClientModel, Client>(product);

                var validName = entidade.ValidarPropriedadeString(product.Name, "Name");
                var getDocumentsExists = this.IProduct.ListAll().FirstOrDefault(x => x.Documento == product.Documento);
                if(getDocumentsExists != null)
                {
                    Document = entidade.ValidarDocumentoString(getDocumentsExists.Tipo.ToString(), "Tipo"); 
                }                

                

                if (validName && Document)
                {
                    await IProduct.Add(entidade);
                }

                product.Notitycoes = new List<Entities.Notify.Notifies>();
                product.Notitycoes = entidade.Notitycoes;
            }
            catch (Exception ex)
            {
                var see = ex.Message;
                
            }   
        }

        public async Task Delete(ClientModel Object)
        {
            var entidade = _mapper.Map<Client>(Object);

            await IProduct.Delete(entidade);
        }

        public async Task<ClientModel> GetEntityById(int Id)
        {
            var entidade = await this.IProduct.GetEntityById(Id);
            return _mapper.Map<ClientModel>(entidade);
        }

        public async Task<List<ClientModel>> List()
        {
            var listaEntidades = await this.IProduct.List();
            var entidade = _mapper.Map<List<ClientModel>>(listaEntidades);
            return entidade;
        }

        public async Task Update(ClientModel product)
        {
            var entidade = _mapper.Map<Client>(product);

            var validName = entidade.ValidarPropriedadeString(product.Name, "Name");
            //var validprice = entidade.ValidarPropriedadeDecimal(product.Price, "Price");

            if (validName)
            {
                try
                {
                    await IProduct.Update(entidade);

                }
                catch (Exception ex)
                {
                    var see = ex.Message;
                }
                
            }
        }

        public List<ClientModel> ListaLL()
        {
            var listaEntidades = this.IProduct.ListAll();
            var entidade = _mapper.Map<List<ClientModel>>(listaEntidades);
            return entidade;
        }
    }
}
