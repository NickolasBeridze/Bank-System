using System;

namespace Bank_System.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        private string _password;
        public decimal Tax { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 8)
                {
                    throw new ArgumentException("Password is Really Short");
                }
                _password = value;
            }
        }

        public BankAccount[] BankAccounts { get; set; } = Array.Empty<BankAccount>();

        public User()
        {
        }

        public User(int id, string fullname, string email, string password, BankAccount[] bankAcc)
        {
            if (id <= 0)
                throw new ArgumentException("User ID is Invalid.");
            if (string.IsNullOrWhiteSpace(fullname))
                throw new ArgumentException("Full Name is Required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is Required.");

            Id = id;
            FullName = fullname;
            Email = email;
            Password = password; 
            BankAccounts = bankAcc ?? Array.Empty<BankAccount>();
        }

        public void ChangeFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName) || fullName.Length < 2)
            {
                throw new ArgumentException("New full name is not valid (must be at least 2 characters).");
            }
            FullName = fullName;
            Console.WriteLine("Full name changed successfully :)");
        }

        public void ChangePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                throw new ArgumentException("Password is Really Short");
            }
            _password = password;
            Console.WriteLine("Password changed successfully UwU");
        }

        public override string ToString()
        {
            return $"[{Id}] , [{FullName}] , [{Email}]";
        }
    }
}