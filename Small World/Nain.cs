﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Nain : Unite
    {
        public Nain(Coordonnee coords) : base (coords)
        {
        }

        public override int getPoints() {
            switch (Carte.getCase(coordonnees).getTypeCase())
            {
                case TypeCases.FORET:
                    return 2;
                case TypeCases.PLAINE:
                    return 0;
                default:
                    return 1;
            }
        }

        public override bool deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return false;

            verifUniteCase(coords);

            mouvement -= 1.0;
            return true;
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            int distance = coordonnees.distance(coords);
            Case c = Carte.getCase(coords);

            if (c.getTypeCase() == TypeCases.EAU) return false;

            Case currentCase = Carte.getCase(coordonnees);
            if (currentCase.getTypeCase() == TypeCases.MONTAGNE && c.getTypeCase() == TypeCases.MONTAGNE && mouvement >= 1) return true;

            if (distance > 1) return false;

            return (mouvement >= 1);
        }

    }
}
