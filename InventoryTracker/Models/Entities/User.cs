﻿namespace InventoryTracker.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Boolean IsActive { get; set; }
    }
}
