using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Domain.Entities.UserEntities
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User:IdentityUser
    {
        public const string AdminUserName = "Admin";
        public const string DefualtAdminPassword = "Qwerty12346789";

        public const string RoleAdmin = "Administrator";
        public const string RoleUser = "User";

    }

}
