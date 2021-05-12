namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a customer
    /// </summary>
    public class Customer
    {
        public Customer(string firstname, string middlename, string lastname) 
        {
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LasttName = lastname;
        }
        public string LasttName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get 
            {
                if (MiddleName == "") return $"{FirstName} {LasttName}";
                return $"{FirstName} {MiddleName} {LasttName}";
            }
        }

        //ToDo: list of orders

        // print customer name and a list of orders
        public override string ToString()
        {
            return base.ToString();
        }

    }
}