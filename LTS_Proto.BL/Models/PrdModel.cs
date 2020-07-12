using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LTS_Proto.BL.Models {
    public enum PrdSt {
        Activo,
        Inactivo
    }

    public class PrdModel {

        [Required]
        public string Prd { get; set; }

        [Required]
        public string Dsc { get; set; }

        public PrdSt St { get; set; }

        public decimal Prc { get; set; }

        public string Mon { get; set; }

    }
}
