using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace MTDDR
{
    class CsvCurveReader
    {
        public List<Data> GetData(string name)
        {
            string[] lines = File.ReadAllLines(name);

            List<Data> points = new List<Data>();
            try
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(';');
                    var ci = CultureInfo.InvariantCulture;
                    points.Add(new Data(Convert.ToDouble(data[0], ci), Convert.ToDouble(data[1], ci), Convert.ToDouble(data[2], ci)));
                }// цикл записи данных из массива типа string в список
            }
            catch
            {
                throw new Exception("использовано некорректное представление данных в таблице.");
            }// проверка ошибки на этапе считывания файла

            points.Sort((x, y) => x.freq.CompareTo(y.freq));

            return points;
        }// метод получения списка точек
    }
}
