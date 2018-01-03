using JISCalculator.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JISCalculator.Models
{
    public class SimpleEquation
    {
        public Decimal FirstArgument { get; set; }
        public Decimal SecondArgument { get; set; }
        public string Operation { get; set; }
        private Regex equationPartsRegex = new Regex("((\\d*\\.\\d+)|(\\d+)|([\\+\\-\\*/\\(\\)]))");


        public SimpleEquation(string expression)
        {
            var parts = equationPartsRegex.Matches(expression).ToList();
            var pos = 0;

            if (parts[pos].Value == "-")
            {
                pos++;
                FirstArgument = (-1) * Decimal.Parse(parts[pos++].Value);
            }
            else
            {
                FirstArgument = Decimal.Parse(parts[pos++].Value);
            }

            Operation = parts[pos++].Value;

            if (parts[pos].Value == "-")
            {
                pos++;
                SecondArgument = (-1) * Decimal.Parse(parts[pos++].Value);
            }
            else
            {
                SecondArgument = Decimal.Parse(parts[pos++].Value);
            }
        }
    }
}
