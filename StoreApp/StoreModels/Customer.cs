namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a customer
    /// </summary>
    public class Customer
    {
        public Customer()
        {}
        public Customer(string firstname, string middlename, string lastname) 
        {
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LasttName = lastname;
        }
        
        public Customer(string firstname, string middlename, string lastname, string phoneNumber) : this (firstname, middlename, lastname)
        {
            this.PhoneNumber = phoneNumber;
        }

        public Customer(int id, string firstname, string middlename, string lastname, string phoneNumber) : this(firstname, middlename, lastname, phoneNumber)
        {
            this.Id = id;
        }
        public string LasttName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get; set; }
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
            return FullName;
        }

    }
}