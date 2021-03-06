﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public enum TypeCarte{DEMO=5, PETITE=10, NORMALE=15};

    public class FabriqueAutre
    {
        public Carte creerCarte(TypeCarte type)
        {
            return new Carte((int)type);
        }

        public Joueur creerJoueur(TypeUnite peuple, int id)
        {
            return new Joueur(peuple, id);
        }
    }
}
