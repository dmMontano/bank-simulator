namespace ATMApp.Services
{
    public static class BankingServices
    {
        // Option 1: Pass-by-value
        public static double GetBalance(double balance)
        {
            return balance;
        }

        // Option 2: ref (Deposit)
        public static bool Deposit(ref double balance, double amount)
        {
            if (amount <= 0)
                return false;

            balance += amount;
            return true;
        }

        // Option 3: ref + out (Withdraw)
        public static void Withdraw(
            ref double balance,
            double amount,
            out bool isSuccessful)
        {
            if (amount <= 0)
            {
                isSuccessful = false;
                return;
            }

            if (amount > balance)
            {
                isSuccessful = false;
                return;
            }

            balance -= amount;
            isSuccessful = true;
        }
    }
}
