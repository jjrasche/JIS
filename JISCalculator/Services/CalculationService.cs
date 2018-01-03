using JISCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;


namespace JISCalculator.Services
{
    public class CalculationService : ICalculationService
    {
        private Regex paranthesisRegex = new Regex("[()]");
        private Regex firstOrderRegex = new Regex("\\([^()]+\\)");
        private Regex secondOrderRegex = new Regex("[0-9.]+[/\\*][/-]?[0-9.]+");
        private Regex thirdOrderRegex = new Regex("[0-9./-]+[+/-][/-]?[0-9.]+");
        private Regex operationsRegex = new Regex("[+/\\*/-]");

        public CalculationService()
        {
        }

        private string RemoveUnnecessaryParanthesis(string input)
        {
            if (HasOutsideParanthesis(input))
            {
                return input.Substring(1, input.Length - 2);
            }
            return input;
        }

        private bool HasOutsideParanthesis(string input)
        {
            return input.FirstOrDefault() == '(' && input.LastOrDefault() == ')';
        }

        public Decimal SolveExpression(string expression)
        {
            var temp = EvaluateExpression(expression);
            return Decimal.Parse(temp);
        }

        public string EvaluateExpression(string expression)
        {
            expression = RemoveUnnecessaryParanthesis(expression);
            var chunk = IsolateSingleOperation(expression);
            Debug.WriteLine(expression + "    :     " + chunk);
            string chunkResult;

            if (IsNumeric(expression) || String.IsNullOrEmpty(chunk))
            {
                return expression;
            }

            if (IsSingleOperation(chunk))
            {
                chunkResult = HandleCalculation(chunk);
                Debug.WriteLine($"calculation result: {chunkResult}");
            }
            else
            {
                chunkResult = EvaluateExpression(chunk);
            }
            var updatedExpression = Regex.Replace(expression, Regex.Escape(chunk), chunkResult);
            Debug.WriteLine($"updatedExpression: {updatedExpression}");

            return EvaluateExpression(updatedExpression);
        }



        public bool IsSingleOperation(string expression)
        {
            try
            {
                var equation = new SimpleEquation(expression);
                return (operationsRegex.IsMatch(equation.Operation));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsNumeric(string expression)
        {
            return decimal.TryParse(expression, out var ret);
        }

        public string HandleCalculation(string expression)
        {
            var equation = new SimpleEquation(expression);
            Decimal ret;

            switch(equation.Operation)
            {
                case "+":
                    ret = equation.FirstArgument + equation.SecondArgument;
                    break;
                case "-":
                    ret = equation.FirstArgument - equation.SecondArgument;
                    break;
                case "*":
                    ret = equation.FirstArgument * equation.SecondArgument;
                    break;
                case "/":
                    ret = equation.FirstArgument / equation.SecondArgument;
                    break;
                default:
                    throw new InvalidOperationException($"An invalid opeation {equation.Operation} was entered.");
            }
            return ret.ToString();
        }

        /*
         *  First order expressions are paranthesis e.g. (1+7)
         *  Second order are division and multiplication
         *  third order are addition and subtraction
         *  
         *  if no single expression exists of the type order, find the next
         *  highest ordered expression
         */
        public string IsolateSingleOperation(string expression)
        {
            var operation = firstOrderRegex.Match(expression);
            if (operation.Success)
            {
                return operation.Value;
            }
            operation = secondOrderRegex.Match(expression);
            if (operation.Success)
            {
                return operation.Value;
            }
            operation = thirdOrderRegex.Match(expression);
            if (operation.Success)
            {
                return operation.Value;
            }
            return null;
        }
    }
}
