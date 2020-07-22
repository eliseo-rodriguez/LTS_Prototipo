using Blazored.SessionStorage;
using LTS_Proto.BL.Models;
using LTS_Proto.DL;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LTS_Proto.Web.Data {
    public class UsrAuthenticationStateProvider : AuthenticationStateProvider {
        private readonly ISessionStorageService SessionStorage;

        public UsrAuthenticationStateProvider(IUsrDM usrDM, ISessionStorageService sessionStorage) {
            SessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            ClaimsIdentity lxIdentity;
            var lxUsrSsn = await SessionStorage.GetItemAsync<string>("usr");

            if(lxUsrSsn != null) {
                var lxNomSsn = await SessionStorage.GetItemAsync<string>("nom");
                var lxRolSsn = await SessionStorage.GetItemAsync<string>("rol");

                lxIdentity = Identity_Obt(lxUsrSsn, lxNomSsn, "", lxRolSsn);
            } else {
                lxIdentity = new ClaimsIdentity();
            }
            var lxUser = new ClaimsPrincipal(lxIdentity);

            return await Task.FromResult(new AuthenticationState(lxUser));
        }

        public async void MarkUsrAsAuth(UsrModel usr) {
            var lxIdentity = Identity_Obt(usr.Usr, usr.Nom, usr.Psw, usr.Rol);
            var lxUser = new ClaimsPrincipal(lxIdentity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(lxUser)));

            await Ssn_Set(usr);
        }

        public async Task MarkUsrAsLoggedOut() {
            await Ssn_Rmv();

            var lxIdentity = new ClaimsIdentity();
            var lxUser = new ClaimsPrincipal(lxIdentity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(lxUser)));
        }

        private ClaimsIdentity Identity_Obt(string usrSsn, string nom, string psw, string rol) {
            ClaimsIdentity lxIdentity;
            var lxClaims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, usrSsn),
                    new Claim(ClaimTypes.Name, nom),
                    new Claim(ClaimTypes.Role, rol),
                    new Claim("Psw", psw)
                };

            lxIdentity = new ClaimsIdentity(lxClaims, "auth");

            return lxIdentity;
        }

        private async Task Ssn_Set(UsrModel usr) {
            await SessionStorage.SetItemAsync("usr", usr.Usr);
            await SessionStorage.SetItemAsync("nom", usr.Nom);
            await SessionStorage.SetItemAsync("rol", usr.Rol);
        }

        private async Task Ssn_Rmv() {
            await SessionStorage.RemoveItemAsync("usr");
            await SessionStorage.RemoveItemAsync("nom");
            await SessionStorage.RemoveItemAsync("rol");
        }
    }
}
