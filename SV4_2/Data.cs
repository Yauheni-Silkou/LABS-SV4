using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SV4_2
{
    public struct Data
    {
        public double XBegin { get; }
        public double XEnd { get; }
        public double XStep { get; }
        public double? A { get; }
        public double? B { get; }
        public double? D { get; }
        public int Thickness { get; }
        public Color GraphColor { get; }
        public string ColorName { get; }

        public Data(double xBegin, double xEnd, double xStep,
            double? a, double? b, double? d, int thickness,
            Color graphColor, string colorName)
        {
            A = a;
            B = b;
            D = d;
            XBegin = xBegin;
            XEnd = xEnd;
            XStep = xStep;
            Thickness = thickness;
            GraphColor = graphColor;
            ColorName = colorName;
        }
    }
}
