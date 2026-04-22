using System;
using System.Windows;

namespace Bewerbungstracker
{
    public partial class BearbeitungsDialog : Window
    {
        public Bewerbung BearbeiteteBewerbung { get; private set; }

        public BearbeitungsDialog(Bewerbung zuBearbeiten)
        {
            InitializeComponent();

            // Daten in die Felder laden
            txtFirma.Text = zuBearbeiten.Firma;
            cbStatus.Text = zuBearbeiten.Status;
            txtAdresse.Text = zuBearbeiten.Adresse;
            cbPlattform.Text = zuBearbeiten.Plattform;
            dpDatum.SelectedDate = zuBearbeiten.Datum;
            txtWebsite.Text = zuBearbeiten.Website;
            txtAnsprechpartner.Text = zuBearbeiten.Ansprechpartner;
            txtTelefon.Text = zuBearbeiten.Telefon;
        }

        private void BtnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // Daten aus den Feldern sammeln
            BearbeiteteBewerbung = new Bewerbung
            {
                Firma = txtFirma.Text.Trim(),
                Status = cbStatus.Text,
                Adresse = txtAdresse.Text.Trim(),
                Plattform = cbPlattform.Text,
                Datum = dpDatum.SelectedDate ?? DateTime.Now,
                Website = txtWebsite.Text.Trim(),
                Ansprechpartner = txtAnsprechpartner.Text.Trim(),
                Telefon = txtTelefon.Text.Trim()
            };

            DialogResult = true;
            Close();
        }

        private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}