using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace FestiSpecWebsite.Models
{

    public class FestispecPrincipal : IPrincipal
    {
        #region Identity Properties  

        public string UserId { get; set; }

        #endregion

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }

        public FestispecPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }
    }
}