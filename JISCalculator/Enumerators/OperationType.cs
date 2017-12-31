using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JISCalculator.Enumerators
{
    public enum OperationType
    {
        None = 0,           // for the first operation in a series. A passthrough to set the current calculation value to Value
        Addition = 1,
        Subtraction = 2,
        Multiplication = 3,
        Division = 4
    }
}
