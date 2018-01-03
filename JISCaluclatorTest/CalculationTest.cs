using JISCalculator.Enumerators;
using JISCalculator.Models;
using JISCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JISCaluclatorTest
{
    public class OperationsComparer : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }

    }
    [TestClass]
    public class CalculationTest
    {

        public ValidationService parsingService;
        public CalculationService calculationService;
        public OperationsComparer operationsComparer;

        [TestInitialize]
        public void testInit()
        {
            parsingService = new ValidationService();
            calculationService = new CalculationService();
            operationsComparer = new OperationsComparer();
        }

        [TestMethod]
        public void ParseCalculation_SimpleAddition_CorrectValueReturned()
        {
            var isolatedExpression = calculationService.IsolateSingleOperation("5+7");
            Assert.AreEqual(isolatedExpression, "5+7");

            var actualResults = calculationService.SolveExpression("5+7");
            var expectedResults = (Decimal)12;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_SimpleSubtraction_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("5-7");
            var expectedResults = (Decimal)(-2);
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_SimpleMultiplication_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("5*7");
            var expectedResults = (Decimal)35;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_SimpleDivision_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("5/7");
            var expectedResults = (Decimal)5 / 7;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_BasicParenthesis_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("(1+7)/5");
            var expectedResults = (Decimal)8 / 5;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_ManyParenthesis_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("((((4-3))))");
            var expectedResults = (Decimal)1;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_NoOperation_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("1");
            var expectedResults = (Decimal)1;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_OOOWithinParanthesisNonNaturalOrder_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("1+(3-5/4)");
            var expectedResults = (Decimal)2.75;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_OOOWithinParanthesisNaturalOrder_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("1+(5/4-3)");
            var expectedResults = (Decimal)(-.75);
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_ComplexOrderOfOperations_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("1+((1+7)/(1+3)-2)/5+7");
            var expectedResults = (Decimal)8;
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void ParseCalculation_negativeSubtraction_CorrectValueReturned()
        {
            var actualResults = calculationService.SolveExpression("-5--3");
            var expectedResults = (Decimal)(-2);
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void IsolateSingleOperation_BasicParathesis_CorrectValueReturned()
        {
            var actualResults = calculationService.IsolateSingleOperation("(1+7)/3");
            var expectedResults = "(1+7)";
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void IsolateSingleOperation_BasicAddition_CorrectValueReturned()
        {
            var actualResults = calculationService.IsolateSingleOperation(".25+1.78-17");
            var expectedResults = ".25+1.78";
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void IsolateSingleOperation_BasicSubtraction_CorrectValueReturned()
        {
            var actualResults = calculationService.IsolateSingleOperation(".25-1.78-17-5");
            var expectedResults = ".25-1.78";
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void IsolateSingleOperation_BasicMultiplication_CorrectValueReturned()
        {
            var actualResults = calculationService.IsolateSingleOperation(".25*1.78-17");
            var expectedResults = ".25*1.78";
            Assert.AreEqual(actualResults, expectedResults);
        }

        [TestMethod]
        public void IsolateSingleOperation_BasicDivision_CorrectValueReturned()
        {
            var actualResults = calculationService.IsolateSingleOperation(".25/1.78/17-3248");
            var expectedResults = ".25/1.78";
            Assert.AreEqual(actualResults, expectedResults);
        }
    }
}
