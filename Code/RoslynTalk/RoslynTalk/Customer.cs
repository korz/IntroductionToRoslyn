using System;

namespace RoslynTalk
{
    /// <summary>
    /// Class representing a Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The unique Id of the Customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the Customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the Customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The birthdate of the Customer
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// The rough age of the Customer
        /// </summary>
        public int Age { get { return (int)(DateTime.Today.Subtract(Birthdate).TotalDays / 365.25); } }
    }
}
