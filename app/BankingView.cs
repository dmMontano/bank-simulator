using ATMApp.Services;

namespace ATMApp.View
{
    public class BankingView
    {
        public static void Run()
        {
            double balance = 1000.00;
    
            Console.WriteLine("Dannicah Montano");
            Console.WriteLine("=== Simple ATM System ===");
            Console.WriteLine($"\nInitial Balance: Php {balance:F2}");
    
            while (true)
            {
                Console.WriteLine("\n1: Check Balance");
                Console.WriteLine("2: Deposit Money");
                Console.WriteLine("3: Withdraw Money");
                Console.WriteLine("4: Print Mini Statement");
                Console.WriteLine("5: Exit");
                Console.Write("Select an option: ");
    
                string input = Console.ReadLine();
    
                switch (input)
                {
                    case "1":
                        double currentBalance = BankingServices.GetBalance(in balance);
                        Console.WriteLine($"Current Balance: Php {currentBalance:F2}");
                        break;
    
                    case "2":
                        Console.Write("Enter amount to deposit: ");
                        if (!double.TryParse(Console.ReadLine(), out double depositAmount))
                        {
                            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                            continue;
                        }
    
                        bool depositSuccess = BankingServices.Deposit(ref balance, depositAmount);
    
                        if (!depositSuccess)
                        {
                            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                            continue;
                        }
    
                        Console.WriteLine("Deposit successful.");
                        Console.WriteLine($"Updated Balance: Php {balance:F2}");
                        break;
    
                    case "3":
                        Console.Write("Enter amount to withdraw: ");
                        if (!double.TryParse(Console.ReadLine(), out double withdrawAmount))
                        {
                            Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                            continue;
                        }
    
                        BankingServices.Withdraw(ref balance, withdrawAmount, out bool withdrawSuccess);
    
                        if (!withdrawSuccess)
                        {
                            if (withdrawAmount <= 0)
                                Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                            else
                                Console.WriteLine("Withdrawal failed. Insufficient balance.");
    
                            continue;
                        }
    
                        Console.WriteLine("Withdrawal successful.");
                        Console.WriteLine($"Updated Balance: Php {balance:F2}");
                        break;
    
                    case "4":
                        var statement = BankingServices.MiniStatement(in balance);
                        Console.WriteLine("\n--- Mini Statement ---");
                        Console.WriteLine($"Current Balance: Php {statement.currentBalance:F2}");
                        Console.WriteLine($"Last Transaction Amount: Php {statement.lastTransaction:F2}");
                        break;
    
                    case "5":
                        Console.WriteLine("\nThank you for using the ATM. Goodbye!");
                        break;
    
                    default:
                        Console.WriteLine("\nInvalid option selected. Please try again.");
                        continue;
                }
    
                if (input == "5")
                    break;
            }
        }
    }
}
