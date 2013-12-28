﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public class Viking : Unite
    {
        public Viking(int id, Coordonnee coords) : base(id, coords)
        {
        }

        public override int getPoints() {

            if (Carte.getCase(coordonnees).getTypeCase() == typeCases.EAU) {
                return 0;
            } else {

                // TODO : Probléme si on est au bord de la map sur une case d'eau (il comptera point double)
                if (Carte.getCase(coordonnees.decaler(0, 1)).getTypeCase() == typeCases.EAU
                    || Carte.getCase(coordonnees.decaler(0, -1)).getTypeCase() == typeCases.EAU
                    || Carte.getCase(coordonnees.decaler(1, 0)).getTypeCase() == typeCases.EAU
                    || Carte.getCase(coordonnees.decaler(-1, 0)).getTypeCase() == typeCases.EAU)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }

        
        }

        public override bool deplacementPossible(Coordonnee coords)
        {
            int distance = coordonnees.distance(coords);
            if (distance > 1) return false;
            return (mouvement >= 1);
        }

        public override bool deplacement(Coordonnee coords)
        {
            if (!deplacementPossible(coords)) return false;
            
            mouvement -= 1.0;
            return true;
        }

    }
}
