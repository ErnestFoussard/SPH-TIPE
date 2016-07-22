using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPH_TIPE
{
    class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Norme { get { return Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public Vector3(double x, double y, double z) 
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static double Distance(Vector3 vect1, Vector3 vect2) 
        {
            return new Vector3(vect1.X - vect2.X, vect1.Y - vect2.Y, vect1.Z - vect2.Z).Norme;
        }

    

    }
}
