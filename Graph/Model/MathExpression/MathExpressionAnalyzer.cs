using System;
using System.Windows;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AnalyzerTester")]
namespace Graph.Model.MathExpression
{
    internal class MathExpressionAnalyzer
    {
        private int _bound;
        private double _a;
        private double _b;

        internal Tuple<(double[], double[]), FunctionAnalysisModel> SolveFormula(FunctionAnalysisModel functionalAnalysis,
            FunctionsEnum.FunctionType functionType, uint numberOfPoints, params double[] arguments)
        {
            (double[], double[]) points = (new double[numberOfPoints], new double[numberOfPoints]);

            int bound = (int)Math.Truncate((double)(numberOfPoints / 2));

            switch (functionType)
            {
                case FunctionsEnum.FunctionType.Linear:
                    SetLinearPoints(ref points);
                    break;
                case FunctionsEnum.FunctionType.Quadratic:
                    SetQuadraticPoints(ref functionalAnalysis, bound, ref points, arguments[0], arguments[1], arguments[2]);
                    break;
                case FunctionsEnum.FunctionType.Power:
                    break;
                case FunctionsEnum.FunctionType.Exponential:
                    break;
                case FunctionsEnum.FunctionType.Logarithmic:
                    break;
                case FunctionsEnum.FunctionType.Sinusoid:
                    //SetSinusoidPoints(ref functionalAnalysis, ref points, arguments[0], arguments[1]);
                    break;
                case FunctionsEnum.FunctionType.Cosine:
                    break;
                case FunctionsEnum.FunctionType.Tangentoid:
                    break;
                case FunctionsEnum.FunctionType.Cotangenoid:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new Tuple<(double[], double[]), FunctionAnalysisModel>(points, functionalAnalysis);
        }

        private void SetLinearPoints(ref (double[], double[]) points)
        {
            points.Item1 = XPointsGetter.GetPoints(points.Item1.Length, _bound);

            for (int i = 0; i < points.Item1.Length; i++)
            {
                points.Item2[i] = _a * points.Item1[i] + _b;
            }
        }

        private static void SetQuadraticPoints(
            ref FunctionAnalysisModel functionAnalysis, int bound, ref (double[], double[]) points, double a, double b, double c)
        {
            if (a == 0)
                throw new ArgumentException("аргумент \"a\" не может быть равным 0");

            double x0 = -b / (2 * a);
            double y0 = c - (Math.Pow(b, 2) / (4 * a));

            double D = Math.Pow(b, 2) - 4 * a * c;
            double? x1, x2;

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

            points.Item1 = XPointsGetter.GetSymmetricalPoints(points.Item1.Length, bound, x0);

            for (int i = 0; i < points.Item1.Length; i++)
            {
                points.Item2[i] = a * Math.Pow(points.Item1[i], 2) + b * points.Item1[i] + c;
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
        }

        [Obsolete("Плохая реализация метода с независимыми переменными")]
        private static void SetSinusoidPoints(ref FunctionAnalysisModel functionalAnalysis, ref (double[], double[]) points, double amplitude, double b)
        {
            if (b == 0)
                throw new ArgumentException("аргумент \"b\" не может быть равным 0");

            double X0 = 2;
            double X1 = 5;
            double scale = 1;
            uint stepsCount = 10;

            (double x0, double x1) = (X0, X1);
            
            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
            }

            double step = (x1 - x0) / stepsCount;

            double offset = 0;
            double x = x0;

            List<Point> newPoints = new List<Point>
            {
                new Point(offset, -Math.Sin(x) * scale)
            };

            for (offset += step, x += step; x < x1; offset += step, x += step)
            {
                newPoints.Add(new Point(offset * scale, -Math.Sin(x) * scale));
            }
            newPoints.Add(new Point((x1 - x0) * scale, -Math.Sin(x1) * scale));

            points.Item1 = new double[newPoints.Count];
            points.Item2 = new double[newPoints.Count];

            for (int i = 0; i < newPoints.Count; i++)
            {
                points.Item1[i] = newPoints[i].X;
                points.Item2[i] = newPoints[i].Y;
            }
        }
    }
}