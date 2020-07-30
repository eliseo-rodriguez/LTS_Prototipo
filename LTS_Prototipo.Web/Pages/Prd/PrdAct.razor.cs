using LTS_Proto.BL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using LTS_Proto.BL.BO;

namespace LTS_Proto.Web.Pages.Prd
{
    public partial class PrdAct: ComponentBase
    {
        [Parameter]
        public string PrdId { get; set; }
        public bool EsEdt { get; set; }

        protected PrdModel Prd = new PrdModel();
        protected string Msg;
        protected string MsgCls;
        protected string PagTit;
        private readonly PrdBO _PrdBO;

        public PrdAct(PrdBO prdBO) {
            _PrdBO = prdBO;
        }

        protected override void OnParametersSet() {
            EsEdt = !(PrdId == null || PrdId == "");

            if(EsEdt) {
                PagTit = L["Edit Product"];
            } else {
                PagTit = L["New Product"];
                Prd = new PrdModel();
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if(firstRender) {
                if(EsEdt) {
                    await js.InvokeVoidAsync("jsfunction.focusElement", "Dsc");
                } else {
                    await js.InvokeVoidAsync("jsfunction.focusElement", "Prd");
                }
            }
        }
        protected override void OnInitialized() {
            try {
                Prd = _PrdBO.Obt(PrdId);

            } catch(Exception ex) {
                (Msg, MsgCls) = Util.Msg_Set(ex.Message, "alert-danger");
            }
        }

        protected void VldFrm() {
            try {
                if(EsEdt) {
                    _PrdBO.Grd(Prd);
                } else {
                    _ = _PrdBO.Cre(Prd);
                }
                NavMgr.NavigateTo("/Prds");
            } catch(Exception ex) {
                (Msg, MsgCls) = Util.Msg_Set(ex.Message, "alert-danger");
            }
        }

        protected void InVldFrm() {

        }
    }
}
