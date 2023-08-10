using System;
using ApplicationServices.Interfaces;

namespace ApplicationServices.Implementation
{
    public class SecurityService : ISecurityService
    {
        public bool IsCurrentUserAdmin => false;

        public string[] CurrentUserPermissions => Array.Empty<string>();
    }
}