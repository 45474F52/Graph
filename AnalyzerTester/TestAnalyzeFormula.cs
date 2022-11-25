using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph.Model.MathExpression;

namespace AnalyzerTester
{
    [TestClass]
    public class TestAnalyzeFormula
    {
        [TestMethod]
        public void AnalyzeFormulas()
        {
            string regExpr = @"[-+]?\d*[Xx]\^[-+]?\d+[-+]?\d*[Xx][-+]\d+";

            string expr1 = "";
            //string expr2 = "";
            //string expr3 = "";
            //string expr4 = "";
            //string expr5 = "";

            string[] expected1 = new string[] { };
            //string[] expected2 = new string[] { };
            //string[] expected3 = new string[] { };
            //string[] expected4 = new string[] { };
            //string[] expected5 = new string[] { };

            string[] values1 = MathExpressionAnalyzer.Analyze(expr1, regExpr);
            //string[] values2 = MathExpressionAnalyzer.Analyze(expr2, regExpr);
            //string[] values3 = MathExpressionAnalyzer.Analyze(expr3, regExpr);
            //string[] values4 = MathExpressionAnalyzer.Analyze(expr4, regExpr);
            //string[] values5 = MathExpressionAnalyzer.Analyze(expr5, regExpr);

            Assert.AreEqual(expected1, values1);
            //Assert.AreEqual(expected2, values2);
            //Assert.AreEqual(expected3, values3);
            //Assert.AreEqual(expected4, values4);
            //Assert.AreEqual(expected5, values5);
        }
    }
}
