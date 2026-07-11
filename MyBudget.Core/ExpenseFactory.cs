using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public static class ExpenseFactory
    {
        // 
        public static decimal ValidateAmount(decimal amount)
        {
            if (amount <= 0 || amount > 1_000_000)
            {
                throw new InvalidExpenseException("Amount must be greater than 0 and less than or equal to 1,000,000.");
            }
            return Math.Round(amount, 2);
        }
        
        public static OneTimeExpense CreateOneTime(
            string description,
            decimal amount,
            ExpenseCategory category,
            DateOnly date)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new InvalidExpenseException("Description cannot be blank.");
            }

            amount = ValidateAmount(amount);

            return new OneTimeExpense(
                Guid.NewGuid(),
                description.Trim(),
                amount,
                category,
                date);
        }

        public static RecurringExpense CreateRecurring(
            string description,
            decimal amount,
            ExpenseCategory category,
            DateOnly date,
            int timesPerMonth)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidExpenseException("Description cannot be blank.");
            }

            if (timesPerMonth < 1)
            {
                throw new InvalidExpenseException("Times per month must be at least 1.");
            }

            amount = ValidateAmount(amount);

            return new RecurringExpense(
                Guid.NewGuid(),
                description,
                amount,
                category,
                date,
                timesPerMonth);
        }
    }
}
