using JISCalculator.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JISCalculator.Models
{
    public class Operation
    {
        public Decimal Value { get; set; }
        public OperationType Op { get; set; }

    }
}
