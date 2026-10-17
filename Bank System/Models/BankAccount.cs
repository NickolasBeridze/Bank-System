using System;

namespace Bank_System.Models
{
    internal class BankAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string PinCode { get; set; }
        public Card Card { get; set; } = new Card();

        public BankAccount() { }

        public BankAccount(int id, decimal balance, Card card)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The account ID must be a positive integer.");
            }

            if (balance < 0)
            {
                throw new ArgumentException("The balance cannot be a negative value.");
            }

            Id = id;
            Balance = balance;
            Card = card ?? throw new ArgumentNullException(nameof(card), "The card cannot be null.");
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The deposit amount must be a positive value.");
            }
            Balance += amount;
        }

        public void Deposit(decimal amount, string who)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The deposit amount must be a positive value.");
            }
            Balance += amount;
            Console.WriteLine($"{amount} , {who} , ( Self )");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The withdrawal amount must be a positive value.");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException($"insufficient funds. Your balance is: {Balance}");
            }

            Balance -= amount;
        }

        public void Withdraw(decimal amount, string who)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("The withdrawal amount must be a positive value.");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException($"insufficient funds. Your balance is: {Balance}");
            }

            Balance -= amount;
            Console.WriteLine($"{amount} , {who} , ( ATM / Cashier )");
        }
    }
}