using System;
using System.Collections.Generic;
using StoreBL;
using StoreDL;
using StoreModels;
namespace StoreUI
{
    public class CustomerMenu : IMenu
    {
        private ICustomerBL _customerBL;

        public CustomerMenu(ICustomerBL customerBL)
        {
            this._customerBL = customerBL;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Hello. Enter Begin to sign up");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] Begin");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        repeat = false;
                        break;
                    case "2":
                        AddCustomer();
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (repeat);
        }

        private void SearchCustomer()
        {
            throw new NotImplementedException();
        }

        private void AddCustomer()
        {
            //ToDo
            // logic validation - check if cutomer already exists on BL
            // gatekeeper validation - check valid string
            try
            {
                Console.WriteLine("\t Enter first name:");
                string firstname = Console.ReadLine();
                Console.WriteLine("\t Enter middle name:");
                string middlename = Console.ReadLine();
                Console.WriteLine("\t Enter last name:");
                string lastname = Console.ReadLine();
                Console.WriteLine("\t Enter last name:");
                string phoneNumber = Console.ReadLine();
                Customer userInput = new Customer(lastname, middlename, firstname, phoneNumber);
                Customer output = _customerBL.AddCustomer(userInput);
                Console.WriteLine($"Customer {output.ToString()} has added successfully");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ViewAllCustomer() 
        {
            List<Customer> customers = _customerBL.GetAllCustomers();
            Action<Object> print = o => Console.WriteLine(o.ToString());
            if (customers.Count == 0) Console.WriteLine("No customers :< You should add some");
            else
            {
                customers.ForEach(print);
            }
        }
    }
}