using LTS_Proto.BL.Models;
using LTS_Proto.DL;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace LTS_Proto.BL.BO {
    public class UsrBO {
        private readonly IUsrDM UsrDM;

        public UsrBO(IUsrDM usrDM) {
            UsrDM = usrDM;
        }
        public UsrModel Auth(string usr, string psw) {
            UsrModel lxUsr = UsrDM.Auth(usr, psw);

            return lxUsr;
        }

        public UsrModel Obt(string usr) {
            UsrModel lxUsr = UsrDM.Obt(usr);
            return lxUsr;
        }

        public async Task<IEnumerable<UsrModel>> Lst_Obt() {
            return await UsrDM.Lst_Obt();
        }
    }
}
