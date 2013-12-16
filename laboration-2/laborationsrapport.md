Laborationsrapport
==================
*Rickard Karlsson - rk222cu*
***

## Optimering

### Externa css-filer
* Observeration - före: Laddningstid: ~2.53s, antal anrop: 20
* Observation - efter: laddningstid: ~518ms, antal anrop: 17

**Referens:** kurslitteraturen - *Rule 1: Make Fewer HTTP Requests*

HTTP anrop är, enligt kurslitteraturen, det som bidrar mest till en lång laddningstid.

Applikationen laddar in CSS från både Google och LNU som används på sidan och genom att spara ner den lokalt i en enda fil så sker det 1 anrop till vår lokala server istället för 2st till Google och 2st till LNU.

Laddningstiden sänktes med cirka 2 sekunder mest på grund av att anrop till LNU:s server är betydligt långsammare än till samma server som applikationen körs på.

### Stilmallar i toppen
* Laddningstid - före: ~508ms
* Laddningstid - efter: ~458ms

**Referens:** kurslitteraturen - *Rule 5: Put Stylesheets at the Top*

Genom att ha alla stilregler och stilmallar i toppen av en sida så kan en sida laddas progressivt. En del webbläsare blockerar utritning av element om det finns stilregler i slutet av dokumentet för att slippa rita om.

Jag flyttade upp den CSS som fanns i slutet av dokumentet för att åtgärda detta och jag fick också en väldigt liten vinst i laddningstid.

### Script i botten
* Laddningstid - före: ~446ms
* Laddningstid - efter: ~498ms

**Referens:** kurslitteraturen - *Rule 6: Put Scripts at the Bottom*

När en webbläsare gör anrop för att hämta in resurser så sker dessa parallellt med max (som standard) 2st per hostname. Problemet är dock att när webbläsaren ska ladda in skript så stoppas denna parallella inladdning och webbläsaren gör inga fler anrop innan skriptet har laddats in helt.

Denna applikation laddade in vissa skript i toppen och efter jag flyttade dem så fick jag faktiskt en något högre laddningstid. Jag tror det beror på att skripten i sig används för att göra AJAX-anrop och fylla på DOM:en och istället för att det sker i mitten av dokumentet innan allting har ritats ut så sker det i slutet och då sker det en omritning. Det är dock inget jag är helt säker på.

Trots den lite högre laddningstiden så valde jag ändå att implementera denna regel eftersom det trots allt är anses vara good practice och ökningen i laddningstiden är också så pass liten att den inte borde räknas.

### Bryta ut inline skript och stilregler
* Observation - före: laddningstid: ~530ms, antal anrop: 17
* Observation - efter: laddningstid: ~515ms, antal anrop: 18

**Referens:** kurslitteraturen - *Rule 8: Make JavaScript and CSS External*

I teorin är det egentligen snabbare att ha skript och stilmallar inline eftersom man slipper extra anrop men i den riktiga världen så kommer skriptfiler att cachas i användarens webbläsare vilket kommer minska datan som sker vid anrop efter det första.

Jag gjorde mina tester med cachning avslaget och jag fick ändå lägre laddningstid trots ett extra anrop. Detta tror jag beror på att jag kunde minska koden i den externa filen.

### Minimera JavaScript
* Laddningstid - före: ~540ms
* Laddningstid - efter: ~503ms

**Referens:** kurslitteraturen - *Rule 10: Minify JavaScript*

Genom att minimera JavaScript-filer så minskas filstorleken och det i sig sänker laddningstiden. I just denna applikation kunde man sänka filstorleken på de JavaScript-filerna som används med drygt 50% genom att köra dem igenom en minifier.

### Felaktiga skriptinladdningar
* Observation - före: laddningstid: ~502ms, antal anrop: 18
* Observation - efter: laddningstid: ~360ms, antal anrop: 14

**Referens:** kurslitteraturen - *Rule 1: Make Fewer HTTP Requests*

I denna applikation så skedde det 4st anrop till två JavaScript-filer som inte finns. Detta bidrog till en ökad laddningstid på nästan 200ms vilket ändå måste ses som en stor förlust i prestanda. Genom att ta bort dessa ur dokumentet så fick jag ner laddningstiden.

Teorin bakom detta är precis som i första punkten. Webbläsaren måste göra färre anrop och kan därför rita ut och ladda in snabbare.

### Reducera omdirigering
* Antal anrop - före: 2
* Antal anrop - efter: 1

**Referens:** kurslitteratur - *Rule 11: Avoid Redirects*

Omdirigeringar leder till en sämre prestanda och genom att ta bort en *"mellanhand"* som fanns i inloggningsskriptet så kunde jag minska laddningstiden och sänka antal anrop med 1.

### Reducera antal AJAX-anrop vid hämtning av meddelanden
* Antal anrop - före: 1...* (beroende på hur många meddelanden som fanns).
* Antal anrop - efter: 1

**Referens:** kurslitteratur - *Rule 1: Make Fewer HTTP Requests*

Applikationen hämtade ut meddelanden till varje producent genom att först hämta ut ID:s tillhörande meddelanden som var associerade med producenten och sedan göra enskilda AJAX-anrop för att hämta innehållet för varje meddelande. Jag fixade detta genom att bara göra ett anrop och få tillbaka alla meddelanden som finns direkt.

## Säkerhetsproblem

### Sparade användaruppgifter
Applikationen sparade sina användaruppgifter i en databas med lösenordet i klartext. Det betyder att om databasen skulle läcka ut på något sätt så skulle vem som helst kunna sno de konton som finns sparade.

Om användarkonton läcker ut på det sättet så kan det naturligtvis också betyda att de drabbade riskerar att få t.ex. sin e-post eller facebookkonto kapat också eftersom det är väldigt många som använder samma lösenord på flera sajter.

Jag åtgärdade problemet genom att använda PHP:s inbyggda API för lösenordhantering (*password_hash()* och *password_verify()*) och genom att spara en hashad och saltad version av lösenordet i databasen.

### Utloggning
Applikationen gjorde ingen riktig utloggning utan den omdirigerade enbart till inloggningssidan. Detta ledde till att det visserligen såg ut som om man blev utloggad men med bara ett klick på Bakåt-knappen så var man tillbaka på den sida som krävde autentisiering utan att ha loggat in egentligen.

Utan att genomföra en riktig utloggning så innebär detta att icke behöriga personer lätt kan ta sig in på sidan efter att en behörig person loggat in, loggat ut och sedan lämnat datorn.

Jag fixade till detta genom att använda den funktionalitet som fanns tillgänglig (metoden *logout()*). Det den gör är att rensa sessionen vilket loggar ut användaren.

### Sessionsstöld
Vid inloggning sparas inte data som skulle kunna användas till att verifiera att det är samma användare som loggade in som den som använder applikationen just nu. Detta leder till att vid en osäker uppkoppling så skulle någon kunna snappa upp sessions id:et som användaren har och kapa personens session och därmed vara inloggad som den personen. Detta kan leda till oanade och olyckliga situationer.

Jag åtgärdade detta genom att se till att spara användarens UserAgent och användarens IP-adress och vid *CheckUser()* jämför jag den sparade datan med den nuvarande användarens UserAgent och IP-adress. Detta är naturligtvis inte en 100% säker lösning på problemet utan för det behöver man köra applikationen över HTTPS men det är ett steg i rätt riktning.

### Oparametriserad och osäkra strängar vid insert
När en användare lägger upp ett meddelande så sker ingen som helst kontroll av vad det är som skjuts in i databasen så applikationen är helt öppen för SQL injection attacker.

Detta kan leda till att någon illasinnad programmerare kan ta bort hela vår databas eller manipulera den på ett oönskvärt sätt. Det är också ett sätt att få ut all sparad data i den. Eftersom applikationen använder två olika SQLite-filer för användare och för meddelanden så är det ingen risk för att användaruppgifter skulle läcka ut men det hade kunnat vara en risk om det inte vore så.

Jag åtgärdade det genom att parametrisera SQL-frågan som sköter detta och genom att *escapea* (i brist på ett bra svenskt ord) strängarna.

### Otillåtna meddelanden
Genom att kalla på applikationen på följande sätt:
<pre>hostname/Webteknik II/Laboration 2/application/functions.php?function=add&name=ola&message=kommentar&pid=41</pre>
så kunde man lägga till meddelanden utan att vara inloggad och utan att frågan ens kommer från rätt server. Detta kan leda till spam och oväntat resultat.

Jag åtgärdade det genom att säkerhetsställa att den som gör anropet för att lägga till ett meddelande är inloggad.

## AJAX
Jag fixade till dennna funktionalitet genom att bryta ut funktionen som ritar ut meddelanden så jag kan återanvända den. Det finns i filen *mess.js* i mappen *js* på rad 97.

I AJAX-anropet som sker när en användare sedan skickar ett meddelande så hämtar jag sedan in alla meddelanden associerade med den producenten igen och ritar ut dessa med funktionen som jag nämnde innan.






