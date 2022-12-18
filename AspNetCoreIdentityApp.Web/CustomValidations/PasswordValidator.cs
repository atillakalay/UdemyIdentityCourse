﻿using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {

            var errors = new List<IdentityError>();

            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new() { Code = "PasswordNoContainUserName", Description = "Şifre alanı kullanıcı adı içeremez." });
            }

            if (password!.ToLower().EndsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordNoEndsWith1234", Description = "Şifre 1, 2, 3, 4 rakamları ile bitemez" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed());
            }

            return Task.FromResult(IdentityResult.Success);

        }
    }

}
