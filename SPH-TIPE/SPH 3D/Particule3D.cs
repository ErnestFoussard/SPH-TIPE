using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SPH_TIPE
{
    class Particule3D
    {
        
        // Propriétés
        public double MasseVolumique { get; set; }
        public int Index { get; set; }
        private Vector3 _Position;
        public Vector3 Position
        {   
            get {return _Position;}
            set
            {
                // On bouge, on change de cellule
                Systeme3D.Grille[Cellule].Remove(Index);
                short x = (short)(Position.X / constants3D.rayonSPH);
                short y = (short)(Position.Y / constants3D.rayonSPH);
                short z = (short)(Position.Z / constants3D.rayonSPH);
                Cellule = new Tuple<short, short, short>(x, y, z);
                Systeme3D.Grille[Cellule].Add(Index);
                _Position = value; // On affecte à la fin la nouvelle valeur de la postion
            }
        }
        public Vector3 Vitesse { get; set; }
        public Vector3 Acceleration { get; set; }
        public Tuple<short, short, short> Cellule
        {
            get;
            //{
            //short x = (short)(Position.X / constants3D.rayonSPH);
            //short y = (short)(Position.Y / constants3D.rayonSPH);
            //short z = (short)(Position.Z / constants3D.rayonSPH);
            //return new Tuple<short, short, short>(x, y, z);
            //}
            set;
        } 

        // Constructeurs
        public Particule3D(Vector3 position, Vector3 vitesse, Vector3 acceleration) 
        {
            this.Position = position;
            this.Vitesse = vitesse;
            this.Acceleration = acceleration;
            this.Index = Systeme3D.NombreParticulesInstanciees++;
        }

        // Méthodes
        public List<int> TrouverVoisines() //On repère toujours les particules par leurs indices
        {

            // On trouve les particules dans les cellules adjacentes
            List<int> ParticulesProches = new List<int>();
            for (short i = -1; i <= 1; i++) //Améliorable avec une jolie requête LINQ?
            {
                for (short j = -1; j <= 1; j++)
                {
                    for (short k = -1; k <= 1; k++)
                    {
                        Tuple<short, short, short> tuple = new Tuple<short, short, short>((short)(Cellule.Item1 + i), (short)(Cellule.Item2 + j), (short)(Cellule.Item3 + k));
                        ParticulesProches.Concat<int>(from index in Systeme3D.Grille[tuple]
                                                         where (Vector3.Distance(Systeme3D.Particules[index].Position, this.Position) <= constants3D.rayonSPH)
                                                         select index);
                    }
                }
            }
            return ParticulesProches;
            // et on garde celles qui nous interessent

            

            

        }

        public Vector3 CalculerAcceleration(List<int> Voisines) // On applique Navier-Stokes pour trouver l'accéleration
        {
            this.MasseVolumique = (constants3D.masse * Voisines.Count()) / (Math.Pow(constants3D.rayonSPH, 3) * (4 / 3) * Math.PI);
            // La masse fois le nombre de particules divisée par le rayon SPH
            foreach (int particule in Voisines)
            { 
            
            }
            throw new NotImplementedException();
        }
        public Vector3 CalculerPosition() // On intègre pour trouver la position
        {
            throw new NotImplementedException();
        }
    }

    static class Systeme3D 
    {
        public static int NombreParticulesInstanciees = -1; // On compte le nombre d'instances (on commence à -1 pour que les index correspondent aux positions dans la liste
        public static Particule3D[] Particules { get; set; } // Avantage: ce tableau sera long à initialiser mais ne bougera plus une fois initialisé
        public static Dictionary<Tuple<short, short, short>, List<int>> Grille { get; set; } // On repère les particules par leurs index

        //On empilera les différentes valeurs dans ces listes au fur et à mesure et modifiera les particules à la toute fin avec Update
        public static List<Vector3> NouvellesPositions { get; set; }
        public static List<Vector3> NouvellesVitesses { get; set; }
        public static List<Vector3> NouvellesAccelerations { get; set; }
        

        public static void Initialisation() 
        {
            throw new NotImplementedException(); // Pour initialiser le système
        }

        public static void CalculerEtatSuivant() 
        {
            throw new NotImplementedException();
        }

        public static void Update()
        {
            for (int i = 0; i < Particules.Length; i++) 
            {
                Particules[i].Acceleration = NouvellesAccelerations[i];  // On a aucun soucis car on manipule des Vector3 créés dans une structure
                Particules[i].Vitesse = NouvellesVitesses[i];
                Particules[i].Position = NouvellesPositions[i];
                NouvellesAccelerations = new List<Vector3>();
                NouvellesVitesses = new List<Vector3>();
                NouvellesPositions = new List<Vector3>();
            }
        }
    }

}
