using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Bewerbungstracker
{
    public partial class MainWindow : Window
    {
        private List<Bewerbung> alleBewerbungen = new List<Bewerbung>();
        private string speicherPfad;

        public MainWindow()
        {
            InitializeComponent();

            // Speicherort: Dokumente-Ordner
            string dokumente = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            speicherPfad = System.IO.Path.Combine(dokumente, "Bewerbungstracker.json");

            LadeDaten();
            AktualisiereListen();
        }

        // BUTTON: Firma prüfen
        private void BtnPruefen_Click(object sender, RoutedEventArgs e)
        {
            string firma = txtFirma.Text.Trim();
            if (string.IsNullOrEmpty(firma))
            {
                MessageBox.Show("Bitte gib einen Firmennamen ein!", "Prüfung",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var gefunden = alleBewerbungen.FirstOrDefault(b =>
                b.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase));

            if (gefunden != null)
            {
                string box = (gefunden.Status.Contains("Absage")) ? "ABSAGEN-Box" : "AKTIVE-Box";
                MessageBox.Show($"⚠️ DIE FIRMA '{firma}' HAST DU BEREITS ANGESPROCHEN!\n\n" +
                    $"📅 Datum: {gefunden.Datum:dd.MM.yyyy}\n" +
                    $"🌐 Plattform: {gefunden.Plattform}\n" +
                    $"📊 Status: {gefunden.Status}\n" +
                    $"📍 Ort: {box}",
                    "Bereits beworben", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtStatus.Text = $"⚠️ {firma} ist bereits in der {box}";
            }
            else
            {
                MessageBox.Show($"✅ GUTE NACHRICHT: '{firma}' ist NOCH NICHT in deiner Liste.\n" +
                    "Du kannst sie jetzt hinzufügen!",
                    "Firma nicht gefunden", MessageBoxButton.OK, MessageBoxImage.Information);
                txtStatus.Text = $"✅ {firma} ist noch nicht in der Liste";
            }
        }

        // BUTTON: Bewerbung speichern
        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string firma = txtFirma.Text.Trim();
            string plattform = (cbPlattform.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
            string status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "📧 Beworben";
            DateTime datum = dpDatum.SelectedDate ?? DateTime.Now;

            if (string.IsNullOrEmpty(firma))
            {
                MessageBox.Show("Bitte gib einen Firmennamen ein!", "Hinweis",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Prüfen ob Firma schon existiert
            if (alleBewerbungen.Any(b => b.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Die Firma '{firma}' ist bereits in deiner Liste!\n" +
                    "Du kannst den Status ändern, indem du den Eintrag löschst und neu hinzufügst.",
                    "Bereits vorhanden", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Bewerbung neueBewerbung = new Bewerbung
            {
                Firma = firma,
                Plattform = plattform,
                Datum = datum,
                Status = status
            };

            alleBewerbungen.Add(neueBewerbung);
            SpeichereDaten();
            AktualisiereListen();

            txtFirma.Clear();
            txtStatus.Text = $"✅ {firma} wurde gespeichert! Status: {status}";
        }

        // BUTTON: Löschen
        private void BtnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            Bewerbung ausgewaehlt = null;

            // Prüfen ob in aktiver Liste oder Absagen-Liste ausgewählt
            if (lstAktive.SelectedItem != null)
                ausgewaehlt = (Bewerbung)lstAktive.SelectedItem;
            else if (lstAbsagen.SelectedItem != null)
                ausgewaehlt = (Bewerbung)lstAbsagen.SelectedItem;

            if (ausgewaehlt == null)
            {
                MessageBox.Show("Bitte wähle zuerst eine Firma aus der Liste aus.",
                    "Löschen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show($"Möchtest du die Bewerbung bei '{ausgewaehlt.Firma}' wirklich LÖSCHEN?\n(Das kann nicht rückgängig gemacht werden)",
                "Löschen bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                alleBewerbungen.Remove(ausgewaehlt);
                SpeichereDaten();
                AktualisiereListen();
                txtStatus.Text = $"🗑 {ausgewaehlt.Firma} wurde gelöscht.";
            }
        }

        // Wenn in aktiver Liste etwas ausgewählt wird
        private void LstAktive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAktive.SelectedItem != null)
            {
                var ausgewaehlt = (Bewerbung)lstAktive.SelectedItem;
                txtStatus.Text = $"Ausgewählt: {ausgewaehlt.Firma} - Status: {ausgewaehlt.Status}";
                btnLoeschen.Visibility = Visibility.Visible;
            }
        }

        // Listen aktualisieren (aktive oben / Absagen unten)
        private void AktualisiereListen()
        {
            // Aktive Bewerbungen (alles außer Absage)
            var aktive = alleBewerbungen
                .Where(b => !b.Status.Contains("Absage"))
                .OrderByDescending(b => b.Datum)
                .ToList();

            // Absagen
            var absagen = alleBewerbungen
                .Where(b => b.Status.Contains("Absage"))
                .OrderByDescending(b => b.Datum)
                .ToList();

            lstAktive.ItemsSource = aktive;
            lstAbsagen.ItemsSource = absagen;

            txtStatus.Text = $"📊 {aktive.Count} aktive Bewerbungen | {absagen.Count} Absagen";
        }

        // Speichern in JSON-Datei
        private void SpeichereDaten()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(alleBewerbungen, options);
                File.WriteAllText(speicherPfad, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Speichern: {ex.Message}", "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Laden aus JSON-Datei
        private void LadeDaten()
        {
            try
            {
                if (File.Exists(speicherPfad))
                {
                    string json = File.ReadAllText(speicherPfad);
                    alleBewerbungen = JsonSerializer.Deserialize<List<Bewerbung>>(json) ?? new List<Bewerbung>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden: {ex.Message}", "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                alleBewerbungen = new List<Bewerbung>();
            }
        }
    }

    // Datenmodell für eine Bewerbung
    public class Bewerbung
    {
        public string Firma { get; set; } = "";
        public string Plattform { get; set; } = "";
        public DateTime Datum { get; set; }
        public string Status { get; set; } = "";

        // Für die Anzeige in der ListBox
        public string DisplayText => $"{Datum:dd.MM.yyyy}  |  {Firma}  |  {Plattform}  |  {Status}";
    }
}