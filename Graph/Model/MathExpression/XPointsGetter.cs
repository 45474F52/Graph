namespace Graph.Model.MathExpression
{
    internal static class XPointsGetter
    {
        internal static double[] GetPoints(int numberOfPoints, int bound)
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

        internal static double[] GetSymmetricalPoints(int numberOfPoints, int bound, double xVertex)
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
    }
}