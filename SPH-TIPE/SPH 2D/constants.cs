using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SPH_TIPE
{
    static class constants
    {
        public static double masse { get; set; }
        public static double rayonSPH { get; set; }
        public static double pasTemporel { get; set; }
        public static double P0 { get; set; } //Coefficient de proportionnalité pour les calculs de pression
        public static double CoeffViscosite { get; set; }
    }
    static class Kernel
    {
        public static double KernelSimple(double r) // Poly 6
        {
            double facteur = 315/(64 * Math.PI * Math.Pow(constants.rayonSPH,10)); // On divise une fois de plus par rayonSPH pour renormaliser
            return r <= constants.rayonSPH ? facteur * Math.Pow(Math.Pow(constants.rayonSPH, 2) - Math.Pow(r, 2), 3) : 0;
            //if(r <= constants.rayonSPH)
            //{
            //    return facteur * Math.Pow(Math.Pow(constants.rayonSPH, 2) - Math.Pow(r, 2), 3);
            //}
            //else
            //{
            //    return 0;
            //}
        }
        public static Vector KernelGradient(Vector r) // "spiky kernel"
        {
            double facteur = -45 / (Math.PI * Math.Pow(constants.rayonSPH, 6));
            return r.Length <= constants.rayonSPH ? facteur * Math.Pow((constants.rayonSPH - r.Length), 2) * r : new Vector(0, 0);
            //if(r.Length <= constants.rayonSPH)
            //{
            //    return facteur * Math.Pow((constants.rayonSPH - r.Length),2) * r;
            //}
            //else
            //{
            //    return new Vector(0,0);
            //}
        }
        public static double KernelLaplacien(double r)
        {
            double facteur = 45 / (Math.PI * Math.Pow(constants.rayonSPH, 6));
            return r <= constants.rayonSPH ? facteur * (constants.rayonSPH - r) : 0;
            //if(r <= constants.rayonSPH)
            //{
            //    return facteur * (constants.rayonSPH - r);
            //}
            //else
            //{
            //    return 0;
            //}
        }
    }
}

