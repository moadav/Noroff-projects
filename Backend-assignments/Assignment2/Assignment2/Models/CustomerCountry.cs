using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{

    /// <summary>A class containing the definition of CustomerCountry object</summary>
    internal readonly record struct CustomerCountry(string Country, int Customers)
    {
    }
}
