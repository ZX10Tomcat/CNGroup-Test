using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CNGroup;


namespace CNGroupTests
{
    [TestClass]
    public class CalculationTest
    {
        [TestMethod]
        public void TestWorking()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\nmultiply 6\r\napply 3\r\n";
            
            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();
            

            // \r\n Because it's line  
            Assert.AreEqual("24\r\n", result);
        }

        [TestMethod]
        public void TestParseContent()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\nmultiply 6\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();

            Assert.AreEqual(24, Calculations.CalcCases[0].result);
        }
        
        [TestMethod]
        public void TestСalculate()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\nmultiply -6\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);

            Assert.AreEqual(1, Calculations.CalcCases.Count);
            Assert.AreEqual(2, Calculations.CalcCases[0].steps.Count);
            Assert.AreEqual("add", Calculations.CalcCases[0].steps[0].Key);
        }

        [TestMethod]
        public void TestBelowZero()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\nmultiply -6\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();

            Assert.AreEqual("-24\r\n", result);
        }

        [TestMethod]       
        public void TestNotANumber()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add W\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();

            Assert.AreEqual(true, Calculations.Errors);
            Assert.AreEqual("W not a number", Calculations.ErrorMessage);
        }

        [TestMethod]        
        public void TestCommandNotPresented()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "take 1\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();

            Assert.AreEqual(true, Calculations.Errors);
            Assert.AreEqual("Operator not found", Calculations.ErrorMessage);
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\ndivide 0\r\napply 3\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();

            Assert.AreEqual(true, Calculations.Errors);
            Assert.AreEqual("Please, check examples on divide by zero", Calculations.ErrorMessage);
        }

        [TestMethod]
        public void TestApplyNotPresented()
        {
            Calculations Calculations = new Calculations();
            string applyWord = "apply";
            string fileContent = "add 1\r\n";

            Calculations.Init();
            Calculations.ParseContent(fileContent, applyWord);
            Calculations.Calculate();
            string result = Calculations.PrintResults();

            Assert.AreEqual("apply word not presented or file is empty", result);
        }


    }
}
