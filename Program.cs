using ATMApp.View;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        BankingView.Run();
    }
}

public class BankingView
{
    public static void Run()
    {
        BankingService service = new BankingService();

        double balance = 1000.00;
        int choice = 0;

        string withdrawalStatus = "None";
        double lastAmount = 0;

        Console.WriteLine("Dannicah Montano");
        Console.WriteLine("===== Simple ATM System =====");
        Console.WriteLine($"Initial Balance: ₱{balance}");

        while (choice != 5)
        {
            Console.WriteLine("\n1: Check Balance");
            Console.WriteLine("2: Deposit Money");
            Console.WriteLine("3: Withdraw Money");
            Console.WriteLine("4: Print Mini Statement");
            Console.WriteLine("5: Exit");

            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Pass-by-value (read only)
                    double currentBalance = service.CheckBalance(balance);
                    Console.WriteLine($"Current Balance: ₱{currentBalance}");
                    break;

                case 2:
                    Console.Write("Enter amount to deposit: ");
                    double depositAmount = double.Parse(Console.ReadLine());

                    if (depositAmount > 0)
                    {
                        // ref modifies balance
                        service.Deposit(ref balance, depositAmount);
                        withdrawalStatus = "Deposit";
                        lastAmount = depositAmount;

                        Console.WriteLine("Deposit successful.");
                        Console.WriteLine($"Updated Balance: ₱{balance}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount.");
                    }
                    break;

                case 3:
                    Console.Write("Enter amount to withdraw: ");
                    double withdrawAmount = double.Parse(Console.ReadLine());

                    if (withdrawAmount > 0)
                    {
                        bool success;

                        // ref updates balance, out tells success/failure
                        service.Withdraw(ref balance, withdrawAmount, out success);

                        if (success)
                        {
                            withdrawalStatus = "Withdraw";
                            lastAmount = withdrawAmount;

                            Console.WriteLine("Withdrawal successful.");
                            Console.WriteLine($"Updated Balance: ₱{balance}");
                        }
                        else
                        {
                            Console.WriteLine("Withdrawal failed. Insufficient balance.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid withdrawal amount.");
                    }
                    break;

                case 4:
                    // Pass-by-value (display only)
                    service.MiniStatement(balance, withdrawalStatus, lastAmount);
                    break;

                case 5:
                    Console.WriteLine("Thank you for using the ATM. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        }
    }
}

public class BankingService
{
    public double CheckBalance(double balance)
    {
        return balance;
    }

    public void Deposit(ref double balance, double amount)
    {
        balance += amount;
    }

    public void Withdraw(ref double balance, double amount, out bool success)
    {
        if (balance >= amount)
        {
            balance -= amount;
            success = true;
        }
        else
        {
            success = false;
        }
    }

    public void MiniStatement(double balance, string withdrawalStatus, double lastAmount)
    {
        Console.WriteLine("\n--- Mini Statement ---");
        Console.WriteLine($"Current Balance: ₱{balance}");

        if (withdrawalStatus == "Deposit")
            Console.WriteLine($"Last Transaction: + ₱{lastAmount}");
        else if (withdrawalStatus == "Withdraw")
            Console.WriteLine($"Last Transaction: - ₱{lastAmount}");
        else
            Console.WriteLine("No transactions yet.");
    }
}
