﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Small_World;

namespace WPF.NouvellePartie
{
    /// <summary>
    /// Interaction logic for NouvellePartie2.xaml
    /// </summary>
    public partial class NouvellePartie : Page
    {
        private MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        private TypeCarte taille;
        private TypeUnite j1;
        private TypeUnite j2;

        private bool gameLaunched = false;

        public NouvellePartie()
        {
            InitializeComponent();
        }

        public NouvellePartie(bool inGame)
        {
            InitializeComponent();
            gameLaunched = inGame;
        }

        private void tailleClick(object sender, RoutedEventArgs e)
        {
            tailleDemo.Style = this.FindResource("smallButton") as Style;
            tailleNormale.Style = this.FindResource("smallButton") as Style;
            taillePetite.Style = this.FindResource("smallButton") as Style;
            Button clicked = (Button)sender;

            clicked.Style = this.FindResource("smallButtonActivated") as Style;
            switch (clicked.Name)
            {
                case "taillePetite":
                    taille = TypeCarte.PETITE;
                    break;
                case "tailleDemo":
                    taille = TypeCarte.DEMO;
                    break;
                case "tailleNoramle":
                    taille = TypeCarte.NORMALE;
                    break;
                default:
                    taille = TypeCarte.NORMALE;
                    break;
            }

            j1ButtonsActivate();
        }

        private void j1ButtonsActivate()
        {
            j1Gaulois.IsEnabled = true;
            j1Nain.IsEnabled = true;
            j1Viking.IsEnabled = true;
        }

        private void j1Click(object sender, RoutedEventArgs e)
        {
            j1Gaulois.Style = this.FindResource("smallButton") as Style;
            j1Nain.Style = this.FindResource("smallButton") as Style;
            j1Viking.Style = this.FindResource("smallButton") as Style;
            Button clicked = (Button)sender;

            clicked.Style = this.FindResource("smallButtonActivated") as Style;
            switch (clicked.Name)
            {
                case "j1Gaulois":
                    j1 = TypeUnite.Gaulois;
                    break;
                case "j1Nain":
                    j1 = TypeUnite.Nain;
                    break;
                case "j1Viking":
                    j1 = TypeUnite.Viking;
                    break;
            }

            j2ButtonsActivate();
        }

        private void j2ButtonsActivate()
        {
            valider.IsEnabled = false;

            j2Gaulois.Style = this.FindResource("smallButton") as Style;
            j2Nain.Style = this.FindResource("smallButton") as Style;
            j2Viking.Style = this.FindResource("smallButton") as Style;

            j2Gaulois.IsEnabled = true;
            j2Nain.IsEnabled = true;
            j2Viking.IsEnabled = true;

            switch (j1)
            {
                case TypeUnite.Gaulois:
                    j2Gaulois.IsEnabled = false;
                    break;
                case TypeUnite.Nain:
                    j2Nain.IsEnabled = false;
                    break;
                case TypeUnite.Viking:
                    j2Viking.IsEnabled = false;
                    break;
            }
        }

        private void j2Click(object sender, RoutedEventArgs e)
        {
            j2Gaulois.Style = this.FindResource("smallButton") as Style;
            j2Nain.Style = this.FindResource("smallButton") as Style;
            j2Viking.Style = this.FindResource("smallButton") as Style;
            Button clicked = (Button)sender;

            clicked.Style = this.FindResource("smallButtonActivated") as Style;
            switch (clicked.Name)
            {
                case "j2Gaulois":
                    j2 = TypeUnite.Gaulois;
                    break;
                case "j2Nain":
                    j2 = TypeUnite.Nain;
                    break;
                case "j2Viking":
                    j2 = TypeUnite.Viking;
                    break;
            }

            valider.IsEnabled = true;
        }

        private void validerClik(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            clicked.Content = "Création en cours...";
            clicked.IsEnabled = false;

            SmallWorld.Instance.nouvellePartie(taille, j1, j2);

            mainWindow.afficherJeu();
        }

        private void annulerClick(object sender, RoutedEventArgs e)
        {
            mainWindow.afficherMenu(gameLaunched);
        }
    }
}
