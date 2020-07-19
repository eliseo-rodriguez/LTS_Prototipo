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
    public interface IUsrDM {
        IDbConnection SqlCnx { get; }

        Task<IEnumerable<UsrModel>> LstUsr_Obt();
        UsrModel Usr_Auth(string usr, string psw);
        void Usr_Brr(string usr);
        Task<int> Usr_Cre(UsrModel usr);
        void Usr_Grd(UsrModel usr);
        UsrModel Usr_Obt(string usr);
    }

    public class UsrDM : IUsrDM {
        private readonly IConfiguration Cfg;
        private readonly string CnxStr;
        public UsrDM(IConfiguration cfg) {
            Cfg = cfg;
            CnxStr = Cfg.GetConnectionString("Default");
        }
        public IDbConnection SqlCnx {
            get {
                return new SqlConnection(CnxStr);
            }
        }

        public async Task<IEnumerable<UsrModel>> LstUsr_Obt() {
            string lxQry =
                "SELECT * " +
                "  FROM [Usr] ";

            try {
                using(IDbConnection cnx = SqlCnx) {
                    cnx.Open();
                    return await cnx.QueryAsync<UsrModel>(lxQry);
                }
            } catch(Exception ex) {
                throw ex;
            }
        }
        public UsrModel Usr_Auth(string usr, string psw) {
            string lxQry =
                "SELECT * " +
                "  FROM [Usr] " +
                " WHERE Usr = @Usr " +
                "   And Psw = @Psw ";

            using(IDbConnection cnx = SqlCnx) {
                cnx.Open();
                return cnx.Query<UsrModel>(lxQry, new { Usr = usr }).FirstOrDefault();
            }
        }

        public void Usr_Brr(string usr) {
            string lxQry =
                "DELETE " +
                "  FROM [Usr] " +
                " WHERE Usr = @Usr";

            using(IDbConnection cnx = SqlCnx) {
                cnx.Open();
                cnx.Query(lxQry, new { Usr = usr });
            }
        }
        public async Task<int> Usr_Cre(UsrModel usr) {
            string lxQry =
                "INSERT INTO [Usr] " +
                "(Usr, Nom, Psw, Rol, St, StmCre, StmMdf) " +
                "OUTPUT Inserted.Usr " +
                "VALUES " +
                "(@Usr, @Nom, @Psw, @Rol, @St, GetDate(), GetDate()) ";

            using(IDbConnection cnx = SqlCnx) {
                cnx.Open();
                var lxRtrnVal = await cnx.ExecuteAsync(lxQry, usr);

                return (int)lxRtrnVal;
            }
        }
        public void Usr_Grd(UsrModel usr) {
            string lxQry =
                "UPDATE [Usr] " +
                "   SET Nom = @Nom, Psw = @Psw, Rol = @Rol, St = @St, StmMdf = GetDate() " +
                " WHERE Usr = @Usr";
            using(IDbConnection cnx = SqlCnx) {
                cnx.Open();
                cnx.Query(lxQry, usr);
            }
        }
        public UsrModel Usr_Obt(string usr) {
            string lxQry =
                "SELECT * " +
                "  FROM [Usr] " +
                " WHERE Usr = @Usr";

            using(IDbConnection cnx = SqlCnx) {
                cnx.Open();
                return cnx.Query<UsrModel>(lxQry, new { Usr = usr }).FirstOrDefault();
            }
        }
    }
}
