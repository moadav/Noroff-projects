﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{

    /// <summary>A class containg the definition of Customer object</summary>
    internal readonly record struct Customer(int CustomerId, string? FirstName, string? LastName, string? Country, string? PostalCode, string? Phone, string? Email)
    {
    }
}
