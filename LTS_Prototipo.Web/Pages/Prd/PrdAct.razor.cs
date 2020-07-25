using LTS_Proto.BL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace LTS_Proto.Web.Pages.Prd
{
    public partial class PrdAct: ComponentBase
    {
        [Parameter]
        public string PrdId { get; set; }

        protected PrdModel Prd = new PrdModel();
        protected string Msg;
        protected string MsgCls;
        protected string PagTit;

        protected override void OnParametersSet() {
            if(PrdId == null || PrdId == "") {
                PagTit = L["New Product"];
                Prd = new PrdModel();
            } else {
                PagTit = L["Edit Product"];
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if(firstRender) {
                await js.InvokeVoidAsync("jsfunction.focusElement", "Prd");
            }
        }
        protected override void OnInitialized() {
            try {
                Prd = PrdDM.Prd_Obt(PrdId);

            } catch(Exception ex) {
                (Msg, MsgCls) = Util.Msg_Set(ex.Message, "alert-danger");
            }
        }

        protected void VldFrm() {
            try {
                if(PrdId == null || PrdId == "") {
                    PrdDM.Prd_Cre(Prd);
                } else {
                    PrdDM.Prd_Grd(Prd);
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
