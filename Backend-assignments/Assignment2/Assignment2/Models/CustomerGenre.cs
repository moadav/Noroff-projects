using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{

    /// <summary>A class containign the definition of CustomerGenre</summary>
    internal readonly record struct CustomerGenre(string Genre, int NumberOfTracks)
    {
    }
}
