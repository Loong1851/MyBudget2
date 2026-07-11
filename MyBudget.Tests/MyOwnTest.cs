using MyBudget.Core;
using MyBudget.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyBudget.Tests
{
    public class MyOwnTest
    {
        [Fact]
        public void Remaining_ShouldReturnCorrectAmount()
        {
            // Arrange
            var service = new BudgetService();
            service.SetMonthlyLimit(1000m);

            // Act
            decimal remaining = service.Remaining(300m);

            // Assert
            Assert.Equal(700m, remaining);
        }



        [Fact]
        public void InCategory_ShouldReturnOnlyMatchingCategory()
        {
            // Arrange
            var store = new InMemoryExpenseStore();
            var repository = new ExpenseRepository(store);

            repository.Add(ExpenseFactory.CreateOneTime(
                "Lunch",
                20m,
                ExpenseCategory.Food,
                DateOnly.FromDateTime(DateTime.Now)));

            repository.Add(ExpenseFactory.CreateOneTime(
                "Bus",
                5m,
                ExpenseCategory.Transport,
                DateOnly.FromDateTime(DateTime.Now)));

            // Act
            var foodExpenses = repository.InCategory(ExpenseCategory.Food);

            // Assert
            Assert.Single(foodExpenses);
        }

        [Fact]
        public void Evaluate_ShouldReturnOnTrack()
        {
            // Arrange
            var service = new BudgetService();
            service.SetMonthlyLimit(1000m);

            // Act
            var status = service.Evaluate(400m);

            // Assert
            Assert.Equal(BudgetStatus.OnTrack, status);
        }
    }
}