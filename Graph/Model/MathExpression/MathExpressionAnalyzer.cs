using System;

namespace Graph.Model.MathExpression
{
    public static class MathExpressionAnalyzer
    {
        public static string[] Analyze(string expression, string regExpr)
        {
            throw new NotImplementedException();
        }

        internal static Tuple<(double[], double[]), FunctionAnalysisModel> SolveFormula(
            FunctionAnalysisModel functionalAnalysis, FunctionType functionType, uint numberOfPoints, params double[] arguments /*a, b, c*/)
        {
            double[] xPoints = new double[numberOfPoints];
            double[] yPoints = new double[numberOfPoints];

            int bound = (int)Math.Truncate((double)numberOfPoints / 2);

            switch (functionType)
            {
                case FunctionType.Linear:
                    yPoints = GetLinearPoints(ref bound, ref xPoints, ref arguments[0], ref arguments[1]);
                    break;
                case FunctionType.Quadratic:
                    yPoints = GetQuadraticPoints(ref functionalAnalysis, ref bound, ref xPoints, ref arguments[0], ref arguments[1], ref arguments[2]);
                    break;
                case FunctionType.Power:
                    break;
                case FunctionType.Exponential:
                    break;
                case FunctionType.Logarithmic:
                    break;
                case FunctionType.Sinusoid:
                    break;
                case FunctionType.Cosine:
                    break;
                case FunctionType.Tangentoid:
                    break;
                case FunctionType.Cotangenoid:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new Tuple<(double[], double[]), FunctionAnalysisModel>(( xPoints, yPoints ), functionalAnalysis);
        }

        private static double[] GetLinearPoints(ref int bound, ref double[] xPoints, ref double a, ref double b)
        {
            int length = xPoints.Length;
            xPoints = GetXPoints(ref length, ref bound);
            double[] yPoints = new double[length];

            for (int i = 0; i < xPoints.Length; i++)
            {
                yPoints[i] = a * xPoints[i] + b;
            }

            return yPoints;
        }

        private static double[] GetQuadraticPoints(
            ref FunctionAnalysisModel functionAnalysis, ref int bound, ref double[] xPoints, ref double a, ref double b, ref double c)
        {
            if (a == 0)
                throw new ArgumentException("аргумент \"a\" не может быть равным 0");

            int length = xPoints.Length;
            double[] yPoints = new double[length];

            //вершина параболы
            double x0 = -b / (2 * a);
            double y0 = c - (Math.Pow(b, 2) / (4 * a));

            double D = Math.Pow(b, 2) - 4 * a * c;
            double? x1, x2;//нули функции (пересечения с осью X)

            if (D > 0)
            {
                double sqrtD = Math.Sqrt(D);
                x1 = (-b + sqrtD) / (2 * a);
                x2 = (-b - sqrtD) / (2 * a);

                functionAnalysis.ZerosOfFunc = $"({x1:N} ; 0) and ({x2:N} ; 0)";
            }
            else if (D == 0)
            {
                functionAnalysis.ZerosOfFunc = $"{x0:N}";
            }
            else
            {
                functionAnalysis.ZerosOfFunc = $"\u2205";
            }

            xPoints = GetSymmetricalXPoints(ref length, ref bound, ref x0);

            for (int i = 0; i < xPoints.Length; i++)
            {
                yPoints[i] = a * Math.Pow(xPoints[i], 2) + b * xPoints[i] + c;
            }

            if (a < 0)
            {
                
                functionAnalysis.DefinitionScope = $"f(x) \u2208 (-\u221e ; {y0:N}]";
                functionAnalysis.Maximum = $"({x0} ; {y0})";
            }
            else
            {
                functionAnalysis.DefinitionScope = $"f(x) \u2208 [{y0:N} ; +\u221e)";
                functionAnalysis.Minimum = $"({x0:N} ; {y0:N})";
            }

            functionAnalysis.ScopeOfValues = "f(x) \u2208 (-\u221e ; +\u221e) (R)";
            functionAnalysis.IntersWithAxisY = $"(0 ; {c})";

            functionAnalysis.Parity = "\u003f\u00bf\u003f";

            return yPoints;
        }

        private static double[] GetXPoints(ref int numberOfPoints, ref int bound)
        {
            double[] xPoints = new double[numberOfPoints];
            int index = 0;

            if (numberOfPoints % 2 == 0)
            {
                for (int j = -bound; j < bound - 1; j++)
                {
                    xPoints[index] = j;
                    index++;
                }
            }
            else
            {
                for (int j = -bound; j < 0; j++)
                {
                    xPoints[index] = j;
                    index++;
                }

                for (int g = 0; g <= bound; g++)
                {
                    xPoints[index] = g;
                    index++;
                }
            }
            return xPoints;
        }

        private static double[] GetSymmetricalXPoints(ref int numberOfPoints, ref int bound, ref double xVertex)
        {
            double[] xPoints = new double[numberOfPoints];
            int pointsCount = 0;
            int firstHalf = 0;
            int secondHalf = 1;

            if (numberOfPoints % 2 == 0)
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    if (pointsCount < bound)
                    {
                        if (firstHalf < bound)
                        {
                            xPoints[i] = xVertex - bound + firstHalf;
                            pointsCount++;
                        }
                        firstHalf++;
                    }
                    else
                    {
                        if (secondHalf <= bound)
                        {
                            xPoints[i] = xVertex + secondHalf;
                            pointsCount++;
                        }
                        secondHalf++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < numberOfPoints; i++)
                {
                    if (pointsCount < bound)
                    {
                        if (firstHalf < bound)
                        {
                            xPoints[i] = xVertex - bound + firstHalf;
                            pointsCount++;
                        }
                        firstHalf++;
                    }
                    else if (pointsCount == bound)
                    {
                        xPoints[i] = xVertex;
                        pointsCount++;
                    }
                    else
                    {
                        if (secondHalf <= bound)
                        {
                            xPoints[i] = xVertex + secondHalf;
                            pointsCount++;
                        }
                        secondHalf++;
                    }
                }
            }

            return xPoints;
        }

        public static FunctionType GetFunctionType(ref string expressionText)
        {
            switch (expressionText)
            {
                case "f(x) = ax + b":
                    return FunctionType.Linear;
                case "f(x) = ax^2 + bx + c":
                    return FunctionType.Quadratic;
                case "f(x) = ax^(b/c)":
                    return FunctionType.Power;
                case "f(x) = a^bx":
                    return FunctionType.Exponential;
                case "f(x) = log_a(bx)":
                    return FunctionType.Logarithmic;
                case "f(x) = asin(bx)":
                    return FunctionType.Sinusoid;
                case "f(x) = acos(bx)":
                    return FunctionType.Cosine;
                case "f(x) = atg(bx)":
                    return FunctionType.Tangentoid;
                case "f(x) = actg(bx)":
                    return FunctionType.Cotangenoid;
                default:
                    return FunctionType.Custom;
            }
        }

        public enum FunctionType
        {
            Custom,
            Linear,
            Quadratic,
            Power,
            Exponential,
            Logarithmic,
            Sinusoid,
            Cosine,
            Tangentoid,
            Cotangenoid
        }
    }
}