using Microsoft.AspNetCore.Mvc;
using JISCalculator.Services;
using System;


namespace JISCalculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        private ICalculationService calculationService;
        public CalculatorController(ICalculationService calculationService)
        {
            this.calculationService = calculationService;
        }

        [HttpPost("[action]")]
        public JsonResult CalculateExpression([FromBody] ExpressionModel data)
        {
            return new JsonResult(calculationService.SolveExpression(data.Expression));
        }

        public class ExpressionModel
        {
            public string Expression { get; set; }
        }
    }
}
