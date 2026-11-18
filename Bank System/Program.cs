using Bank_System.Models;

namespace Bank_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Version 1.0
            Console.WriteLine(" Bank System Testing Started ===");
            // Version 1.0
            Bank centralBank = null;
            Employe employee = null;
            List<User> users = new List<User>();
            List<BankAccount> accounts = new List<BankAccount>();

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1 - Create and Setup Bank & Users");
                Console.WriteLine("2 - Deposit Money");
                Console.WriteLine("3 - Withdraw Money");
                Console.WriteLine("4 - Give Loan (Select User)");
                Console.WriteLine("5 - Show Details");
                Console.WriteLine("6 - Exit");
                Console.Write("Enter your choice (1-6): ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            centralBank = new Bank(1, "Georgian Bank", 50000m);
                            Console.WriteLine($"\nBank Created: {centralBank.BankName}, Budget: {centralBank.Budget}");

                            var card1 = new Card(1);
                            var acc1 = new BankAccount(101, 1000m, card1);
                            accounts.Add(acc1);

                            var card2 = new Card(2);
                            var acc2 = new BankAccount(102, 2500m, card2);
                            accounts.Add(acc2);

                            var user1 = new User(1, "George Beridze", "giorgi@gmail.com", "SecurePassword123", new BankAccount[] { acc1 });
                            var user2 = new User(2, "Nino Kapanadze", "nino@gmail.com", "Password456", new BankAccount[] { acc2 });

                            users.Add(user1);
                            users.Add(user2);

                            Console.WriteLine($"User Created: {user1}");
                            Console.WriteLine($"User Created: {user2}");

                            employee = new Employe(1, "Nino Maisuradze", "nino@bank.ge", "EmployeePass123", new BankAccount[] { }, 2500m, centralBank);
                            Console.WriteLine($"Employee Created: {employee.FullName}, Salary: {employee.Salary}");
                            break;

                        case "2":
                            if (accounts.Count == 0)
                            {
                                throw new InvalidOperationException("Please initialize the system first using option 1.");
                            }
                            Console.WriteLine("\n--- Available Accounts ---");
                            foreach (var acc in accounts)
                            {
                                Console.WriteLine($"Account ID: {acc.Id} | Balance: {acc.Balance}");
                            }
                            Console.Write("Enter Account ID to deposit into: ");
                            if (int.TryParse(Console.ReadLine(), out int depAccId))
                            {
                                var targetAcc = accounts.FirstOrDefault(a => a.Id == depAccId);
                                if (targetAcc == null)
                                {
                                    throw new ArgumentException("Account not found.");
                                }

                                var owner = users.FirstOrDefault(u => u.BankAccounts.Contains(targetAcc));
                                string ownerName = owner != null ? owner.FullName : "Unknown";

                                Console.Write("Enter amount to deposit: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                                {
                                    targetAcc.Deposit(depositAmount, ownerName);
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid deposit amount format.");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Invalid Account ID format.");
                            }
                            break;

                        case "3":
                            if (accounts.Count == 0)
                            {
                                throw new InvalidOperationException("Please initialize the system first using option 1.");
                            }
                            Console.WriteLine("\n--- Available Accounts ---");
                            foreach (var acc in accounts)
                            {
                                Console.WriteLine($"Account ID: {acc.Id} | Balance: {acc.Balance}");
                            }
                            Console.Write("Enter Account ID to withdraw from: ");
                            if (int.TryParse(Console.ReadLine(), out int drawAccId))
                            {
                                var targetAcc = accounts.FirstOrDefault(a => a.Id == drawAccId);
                                if (targetAcc == null)
                                {
                                    throw new ArgumentException("Account not found.");
                                }

                                var owner = users.FirstOrDefault(u => u.BankAccounts.Contains(targetAcc));
                                string ownerName = owner != null ? owner.FullName : "Unknown";

                                Console.Write("Enter amount to withdraw: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                                {
                                    targetAcc.Withdraw(withdrawAmount, ownerName);
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid withdrawal amount format.");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Invalid Account ID format.");
                            }
                            break;

                        case "4":
                            if (employee == null || users.Count == 0)
                            {
                                throw new InvalidOperationException("Please initialize the system first using option 1.");
                            }

                            Console.WriteLine("\n--- Available Users ---");
                            foreach (var u in users)
                            {
                                string accInfo = u.BankAccounts.Length > 0 ? $"Account ID: {u.BankAccounts[0].Id} (Balance: {u.BankAccounts[0].Balance})" : "No Accounts";
                                Console.WriteLine($"User ID: {u.Id} | Name: {u.FullName} | {accInfo}");
                            }

                            Console.Write("Enter User ID to give loan to: ");
                            if (int.TryParse(Console.ReadLine(), out int userId))
                            {
                                var selectedUser = users.FirstOrDefault(u => u.Id == userId);
                                if (selectedUser == null)
                                {
                                    throw new ArgumentException("User not found.");
                                }

                                if (selectedUser.BankAccounts == null || selectedUser.BankAccounts.Length == 0)
                                {
                                    throw new InvalidOperationException("The selected user does not have any bank accounts.");
                                }

                                Console.Write("Enter loan amount: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal loanAmount))
                                {
                                    employee.GiveLoan(selectedUser.BankAccounts[0], loanAmount, selectedUser.FullName);
                                }
                                else
                                {
                                    throw new ArgumentException("Invalid loan amount format.");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Invalid User ID format.");
                            }
                            break;

                        case "5":
                            if (centralBank == null)
                            {
                                throw new InvalidOperationException("Please initialize the system first using option 1.");
                            }
                            Console.WriteLine($"\n--- Current Status ---");
                            Console.WriteLine($"Bank: {centralBank.BankName} | Budget: {centralBank.Budget}");
                            Console.WriteLine($"Employee: {employee.FullName} | Salary: {employee.Salary}");
                            Console.WriteLine("\n--- Users & Accounts ---");
                            foreach (var u in users)
                            {
                                Console.WriteLine($"User: {u}");
                                foreach (var acc in u.BankAccounts)
                                {
                                    Console.WriteLine($"  -> Account ID: {acc.Id} | Balance: {acc.Balance}");
                                }
                            }
                            break;

                        case "6":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select an option between 1 and 6.");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\n[Error (ArgumentException)]: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"\n[Error (InvalidOperationException)]: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Error (Exception)]: {ex.Message}");
                }
            }

            Console.WriteLine("\n=== Testing Completed ===");
        }
    }
}