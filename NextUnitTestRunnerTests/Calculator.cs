using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUnitTestRunnerTests
{
    public class CalculatorCore
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }

    public class Calculator
    {
        private CalculatorCore _calculatorCore = null;
        public Calculator(CalculatorCore calculatorCore)
        {
            _calculatorCore = calculatorCore;
        }

        public int Sum(int x, int y)
        {
            return _calculatorCore.Sum(x, y);
        }
    }
}
