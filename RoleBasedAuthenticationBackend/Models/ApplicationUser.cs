﻿using Microsoft.AspNetCore.Identity;

namespace WebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
