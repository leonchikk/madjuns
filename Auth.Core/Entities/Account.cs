using Auth.Core.Enumerations;
using Common.Core.Helpers;
using Common.Core.Models;
using System;

namespace Auth.Core.Entities
{
    public class Account : BaseEntity
    {
        private Account() { }

        public Account(string email, string password, string username, DateTime birthDay, SystemRoles accountRole)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = CryptographyHelper.EncryptString(password);
            UserName = username;
            BirthDay = birthDay;
            IsEmailVerified = false;
            VerifyEmailToken = Guid.NewGuid().ToString();
            ForgotPasswordToken = null;
            SystemRole = (int)accountRole;
        }

        public void VerifyEmail()
        {
            IsEmailVerified = true;
        }

        public void ChangePassword(string password)
        {
            Password = CryptographyHelper.EncryptString(password);
            ForgotPasswordToken = null;
        }

        public void GenerateForgotPasswordToken()
        {
            ForgotPasswordToken = Guid.NewGuid().ToString();
        }

        public void SetSystemRole(SystemRoles role) => SystemRole = (int)role;

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDay { get; set; }

        public string ProviderId { get; set; }

        public bool IsEmailVerified { get; set; }

        public string VerifyEmailToken { get; private set; }

        public string ForgotPasswordToken { get; set; }

        public int SystemRole { get; set; }

        public string Password { get; set; }
    }
}
