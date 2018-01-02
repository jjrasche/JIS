using JISCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JISCalculator.Services
{
    public interface ICalculationService
    {
        Decimal SolveExpression(string expression);
        string IsolateSingleOperation(string expression);
    }
}
