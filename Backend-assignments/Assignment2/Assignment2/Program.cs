using Assignment2.DataAccess;
using Assignment2.Repositories.CustomerRepository;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories.CustomerRepository;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ConnectionStringAccess access = new();
            //string conn = access.ConnectionString();

            ICustomerRepository customerRepository = new CustomerRepositoryImplem(ConnectionStringAccess.ConnectionString());
            customerRepository.GetCustomerTopGenre(9);
        }

    }
}