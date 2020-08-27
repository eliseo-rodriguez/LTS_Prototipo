using LTS_Proto.BL.BO;
using LTS_Proto.BL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTS_Proto.Web.Pages.Prd {
    public partial class PrdDet : ComponentBase {
        public string PrdId { get; set; }
        private readonly PrdBO _PrdBO;
        public PrdModel Prd { get; set; }
        protected string Msg;
        protected string MsgCls;
        protected string PagTit;

        public PrdDet(PrdBO prdBO) {
            _PrdBO = prdBO;
        }

        protected override void OnInitialized() {
            try {
                Prd = _PrdBO.Obt(PrdId);
            } catch(Exception ex) {
                (Msg, MsgCls) = Util.Msg_Set(ex.Message, "alert-danger");
            }

        }
    }
}
