﻿namespace Auth.Models.Requests
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public string RedirectUrl { get; set; }
    }
}
