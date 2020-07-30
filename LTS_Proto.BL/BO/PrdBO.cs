using LTS_Proto.BL.Models;
using LTS_Proto.DL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LTS_Proto.BL.BO {
    public class PrdBO {
        private readonly IPrdDM PrdDM;

        public PrdBO(IPrdDM prdDM) {
            PrdDM = prdDM;
        }
        public void Brr(string PrdId) {
            try {
                PrdDM.Prd_Brr(PrdId);
            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<int> Cre(PrdModel prd) {
            try {
                var lxRows = await PrdDM.Prd_Cre(prd);
                return lxRows;
            } catch(Exception ex) {
                throw ex;
            }
        }
        public void Grd(PrdModel prd) {
            try {
                PrdDM.Prd_Grd(prd);

            } catch(Exception ex) {
                throw ex;
            }
        }

        public PrdModel Obt(string prd) {
            try {
                PrdModel lxPrd = PrdDM.Prd_Obt(prd);
                return lxPrd;
            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<IEnumerable<PrdModel>> Lst_Obt() {
            try {
                return await PrdDM.LstPrd_Obt();
            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}
