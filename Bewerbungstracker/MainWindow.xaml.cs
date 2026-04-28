using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace Bewerbungstracker
{
    public partial class MainWindow : Window
    {
        private List<Bewerbung> alleBewerbungen = new List<Bewerbung>();
        private string speicherPfad;

        public MainWindow()
        {
            InitializeComponent();

            string dokumente = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            speicherPfad = System.IO.Path.Combine(dokumente, "Bewerbungstracker.json");

            LadeDaten();
            AktualisiereListen();
            _ = PruefeAufUpdates();
        }

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
                    $"📍 Adresse: {gefunden.Adresse}\n" +
                    $"👤 Ansprechpartner: {gefunden.Ansprechpartner}\n" +
                    $"📞 Telefon: {gefunden.Telefon}\n" +
                    $"🔗 Website: {gefunden.Website}\n" +
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

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            string firma = txtFirma.Text.Trim();
            string status = (cbStatus.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "📧 Beworben";
            string adresse = txtAdresse.Text.Trim();
            string plattform = (cbPlattform.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";
            DateTime datum = dpDatum.SelectedDate ?? DateTime.Now;
            string website = txtWebsite.Text.Trim();
            string ansprechpartner = txtAnsprechpartner.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string kommentar = txtKommentar.Text.Trim();  // NEU

            if (string.IsNullOrEmpty(firma))
            {
                MessageBox.Show("Bitte gib einen Firmennamen ein!", "Hinweis",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (alleBewerbungen.Any(b => b.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Die Firma '{firma}' ist bereits in deiner Liste!",
                    "Bereits vorhanden", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Bewerbung neueBewerbung = new Bewerbung
            {
                Firma = firma,
                Status = status,
                Adresse = adresse,
                Plattform = plattform,
                Datum = datum,
                Website = website,
                Ansprechpartner = ansprechpartner,
                Telefon = telefon,
                Kommentar = kommentar  
            };

            alleBewerbungen.Add(neueBewerbung);
            SpeichereDaten();
            AktualisiereListen();

            txtFirma.Clear();
            txtAdresse.Clear();
            txtWebsite.Clear();
            txtAnsprechpartner.Clear();
            txtTelefon.Clear();
            txtKommentar.Clear();  
            txtStatus.Text = $"✅ {firma} wurde gespeichert! Status: {status}";
        }

        private void BtnLoeschen_Click(object sender, RoutedEventArgs e)
        {
            Bewerbung ausgewaehlt = null;

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

        private void BtnBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            Bewerbung ausgewaehlt = null;

            if (lstAktive.SelectedItem != null)
                ausgewaehlt = (Bewerbung)lstAktive.SelectedItem;
            else if (lstAbsagen.SelectedItem != null)
                ausgewaehlt = (Bewerbung)lstAbsagen.SelectedItem;

            if (ausgewaehlt == null)
            {
                MessageBox.Show("Bitte wähle zuerst eine Bewerbung aus!", "Bearbeiten",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new BearbeitungsDialog(ausgewaehlt);
            if (dialog.ShowDialog() == true)
            {
                ausgewaehlt.Firma = dialog.BearbeiteteBewerbung.Firma;
                ausgewaehlt.Status = dialog.BearbeiteteBewerbung.Status;
                ausgewaehlt.Adresse = dialog.BearbeiteteBewerbung.Adresse;
                ausgewaehlt.Plattform = dialog.BearbeiteteBewerbung.Plattform;
                ausgewaehlt.Datum = dialog.BearbeiteteBewerbung.Datum;
                ausgewaehlt.Website = dialog.BearbeiteteBewerbung.Website;
                ausgewaehlt.Ansprechpartner = dialog.BearbeiteteBewerbung.Ansprechpartner;
                ausgewaehlt.Telefon = dialog.BearbeiteteBewerbung.Telefon;

                SpeichereDaten();
                AktualisiereListen();
                txtStatus.Text = $"✅ {ausgewaehlt.Firma} wurde aktualisiert!";
            }
        }

        private void BtnFilterReset_Click(object sender, RoutedEventArgs e)
        {
            if (cbDatumFilter != null)
                cbDatumFilter.SelectedIndex = 0;
            if (cbStatusFilter != null)
                cbStatusFilter.SelectedIndex = 0;
            if (dpBestimmtesDatum != null)
                dpBestimmtesDatum.SelectedDate = null;
            Filter_Changed(null, null);
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (cbDatumFilter == null || cbStatusFilter == null || lstAktive == null)
                return;

            var alleAktiven = alleBewerbungen
    .Where(b => !b.Status.Contains("Absage") && !b.Status.Contains("selbst zurückgezogen"))
    .OrderByDescending(b => b.Datum)
    .ToList();

            DateTime? bestimmtesDatum = dpBestimmtesDatum?.SelectedDate;

            List<Bewerbung> nachDatumGefiltert;

            if (bestimmtesDatum.HasValue)
            {
                nachDatumGefiltert = alleAktiven
                    .Where(b => b.Datum.Date == bestimmtesDatum.Value.Date)
                    .ToList();
            }
            else
            {
                string datumFilter = (cbDatumFilter.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Alle";
                DateTime heute = DateTime.Now.Date;
                DateTime filterDatum = heute;

                switch (datumFilter)
                {
                    case "Letzte 7 Tage":
                        filterDatum = heute.AddDays(-7);
                        break;
                    case "Letzte 30 Tage":
                        filterDatum = heute.AddDays(-30);
                        break;
                    case "Letzte 90 Tage":
                        filterDatum = heute.AddDays(-90);
                        break;
                    case "Letztes Jahr":
                        filterDatum = heute.AddYears(-1);
                        break;
                    default:
                        filterDatum = DateTime.MinValue;
                        break;
                }

                nachDatumGefiltert = alleAktiven;
                if (filterDatum != DateTime.MinValue)
                {
                    nachDatumGefiltert = alleAktiven.Where(b => b.Datum.Date >= filterDatum).ToList();
                }
            }

            string statusFilter = (cbStatusFilter.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Alle Status";

            var gefiltert = nachDatumGefiltert;
            if (statusFilter != "Alle Status")
            {
                gefiltert = nachDatumGefiltert.Where(b => b.Status == statusFilter).ToList();
            }

            lstAktive.ItemsSource = gefiltert;

            int absagenCount = alleBewerbungen.Count(b => b.Status.Contains("Absage"));
            txtStatus.Text = $"📊 {gefiltert.Count} aktive Bewerbungen (gefiltert) | {absagenCount} Absagen | Gesamt: {alleBewerbungen.Count}";
        }

        private void LstAktive_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstAktive.SelectedItem != null)
            {
                var ausgewaehlt = (Bewerbung)lstAktive.SelectedItem;
                if (!string.IsNullOrEmpty(ausgewaehlt.Website))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = ausgewaehlt.Website,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Website konnte nicht geöffnet werden:\n{ex.Message}", "Fehler",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private void LstAktive_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAktive.SelectedItem != null)
            {
                var ausgewaehlt = (Bewerbung)lstAktive.SelectedItem;
                txtStatus.Text = $"Ausgewählt: {ausgewaehlt.Firma} - Status: {ausgewaehlt.Status}";
                btnBearbeiten.Visibility = Visibility.Visible;
                btnLoeschen.Visibility = Visibility.Visible;
            }
            else
            {
                btnBearbeiten.Visibility = Visibility.Collapsed;
                btnLoeschen.Visibility = Visibility.Collapsed;
            }
        }

        private void LstAbsagen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAbsagen.SelectedItem != null)
            {
                var ausgewaehlt = (Bewerbung)lstAbsagen.SelectedItem;
                txtStatus.Text = $"Ausgewählt: {ausgewaehlt.Firma} - Status: {ausgewaehlt.Status}";
                btnBearbeiten.Visibility = Visibility.Visible;
                btnLoeschen.Visibility = Visibility.Visible;
            }
            else
            {
                btnBearbeiten.Visibility = Visibility.Collapsed;
                btnLoeschen.Visibility = Visibility.Collapsed;
            }
        }

        private void AktualisiereListen()
        {
            // Alle Absagen (inkl. der neuen "Absage meine Seite")
            var absagen = alleBewerbungen
                .Where(b => b.Status.Contains("Absage") || b.Status.Contains("selbst zurückgezogen"))
                .OrderByDescending(b => b.Datum)
                .ToList();

            lstAbsagen.ItemsSource = absagen;
            Filter_Changed(null, null);
        }

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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = e.Uri.AbsoluteUri,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Link konnte nicht geöffnet werden:\n{ex.Message}", "Fehler",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            e.Handled = true;
        }
        private async void txtFirma_TextChanged(object sender, TextChangedEventArgs e)
        {
            string firma = txtFirma.Text.Trim();

            if (string.IsNullOrEmpty(firma))
            {
                txtFirma.Background = System.Windows.Media.Brushes.White;
                return;
            }

            bool existiert = alleBewerbungen.Any(b =>
                b.Firma.Equals(firma, StringComparison.OrdinalIgnoreCase));

            if (existiert)
            {
                txtFirma.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightCoral);

                // ROTE MESSAGEBOX (Popup)
                MessageBox.Show($"⚠️ VORSICHT!\n\nDie Firma '{firma}' ist bereits in deiner Liste!\n\n" +
                    "Du hast dich dort schon beworben oder sie ist als potenziell markiert.",
                    "Doppelte Bewerbung verhindern",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                txtFirma.Background = System.Windows.Media.Brushes.White;
            }
            else
            {
                txtFirma.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightGreen);
                await Task.Delay(500);
                txtFirma.Background = System.Windows.Media.Brushes.White;

                txtStatus.Text = $"✅ '{firma}' ist verfügbar";
                await Task.Delay(1500);
                txtStatus.Text = "Bereit";
            }
        }
        // In MainWindow.xaml.cs bei Programmstart
        private async Task PruefeAufUpdates()
        {
            try
            {
                string aktuelleVersion = "1.0";
                string githubVersion = await HoleLetzteVersionVonGitHub();

                if (githubVersion != aktuelleVersion)
                {
                    var result = MessageBox.Show(
                        $"Neue Version {githubVersion} verfügbar!\n\nMöchtest du zur Download-Seite gehen?",
                        "Update verfügbar",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(
                            "https://github.com/Adil-si/Bewerbungstracker/releases");
                    }
                }
            }
            catch { /* Kein Internet oder Fehler */ }
        }

        private async Task<string> HoleLetzteVersionVonGitHub()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Bewerbungstracker");
                var response = await client.GetStringAsync(
                    "https://api.github.com/repos/Adil-si/Bewerbungstracker/releases/latest");

                // Einfaches Parsen (mit JSON)
                var json = System.Text.Json.JsonDocument.Parse(response);
                return json.RootElement.GetProperty("tag_name").GetString();
            }
        }
        private void LstAktive_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (lstAktive.SelectedItem != null)
            {
                var item = (Bewerbung)lstAktive.SelectedItem;
                txtStatus.Text = $"ℹ️ {item.Firma} - {item.Status} - {item.Plattform}";
            }
        }

        private void LstAktive_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            txtStatus.Text = "Bereit";
        }

    }

    public class Bewerbung
    {
        public string Firma { get; set; } = "";
        public string Status { get; set; } = "";
        public string Adresse { get; set; } = "";
        public string Plattform { get; set; } = "";
        public DateTime Datum { get; set; }
        public string Website { get; set; } = "";
        public string Ansprechpartner { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string Kommentar { get; set; } = "";  

        public string DisplayText => $"{Datum:dd.MM.yyyy}  |  {Firma}  |  {Plattform}  |  {Status} | {Ansprechpartner}";

        public string TooltipText
        {
            get
            {
                return $"📌 FIRMA: {Firma}\n" +
                       $"📅 Datum: {Datum:dd.MM.yyyy}\n" +
                       $"📊 Status: {Status}\n" +
                       $"🌐 Plattform: {Plattform}\n" +
                       $"📍 Adresse: {(string.IsNullOrEmpty(Adresse) ? "—" : Adresse)}\n" +
                       $"👤 Ansprechpartner: {(string.IsNullOrEmpty(Ansprechpartner) ? "—" : Ansprechpartner)}\n" +
                       $"📞 Telefon: {(string.IsNullOrEmpty(Telefon) ? "—" : Telefon)}\n" +
                       $"💬 Kommentar: {(string.IsNullOrEmpty(Kommentar) ? "—" : Kommentar)}\n" +
                       $"🔗 Website: {(string.IsNullOrEmpty(Website) ? "—" : Website)}";
            }
        }
    }
}