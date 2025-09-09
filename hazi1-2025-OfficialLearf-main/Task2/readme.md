# 2. Feladat megoldásának bemutatása
Mivel különböző típusu objektumokat kell tárolunk ezért egy heterogén kollekciót kell elkészíteni plusz az egyik osztálynak van már ősosztálya, ezért ehhez kelleni fog egy interfész és egy absztrakt osztály.
Először felvettem az osztályokat, az alakzatok a Circle,Square és TextArea. Ezekhez megcsináltam egy interface-t IShapes néven amiben az alap metódusok vannak
mint a terület és típus lekérdezése és az X,Y koordináta. Létrehoztam egy ShapeBase absztrakt osztályt is amiben az alakzatok által megvalósítandó funkciókat neveztem meg. Ez az absztrakt osztály az interfaceből származik és az alakzatok pedig az absztrakt osztályból, kivéve a TextArea, ő a hozzáadott TextBox osztályból és az interfaceből egyaránt.

A ShapeInventory osztály felelős az adatok tárolásáért és a konzolra való listázásért. Egy listában tárolom az elemeket amik IShapes (interface) típusuak, ez a heterogén kollekcióm. Az osztály ListShapes funkciója kilistázza az alakzatok típusát,koordinátáját és területét.
TODO