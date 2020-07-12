using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LTS_Proto.BL.Models
{
    public  enum UsrSt
    {
        Activo,
        Inactivo
    }
    public class UsrModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Usr { get; set; }

        public string Nom { get; set; }

        public string Psw { get; set; }

        public string Rol { get; set; }

        public UsrSt St { get; set; }

        public DateTime StmCre { get; set; }
        public DateTime StmMdf { get; set; }
    }
}
