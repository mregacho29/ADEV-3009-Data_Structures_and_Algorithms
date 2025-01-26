using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;

namespace A1_MaCrizzaRegacho
{
    public class Employee : IComparable<Employee>
    {

        // Properties for EmployeeId, FirstName, and LastName
        // These properties are read-only, meaning they can only be set through the constructors
        public int EmployeeID { get; }
        public string FirstName { get; }
        public string LastName { get; }

        /// <summary>
        /// Constructor that initializes only the EmployeeId
        /// </summary>
        /// <param name="employeeId">The ID of the Employee</param>
        public Employee(int employeeId)
        {
            // Set the EmployeeId property
            EmployeeID = employeeId;

        }

        /// <summary>
        /// Constructor that initializes the EmployeeId, FirstName, and LastName
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        public Employee(int employeeId, string firstName, string lastName)
        {
            // Set the EmployeeId property
            EmployeeID = employeeId;

            // Set the FirstName and LastName properties
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Compare two Employee objects based on their EmployeeId
        /// </summary>
        /// <param name="other">other</param>
        /// <returns></returns>
        public int CompareTo(Employee other)
        { 
            if (other == null) return 1;
            return EmployeeID.CompareTo(other.EmployeeID);
        }

        /// <summary>
        /// Override the ToString method to return the EmployeeId, FirstName, and LastName
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        { 
            string firstName = FirstName ?? "null";
            string lastName = LastName ?? "null";
            return $"{EmployeeID} {firstName} {lastName}";
        }
    }
}