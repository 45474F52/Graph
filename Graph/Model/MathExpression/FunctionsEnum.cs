namespace Graph.Model.MathExpression
{
    internal static class FunctionsEnum
    {
        internal static FunctionType GetFunctionType(ref string expressionText)
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

        internal enum FunctionType
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