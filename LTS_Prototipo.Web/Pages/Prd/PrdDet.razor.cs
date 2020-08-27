using LTS_Proto.BL.BO;
using LTS_Proto.BL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTS_Proto.Web.Pages.Prd {
    public partial class PrdDet : ComponentBase {
        [Parameter]
        public string PrdId { get; set; }
        
        public PrdModel Prd { get; set; }
        protected string Msg;
        protected string MsgCls;
        protected string PagTit;
        public PrdDet() {

        }

        protected override void OnInitialized() {
            try {
                Prd = _PrdBO.Obt(PrdId);

                if(Prd == null) {
                    (Msg, MsgCls) = Util.Msg_Set("No Data", "alert-warning");
                }
            } catch(Exception ex) {
                (Msg, MsgCls) = Util.Msg_Set(ex.Message, "alert-danger");
            }

        }
    }
}
