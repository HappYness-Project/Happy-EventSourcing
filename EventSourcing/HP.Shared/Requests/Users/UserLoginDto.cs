﻿namespace HP.Shared.Requests.Users
{
    public record UserLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
