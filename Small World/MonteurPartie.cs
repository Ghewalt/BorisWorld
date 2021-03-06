﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class MonteurPartie
    {
        private FabriqueAutre fabAutre;
        private FabriqueUnite fabUnit;
        private Random r;

        public MonteurPartie()
        {
            fabAutre = new FabriqueAutre();
        }

        public void nouvellePartie(TypeCarte taille, TypeUnite typeJ1, TypeUnite typeJ2)
        {
            SmallWorld.Instance.carte = fabAutre.creerCarte(taille);
            SmallWorld.Instance.carte.initialiseVortexs();
            SmallWorld.Instance.carte.print();

            List<Joueur> joueurs = new List<Joueur>();

            r = new Random();
            SmallWorld.Instance.premierJoueur = r.Next(SmallWorld.NOMBRE_JOUEURS);
            SmallWorld.Instance.joueurCourant = SmallWorld.Instance.premierJoueur;
            Console.WriteLine("Premier joueur : " + SmallWorld.Instance.premierJoueur);

            joueurs.Add(fabAutre.creerJoueur(typeJ1, 0));
            joueurs.Add(fabAutre.creerJoueur(typeJ2, 1));

            SmallWorld.Instance.joueurs = joueurs;
            SmallWorld.Instance.nbTours = 0;

            int nombreUnites = SmallWorld.Instance.carte.getNombreUniteMax();
            List<Coordonnee> posDepartJoueurs = SmallWorld.Instance.carte.getDepartJoueurs();
            foreach (Joueur j in SmallWorld.Instance.joueurs)
            {

                switch (j.Peuple)
                {
                    case TypeUnite.Gaulois:
                        fabUnit = new FabriqueGaulois();
                        break;
                    case TypeUnite.Viking:
                        fabUnit = new FabriqueViking();
                        break;
                    case TypeUnite.Nain:
                        fabUnit = new FabriqueNain();
                        break;
                    default:
                        throw new Exception("Type unité non reconnue");
                }


                for (int i = 0; i < nombreUnites; i++)
                {
                    j.addUnite(fabUnit.fabriquerUnite(posDepartJoueurs[j.idJoueur])); 
                    
                }
            }
            
        }

    }
}
