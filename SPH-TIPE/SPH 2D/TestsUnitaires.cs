using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows;
using System.IO;

namespace SPH_TIPE
{
    static class TestsUnitaires
    {
        public static void TestInit()
        {
            Systeme.Initialisation("init.xml",new Tuple<float,float>(-50,-50), new Tuple<float,float>(50,50));
            Console.WriteLine("masse" + constants.masse);
            Console.WriteLine("masse" + constants.pasTemporel);
            Console.WriteLine("masse" + constants.rayonSPH);
            Console.WriteLine(Systeme.Particules[1531].Cellule);
            Console.ReadKey();
        }

        public static void TestHydrostatique0()
        {
            Systeme.Initialisation(@"C:\Users\Ernest\Desktop\Conditions initiales TIPE\Hydrostatique-2071718.xml", new Tuple<float, float>(-50, -50), new Tuple<float, float>(50, 50));
            Console.WriteLine("Masse:" + constants.masse);
            Console.WriteLine("Pas Temporel:" + constants.pasTemporel);
            Console.WriteLine("Rayon SPH:" + constants.rayonSPH);
            AfficherTableau();
            for (int i = 0; i < Systeme.NombreParticulesInstanciees; i++)
            {
                List<int> Voisines = Systeme.Particules[i].TrouverVoisines();
                Systeme.Particules[i].CalculerAcceleration(Voisines);
                Console.WriteLine(Systeme.Particules[i].MasseVolumique);
            }
            Console.ReadKey();
        }

        public static void TestHydrostatique1()
        {
            Systeme.Initialisation(@"C:\Users\Ernest\Desktop\Conditions initiales TIPE\Hydrostatique-181054.xml", new Tuple<float, float>(-50000, -50000), new Tuple<float, float>(50000, 50000));
            string chemin = Environment.CurrentDirectory + "/Experiences/Hydrostatique-" + DateTime.Now.Day 
                                                                                 + DateTime.Now.Month 
                                                                                 + DateTime.Now.Hour 
                                                                                 + DateTime.Now.Minute 
                                                                                 + ".xml";
            Systeme.Experience(Environment.CurrentDirectory + "/Experiences/Vide.xml",chemin,10);
        }

        public static void TestCreationSystemeHydrostatique()
        {
            string chemin = Environment.CurrentDirectory + "/Hydrostatique-" + DateTime.Now.Day 
                                                                                 + DateTime.Now.Month 
                                                                                 + DateTime.Now.Hour 
                                                                                 + DateTime.Now.Minute 
                                                                                 + ".xml";
            Init.XFichier = XDocument.Load(Environment.CurrentDirectory + "/Vide.xml");
            Init.SystemeHydrostatiqueSimple(1, 0.1, 100, new Vector(0, 0), new Vector(1000, 1000));
            Init.XFichier.Save(chemin);
            Console.WriteLine("done");
            Console.ReadKey();
        }

        public static void AfficherTableau()

        {
            foreach (Particule particule in Systeme.Particules)
            {
                Console.Write(particule.Index + " ");
                Console.Write(particule.Position.X + " " + particule.Position.Y + " ");
                Console.Write(particule.Cellule + " ");
                Console.Write(particule.MasseVolumique + " ");
                Console.WriteLine(particule.Mobile);
            }
        }

        public static void TestTrouverVoisines(int indexParticule)
        {
            List<int> Voisines = Systeme.Particules[indexParticule].TrouverVoisines();
            foreach (int i in Voisines) Console.WriteLine(i);
        }

    }

    static class Debugg
    {
        public static T Espion<T>(T Valeur, Action<T> Expression)// Pour afficher facilement les valeurs dans les requetes LINQ
        {
            Expression(Valeur);
            return Valeur;
        }
    }

}
