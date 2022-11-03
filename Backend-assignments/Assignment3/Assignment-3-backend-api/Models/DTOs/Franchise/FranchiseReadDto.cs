﻿namespace Assignment_3_backend_api.Models.DTOs.Franchise
{
    public class FranchiseReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        //navigation
        public List<int> Movies { get; set; }
    }
}
