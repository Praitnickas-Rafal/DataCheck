using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace M.M_Va
{
    public partial class DataCheck : Form
    {
        private List<string> listaDanych = new List<string>(); // utworzenie listy
        public DataCheck()
        {
            InitializeComponent();
            InTextBox.Text = listaDanych.Count.ToString();
        }
        
        private void InButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InTextBox.Text))
            {
                string tekst = InTextBox.Text; // pobranie tekstu z pola tekstowego
                listaDanych.Add(tekst); // dodanie tekstu do listy

                // wyświetlenie danych z listy w drugim polu tekstowym
                string tekstDoWyswietlenia = "";
                foreach (string dane in listaDanych)
                {
                    tekstDoWyswietlenia += dane + "\r\n"; // dodanie każdego elementu z listy i nowej linii
                }
                AlfabetTextBox.Text = tekstDoWyswietlenia;
            }
            else
            {
                MessageBox.Show("Proszę coś wpisać", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            InTextBox.Clear();
        }


        // Wyczyść lista oraz text z alfabetem
        private void ClearButton_Click(object sender, EventArgs e)
        {
            AlfabetTextBox.Clear();
            listaDanych.Clear();
            DaneTrueLabel.Visible = false;
            DaneFalseLabel.Visible = false;
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
          if(CheckEnabled() == true && !string.IsNullOrWhiteSpace(AlfabetTextBox.Text))
            {
                HeadCheck();
            }
            else if ( CheckEnabled() == false || string.IsNullOrWhiteSpace(AlfabetTextBox.Text))
            {
                MessageBox.Show("Musisz zaznaczyć co najmniej jedną literę alfabetu oraz wpisać tekst przed sprawdzeniem.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        // Przypadki sprawdzeniów
        bool FirstCheck()  
        {
            if (listaDanych.Count != 3)
            {
                return true;
            }
            return false;
        }
        bool SecondCheck()
        {
            if (!listaDanych[0].StartsWith(listaDanych[1]))
            {
                return true;
            }
            return false;
        }
        bool ThirdCheck()
        {
            if (FirstCheck() || SecondCheck())
            {
                return true;
            }
            return false;
        }
        bool FourthCheck()
        {
            if (listaDanych.Count >= 3 && !listaDanych[1].StartsWith(listaDanych[2]))
            {
                return true;
            }
            return false;
        }

        bool FifthCheck()
        {
            foreach (string dane in listaDanych)
            {
                foreach (char c in dane)
                {
                    if (!Char.IsLetter(c))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        // Sprawdzenie, czy jest zaznaczone CheckBox
        bool CheckEnabled()
        {
            if (TrzyZnakoweCheckBox.Checked || PierwszymZnakiemCheckBox.Checked ||
                R1R2CheckBox.Checked || DrugiZnakiemCheckBox.Checked || TylkoLiteryCheckBox.Checked)

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void HeadCheck()
        {
            if (!string.IsNullOrEmpty(AlfabetTextBox.Text))
            {

                bool error = false; // zmienna przechowująca informację o wystąpieniu błędu

                // Sprawdzenie wybranych reguł
                if (TrzyZnakoweCheckBox.Checked && FirstCheck())
                {
                    MessageBox.Show("Lista musi mieć dokładnie trzy elementy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    error = true;
                }
                if (PierwszymZnakiemCheckBox.Checked && SecondCheck())
                {
                    MessageBox.Show("Zwróć uwagę na pierwszy i drugi znak", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    error = true;
                }
                if (ThirdCheck() && R1R2CheckBox.Checked)
                {
                    MessageBox.Show("Nie spełniają reguły R3, ponieważ nie spełniają reguł R1 i R2", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    error = true;
                }
                if (FourthCheck() && DrugiZnakiemCheckBox.Checked)
                {
                    MessageBox.Show("Zwróć uwagę na drugi i trzeci znak", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    error = true;
                }
                if (!FifthCheck() && TylkoLiteryCheckBox.Checked)
                {
                    MessageBox.Show("Wprowadzono niepoprawne dane. Tylko litery są dozwolone.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    error = true;
                }
                // Jeśli nie ma błędów, wykonaj pozostałe działania
                if (!error)
                {
                    DaneTrueLabel.Visible = true;
                    DaneFalseLabel.Visible = false;
                }
                else
                {
                    DaneTrueLabel.Visible = false;
                    DaneFalseLabel.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Proszę coś wpisać do Alfabetu A", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TrzyZnakoweCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = CheckEnabled();
        }

        private void PierwszymZnakiemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = CheckEnabled();
        }

        private void R1R2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = CheckEnabled();
        }

        private void DrugiZnakiemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = CheckEnabled();
        }

        private void TylkoLiteryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckButton.Enabled = CheckEnabled();
        }


        // Koniec programu
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }

}

