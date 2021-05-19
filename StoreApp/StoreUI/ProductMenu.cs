using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ProductMenu : IMenu
    {
        private IProductBL _productBL;
        private  IValidationService _validate;
        
        public ProductMenu(IProductBL productBL, IValidationService validate)
        {
            this._productBL = productBL;
            this._validate = validate;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Product Menu");
                Console.WriteLine("[0] Go back to main");
                Console.WriteLine("[1] Add a new product");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0" :
                        repeat = false;
                        break;   
                    case "1" : 
                        AddProduct();
                        break;                
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                } 
            } while (repeat);
        }

        private void AddProduct()
        {
            string name = _validate.ValidateEmptyInput("Enter Product Name:");
            decimal price = _validate.ValidatePrice("Enter product Price");
            string barcode = _validate.ValidateEmptyInput("Enter Product Barcode:");
            try
            {
                Product newProdcut = new Product(name, price, barcode);
                Product createdProduct = _productBL.AddProduct(newProdcut);
                Console.WriteLine($"Product: {createdProduct.Name} is created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}