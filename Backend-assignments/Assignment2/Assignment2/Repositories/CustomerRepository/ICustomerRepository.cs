using Assignment2.Models;
using ConsoleApp1.Models;
using ConsoleApp1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories.CustomerRepository
{
    internal interface ICustomerRepository : ICRUDrepository<Customer, int>
    {

        /// <summary>A function to get a list of customers by name</summary>
        /// <param name="name">The name parameter to find the customer name</param>
        /// <returns>returns a list of customers</returns>
        List<Customer> GetCustomersByName(string name);

        /// <summary>A function that gets the customer by Limit and offset and returns a list of Customers</summary>
        /// <param name="limit">The limit parameter which limits the customers returned</param>
        /// <param name="offset">The offset parameter that returns an offset of customers specified</param>
        /// <returns>
        ///   A list of Customer objects
        /// </returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with specified Name</exception>

        List<Customer> GetCustomersByLimitAndOffset(int offset, int limit);
        /// <summary>A function that gets the customers by country</summary>
        /// <returns>Returns a list of CustomerCountry objects</returns>
        List<CustomerCountry> GetCustomersByCountry();


        /// <summary>A function that finds the highest spenders and returns a list of customerSpender objects</summary>
        /// <returns>Returns a list of CustomerSpender objects</returns>

        List<CustomerSpender> GetHighestSpenders();

        /// <summary>A function that gets the customer top genre and returns a list of customerGenre objects</summary>
        /// <param name="id">The customer Id</param>
        /// <returns>Returns a list of custtomerGenre</returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with the specified ID</exception>

        List<CustomerGenre> GetCustomerTopGenre(int id);


    }
}
