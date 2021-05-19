using System;
namespace StoreUI
{
    public interface IValidationService
    {
        string ValidateEmptyInput(string prompt);
        decimal ValidatePrice(string prompt);
        int ValidateQuantity(string prompt);
        string ValidateBarcode(string prompt);
        DateTime ValidateDate(string prompt);
        public string ValidateValidCommand(string prompt);
    }
}