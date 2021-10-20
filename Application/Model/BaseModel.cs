using Entities.Notify;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Model
{
    public class BaseModel : Notifies
    {        
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Favor preencher o campo Nome")]        
        public string Name { get; set; }
    }
}
