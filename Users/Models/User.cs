using System;

namespace Users.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string SurName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}