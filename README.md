# SimpleDockerRegistryUI
Implementació de una visualització simple de les imatges publicades en un registre de imatges Docker privat.

# Que és un Docker Registry?
Un registre de docker, ve a ser un magatzem de imatges de 'solucions' a punt per ser desplegades en contenidors Docker.
N'hi ha de publics, de privats, gratuits, de pagament, disponibles al núvol. També en podem desplegar un localment o en el nostre CPD.
Per fer-ho, podem seguir el següent enllaç https://docs.docker.com/registry/

# Descripció del projecte
Aquest projecte és una prova de concepte per aprofitar l' api del registry, per visualitzar mitjançant una plana html, les imatges i versions d'aquestes que té.

A tenir en compte: en el fitxer **appsettings.json** hi ha dos paràmetres de configuració, la url del registre, i les credencials.
Si mireu la documentació, veureu que es pot securitzar l'accés de diferents maneres, una de les quals és Auth Basic. Si securitzem
l'accés a la api, en la configuració especifiquem les credencials.


