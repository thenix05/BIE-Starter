# Antworten zum Arbeitsauftrag M450

## Testebenen einordnen

- Eine Person liest den Code: statischer Review / Code Review.
- Eine Methode wird einzeln geprüft: Unit-Test.
- API und Datenbank arbeiten zusammen: Integrationstest.
- Ein Browser bedient die ganze App: End-to-End-Test (E2E).

## Aufgabe 2: Warum ist 30 aussagekräftiger als 10?

30 ist der Grenzwert aus R3. Ein Test mit genau 30 Tagen entdeckt einen Fehler an der Grenze, zum Beispiel `>= 30` statt `> 30`. Bei 10 Tagen wären beide Bedingungen genehmigt; der Fehler bliebe unentdeckt.

## Aufgabe 4: Ergebnisse auswerten

Der Integrationstest kann zusätzlich Fehler bei HTTP-Status und API-Routen, beim Serialisieren und Deserialisieren sowie beim Speichern und erneuten Auslesen aus SQLite entdecken. Die Unit-Tests rufen nur die Fachlogik auf.

Keiner der beiden Tests bedient das Formular im Browser. Sie prüfen daher nicht, ob Eingaben und Klicks funktionieren, die Entscheidung sichtbar erscheint oder der gespeicherte Fall nach dem Neuladen in der Liste dargestellt wird. Das prüft der E2E-Test.

## Aufgabe 5: Selenium-Test

Nach dem Absenden wird geprüft, dass die Entscheidung sichtbar als «GENEHMIGT» erscheint. Anschliessend wird die Seite neu geladen und derselbe Fall anhand seiner ID im Register gesucht. So wird geprüft, dass er tatsächlich gespeichert und erneut geladen wurde.

Auf einen Zustand zu warten ist zuverlässiger als eine feste Pause: Der Test läuft weiter, sobald das Ergebnis verfügbar ist. Eine feste Pause wäre entweder unnötig lang oder könnte zu kurz sein.

## Aufgabe 6: Reviewbefund

- **Fundstelle:** `src/BIE.Core/EntryPolicy.cs`, Tourismus-Grenze.
- **Beobachtung:** Mit `Days >= 30` wurde auch ein Aufenthalt von genau 30 Tagen abgelehnt.
- **Anforderungsbezug:** R3 erlaubt Tourismus bis einschliesslich 30 Tage.
- **Verbesserung:** Die Bedingung muss `Days > 30` lauten. Die Tests für 29, 30 und 31 Tage sichern die Grenze ab.

## Abschlussfragen

1. **Kann ein Unit-Test ein Regressionstest sein?** Ja. Ein Unit-Test wird zum Regressionstest, wenn er einen früheren Fehler festhält und bei einer späteren Änderung sicherstellt, dass derselbe Fehler nicht zurückkommt.
2. **Warum kann ein E2E-Test trotz grüner Unit-Tests fehlschlagen?** Weil er zusätzlich Browser, JavaScript, HTTP-Kommunikation, Speicherung, DOM-Selektoren und das Zusammenspiel der Komponenten prüft. Ein Fehler dort betrifft die einzelne Fachmethode nicht.
3. **Wann bleibt der Merge trotz grüner Tests blockiert?** Wenn ein erforderliches Review fehlt oder durch neue Commits ungültig wurde, der Branch nicht aktuell ist oder eine andere Branch-Schutzregel noch nicht erfüllt ist.

## Zusatzauftrag

- Tourismus: 29 Tage genehmigt, 30 Tage genehmigt, 31 Tage abgelehnt.
- Arbeit: Kira mit 90 Tagen und Borin mit 180 Tagen werden genehmigt; für Arbeit gilt die allgemeine Grenze von 1 bis 365 Tagen.
- Ablehnung vor Quarantäne: Ein Fall mit schwarzem Loch und Weltraumpilz wird abgelehnt, weil die Ablehnungsregel vor der Quarantäneregel greift.

Diese Fachregeln gehören in Unit-Tests. Ein zusätzlicher E2E-Test kann ausserdem prüfen, ob ein ausgewähltes Musterprofil korrekt in das Formular übernommen und anschliessend richtig angezeigt wird.
