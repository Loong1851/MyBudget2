using MyBudget.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IExpenseStore _store;
        private readonly List<Expense> _expenses;

        public ExpenseRepository(IExpenseStore store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            _expenses = _store.Load().ToList();
        }

        // all expenses ordered by date
        public IReadOnlyList<Expense> GetAll()
        {
            return _expenses.OrderBy(e => e.Date).ToList();
        }

        // append (reject null).
        public void Add(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _expenses.Add(expense);
        }

        //  sum of every expense's MonthlyImpact
        public decimal Total()
        {
            return _expenses.Sum(e => e.MonthlyImpact);
        }

        // group by category and sum into a dictionary 
        public IReadOnlyDictionary<ExpenseCategory, decimal> TotalsByCategory()
        {
            return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(e => e.MonthlyImpact));
        }

        // InCategory(ExpenseCategory category) — filter to one category, ordered by date.

        public IReadOnlyList<Expense> InCategory(ExpenseCategory category)
        {
            return _expenses
                .Where(e => e.Category == category)
                .OrderBy(e => e.Date).ToList();
        }

        // save
        public void Save()
        {
            _store.Save(_expenses);
        }
    }
}
