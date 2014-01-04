﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Small_World
{
    public class GestionFichiers
    {
        const string extentionSauvegarde = ".lol";
        const string path = @"Sauvegardes";

        public static List<String> getFichiers()
        {
            List<String> fichiers = new List<String>();

            checkDossierSauvegarde();
           
            string[] files = Directory.GetFiles(path);
            foreach (String file in files)
            {
                if (Path.GetExtension(file) == extentionSauvegarde)
                {
                    fichiers.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

            return fichiers;
        }

        private static void checkDossierSauvegarde()
        {
            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur creation dossier sauvegarde: {0}", e.ToString());
            } 

        }

        private static string ajouterExtention(string nom)
        {
            nom += extentionSauvegarde;
            return nom;
        }


        /*
       * A GARDER POUR LES TESTS
            
      Coordonnee.initialiser(5); 

      BinaryFormatter formatter = new BinaryFormatter();
      Stream stream = new FileStream(nomFichier, FileMode.Create, FileAccess.Write, FileShare.None);

      FabriqueUnite f;
      Joueur j1 = new Joueur(TypeUnite.Gaulois, 1);
      Joueur j2 = new Joueur(TypeUnite.Nain, 2);
      f = new FabriqueGaulois();
      j1.addUnite(f.fabriquerUnite(new Coordonnee(1,1)));
      j1.addUnite(f.fabriquerUnite(new Coordonnee(2,2)));
      j1.pointJoueur += 5;

      f = new FabriqueNain();
      j2.addUnite(f.fabriquerUnite(new Coordonnee(3,3)));
      j2.addUnite(f.fabriquerUnite(new Coordonnee(5,5)));
      j2.pointJoueur += 1;

      Console.WriteLine("j1.peuble: {0} // j2.peuble: {1}", j1.Peuple, j2.Peuple);
      Console.WriteLine("j1.pointJoueur: {0} // j2.pointJoueur: {1}", j1.pointJoueur, j2.pointJoueur);
      Console.WriteLine("j1.getUnites()[0].coordonnees.getX(): {0} // j2.getUnites()[0].coordonnees.getX()", j1.getUnites()[0].coordonnees.getX(), j2.getUnites()[0].coordonnees.getX());


      formatter.Serialize(stream, j1);
      formatter.Serialize(stream, j2);
      stream.Close();


      IFormatter formatter2 = new BinaryFormatter();
      Stream stream2 = new FileStream(nomFichier, FileMode.Open, FileAccess.Read, FileShare.Read);
      Joueur j1res = (Joueur)formatter2.Deserialize(stream2);
      Joueur j2res = (Joueur)formatter2.Deserialize(stream2);
      stream2.Close();

      // Voici la preuve
      Console.WriteLine("j1res.peuble: {0} // j2res.peuble: {1}", j1res.Peuple, j2res.Peuple);
      Console.WriteLine("j1res.pointJoueur: {0} // j2res.pointJoueur: {1}", j1res.pointJoueur, j2res.pointJoueur);
      Console.WriteLine("j1res.getUnites()[0].coordonnees.getX(): {0} // j2res.getUnites()[0].coordonnees.getX() {1}", j1res.getUnites()[0].coordonnees.getX(), j2res.getUnites()[0].coordonnees.getX());
       */

        public static void sauvegarder(string nomFichier)
        {
            checkDossierSauvegarde();
            nomFichier = ajouterExtention(nomFichier);
            
                //BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path + Path.DirectorySeparatorChar + nomFichier, FileMode.Create, FileAccess.Write, FileShare.None);
               // formatter.Serialize(stream, SmallWorld.getInstance());
                stream.Close();
             
        }

        public static void charger(string nomFichier)
        {
            /*
                nomFichier = ajouterExtention(nomFichier);
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(nomFichier, FileMode.Create, FileAccess.Write, FileShare.None);
            SmallWorld.remplacerInstance( (SmallWorld) formatter.Deserialize(stream2) );
            stream.Close();
         */
        }
    }
}
