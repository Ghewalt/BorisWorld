﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Gaulois : Unite
    {

        public Gaulois(Coordonnee coords) : base(coords)
        {
        }
        
        public override int getPoints() {
            switch (Carte.getCase(coordonnees).getTypeCase())
            {
                case TypeCases.PLAINE:
                    return 2;
                case TypeCases.MONTAGNE:
                    return 0;
                default:
                    return 1;
            }
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            Case c = Carte.getCase(coords);
            int distance = coordonnees.distance(coords);
            if (distance > 1 || c.getTypeCase() == TypeCases.EAU) return false;

            if (c.getTypeCase() == TypeCases.PLAINE && mouvement >= 0.5) return true;

            return (mouvement >= 1);
        }

        public override resultatCombat deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return resultatCombat.DEPLACEMENT_IMPOSSIBLE;

            resultatCombat retour = verifUniteCase(coords);

            Case c = Carte.getCase(coords);
            if (c.getTypeCase() == TypeCases.PLAINE) mouvement -= 0.5;
            else mouvement -= 1.0;
            makeResultatCombat(retour, coords);

            return retour;
        }


    }
}
