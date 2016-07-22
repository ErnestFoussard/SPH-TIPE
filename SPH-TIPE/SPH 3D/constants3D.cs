using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPH_TIPE
{
    static class constants3D
    {
        public const double masse = 0;
        public const double rayonSPH = 0;
        public static double Kernel(double q) 
        {
            double facteur = 1 / (Math.PI * Math.Pow(rayonSPH, 3));
            if (0 <= q && q <= 1) return facteur * (1 - 1.5 * q * q * (1 - q / 2));
            if (1 < q && q <= 2) return facteur * 0.25 * Math.Pow(2 - q, 3);
            return 0;
        }
        public static Vector3 KernelGradient()
        {
            throw new NotImplementedException();
        }
        public static Vector3 KernelLaplacien()
        {
            throw new NotImplementedException();
        }
    }
}
