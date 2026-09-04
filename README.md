# 📋 Bewerbungstracker 

Eine einfache Windows-App zum Verwalten deiner Bewerbungen.  
**Kein Visual Studio nötig!** Die App läuft auf jedem Windows-PC.

---

## 📦 Was du brauchst

- Windows 10 oder Windows 11
- Keine Installation von Visual Studio
- Keine Programmierkenntnisse

---

## 🚀 Schritt-für-Schritt: App starten (für Anfänger)

### Schritt 1: Datei herunterladen

1. Gehe zu: `https://github.com/DEIN_USERNAME/Bewerbungstracker`
2. Klicke auf **Releases** (rechte Seite)
3. Lade die Datei `Bewerbungstracker.zip` herunter
   - (Oder: Grüner Button **Code** → **Download ZIP**)

### Schritt 2: Entpacken

1. Rechtsklick auf die heruntergeladene ZIP-Datei
2. **Alle extrahieren** oder **Extract All**
3. Zielordner auswählen (z.B. `Desktop` oder `Dokumente`)
4. **Extrahieren** klicken

### Schritt 3: App starten

1. In den entpackten Ordner gehen
2. **Doppelklick** auf `Bewerbungstracker.exe`
3. Die App öffnet sich – **fertig!**

> ⚠️ **Hinweis:** Windows zeigt evtl. eine Sicherheitswarnung.  
> Klicke auf **"Trotzdem ausführen"** oder **"Weitere Informationen → Trotzdem ausführen"**

### Schritt 4: App nutzen

| Feld | Was du einträgst |
|------|------------------|
| **Firma** | z.B. `Siemens AG` |
| **Plattform** | Wo hast du die Stelle gefunden? (LinkedIn, Indeed, Xing, StepStone, Direkt) |
| **Datum** | Datum deiner Bewerbung |
| **Status** | Aktueller Stand |

**So prüfst du, ob du dich schon beworben hast:**
1. Firmennamen eingeben
2. **"Firma prüfen"** klicken
3. App sagt: "Schon angesprochen" oder "Noch nicht"

**So speicherst du eine neue Bewerbung:**
1. Alle Felder ausfüllen
2. **"Bewerbung speichern"** klicken

---

## 💾 Wo werden meine Daten gespeichert?

Die App speichert automatisch im **Dokumente-Ordner**:
-----------------------------------------------------------------------------------------------------------------------
C:\Benutzer\DEIN_NAME\Dokumente\Bewerbungstracker.json


**Das ist eine normale Textdatei – du kannst sie:**
- Mit Notepad öffnen
- Sichern (auf USB-Stick, in die Cloud)
- Auf einen anderen PC kopieren

---

## 📦 App auf einen anderen PC bringen

### Variante A: Komplette App kopieren

1. Den gesamten Ordner mit der `.exe` auf USB-Stick kopieren
2. Auf anderem PC einfügen
3. Doppelklick auf `.exe` – fertig

### Variante B: Nur die Daten mitnehmen

1. Die Datei `Bewerbungstracker.json` aus dem Dokumente-Ordner kopieren
2. Auf anderem PC in dessen Dokumente-Ordner einfügen
3. App starten – alle Bewerbungen sind da

---

## ❌ Fehlerbehebung

| Problem | Lösung |
|---------|--------|
| **"Windows hat die Ausführung geschützt"** | Klicke auf "Weitere Informationen" → "Trotzdem ausführen" |
| **.exe fehlt** | Du hast die falsche ZIP heruntergeladen. Lade die **veröffentlichte Version** aus "Releases" |
| **App startet nicht** | Stelle sicher, dass du .NET 8.0 installiert hast: [Download hier](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) |
| **Daten sind weg** | Prüfe ob die JSON-Datei im Dokumente-Ordner existiert |

---

## 🛠️ Für Entwickler (mit Visual Studio)

Falls du den Code selbst ändern willst:

1. **Visual Studio 2022-2026** installieren (Community Edition – kostenlos)
2. Repository klonen:
   ```bash
   git clone https://github.com/DEIN_USERNAME/Bewerbungstracker.git

   -------------------------------------------------------------------------------------------------------------------------

---

## Zusätzlich: Release erstellen (damit andere die .exe herunterladen können)

Damit andere die `.exe` ohne Visual Studio starten können, musst du ein **Release** auf GitHub erstellen:

### Schritt für Schritt:

1. **In Visual Studio:** Rechtsklick auf Projekt → **Veröffentlichen**
2. **Ordner auswählen** → **Veröffentlichen**
3. Es entsteht ein Ordner `publish` mit der `.exe`

4. **Auf GitHub:**
   - Gehe zu deinem Repository
   - Klicke rechts auf **Releases**
   - **Create a new release**
   - **Tag version:** `v1.5`
   - **Title:** `Bewerbungstracker v1.5`
   - **Attach binaries:** Ziehe den gesamten `publish`-Ordner per Drag & Drop rein (oder als ZIP)
   - **Publish release** klicken

Jetzt können andere die `.exe` herunterladen und einfach starten!

---

## Kurzzusammenfassung für  README:

| Was der Benutzer tun muss | In deiner README steht es unter |
|---------------------------|--------------------------------|
| App herunterladen | Schritt 1 |
| Entpacken | Schritt 2 |
| Starten | Schritt 3 |
| Bedienung | Schritt 4 |
| Daten sichern | "Wo werden meine Daten gespeichert?" |
| Fehler lösen | "Fehlerbehebung" |

---
## 📂 Unternehmensliste für Anwendungsentwickler – zum direkten Bewerben

Im Repository findest du eine **fertig zusammengestellte Liste** von Unternehmen, die für Umschüler und Quereinsteiger im Bereich **Anwendungsentwicklung** relevant sind.

**📁 Datei:**  
[`data/potenzielle_arbeitgeber.json`](https://github.com/Adil-si/Bewerbungstracker/blob/main/data/potenzielle_arbeitgeber.json)

### 🎯 Was die Liste bietet:

| Vorteil | Beschreibung |
|---------|--------------|
| ✅ **Fertig recherchiert** | Unternehmen aus der Region Düsseldorf, Solingen, Wuppertal, Köln und Umgebung |
| ✅ **Kontaktdaten enthalten** | Adressen, Telefonnummern, E-Mail-Kontakte, direkte Karriere-Webseiten |
| ✅ **Ansprechpartner** | Viele Einträge enthalten konkrete Ansprechpartner (z. B. Personalverantwortliche) |
| ✅ **Kein Suchen nötig** | Einfach die Liste durchgehen und direkt bewerben |
| ✅ **Für Anwendungsentwickler** | Speziell für Fachinformatiker (Anwendungsentwicklung) und Quereinsteiger |
| ✅ **Aktuell** | Stand: September 2026 – ca. **90 Unternehmen** |

### 📊 Was die Liste enthält:

- Firmenname
- Adresse (vollständig oder Stadt)
- Plattform (LinkedIn, Indeed, Andere)
- Website (direkt zur Karriere-Seite)
- Ansprechpartner (Name + E-Mail, falls vorhanden)
- Telefonnummer
- Kommentar (z. B. „Bietet explizit Praktika an“ oder „Initiativbewerbung möglich“)

### 📥 Wie du die Liste nutzt:

**Variante A – In der App verwenden:**
1. Lade die JSON-Datei von GitHub herunter
2. Öffne deinen Bewerbungstracker
3. Trage die Firmen manuell ein (oder verwende später den geplanten Import)

**Variante B – Direkt bewerben:**
1. Lade die JSON-Datei herunter oder öffne sie auf GitHub
2. Öffne die enthaltenen Links in deinem Browser
3. Finde offene Stellen und bewirb dich direkt

**Variante C – Inspirationsquelle:**
- Nutze die Liste, um einen Überblick über den regionalen Arbeitsmarkt zu bekommen
- Identifiziere Unternehmen, die regelmäßig Entwickler suchen

### 🏢 Radios der Unternehmen

Die Liste enthält Unternehmen aus verschiedenen Branchen und Größenordnungen:

| Region | Anzahl (ungefähr) | Beispiele |
|--------|-------------------|-----------|
| **Düsseldorf & Umgebung** | ~40 | Deloitte, KPMG, CGI, adesso SE, CONET Group |
| **Langenfeld, Haan, Erkrath** | ~25 | L&W CONSOLIDATION, Interflex, ITSM GmbH, CPA SoftwareConsult |
| **Solingen, Wuppertal** | ~15 | TimoCom, Codecentric, IPKS GmbH, Zierhut IT |
| **Köln, Neuss, Leverkusen** | ~10 | 360 Consulting, Lise GmbH, Open Digitalgruppe, Med 360° |

> 💡 **Hinweis:** Die Liste enthält nur **öffentlich zugängliche Informationen** – keine persönlichen Bewerbungsdaten. Sie dient als **Inspiration und Startpunkt** für deine eigene Bewerbungsphase.

---

**Viel Erfolg bei deiner Bewerbungsphase! 🚀**


**Sag mir Bescheid, wenn du  Hilfe bei einem Schritt brauchst!** 🚀


<img width="1871" height="1022" alt="Screenshot 2026-04-28 152656" src="https://github.com/user-attachments/assets/15d95af6-3471-4b91-b570-559148174a94" />

