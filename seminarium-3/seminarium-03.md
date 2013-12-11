SEMINARIUM 03
=============

### Del 1 - projektidé ###

Min projektidé är inget revolutionerande det men det är iallafall inte en vädertjänst. Jag tänker mig en webbplats där man för det första kan se en lista på hashtags som är populära just nu och genom att antingen använda någon i listan eller genom att själv ange en term så ska applikationen visa inlägg från åtminstone Instagram och Twitter som är associerade till hashtagen. På det sättet blir det enkelt att hitta intressanta saker vid stora händelser. Jag tänker mig att man till exempel vid vinter-OS som snart är här så kan man väldigt snabbt hitta populära foton från Instagram och tweets från Twitter.

** Läs igenom dokumentationen till de API:er du använder. Vad är dina tankar om den? **

Om vi börjar med att gå igenom Twitters dokumentation så kan vi för det första fastslå att den är väldigt omfattande. Eftersom man kan få ut så mycket information i olika former (timelines, tweets, sökningar, strömmar, privata meddelanden, användare med mera) så finns det också väldigt mycket information. Man kan givetvis gå igenom allt men det känns irrelevant om man inte självklart är intresserad av alla punkter. För mig som egentligen bara är intresserad av att hämta ut tweets med en specifik hashtag så är bara ett fåtal sidor relevanta för mig.

Med det sagt så tycker jag också att deras dokumentation är väldigt bra formaterad och utförlig. Varje metods sida är uppbyggd på samma sätt som de andra. Det finns en beskrivning av vad metoden är till för och hur den fungerar, URL:en, följt av parametrar man kan använda och sedan ett exempelt. I och med att alla sidor ser likadana ut så är det lätt för mig som utvecklare att snabbt hitta till exemplet (exempel är trots allt enklaste eller åtminstone snabbaste sättet att att lära sig) och det är också väldigt bra att till och med varje parameter har en beskrivning för vad den används för.

Om vi sedan går vidare till Instagrams dokumentation så märker jag ganska så snabbt att den är något förvirrande. Vissa delar av sidan är på mitt valda språk, svenska, medan andra är på engelska. Det gör det till en början lite jobbigt att navigera. Det är självklart en ganska så värdslig sak men trots allt något jag reagerade på initialt.

Vidare så är även denna dokumentation väldigt bra. Den är utformad på ett enkelt sätt när man väl kommit över delen där vissa delar är översätta men andra inte och det är lätt att navigera för att hitta den information man behöver. För att till exempel hitta exempel på hur man hämtar ut inlägg baserad på en hashtag så var det bara två klick.

När det gäller exempel på Instagrams dokumentation så är dom faktiskt väldigt bra. Det är väldigt tydligt vilken URL som ska användas och varje parameter tillgänglig har en kort och koncis beskrivning och genom ett musklick så får man också se ett exempel response vilket alltid är bra. Det är också värt att notera att Instagram också har en konsol på sin webbplats där man kan testa API:et direkt utan att behöva skriva någon programmeringskod.

** Vilket/vilka dataformat 	kan dina valda API:er leverera? **

Twitter levererar sin data som XML, JSON, RSS eller ATOM.

Instagram levererar sitt som JSON.

** Finns det några specifika krav för att använda de API:er du valt? Kostnad, begräsningar etc **

Twitters API har en hel del databegräsningar. Det finns specifierat på deras dokumentation vilka exakta siffror vi pratar om och det är beroende på vilken metod man ska använda. Deras begränsningar är räknade på en 15-minuters cykel så om vi till exempel tar deras sökningar som är begränsad till 180 st per användare så betyder det att man får göra 180 st sökningar under en given 15-minuters period och sedan har man ytterligare 180 sökningar när de 15 minuterna har passerat.

Instagram har satt sin datagräns vid 5000 förfrågningar per timme men om man autentiserar sin användare så är det ganska så lätt att kringgå denna gräns.

När det gäller Instagram så är det också värt att notera att de inte vill att man ska cacha deras data medan Twitter å andra sidan uppmuntrar att man cachar tweets.

I övrigt så har båda tjänsterna regler på hur man ska använda deras tjänster och dessa är ganska så generella och återkommer i bådas Terms of Use. Dessa är till exempel att man inte ska manipulera en användares data utan tillstånd. Man ska tydligt visa vart datan kommer ifrån och man får generellt sätt inte försöka förvirra eller lura användaren.

** Vilka risker ser du med att bygga en tjänst kring de API:er du valt? **

I och med att de API:er jag valt är så pass stora och eftersom de används över hela världen så är risken för att deras tjänster ska ligga nere väldigt låga, men det är såklart alltid en risk.

När det kommer till uppdatering av deras API:er så har båda uppdateringsbloggar som man som utvecklare får se till att följa där de skriver om vad som kommer förändras och så vidare. Detta borde förhoppningsvis betyda att man aldrig behöver vakna en dag och se att ens applikation inte längre fungerar utan det borde vara lätt att se till så att även sin egen applikation hänger med i uppdateringarna.

Om jag tänker lite mer specifikt så kan det eventuellt bli problem med Twitters sökning. Eftersom jag inte provat detta än så är jag inte helt säker på hur det kommer bli med bara 180 förfrågningar per 15 minuter, men det ska finnas sätt att komma runt detta genom att cacha och även eventuellt använda strömmar, men detta är något jag måste ta i itu med när jag väl kommit in i utvecklingsfasen.

### Del 2 - fallstudie - exempel på en bra befintlig mashup-applikation ###

För detta exempel så har jag valt mashup-applikationen dotabuff. Det är en webbapplikation där man kan framställa statistik och information angående spelare och lag i spelet dota2.

URL: http://dotabuff.com/

exempel: http://dotabuff.com/players/64881270 (en väns profil).

** Varför är denna applikation ett bra exempel på mashup-applikation? **

Jag tycker att detta är ett bra exempel för att applikationen har egentligen ett till synes enkelt syfte, att visa statistik från spelet dota2, men det sker så mycket i bakgrunden och sättet som statistiken presenteras på är också väldigt tilltalande. Genom att hämta in information från flera olika källor så visas allting på ett väldigt prydligt sätt.

** På vilket sätt kombineras datakällorna och vilken ny effekt får de tillsammans? **

Deras mashup-applikation består i grunden av en förfrågning till spelutvecklarens officiella API där man kan kolla upp till exempel en spelare. Sedan så antar jag att applikationen får tillbaka JSON med olika information och sedan så hämtar den bland annat in information om en spelare från Steams API, matchinformation från utvecklaren och så vidare.

Resultatet av allt detta är en visuellt tilltalande sida med tydlig statistik om spelaren och spelarens senaste matcher och mycket, mycket mer.