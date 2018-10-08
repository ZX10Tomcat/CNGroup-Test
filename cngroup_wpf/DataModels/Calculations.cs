using System;
using System.Collections.Generic;
namespace CNGroup
{
    public class Calculations
    {
        public List<CalcCase> CalcCases;

        public static bool Errors = false;
        public static string ErrorMessage = String.Empty;

        public Calculations()
        {
            CalcCases = new List<CalcCase>();
            CalcCases.Clear();
        }

    }
}
