using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNGroup
{
    public static class Operators
    {
        public static Dictionary<string, string> operators;
        static Operators()
        {
            operators = new Dictionary<string, string>();
            operators.Add("+", "add");
            operators.Add("-", "subtract");
            operators.Add("*", "multiply");
            operators.Add("/", "divide");
        }
    }
}
