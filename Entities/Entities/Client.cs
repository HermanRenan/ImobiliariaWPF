using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Client : Base
    {   
        public string Endereço { get; set; }

        [Column("Tipo", TypeName = "varchar(50)")]
        public TipoConta Tipo { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
