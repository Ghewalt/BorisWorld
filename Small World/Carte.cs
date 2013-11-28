﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace Small_World
{

    unsafe public class Carte
    {
        static int** grid;
        static int taille;

        unsafe public Carte(int taille)
        {
            WrapperMap wrapper = new WrapperMap();
            grid = wrapper.genererMap(taille);
            Carte.taille = taille;
        }

        static public Case getCase(int X, int Y)
        {
            if (X < 0 || X > taille || Y < 0 || Y > taille)
            {
                throw new Exception("Coordonnées invalides");
            }
            int type = grid[X][Y];

            switch (type)
            {
                case (int)typeCases.DESERT:
                    return FabriqueCase.obtenirCase(typeCases.DESERT);
                case (int)typeCases.EAU:
                    return FabriqueCase.obtenirCase(typeCases.EAU);
                case (int)typeCases.FORET:
                    return FabriqueCase.obtenirCase(typeCases.FORET);
                case (int)typeCases.MONTAGNE:
                    return FabriqueCase.obtenirCase(typeCases.MONTAGNE);
                case (int)typeCases.PLAINE:
                default:
                    return FabriqueCase.obtenirCase(typeCases.PLAINE);
            }

        }

        unsafe public void print()
        {
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}