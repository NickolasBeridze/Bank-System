using System;

namespace Bank_System.Models
{
    internal class Bank
    {
        public int Id { get; }
        public string BankName { get; }
        public decimal Budget { get; private set; }

        public Bank()
        {

        }
        public Bank(int id, string bankName, decimal budget)
        {
            Id = id;
            BankName = bankName ?? throw new ArgumentNullException(nameof(bankName));
            Budget = budget;
        }

        public bool TryWithdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));
            if (Budget < amount) return false;
            Budget -= amount;
            return true;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));
            Budget += amount;
        }

        public override string ToString() => $"{BankName} (Id: {Id}) - Budget: {Budget}";
    }
}
