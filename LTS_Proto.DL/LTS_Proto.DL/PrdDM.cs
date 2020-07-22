using Dapper;
using LTS_Proto.BL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace LTS_Proto.DL {
    public interface IPrdDM {
        IDbConnection SqlCnx { get; }

        Task<IEnumerable<PrdModel>> LstPrd_Obt();
        void Prd_Brr(string prd);
        Task<int> Prd_Cre(PrdModel prd);
        void Prd_Grd(PrdModel prd);
        PrdModel Prd_Obt(string prd);
    }

    public class PrdDM : IPrdDM {
        private readonly IConfiguration Cfg;
        private readonly string CnxStr;

        public PrdDM(IConfiguration cfg) {
            Cfg = cfg;
            CnxStr = Cfg.GetConnectionString("Default");
        }

        public IDbConnection SqlCnx {
            get {
                return new SqlConnection(CnxStr);
            }
        }
        public async Task<IEnumerable<PrdModel>> LstPrd_Obt() {
            string lxQry =
                "SELECT * " +
                "  FROM [Prd] ";

            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    return await cnx.QueryAsync<PrdModel>(lxQry);
                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        public void Prd_Brr(string prd) {
            string lxQry =
                "DELETE " +
                "  FROM [Prd] " +
                " WHERE Prd = @Prd";
            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    cnx.Query(lxQry, new { Prd = prd });
                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        public async Task<int> Prd_Cre(PrdModel prd) {
            string lxQry =
                "INSERT INTO [Prd] " +
                "(Prd, Dsc, St, Prc, Mon) " +
                "OUTPUT Inserted.Prd " +
                "VALUES " +
                "(@Prd, @Dsc, @St, @Prc, @Mon) ";

            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    var lxRtrnVal = await cnx.ExecuteAsync(lxQry, prd);

                    return (int)lxRtrnVal;
                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        public void Prd_Grd(PrdModel prd) {
            string lxQry =
                "UPDATE [Prd] " +
                "   SET Dsc = @Dsc, St = @St, Prc = @Prc, Mon = @Mon " +
                " WHERE Prd = @Prd";
            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    cnx.Query(lxQry, prd);
                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        public PrdModel Prd_Obt(string prd) {
            string lxQry =
                "SELECT * " +
                "  FROM [Prd] " +
                " WHERE Prd = @Prd";

            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    return cnx.Query<PrdModel>(lxQry, new { Prd = prd }).FirstOrDefault();
                }

            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}
