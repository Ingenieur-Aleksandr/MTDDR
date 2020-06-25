using System;
using System.Collections.Generic;
using static System.Math;

namespace MTDDR
{
    class MathApp
    {
        public static double Interpolate(double x, List<double> dataX, List<double> dataY)
        {
            int i = -1;

            if (x < dataX[1])
                i = 1;

            if (x > dataX[dataX.Count - 1])
                i = dataX.Count - 1;

            if (i == -1)
            {
                i = 1;

                while (i < dataX.Count && x > dataX[i])
                    i++;
            }

            return LinearInterpolationOperator(x, dataX[i - 1], dataY[i - 1], dataX[i], dataY[i]);
        }

        public static double LinearInterpolationOperator(double x, double x1, double y1, double x2, double y2)
        {
            if (x == 0 ^ x1 == 0 ^ x2 == 0 ^ x2 - x1 == 0)
                throw new Exception("нулевые значения недопустимы.");

            return y1 + (y2 - y1) * (x - x1) / (x2 - x1);
        }// оператор линейной интерполяции по двум точкам

        public static double Bode(double u)
        {
            double arg = Abs(u) / 2;
            double coth = Cosh(arg) / Sinh(arg);

            return 2 * Log(coth) / (PI * PI);
        }// ядро Боде
    }
}
