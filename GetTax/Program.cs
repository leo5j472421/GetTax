using System;
using System.Collections.Generic;
using System.Linq;

namespace GetTax
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<TaxConfig> taxConfigs = GetTaxConfigs();
            var income = 100000000m;
            var taxConfig = taxConfigs.FirstOrDefault(x => x.IsIncomInRange(income));
            Console.Write($"Income : {income} , Tax {taxConfig.GetTax(income)}");
        }

        private static IEnumerable<TaxConfig> GetTaxConfigs()
        {
            yield return new TaxConfig(0, 540000, 0.05m);
            yield return new TaxConfig(540001, 1210000, 0.12m);
            yield return new TaxConfig(1210001, 2420000, 0.2m);
            yield return new TaxConfig(2420001, 4530000, 0.3m);
            yield return new TaxConfig(4530001, 10310000, 0.4m);
            yield return new TaxConfig(10310001, int.MaxValue, 0.05m);
        }
    }

    internal class TaxConfig
    {
        public TaxConfig(int min, int max, decimal taxRate)
        {
            Min = min;
            Max = max;
            TaxRate = taxRate;
        }

        public bool IsIncomInRange(decimal income)
        {
            return income > Min && income < Max;
        }
        public int Min { get; set; }
        public int Max { get; set; }
        public decimal TaxRate { get; set; }

        public decimal GetTax(decimal income)
        {
            return income * TaxRate;
        }
    }
}
