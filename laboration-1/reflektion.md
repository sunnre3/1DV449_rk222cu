# Laboration 1 - Reflektion #

** Ni är fria att välja sätt och läsa in och extrahera data ur webbsidorna. Motivera ditt val! **

Jag valde att använda mig av PHP och cURL för att koppla upp mig till datakällan och hämta ner HTML-koden. PHP är det serverspråk jag känner mig mest bekväm med så det var en självklarhet. CURL däremot har jag inte tidigare använt men det visade sig vara väldigt enkelt och lätt att komma igång med. För att sedan behandla källkoden som jag fick ut för att hämta ut den informationen jag var ute efter så använde jag xpath som jag använt tidigare när jag webbskrapat. Det är absolut ingenting jag är speciellt bra på men jag vet hur det fungerar så jag stötte inte på några egentliga problem.

** Vad finns det för risker med applikationer som innefattar automatisk skrapning av webbsidor? **

En applikation som är beroende av webbskrapning är för det första väldigt ömtålig. Källan som applikationen skrapar ifrån kommer med största sannolikhet att ändra sin design, layout eller markup vid något tillfälle och det självklart ske utan att vi som skrivit applikationen vet om det. En jätteliten förändring i källan kan förstöra en hel applikation. Risken för det är mycket mindre om man arbetar mot ett välkänt och stort API.

Om man sedan tänker sig att applikationen skrapar källan vid varje anrop automatiskt utan att spara datan i någon slags lagring så uppstår problemet med att applikationen inte kommer kunna köras om källan ligger nere eller i värsta fall till och med blockerat applikationen. Det är inte alls helt otänkbart att en sida vid något tillfälle kommer ha problem. Till och med Google har gått ner.

Applikationer som skrapar löper också en risk att få juridiska problem. Som skrapare gäller det att man är säker på att sidan man vill skrapa ifrån tillåter det.

** Tänk dig att du skulle skrapa en sida gjord i ASP.NET WebForms. Vad för extra problem skulle man kunna få då? **

Webbsidor som är skapade med WebForms har väldigt ofta dolda fält som innehåller "tokens" som håller koll på användaren och ser till att rätt innehåll visas för rätt användare. När man skrapar kan det bli ett problem. Ett sätt att komma förbi detta problem är att använda online proxy-servrar.

** Har du gjort något med din kod för att vara "en god webbskrapare" med avseende på hur du skrapar sidan? **

Jag har sett till att jag inte skrapar automatiskt. Även om databasen är tom och inte innehåller någon data så kommer inte min applikation att skrapa från källan utan att en användare väljer att uppdatera. Detta är naturligtvis inte perfekt men det skär ändå ner på antal anrop till källan. Det största problemet med att göra så är att det är användaren som bestämmer när ett anrop ska ske och utan att ha några restriktioner i antal anrop som det är i min applikation så skulle det potentiellt bli många. Speciellt om man tänker sig att man får ett skript att utföra operationen istället för en människa.

 ** Vad har du lärt dig av uppgiften? **

 Jag har lärt mig lite kort om SQLite och cURL. Resten av uppgiften var saker jag redan kunde.