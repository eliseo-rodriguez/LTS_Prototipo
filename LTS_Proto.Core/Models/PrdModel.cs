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
        const int MAX_DSC = 250;

        [Required(ErrorMessage = "El Código de producto en requerido")]
        public string Prd { get; set; }

        [Required(ErrorMessage = "La Descripción es requerida")]
        [StringLength(MAX_DSC)]
        public string Dsc { get; set; }

        public PrdSt St { get; set; }

        [Range(0.01,99999999,ErrorMessage = "El precio debe ser mayor que 0.01")]
        public decimal Prc { get; set; }

        public string Mon { get; set; }

    }
}
