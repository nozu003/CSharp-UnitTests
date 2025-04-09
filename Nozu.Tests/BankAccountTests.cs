using System;
using Nozu.Domain;

namespace Nozu.Tests
{
    public class BankAccountTests
    {
        private readonly BankAccount _account;

        public BankAccountTests()
        {
            _account = new BankAccount(123456);
        }

        [Theory]
        [InlineData(100d)]
        [InlineData(500d)]
        [InlineData(1000d)]
        public void Deposit_ShouldReturnNewBalance_WhenAmountIsGreaterThanZero(double amount)
        {
            var initialBalance = _account.GetBalance();

            var result = _account.Deposit(amount);

            Assert.Equal((initialBalance + amount), result);
        }

        [Theory]
        [InlineData(0d)]
        [InlineData(-100d)]
        public void Deposit_ShouldThrowException_WhenAmountIsLesserThanOrEqualToZero(double amount)
        {
            Assert.Throws<InvalidOperationException>(() => _account.Deposit(amount));
        }

        [Fact]
        public void Deposit_ShouldThrowException_WhenAmountIsMaximumValue()
        {
            var amount = double.MaxValue;

            Assert.Throws<InvalidOperationException>(() => _account.Deposit(amount));
        }

        [Fact]
        public void Deposit_ShouldThrowException_WhenAccountIsLocked()
        {
            _account.LockAccount();

            Assert.Throws<InvalidOperationException>(() => _account.Deposit(100d));
        }

        [Theory]
        [InlineData(2000d, 1000d, 1000d)]
        [InlineData(5000d, 3500d, 1500d)]
        public void Withdraw_ShouldReturnAmountAndRemainingBalance_WhenBalanceIsSufficient(double initialBalance, double amount, double remainingBalance)
        {
            _account.Deposit(initialBalance);
            var result = _account.Withdraw(amount);

            Assert.Equal(amount, result.Amount);
            Assert.Equal(remainingBalance, result.RemainingBalance);
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenWithdrawalAmountIsZero()
        {
            _account.Deposit(100d);

            Assert.Throws<InvalidOperationException>(() => _account.Withdraw(0));
        }

        [Theory]
        [InlineData(500d, 1000d)]
        [InlineData(1000d, 2500d)]
        public void Withdraw_ShouldThrowException_WhenBalanceIsInsufficient(double initialBalance, double amount)
        {
            _account.Deposit(initialBalance);

            Assert.Throws<InvalidOperationException>(() => _account.Withdraw(amount));
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenAccountIsLocked()
        {
            _account.Deposit(100d);
            _account.LockAccount();

            Assert.Throws<InvalidOperationException>(() => _account.Withdraw(100d));
        }

        [Fact]
        public void ChangePin_ShouldThrowException_WhenOldPinIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => _account.ChangePin(456789, 456789, 456789));
        }

        [Fact]
        public void ChangePin_ShouldThrowException_WhenNewPinAndConfirmPinDoesNotMatch()
        {
            Assert.Throws<ArgumentException>(() => _account.ChangePin(456789, 456789, 567890));
        }

        [Fact]
        public void ChangePin_ShouldReturnTrue_WhenOldPinAndNewPinAreValid()
        {
            var result = _account.ChangePin(123456, 456789, 456789);

            Assert.True(result);
        }
    }
}

