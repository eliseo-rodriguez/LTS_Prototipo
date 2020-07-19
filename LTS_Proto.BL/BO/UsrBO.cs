using LTS_Proto.BL.Models;
using LTS_Proto.DL;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace LTS_Proto.BL.BO {
    public class UsrBO {
        private readonly IUsrDM UsrDM;

        public UsrBO(IUsrDM usrDM) {
            UsrDM = usrDM;
        }
        public UsrModel Auth(string usr, string psw) {
            UsrModel lxUsr = UsrDM.Usr_Auth(usr, psw);

            return lxUsr;
        }

        public UsrModel Obt(string usr) {
            UsrModel lxUsr = UsrDM.Usr_Obt(usr);
            return lxUsr;
        }
    }
}
