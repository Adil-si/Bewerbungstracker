# 📋<img width="1229" height="808" alt="Bewerbungstracker png" src="https://github.com/user-attachments/assets/c32626db-9e84-4993-b9b4-804626cfc966" />
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
   - **Tag version:** `v1.0`
   - **Title:** `Bewerbungstracker v1.0`
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



**Sag mir Bescheid, wenn du  Hilfe bei einem Schritt brauchst!** 🚀

<img width="1229" height="808" alt="Screenshot 2026-04-17 152905" src="https://github.com/user-attachments/assets/e8501356-c711-4748-b8d7-8e1e79e47013" />

