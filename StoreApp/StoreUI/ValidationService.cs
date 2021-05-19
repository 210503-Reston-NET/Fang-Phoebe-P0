using System;
using System.Text.RegularExpressions;

namespace StoreUI
{
    public class ValidationService : IValidationService
    {
        public string ValidateBarcode(string prompt)
        {
            return null;
        }

        public int ValidateQuantity(string prompt)
        {
            string response;
            int quantity;
            bool repeat;
            do{
                response = ValidateEmptyInput(prompt);
                quantity = Int32.Parse(response);
                repeat = quantity <= 0;
                if (repeat) Console.WriteLine("Quantiy can't be 0 or less");
            }
            while (repeat);
            return quantity;
        }

        public decimal ValidatePrice(string prompt)
        {
            string response;
            decimal price;
            bool repeat;
            do{
                response = ValidateEmptyInput(prompt);
                price = Decimal.Parse(response);
                repeat = decimal.Round(price, 2) != price;
                if (repeat) Console.WriteLine("Invalid price input.");
            } while (repeat);
            return price;
        }

        public string ValidateEmptyInput(string prompt)
        {
            string response;
            bool repeat;
            do{
                Console.WriteLine(prompt);
                response = Console.ReadLine();
                repeat = String.IsNullOrWhiteSpace(response);
                if (repeat) Console.WriteLine("This field can't be empty");
            }while (repeat);
            return response;
        }

        public string ValidateValidCommand(string prompt)
        {
            string response;
            bool repeat = true;
            do{
                response = ValidateEmptyInput(prompt);
                if(response.ToLower() == "y" || response.ToLower() == "n") repeat = false;
                if (repeat) Console.WriteLine("Enter valid input");
            } while(repeat);
            return response;
        }
        public DateTime ValidateDate(string prompt)
        {
            string response;
            bool repeat = true;
            DateTime date;
            do{
                response = ValidateEmptyInput(prompt);
                date = DateTime.Parse(response);
                if (DateTime.TryParse(response, out date)) repeat = false;
                if (repeat) Console.WriteLine("Please enter valid date");
            }while (repeat);
            return date;
        }
    }
}