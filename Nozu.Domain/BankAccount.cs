using System;
namespace Nozu.Domain
{
    public class BankAccount
    {
        protected double _balance;
        protected bool _isLocked;
        protected int _pin;

        public BankAccount(int pin)
        {
            _pin = pin;
        }

        public double GetBalance()
        {
            return _balance;
        }

        public double Deposit(double input)
        {
            if (_isLocked) throw new InvalidOperationException("Account is locked");
            if (input <= 0) throw new InvalidOperationException("Enter a valid amount");
            if (input >= double.MaxValue) throw new InvalidOperationException("Large amount not allowed");
            _balance += input;
            return _balance;
        }

        public WithdrawDto Withdraw(double input)
        {
            if (_isLocked) throw new InvalidOperationException("Account is locked");
            if (input <= 0) throw new InvalidOperationException("Withdrawal amount should be grater than zero");
            if (_balance < input) throw new InvalidOperationException("Insufficient balance");
            _balance -= input;
            return new WithdrawDto
            {
                Amount = input,
                RemainingBalance = _balance
            };
        }

        public bool ChangePin(int oldPin, int newPin, int confirmNewPin)
        {
            if (_pin != oldPin) throw new ArgumentException("Old pin is incorrect");
            if (newPin != confirmNewPin) throw new ArgumentException("New pin does not match");
            _pin = newPin;
            return true;
        }

        public void LockAccount()
        {
            _isLocked = true;
        }

    }

    public class WithdrawDto
    {
        public double Amount { get; set; }
        public double RemainingBalance { get; set; }
    }
}

