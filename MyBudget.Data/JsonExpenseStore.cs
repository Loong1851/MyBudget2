using MyBudget.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace MyBudget.Data
{
    public class JsonExpenseStore : IExpenseStore
    {
        private string _path;

        public JsonExpenseStore(string path)
        {
            _path = path;
        }

        public IReadOnlyList<Expense> Load()
        {
            if (!File.Exists(_path))
            {
                return new List<Expense>();
            }

            string json = File.ReadAllText(_path);

            if (string.IsNullOrEmpty(json))
            {
                return new List<Expense>();
            }

            return JsonSerializer.Deserialize<List<Expense>>(json)
                   ?? new List<Expense>();
        }

        public void Save(IEnumerable<Expense> expenses)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(expenses, options);

            File.WriteAllText(_path, json);
        }
    }
}
