using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_System.Models
{
    internal class Employe : User
    {
        public decimal Salary { get; set; }
        public Bank Bank { get; set; } = new Bank(0, "", 0m);

        public Employe() { }

        public Employe(int id, string fullname, string email, string password, BankAccount[] bankAcc, decimal salary, Bank bank)
            : base(id, fullname, email, password, bankAcc)
        {
            if (salary < 0)
            {
                throw new ArgumentException("The salary cannot be a negative value.");
            }

            Salary = salary;
            Bank = bank ?? throw new ArgumentNullException(nameof(bank), "The bank object cannot be null.");
        }

        public void GiveLoan(BankAccount targetAccount, decimal amount, string recipientName)
        {
            if (targetAccount == null)
            {
                throw new ArgumentNullException(nameof(targetAccount), "The target account cannot be null.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("The loan amount must be a positive value.");
            }

            targetAccount.Deposit(amount, recipientName);
            Console.WriteLine($"{amount} , {FullName} (Employee) , ( {recipientName} )");
        }

        public void TransferBalance(BankAccount from, BankAccount to, decimal amount, string senderName, string recipientName)
        {
            if (from == null || to == null)
            {
                throw new ArgumentNullException("The source or destination account cannot be null.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("The transfer amount must be a positive value.");
            }

            from.Withdraw(amount, senderName);
            to.Deposit(amount, recipientName);
            Console.WriteLine($"\n Success: {amount} Money Transferred.");
        }
    }
}