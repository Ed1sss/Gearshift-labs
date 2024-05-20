# Projekto naudojimas
## Žaidimo paleidimas
### Executable parsisiuntimas
 ### 1. Parsisiuskite žaidimo failus
Nuekite į https://github.com/Ed1sss/Gearshift-labs/tree/game-prototype/Unity/Game_Final ir parsisiuskite sukompiliuotus žaidimo failus.
 ### 2. Išarchivuokite parsisiustus failus į atskira aplanką
 ### 3. Paleiskite žaidimą
 Nueje į aplanką kuriame išarchivavote sukompiliuotus žaidimo failus, paleiskite programą pavadinimų `Gearshift Labs.exe`
 ### 4. Žaiskite

### Žaidimo paleidimas per UNITY
### 1. Nusiklonuokite žaidimo projekto failus
Nueikite į direktoriją kurioje norite parsisiuti žaidimo failus ir naudodami komandą `git clone https://github.com/Ed1sss/Gearshift-labs.git` parsisiuskite juos.
### 2. Jei dar neturite UNITY atsisiuskite
Jei neturite UNITY ją galite atsisiuti iš https://unity.com/download. Instrukcijos UNITY instaliacijai: https://support.unity.com/hc/en-us/articles/205637449-How-do-I-download-Unity
### 3. Atsidarykite UNITY ir importuokite projektą
Paspauskite mygtuką add ir tada pasirinkite add project from disk ir nuekite į direktoriją kurioje parsisiuntete žaidimo failus
![](https://i.ibb.co/PYBGycr/Screenshot-2024-05-18-183629.png)
### 4. Atsidarykite žaidimo projektą
Importavus projektą projektų sąraše atsiras mūsų žaidimas, paspaudus ant jo jis bus atidarytas
![](https://i.ibb.co/DYJhtCJ/Screenshot-2024-05-18-183829.png)
## Interpretatoriaus naudojimas
Norint naudoti interpretatoriu pirmiausia kompiuteryje reikės įsidiegti, python, vjoy, x360ce emulator.
### Python diegimas
Parsisiusti python galima iš: https://www.python.org/downloads/
### Vjoy diegimas
Parsisiuti Vjoy galima iš: https://sourceforge.net/projects/vjoystick/
### X360ce diegimas
Parsisiusti x360ce galima iš: https://www.x360ce.com/
### Interpretaroriaus naudojimos instrukcijos
### 1. Atsisiuskite interpretatoriaus scriptą
Interpretatoriaus scriptą galite rasti čia: https://github.com/Ed1sss/Gearshift-labs/blob/main/interpreter/interpreter.py
### 2. Prijunkite arduino prie kompiuterio
Antras žingsnis yra prisijungti arduino prie kompiuterio. Arduino reikiamas valdymo reikšmes turi išvesti per serial portą tokiu formatu `Akseleratorius stabdis vairas`
Pavyzdys kaip turi atrodyti arduino išvedami duomenys: `80 20 100`
### 3. Paleiskite interpretatoriaus scriptą
Interpretatoriaus scriptą galima pasileisti per komandine eilute. Pirmiausia reikia nueiti į direktoriją kurioje yra išsaugots scriptas. Tai galima padaryti naudojant komandą `cd /direktorija`. Norint paleisti scriptą reikia įvykdyti komdanda `python ./interpreter.py`. Paleidus bus paprašyta nurodyti kurį serijinį portą naudoja arduino. Tai galite surasti atsidare device manager ir ports skiltyje susirade arduino

![](https://www.mathworks.com/help/matlab/supportpkg/win_dev_mngr_port.png)

Pateiktame pavyzdyje reikėtų įvesti COM3
### 4. Paleidus interpretatorių pasirinkite interpretatoriaus režimą
Paleidus interpretatoriu ir pasirinkus serial portą gausite tris pasirinkimus

 1. Kalibracijos režimas (Calibration mode)
 2. Vairavimo režimas (Drive mode)
 3. Išeiti (Quit)
 
 Į komandine eilute įvedus pasirinkimo skaičių nueisite į atitinkamą meniu. Kalibracijos režime galite susikalibruoti savo įvestis, nuejus į ji ekrane bus išvestos instrukcijos kaip sukalibruoti įvestis.
 Vairavimo režime įvestis bus konvertuojamos į valdiklio įvestis ir galėsite žaisti žaidimus
 Galiausiai pasirinkus išeiti interpretatorius baigs savo darbą.
### 5. Susikonfiguokite x360ce emuliatorių

Pasileiskite x360ce emuliatorių, tai padare išvysite tokį vaizdą:


![](https://i.ibb.co/SDTD9Nk/Screenshot-2024-05-18-191840.png)

Priklausomai nuo žaidžiamo žaidimo įvesčių reikia jas susikonfiguruoti. Tai galima padaryti paspaudus ant norimo konfiguruoti valdiklio mygtuko. Tai padarius reikės paspausti atitinkamą įvestį pvz. stabdžio pedalą kuria norime pririšti prie to mygtuko. Tai padarius programa automatiškai tai aptiks ir visas stabžio pedalo įvestis perduos kaip to mygtuko nuspaudimą ar panašiai. Mūsų žaidimui greičio pedalo įvestis turi būti pririšta prie Right bumper, stabdžio pedalas prie Left bumper, vairo įvestys prie stick axis x, pavaros padidinimas prie Y, mažinimas prie X mygtukų 
Taigi galiausiai beliks pasileisti žaidimą palaikantį valdiklių įvestis ir jį žaisti


