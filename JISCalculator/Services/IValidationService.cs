using JISCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JISCalculator.Services
{
    public interface IValidationService
    {
        void ValidateCacluationString(String calculationString);
    }
}
