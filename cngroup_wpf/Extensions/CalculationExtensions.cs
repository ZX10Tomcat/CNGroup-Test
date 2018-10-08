using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Instruction = System.Collections.Generic.KeyValuePair<string, double>;

namespace CNGroup
{
    public static class CalculationsExtensions
    {        
        
        public static void Init(this Calculations Calculations)
        {
            Calculations.CalcCases.Clear();
            Calculations.Errors = false;
            Calculations.ErrorMessage = String.Empty;
        }

        /// <summary>
        /// Go through file content, create list of cases and changes keywords on operators
        /// </summary>        
        public static void ParseContent(this Calculations Calculations, string fileContent, string applyWord)
        {
            if (fileContent == String.Empty || applyWord == String.Empty)
            {
                Calculations.Errors = true;
                Calculations.ErrorMessage = "File is empty or applyWord not set";
                return;
            }

            CalcCase Case = new CalcCase();
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = fileContent.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                string[] words = line.Trim().Split(' ');

                Double digit;
                bool number = Double.TryParse(words[1].Trim(), out digit);

                if (!number)
                {
                    Calculations.Errors = true;
                    Calculations.ErrorMessage = words[1] + " not a number";
                    return;                    
                }

                if (words[0] == applyWord)
                {
                    Case.result = Convert.ToInt32(words[1].Trim());
                    Calculations.CalcCases.Add(Case);
                    Case = new CalcCase();
                }
                else
                {
                    Case.steps.Add(new Instruction(words[0].Trim(), Convert.ToDouble(words[1].Trim())));
                }

            }
        }

        /// <summary>
        /// Go through Calculation cases, calculate the result for each case
        /// </summary>
        public static void Calculate(this Calculations Calculations)
        {
            foreach (var calcCase in Calculations.CalcCases)
            {
                foreach (var step in calcCase.steps)
                {
                    calcCase.result = CalculateStep(step, calcCase.result);
                }
            }
        }

        /// <summary>
        /// Calculates step of CalcCase.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        static private Double CalculateStep(Instruction step, Double result)
        {
           
            string op = ConvertToOperator(step.Key);

            DataTable dt = new DataTable();
            var res = dt.Compute(result + op + step.Value, "");

            if (Convert.ToDouble(res) == Double.PositiveInfinity)
            {
                Calculations.Errors = true;
                Calculations.ErrorMessage = "Please, check examples on divide by zero";
                //throw new DivideByZeroException("Please, check examples on divide by zero");
            }

            return Convert.ToDouble(res);
            
            /*
            switch (op)
            {
                case "*": return result * step.Value;
                case "/": return result / step.Value;
                case "+": return result + step.Value;
                case "-": return result - step.Value;
            }
            */

        }

        /// <summary>
        /// Converts keyword to operator.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        private static string ConvertToOperator(string keyword)
        {   
            var OperatorsDictionary = Operators.operators;
            if (OperatorsDictionary.ContainsValue(keyword))
            {
                return OperatorsDictionary.FirstOrDefault(x => x.Value == keyword).Key;
            }

            Calculations.Errors = true;
            Calculations.ErrorMessage = "Operator not found";
            return String.Empty;
            //throw new Exception("Operator not found");
        }

        /// <summary>
        /// Prints the results.
        /// </summary>
        public static string PrintResults(this Calculations Calculations)
        {   
            if (Calculations.CalcCases.Count() == 0)
            {
                return "apply word not presented or file is empty";
            }

            StringBuilder builder = new StringBuilder();
            foreach (var calcCase in Calculations.CalcCases)
            {
                builder.AppendLine(calcCase.result.ToString("#.#####"));
            }

            if (Calculations.Errors)
            {
                builder.AppendLine("Error: " + Calculations.ErrorMessage);
            }
            return builder.ToString();
        }
    }
}
