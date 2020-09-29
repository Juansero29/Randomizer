# Consignes d'utilisation de ce script :
#
# - Il faut lancer ce script dans un r�pertoire qui contient � la racine une solution Visual Studio (.sln)
#
# - Il est pr�f�rable que Visual Studio soit ferm� pendant que le script se �x�cute
#
# - Il est pr�f�rable que on aie fait un "Get Latest" des fichiers concern�es par cette 
#   solution avant de lancer le script
#
# - Il faut s'assurer que toutes les variables d'environnement syst�mes n�cessaires soient 
#   pr�sentes pour pouvoir �x�cuter les commandes. � savoir :
#    - Team Explorer 
#      (C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer)
#
#    - NuGet  
#      (.exe � t�l�charger et mettre, par exemple, dans "C:\path\nuget.exe". T�l�charger ici : https://www.nuget.org/downloads (la version stable recommand�e))
#
#    - MSBuild [Facultatif, pas utilis� car ligne comment�e] 
#      (C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\)
#
# Le script peut r�soudre des probl�mes avec des solutions Xamarin, notamment les erreurs 
# � la compilation li�es au 'Linker' ou aux fichiers '.target'.

$folderPath = '.\'
$value = $false
#$tfPath = "$/Trade NG/Main"

# On parcourt tous les fichiers, et on force le drapeau "IsReadOnly" � faux
"Setting  ReadOnly property to false"
Get-ChildItem -Path $folderPath -Include bin,obj -Recurse | ForEach-Object ($_) {
    $item = Get-Item -Path $_.FullName
    if($item -isnot [System.IO.DirectoryInfo])
    {
        # Message d'information
        #"Setting " + $item.Name + " ReadOnly property to false"

        # On set la propri�t� lecture seule  � faux a fin de pouvoir effacer les fichiers qu'on veut
        $item.IsReadOnly = $value


    }
}

# On re-parcourt tous les fichiers, on inclut seulement ceux qui sont nomm�es "bin" ou "obj", on efface tous ces items (dossiers) de fa�on r�cursive
Get-ChildItem -Path $folderPath -Include bin,obj -Recurse | ForEach-Object ($_) { 
    if(!$_.FullName.Contains("References\bin"))
    {
        Remove-Item $_.FullName -Force -Recurse
        $_.FullName + " deleted"
    }
}

# On parcourt le r�pertoire o� le script � �t� lanc�, et pour chaque fichier ".sln" trouv�, on fait la suite d'actions suivante...
Get-ChildItem -Path $folderPath | Where-Object { $_.Extension -eq ".sln"} | ForEach-Object ($_) {

    # Message d'information
    #"Getting Latest for " + $_.FullName

    
    # Get Latest pour corriger des �ventuels fichiers qui devaient pas �tre supprim�s des bin/obj
    #tf.exe get $tfPath /recursive

    # Message d'information
    "Nuget Restoring " + $_.FullName

    # On restaure les paquets nuget de la solution pour que tout soit propre quand on r�ouvre la solution
    .\nuget.exe restore $_.FullName;


    # Message d'information
    #"Building " + $_.FullName

    # On build la solution (pas obligatoire)
    # MsBuild $_.FullName -p:Configuration=Debug 

}

