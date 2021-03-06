#include "MapLib.h"
#include <time.h>
#include <stdlib.h>
#include <cmath>
#include "Coordonnee.h"
#include <stdio.h>
#include <stdio.h>

/**
* G�n�re la carte de taille taille et la retourne sous tableau d'int
* @param int taille La taille de la carte en tiles
* @return int** Le tableau repr�sentant la carte
* @todo Donner des valeurs d'apparitions aux cases, sinon les vikngs sont meilleurs
*/
int** MapLib::genererMap(int taille, std::string chaine){
	nbTypes = MapLib::NBCASE;
	tailleCourante = taille;

	int randSeed = atoi(chaine.c_str());
	srand (randSeed);

	Coordonnee::initialise(taille);
	
	carte = NULL;
	carte = new int* [taille];
	for(int x=0; x<taille; x++){
		carte[x] = new int [taille];
		for(int y=0; y<taille; y++){
			bool caseOk = false;
			while(!caseOk){
				int random = rand() % nbTypes;
				carte[x][y] = random;

				//Diviser les chances d'apparition d'eau et de vortex par 2
				if(random == MapLib::typeCases::EAU || random == MapLib::typeCases::VORTEX)
				{
					int chance2 = rand() % 2;
					if(chance2 == 1)
						caseOk = true;
				}
				else
				{
					caseOk = true;
				}
			}
		}
	}

	tailleCarte = taille;
	init = true;

	positionX = 0;
	positionY = 0;

	return carte;
}



/**
* Rend la positions de d�part des deux joueurs, en tiles.
* Le retour est ordonn� de la mani�re suivante : 
   - int[0][0] : joueur1.x
   - int[0][1] : joueur1.y
   - int[1][0] : joueur2.x
   - int[1][1] : joueur2.y
   @return int** La position de d�part en tiles
*/
int** MapLib::posJoueurs(){
	int* joueur1 = new int[2];
	int* joueur2 = new int[2];
	int** positions = NULL;
	positions = new int* [2];
	srand(time(0));

	int deltaRand = 2;

	bool posJoueur1 = false;
	while(!posJoueur1){
		//Le joueur peut �tre au nord, est, sud ou ouest
		int pos = rand() % 4;
		Coordonnee coords;
		switch(pos)
		{
		case 0:
		default:
			coords.setY(1 + rand() % deltaRand);
			coords.setX(rand() % tailleCarte + 1);
			break;
		case 1:
			coords.setX(tailleCarte - rand() % deltaRand);
			coords.setY(rand() % tailleCarte + 1);
			break;
		case 2:
			coords.setY(tailleCarte - rand() % deltaRand);
			coords.setX(rand() % tailleCarte + 1);
			break;
		case 3:
			coords.setX(1 + rand() * deltaRand);
			coords.setY(rand() % tailleCarte + 1);
			break;
		}
		if(getCase(coords) != MapLib::typeCases::EAU){
			joueur1 = coords.toArray();
			posJoueur1 = true;
		}
	}

	bool posJoueur2 = false;
	//TODO : Risque peut-�tre d'�tre trop proche du centre parfois.
	while(!posJoueur2){
		int deltaPlusMoins = rand() % 2 - 1; //On ajoute ou on retrait
		Coordonnee coords;
		coords.setX(tailleCarte - joueur1[0] + 1 + (rand() % deltaRand) * deltaPlusMoins);
		coords.setY(tailleCarte - joueur1[1] + 1 + (rand() % deltaRand) * deltaPlusMoins);

		if(getCase(coords) != MapLib::typeCases::EAU){
			joueur2 = coords.toArray();
			posJoueur2 = true;
		}
	}
	//Fin d�finition des positions

	positions[0] = joueur1;
	positions[1] = joueur2;

	return positions;
}

//En fonction du peuple de l'unit� et de sa position actuelle, voir si elle ne serait pas mieux � proximit�
int** MapLib::suggestions(int peuple, int x, int y){
	int** suggestions = NULL;
	int precision = 1; //A une case de la case actuelle
	suggestions = new int* [3]; //Jusqu'� 3 propositions
	for(int i=0; i<3; i++){
		suggestions[i] = new int[2]; //X,Y
		for(int j=0; j<2; j++)
			suggestions[i][j] = 0;
	}

	Coordonnee coordonnees = Coordonnee(x, y);

	//On commence par v�rifier que l'on n'est pas sur une case au max de nos points
	switch(peuple){
	case MapLib::typeUnite::GAULOIS :
		if(getCase(coordonnees) == MapLib::typeCases::PLAINE)
			return suggestions;
		break;
	case MapLib::typeUnite::NAIN :
		if(getCase(coordonnees) == MapLib::typeCases::FORET)
			return suggestions;
		break;
	case MapLib::typeUnite::VIKING :
		if(bordEau(coordonnees))
			return suggestions;
		break;
	}

	//TODO : Coder la recherche de suggestions !
	//On parcoure les cases � proximit� de notre case
	for(int i=-precision; i<precision+1; i++){
		for(int j=-precision; j<precision+1; j++){
			Coordonnee testCoords = coordonnees.decaler(i,j);
			bool interesting = false;

			switch(peuple){
			case MapLib::typeUnite::GAULOIS :
				if(getCase(testCoords) == MapLib::typeCases::PLAINE)
					interesting = true;
				break;
			case MapLib::typeUnite::NAIN :
				if(getCase(testCoords) == MapLib::typeCases::FORET)
					interesting = true;
				break;
			case MapLib::typeUnite::VIKING :
				if(bordEau(testCoords) && getCase(testCoords) != MapLib::typeCases::VORTEX)
					interesting = true;
				break;
			}

			//Si la case est int�ressante et moins loin que les autres, on la prend !
			if(interesting){
				int testDistance = std::abs(i) + std::abs(j);
				for(int a=0; a<3; a++){
					int tempX = suggestions[a][0];
					int tempY = suggestions[a][1];
					int distance = std::abs(tempX - x) + std::abs(tempY - y);
					if(testDistance < distance){
						for(int b=2; b>a; b--){ //On d�cale les suggestions
							suggestions[b] = suggestions[b-1];
						}
						//On mets celle-ci � la place
						suggestions[a] = testCoords.toArray();
						break;
					}
				}
			} //end if(interesting)
		}
	}
	return suggestions;
}

int MapLib::getCase(Coordonnee coords){
	return carte[coords.getX()-1][coords.getY()-1];
}

bool MapLib::bordEau(Coordonnee coords){
	return ( (coords.getX() > 1 && getCase(coords.decaler(-1,0)) == MapLib::typeCases::EAU) ||
		(coords.getY() > 1 && getCase(coords.decaler(0,-1)) == MapLib::typeCases::EAU) ||
		(coords.getX() < tailleCarte && getCase(coords.decaler(1,0)) == MapLib::typeCases::EAU) ||
		(coords.getY() < tailleCarte && getCase(coords.decaler(0,1)) == MapLib::typeCases::EAU));
}

MapLib* MapLib_new() { return new MapLib(); }
void MapLib_delete(MapLib* maplib) { delete maplib; }

int** MapLib_genererMap(MapLib* maplib, int taille, std::string chaineAleatoire){ return maplib->genererMap(taille, chaineAleatoire); }
int** MapLib_posJoueurs(MapLib* maplib){ return maplib->posJoueurs(); }
int** MapLib_suggestions(MapLib* maplib, int peuple, int x, int y){ return maplib->suggestions(peuple, x, y); }