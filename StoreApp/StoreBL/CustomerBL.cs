using System.Collections.Generic;
using StoreModels;
using StoreDL;
namespace StoreBL
{
    public class CustomerBL : ICustomerBL
    {
        private IRepository _repo;
        public CustomerBL(IRepository repo)
        {
            this._repo = repo;
        }
        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }
        public Customer GetCustomer(string phoneNumber)
        {
            return _repo.GetCustomer(phoneNumber);
        }

        public Customer GetCustomer(Customer customer)
        {
            return _repo.GetCustomer(customer);
        }
        public Customer AddCustomer(Customer input)
        {
            //ToDo - check if the customer already in the system
            return _repo.AddCustomer(input);
        }
        
    }


}