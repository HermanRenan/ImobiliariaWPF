using Application.Model;
using AutoMapper;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public class ProductMappers : Profile
    {
        public ProductMappers()
        {
            CreateMap<Client, ClientModel>().ReverseMap();
            CreateMap<ClientModel, Client>().ReverseMap();
        }
    }
}
