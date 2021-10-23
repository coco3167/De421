using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace De421
{
    public class De
    {
        public int nbFaces = 6;
        protected Random _random = new Random();
        public int Face { get; protected set; }
        private List<List<int>> tableauDe = new List<List<int>>();

        public De()
        {
            TabInit();
        }
        public De(int nbFaces)
        {
            this.nbFaces = nbFaces;
            TabInit();
        }

        private void TabInit()
        {
            tableauDe.Add(new List<int>{2,3,4,5,6});
            tableauDe.Add(new List<int>{});
            tableauDe.Add(new List<int>{4,5,6});
            tableauDe.Add(new List<int>{6});
            tableauDe.Add(new List<int>{1,3,5});
            tableauDe.Add(new List<int>{6});
            tableauDe.Add(new List<int>{4,5,6});
            tableauDe.Add(new List<int>{});
            tableauDe.Add(new List<int>{2,3,4,5,6});
        }

        new public string[] ToString()
        {
            string[] lignes = new string[5];
            if (Face > 6)
            {
                return (new string[]{Face.ToString()});
            }
            else
            {
                 lignes[0] = " _________ ";
                for (int loop = 0; loop < 3; loop++)
                {
                    lignes[loop+1] += "|";
                    for (int loop1 = 0; loop1 < 3; loop1++)
                    {
                        if (tableauDe[loop*3 + loop1].Contains(Face))
                        {
                            lignes[loop+1] += " * ";
                        }
                        else
                        {
                            lignes[loop+1] += "   ";
                        }
                    }

                    lignes[loop+1] += "|";
                }

                lignes[4] += " --------- ";
                return lignes;
            }
        }

        public void Lancer()
        {
            Face = _random.Next(nbFaces) + 1;
        }
    }

    class DeTruque : De
    {
        public new void Lancer()
        {
            int index = _random.Next(60);
            if (index < 30)
                this.Face = 6;
            else if (index < 40)
                this.Face = 5;
            else
                this.Face = _random.Next(4) + 1;
        }
    }

    class Jeu
    {
        readonly int nbManches;
        readonly int nbDesNorm = 0;
        readonly int nbDesTruques = 0;
        private List<De> Des = new List<De>();
        
        public Jeu()
        {
            this.nbManches = 5;
            this.nbDesNorm = 5;
            CreaDes();
        }

        public Jeu(int nbManches, int nbDes)
        {
            this.nbManches = nbManches;
            this.nbDesNorm = nbDes;
            CreaDes();
        }

        public Jeu(int nbManches, int nbDesNorm, int nbDesTruques)
        {
            this.nbManches = nbManches;
            this.nbDesNorm = nbDesNorm;
            this.nbDesTruques = nbDesTruques;
            CreaDes();
        }

        private void CreaDes()
        {
            for (int loop = 0; loop < nbDesNorm; loop++)
            {
                Des.Add(new De());
            }

            for (int loop = 0; loop < nbDesTruques; loop++)
            {
                Des.Add(new DeTruque());
            }
        }

        public void Relancer(int i)
        {
            Des[i].Lancer();
        }

        public int Score()
        {
            return Des.Sum(x => x.Face);
        }

        public void Run()
        { 
            for (int loop = 0; loop < nbDesNorm + nbDesTruques; loop++)
            {
                Relancer(loop);
            }
            ToString();
            for (int loop = 0; loop < nbManches; loop++)
            {
                Console.Write("Quels Dé souhaitez vous relancer ?");
                string relance = Console.ReadLine();
                foreach (char num in relance)
                {
                    if (Char.IsNumber(num))
                    {
                        Console.WriteLine(num);
                        Relancer(int.Parse(Char.ToString(num))-1);
                    }
                }
                ToString();
            }
            Console.WriteLine($"Vous avez un score de {Score()}.");
        }

        public void ToString()
        {
            string[] lignes = new string[5];
            foreach (De de in Des)
            {
                string[] lignes2 = de.ToString();
                for (int loop = 0; loop < 5; loop++)
                {
                    lignes[loop] += lignes2[loop] + " ";
                }
            }

            foreach (string ligne in lignes)
            {
                Console.WriteLine(ligne);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            jeu.Run();
        }
    }
}