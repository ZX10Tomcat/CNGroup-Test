using System;
using System.Collections.Generic;
using Instruction = System.Collections.Generic.KeyValuePair<string, double>;

namespace CNGroup
{
    public class CalcCase
    {
        public List<Instruction> steps = new List<Instruction>();
        public Double result;
    }
}
