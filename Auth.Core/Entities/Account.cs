using Common.Core.Helpers;
using Common.Core.Models;
using System;

namespace Auth.Core.Entities
{
    public class Account : BaseEntity
    {
        private Account() { }

        public Account(string email, string password, string username, DateTime birthDay)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = CryptographyHelper.EncryptString(password);
            UserName = username;
            BirthDay = birthDay;
            IsEmailVerified = false;
            VerifyEmailToken = Guid.NewGuid().ToString();
            ForgotPasswordToken = null;
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

        public string Email { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDay { get; set; }

        public string ProviderId { get; set; }

        public bool IsEmailVerified { get; set; }

        public string VerifyEmailToken { get; private set; }

        public string ForgotPasswordToken { get; set; }

        public string Password { get; set; }
    }
}
