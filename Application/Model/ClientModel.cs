using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Model
{
    public class ClientModel : BaseModel
    {
        public string Endereço { get; set; }
        public TipoConta Tipo { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
