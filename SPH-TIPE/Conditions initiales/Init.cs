using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace SPH_TIPE
{
    public class Init
    {
        public static XDocument XFichier { get; set; }
        
        public static void SystemeHydrostatiqueSimple(double masse, double pasTemporel, double rayonSPH, Vector TopLeft, Vector BottomRight)
        {
            XElement XPasTemporel = new XElement("pasTemporel", pasTemporel);
            XElement XMasse = new XElement("masse", masse);
            XElement XRayonSPH = new XElement("rayonSPH", rayonSPH);
            //List<XElement> liste = new List<XElement>{Masse,PasTemporel,RayonSPH};
            double Espacement = 0.1 * rayonSPH;
            int NombreX = (int)Math.Floor((BottomRight.X - TopLeft.X)/Espacement);
            int NombreY = (int)Math.Floor((BottomRight.Y - TopLeft.Y)/Espacement);
            XFichier.Element("conditionsInitiales").Add(XPasTemporel);
            XFichier.Element("conditionsInitiales").Add(XMasse);
            XFichier.Element("conditionsInitiales").Add(XRayonSPH);
            for (int x = 0; x <= NombreX; x++)
            {
                for (int y = 0; y <= NombreY; y++)
                {
                    XFichier.Element("conditionsInitiales").Add(XCreerParticule(new Vector(x,y),new Vector(0,0), new Vector(0,0),x*(NombreY + 1) + y,
                                                                                                      !(x==0 || x==NombreX || y==0 || y==NombreY)));
                                                                                                      //Si on est aux limites, on met des particules fixes
                }
            }
            
        }

        static XElement XCreerParticule(Vector position, Vector vitesse, Vector acceleration, int id, bool mobile)
        {
            XElement XParticule = new XElement("particule",
                                    new XAttribute ("id",id),
                                    new XElement("X",position.X),
                                    new XElement("Y",position.Y),
                                    new XElement("Vx",vitesse.X),
                                    new XElement("Vy",vitesse.Y),
                                    new XElement("Ax",acceleration.X),
                                    new XElement("Ay",acceleration.Y),
                                    new XElement("mobile",mobile));
                                    
            return XParticule;
        }

        static void Ecrire(XElement output,string cheminOutput)
        {
            try
            {
                XDocument fichier = XDocument.Load(cheminOutput);              
                fichier.Element("conditionsInitiales").Add(output);
                fichier.Save(cheminOutput);
            }
            finally { }
        }
    }
}
