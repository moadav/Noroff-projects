using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    /// <summary>A class containign the definition of CustomerSpender</summary>

    internal readonly record struct CustomerSpender(int Id, string FirstName, string LastName, decimal InvoiceTotal)
    {
    }
}
