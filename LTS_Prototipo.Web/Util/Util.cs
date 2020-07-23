using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTS_Proto.Web {
    public static class Util {
        public static (string, string) Msg_Set(string msg = "", string msgCls = "alert-info") {
            return (msg, msgCls);
        }
        public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data) {
            return js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
        }
    }
}
