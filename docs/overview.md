# Vue d'ensemble

## Le scénario

Version courte qui met l'accent sur les interactions du jeu, pour la version longue, se référer au rapport `G1 - Time Travel 2019.pdf` section 2 "Présentation de l'application"

Le joueur est d'abord dans la temporalité années '80.

* Son soi futur l'invite à venir voir les dégâts de la pollution (**TODO**):  il joue la cassette avec le walkman pour se téléporter dans le futur.

* Il joue une musique western sur le synthétiseur, qui fait apparaître une cassette.
On utilise cette cassette pour se téléporter dans la temporalité Western.

* Le joueur détruit un mur fissuré avec de la dynamite. Il trouve la clé du coffre-fort. Il retourne dans le futur pour trouver la cassette années '80.

* Il place la cassette non-rembobinée dans le tiroir inter-dimensionnel, et retourne dans le western. Il la rembobine avec un crayon (**TODO**), place la clé dans le tiroir et se téléporte dans les années '80.

  **NB**: L'énigme du rembobinage n'a pas été faite par manque de temps.

* Il récupère la clé, ouvre le coffre, prend la graine et la plante (**TODO**).

  **NB**: Script OuvrirCoffre (normalement) créé, mais pas ajouté à la scène. Vérifier qu'il existe dans l'autre version du projet.

* Il peut aller dans le futur voir des arbres.

  **NB**: scène créée mais transition non implémentée

## La scène

Le jeu se déroule dans 3 époques différentes: Western, années '80 et années 2050 futuriste.

Dans la scène, ces trois environnements sont représentés par des préfabs: _Western_ et _Point Light_ constituent l'espace Western, _80s_large_pour les années '80 et _2050_ … pour le temps 2050

_Sun_Lamp_ s'applique partout puisque c'est une lumière directionnelle.
_[CameraRig]_ et _[SteamVR]_ proviennent de SteamVR.

Enfin on a le _walkman_ qui sera téléporté avec le joueur.

Le choix du design est de faire tout à la main dans l'esprit de la game jam.

## Notions sur les scripts

### Les trois époques

**IDEA**: séparer ces trois époque dans des scènes différentes pour faciliter le développement. À creuser.

**NB**: Le choix d'avoir les trois lieux dans la scène permet de téléporter facilement par translation.

Les différentes époques sont modélisées et gérées par la classe `ZoneManager`. Il est attaché au walkman.
On y définit la classe Zone ainsi:

```cs
enum Zone { A, B, C }
```

`Zone.A` correspond au Western, `Zone.B` aux années '80, et `Zone.C` au futur.

Ce script expose les fonctions `GoToX` qui permettent de téléporter le joueur et certains objets dans les différentes temporalités.

### Commentaires

Les commentaires de la forme `// 2 -` ont été ajoutés par le deuxième groupe de projet (2020). À cause du coronavirus, nous n'avons pas eu le temps de faire grand chose.

## Comportements et fonctionalités

Dans cette section, on décrit le comportement des scripts.

### Joueur
#### Déplacements

On utilise la téléportation pour se déplacer. Elle est implémentée dans LaserPointer. On utilise un cube aplati pour le laser.

**IDEA**: Utiliser un arc au lieu d'une ligne droite pour sélectionner l'endroit où se téléporter car cela permet d'avoir plus de précision lorsqu'on essaye de se déplacer loin. Si on est en ligne droite on a un petit angle de travail. Avec un arc, on peut compenser. cf. Le déplacement dans les démos SteamVR.

#### Grab

Implémenté dans ControllerGrabObject, on attache l'objet au controlleur sur l'appui d'un bouton.

On utilise une boite de collision devant le contrôleur qui est "trigger" lorsqu'il entre en collision avec un rigidbody. On attache alors l'objet à notre contrôleur.

### Mécaniques du jeu

#### Walkman et cassettes

Le walkman entre en collision avec la cassette. Celle-ci entre dans le walkman, et la musique est jouée. On ne peut pas la jouer deux fois de suite. La première fois qu'on essaye de jouer la cassette, on est téléporté devant le tiroir. Sans doute pour que la téléportation se passe bien ?

Les fonction ZoneManager.GoToX sont appelées par l'évènement onMusicChange de chaque cassette.

**IDEA**: Pour le moment, c'est la cassette qui joue la musique. Un essai a été commencé de déplacer la musique dans la classe walkman. Je propose qu'on laisse la cassette avoir sa musique, et que le walkman récupère le fichier de cette musique pour la jouer.

**IDEA**: Pour le moment, on ne se retéléporte pas dans le même monde car on empêche de jouer la même cassette deux fois de suite. On pourrait plutot vérifier dans les functions ZoneManager que on va dans une temporalité différente.

**IDEA**: On peut simplifier ZoneManager.GoToX par une fonction ZoneManager.GoTo(Zone z) et choisir la bonne temporalité dans l'inspecteur Unity. 

**IDEA**: Déplacer le code de téléportation dans classe walkman et de laisser en attribut de la cassette, la temporalité de destination. Cela permet de simplifier les états, et d'avoir tout dans le code et pas sur les objets.

**NB**: Le walkman contient le code pour l'easter egg Buttlicker.

**TODO**: Fadeout lorsqu'on utilise une cassette

**TODO?**: Bruit de cassette abîmé pour les mauvaises.

#### Synthétiseur

Lorsqu'on touche le synthé pour la première fois, il joue la première note de la musique et démarre un timer. Durant ce temps imparti, il faut finir la mélodie en posant ses mains sur le piano pour que la cassette apparaisse.

**TODO**: Implémenter la mise en pause de la musique lorsque le joueur ne touche pas le piano. Pour le moment, la musique se joue d'elle même et la cassette apparaît dans tous les cas.

#### Destruction du mur avec de la dynamite

Le script Dynamite prend en charge l'animation et la réapparition de la dynamite. Il déclenche aussi la fracturation du mur. La mèche commence à brûler lorsqu'on attrape la dynamite.
La fracturation du mur est implémentée dans Fracturable. Il va ajouter une force sur les morceaux, et détruire le tout dès qu'il touche le sol.

**NB**: S'assurer que la fonction Grab de la Dynamite est appelée pour déclencher la mèche.

**IDEA**: La condition de destruction de l'objet est bizarre: parent (abstrait) qui touche le sol.

**TODO**: Les scripts ne sont pas encore réattachés à la dynamite et au mur qui doit se casser, respectivement.

**TODO**: S'assurer que l'objet garde sa vitesse lorsqu'on le lance, sinon il va tomber droit par terre.

#### Tiroir inter-dimensionnel

Le script tiroir expose une fonction qui permet de récupérer ses enfants pour les déplacer lorsqu'on change d'époque.

**NB**: Je n'arrive pas à trouver où cette fonction est utilisée. (**TODO**)

**TODO**: Il manquerait alors une méthode pour placer des objets dans le tiroir ?

Le script 80s/Drawer_constraint restreint la position du tiroir selon l'axe X jusqu'à une position d'arrêt. Il est uniquement appliqué sur le tiroir des années '80.

**IDEA**: On peut sans doute le généraliser pour appliquer les contraintes sur les autres tiroirs. Une autre option est d'utiliser un joint personnalisé: <https://gamedev.stackexchange.com/questions/129659/how-do-i-configure-a-joint-for-a-sliding-door-in-unity>

#### Rembobinage au crayon

**TODO**: Faire le crayon, le mettre en valeur. L’interaction pour rembobiner une cassette.

#### Ouverture du coffre

**TODO**: À faire, inspiré du script OuvrirCoffre (qui existe quelque part)

#### Planter la graine

**TODO**: Peut-être une animation, mais au moins un changement si on fait rentrer la graine dans le pot.

### Autres choses

**TODO?**: Faire le dialogue d'introduction (cf. scénario) ainsi que l'avatar du soi futur.

**TODO?**: Écran de fin de jeu.

---

## Fichiers de test qui peuvent être ignorés (et supprimés)

* ViveControllerInputTest
* grabou

Pour les codes mis en commentaire, et les commentaires, à vous de juger.