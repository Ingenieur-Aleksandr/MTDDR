namespace MTDDR
{
    struct  Data
    {
        public double freq; // частота
        public double rho; // сопртивление
        public double phi; // фаза

        public Data(double f, double r, double p)
        {
            freq = f;
            rho = r;
            phi = p;
        }// конструктор для работы с данными
    }
}
