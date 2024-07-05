﻿namespace InventoryTracker.Models.Entities
{
    public class Items
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}
