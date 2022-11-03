using Assignment2.Exception;
using Assignment2.Models;
using ConsoleApp1.Models;
using ConsoleApp1.Repositories.CustomerRepository;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Assignment2.Repositories.CustomerRepository
{
    internal class CustomerRepositoryImplem : ICustomerRepository
    {

        /// <summary>The connection string</summary>
        private readonly string _connectionString;

        public CustomerRepositoryImplem(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>Inserts a new row into the database based on the paramter.</summary>
        /// <param name="obj">Adds a customer object to the database</param>
        public void Add(Customer obj)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email) VALUES" +
                " (@FirstName, @LastName, @Country, @PostalCode, @Phone, @Email)";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@FirstName", obj.FirstName);
            command.Parameters.AddWithValue("@LastName", obj.LastName);
            command.Parameters.AddWithValue("@Country", obj.Country);
            command.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
            command.Parameters.AddWithValue("@Phone", obj.Phone);
            command.Parameters.AddWithValue("@Email", obj.Email);

            command.ExecuteNonQuery();

        }

        /// <summary>Deteles a record by its ID.</summary>
        /// <param name="id">The Id of the Customer</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>Retrieves all instances of customers from the database.</summary>
        /// <returns>A list of Customers</returns>
        public List<Customer> GetAll()
        {

            //int CustomerId, string FirstName, string LastName, string Country, string PostalCode, string Phone, string Email
            List<Customer> customers = new();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customer(
                    CustomerId: reader.GetInt32(0),
                    FirstName: reader.IsDBNull(1) ? null : reader.GetString(1),
                    LastName: reader.IsDBNull(2) ? null : reader.GetString(2),
                    Country: reader.IsDBNull(3) ? null : reader.GetString(3),
                    PostalCode: reader.IsDBNull(4) ? null : reader.GetString(4),
                    Phone: reader.IsDBNull(5) ? null : reader.GetString(5),
                    Email: reader.IsDBNull(6) ? null : reader.GetString(6)

                    ));
            }
            return customers;
        }

        /// <summary>Retrieves a particular instance from the database by its ID.</summary>
        /// <param name="id">The Id of the customer to get</param>
        /// <returns>Returns a single Customer object that it finds with the ID</returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with specified ID</exception>
        public Customer GetById(int id)
        {
            Customer customer;
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @Id";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                customer = new Customer(
                    CustomerId: reader.GetInt32(0),
                    FirstName: reader.IsDBNull(1) ? null : reader.GetString(1),
                    LastName: reader.IsDBNull(2) ? null : reader.GetString(2),
                    Country: reader.IsDBNull(3) ? null : reader.GetString(3),
                    PostalCode: reader.IsDBNull(4) ? null : reader.GetString(4),
                    Phone: reader.IsDBNull(5) ? null : reader.GetString(5),
                    Email: reader.IsDBNull(6) ? null : reader.GetString(6)

                    );
            }
            else
            {
                throw new CustomerNotFoundException("Could not find Customer with specified ID");


            }
            return customer;
        }

        /// <summary>A function that gets the customers by country</summary>
        /// <returns>Returns a list of CustomerCountry objects</returns>
        public List<CustomerCountry> GetCustomersByCountry()
        {
            List<CustomerCountry> customerByCountry = new();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT Country, COUNT(FirstName) as NumberOfCustomers FROM CUSTOMER GROUP BY Country ORDER BY NumberOfCustomers DESC";
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customerByCountry.Add(new CustomerCountry
                {
                    Country = reader.GetString(0),
                    Customers = reader.GetInt32(1)
                });
            }
            return customerByCountry;
        }

        /// <summary>A function that gets the customer by Limit and offset and returns a list of Customers</summary>
        /// <param name="limit">The limit parameter which limits the customers returned</param>
        /// <param name="offset">The offset parameter that returns an offset of customers specified</param>
        /// <returns>
        ///   A list of Customer objects
        /// </returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with specified Name</exception>
        public List<Customer> GetCustomersByLimitAndOffset(int limit, int offset)
        {
            List<Customer> customers = new();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerId OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Limit", limit);
            command.Parameters.AddWithValue("@Offset", offset);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                customers.Add(new Customer(
                   CustomerId: reader.GetInt32(0),
                   FirstName: reader.IsDBNull(1) ? null : reader.GetString(1),
                   LastName: reader.IsDBNull(2) ? null : reader.GetString(2),
                   Country: reader.IsDBNull(3) ? null : reader.GetString(3),
                   PostalCode: reader.IsDBNull(4) ? null : reader.GetString(4),
                   Phone: reader.IsDBNull(5) ? null : reader.GetString(5),
                   Email: reader.IsDBNull(6) ? null : reader.GetString(6)

                   ));
            }
            else
            {
                throw new CustomerNotFoundException("Could not find Customer with specified Name");


            }

            return customers;
        }

        /// <summary>A function that returns a lsit of customer objects depending on the name parameter</summary>
        /// <param name="name">The name of the Customer</param>
        /// <returns>Returns a list of Customer objects</returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with specified Name</exception>
        public List<Customer> GetCustomersByName(string name)
        {
            List<Customer> customers = new();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE @Name";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Name", name);
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                customers.Add(new Customer(
                   CustomerId: reader.GetInt32(0),
                   FirstName: reader.IsDBNull(1) ? null : reader.GetString(1),
                   LastName: reader.IsDBNull(2) ? null : reader.GetString(2),
                   Country: reader.IsDBNull(3) ? null : reader.GetString(3),
                   PostalCode: reader.IsDBNull(4) ? null : reader.GetString(4),
                   Phone: reader.IsDBNull(5) ? null : reader.GetString(5),
                   Email: reader.IsDBNull(6) ? null : reader.GetString(6)

                   ));
            }
            else
            {
                throw new CustomerNotFoundException("Could not find Customer with specified Name");


            }

            return customers;
        }

        /// <summary>A function that gets the customer top genre and returns a list of customerGenre objects</summary>
        /// <param name="id">The customer Id</param>
        /// <returns>Returns a list of custtomerGenre</returns>
        /// <exception cref="Assignment2.Exception.CustomerNotFoundException">Could not find Customer with the specified ID</exception>
        public List<CustomerGenre> GetCustomerTopGenre(int id)
        {
            List<CustomerGenre> customersByGenre = new();
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT Name, NumberOfTracks FROM (SELECT Genre.Name, COUNT(Track.TrackId) as NumberOfTracks," +
                " RANK() OVER(ORDER BY COUNT(Track.TrackId) DESC) Rank FROM Invoice INNER JOIN InvoiceLine ON Invoice.InvoiceId = InvoiceLine.InvoiceId " +
                "INNER JOIN Track ON InvoiceLine.TrackId = Track.TrackId RIGHT JOIN Genre ON Track.GenreId = Genre.GenreId WHERE Invoice.CustomerId = @Id " +
                "GROUP BY Genre.Name) t WHERE t.Rank = 1";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                customersByGenre.Add(new CustomerGenre
                {
                    Genre = reader.GetString(1),
                    NumberOfTracks = reader.GetInt32(1)
                });
            }
            else
            {
                throw new CustomerNotFoundException("Could not find Customer with the specified ID");
            }
            return customersByGenre;
        }

        /// <summary>A function that finds the highest spenders and returns a list of customerSpender objects</summary>
        /// <returns>Returns a list of CustomerSpender objects</returns>
        public List<CustomerSpender> GetHighestSpenders()
        {
            List<CustomerSpender> customerSpender = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "SELECT Invoice.CustomerId, Customer.FirstName, Customer.Lastname, SUM(Total) as InvoiceTotal FROM Invoice" +
                " INNER JOIN Customer ON Invoice.CustomerId = Customer.CustomerId GROUP BY Invoice.CustomerId, Customer.FirstName, Customer.LastName " +
                "ORDER BY InvoiceTotal DESC";


            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                customerSpender.Add(new CustomerSpender
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    InvoiceTotal = reader.GetInt32(3)
                });

    
            }

            return customerSpender;
        }

        /// <summary>Updates an existing row based on the provided parameters.</summary>
        /// <param name="obj">The Customer Object that will update the existing record</param>
        public void Update(Customer obj)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, " +
                "Country = @Country, PostalCode = @PostalCode, Phone = @Phone, Email = @Email WHERE CustomerId = @CustomerId";
            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            command.Parameters.AddWithValue("@FirstName", obj.FirstName);
            command.Parameters.AddWithValue("@LastName", obj.LastName);
            command.Parameters.AddWithValue("@Country", obj.Country);
            command.Parameters.AddWithValue("@PostalCode", obj.PostalCode);
            command.Parameters.AddWithValue("@Phone", obj.Phone);
            command.Parameters.AddWithValue("@Email", obj.Email);
            command.ExecuteNonQuery();

        }




    }
}
