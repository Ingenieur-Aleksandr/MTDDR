using System.Collections.Generic;
using static System.Math;

namespace MTDDR
{
    class DispersionCalculationsForPhase
    {
        const double dx = 0.1;

        double InterpolateResistance(double x, List<Data> data)
        {
            var logF = data.ConvertAll(d => Log10(d.freq));
            var mag = data.ConvertAll(d => Log10(d.rho));

            return MathApp.Interpolate(x, logF, mag);
        }// линейная интерполяция

        double DerivativeResistance(double x0, List<Data> points)
        {
            return (InterpolateResistance(x0 + dx, points) - InterpolateResistance(x0 - dx, points)) / (2 * dx);
        }// производная функции ln(rho(ln(freq)))

        public double DispersePhase(int N, double t, List<Data> points)
        {
            double sum = 0;
            double sumB = 0;

            for (int x = -N; x <= N; x++)
            {
                if (x == 0)
                    continue;

                double shift = x / (double)N * 2;
                double ln = Log(Pow(10, shift));

                sumB += MathApp.Bode(ln);
                sum += MathApp.Bode(ln) * DerivativeResistance(t - shift, points);
            }// цикл конечной суммы для приближенного расчёта несобственного интеграла

            sumB = 1.0 / sumB; // поправочный коэффициент

            return (PI / 4) + sum * sumB * PI/4 ;
        }// свёртка и линейное преобразование 
    }// класс расчёта дисперсионного соотношения фазы от сопротивления

    
}
