using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface ICustomerBL
    {
        List<Customer> GetAllCustomers(); 
        
        Customer GetCustomer(string phoneNumber);
        Customer GetCustomer(Customer customer);
        Customer AddCustomer(Customer input);
        
    }
}