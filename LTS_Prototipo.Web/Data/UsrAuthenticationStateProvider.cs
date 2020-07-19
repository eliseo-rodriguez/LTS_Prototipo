using Blazored.SessionStorage;
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
                var lxClaims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, lxUsrSsn)
                };

                lxIdentity = new ClaimsIdentity(lxClaims, "auth");
            } else {
                lxIdentity = new ClaimsIdentity();
            }
            var lxUser = new ClaimsPrincipal(lxIdentity);

            return await Task.FromResult(new AuthenticationState(lxUser));
        }

        public void MarkUsrAsAuth(string usr) {
            var lxIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usr),
            }, "apiauth_type");
            var lxUser = new ClaimsPrincipal(lxIdentity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(lxUser)));
        }

        public async Task MarkUsrAsLoggedOut() {
            await SessionStorage.RemoveItemAsync("usr");
            var lxIdentity = new ClaimsIdentity();
            var lxUser = new ClaimsPrincipal(lxIdentity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(lxUser)));
        }

        private ClaimsIdentity Identity_Obt(string usrSsn, string nom, string psw, string rol)  {
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
    }
}
