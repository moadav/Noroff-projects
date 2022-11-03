# CustomerTrack Application
In CustomerTrack application, you can view different type of data with how a customer and track is used


# Description
CustomerTrack offers Create, read, update functionalities where you can use these methods corresponding to edit the database. It also contains built in methods to view such as the genre, big spenders and Countries of customers and more!

# Usage
```
    ICustomerRepository customerRepository = new CustomerRepositoryImplem(ConnectionStringAccess.ConnectionString());
    customerRepository.GetCustomerTopGenre(9);
```

# Requirements
To use this application, you will need to access the customer database found at assignment2 from noroff
Where you download the sql file and then connect it to the sql database in visual studio.
Then you copy the ConnectionString and head to "DataAccess/ConnectionStringAccess.cs" and edit the ConnectionString method to contain 
your connection string.

# License

Project created by Mohammed Ali Davami and Simon Sutterud



