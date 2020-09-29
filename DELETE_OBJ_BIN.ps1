# Consignes d'utilisation de ce script :
#
# - Il faut lancer ce script dans un répertoire qui contient à la racine une solution Visual Studio (.sln)
#
# - Il est préférable que Visual Studio soit fermé pendant que le script se éxécute
#
# - Il est préférable que on aie fait un "Get Latest" des fichiers concernées par cette 
#   solution avant de lancer le script
#
# - Il faut s'assurer que toutes les variables d'environnement systèmes nécessaires soient 
#   présentes pour pouvoir éxécuter les commandes. À savoir :
#    - Team Explorer 
#      (C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer)
#
#    - NuGet  
#      (.exe à télécharger et mettre, par exemple, dans "C:\path\nuget.exe". Télécharger ici : https://www.nuget.org/downloads (la version stable recommandée))
#
#    - MSBuild [Facultatif, pas utilisé car ligne commentée] 
#      (C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\)
#
# Le script peut résoudre des problèmes avec des solutions Xamarin, notamment les erreurs 
# à la compilation liées au 'Linker' ou aux fichiers '.target'.

$folderPath = '.\'
$value = $false
#$tfPath = "$/Trade NG/Main"

# On parcourt tous les fichiers, et on force le drapeau "IsReadOnly" à faux
"Setting  ReadOnly property to false"
Get-ChildItem -Path $folderPath -Include bin,obj -Recurse | ForEach-Object ($_) {
    $item = Get-Item -Path $_.FullName
    if($item -isnot [System.IO.DirectoryInfo])
    {
        # Message d'information
        #"Setting " + $item.Name + " ReadOnly property to false"

        # On set la propriété lecture seule  à faux a fin de pouvoir effacer les fichiers qu'on veut
        $item.IsReadOnly = $value


    }
}

# On re-parcourt tous les fichiers, on inclut seulement ceux qui sont nommées "bin" ou "obj", on efface tous ces items (dossiers) de façon récursive
Get-ChildItem -Path $folderPath -Include bin,obj -Recurse | ForEach-Object ($_) { 
    if(!$_.FullName.Contains("References\bin"))
    {
        Remove-Item $_.FullName -Force -Recurse
        $_.FullName + " deleted"
    }
}

# On parcourt le répertoire où le script à été lancé, et pour chaque fichier ".sln" trouvé, on fait la suite d'actions suivante...
Get-ChildItem -Path $folderPath | Where-Object { $_.Extension -eq ".sln"} | ForEach-Object ($_) {

    # Message d'information
    #"Getting Latest for " + $_.FullName

    
    # Get Latest pour corriger des éventuels fichiers qui devaient pas être supprimés des bin/obj
    #tf.exe get $tfPath /recursive

    # Message d'information
    "Nuget Restoring " + $_.FullName

    # On restaure les paquets nuget de la solution pour que tout soit propre quand on réouvre la solution
    .\nuget.exe restore $_.FullName;


    # Message d'information
    #"Building " + $_.FullName

    # On build la solution (pas obligatoire)
    # MsBuild $_.FullName -p:Configuration=Debug 

}

