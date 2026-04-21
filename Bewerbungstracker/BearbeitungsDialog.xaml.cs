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
            cbPlattform.Text = zuBearbeiten.Plattform;
            dpDatum.SelectedDate = zuBearbeiten.Datum;
            cbStatus.Text = zuBearbeiten.Status;
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
                Plattform = cbPlattform.Text,
                Datum = dpDatum.SelectedDate ?? DateTime.Now,
                Status = cbStatus.Text,
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