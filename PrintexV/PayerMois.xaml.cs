using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintexV
{
    /// <summary>
    /// Logique d'interaction pour PayerMois.xaml
    /// </summary>
    public partial class PayerMois : UserControl
    {
        public PayerMois()
        {
            InitializeComponent();
        }

        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Nom.Text = "";
            Prena.Text = "";
            profa.Text = "";
            matira.Text = "";
            nueva.Text = "";
            grupo.Text = "";
            Prix.Text = "";
            Nom.Focus();
        }

        private void CreatePdf_Click(object sender, RoutedEventArgs e)
        {
            if (Nom.Text == "" || Prena.Text == "" || nueva.Text == "" || profa.Text == "" || matira.Text == "" || grupo.Text == "" || Prix.Text == "")
            {
                ErrMsg.Text = "Veuillez remplir les champs vides !";
            }
            else
            {
                MainWindow main = new MainWindow();
                main.CreatePdf(Nom.Text, Prena.Text, profa.Text, nueva.Text, matira.Text, grupo.Text, Prix.Text);
                //main.CreatePreview();
            }
        }

        private void AutoForm()
        {
            if (profa.Text == "Rahab")
                matira.Text = "Maths";
        }

        public void resetta()
        {
            Nom.Text = Nom.Text.Remove(0, Nom.Text.Length);
            Prena.Text = "";
            profa.Text = "";
            matira.Text = "";
            nueva.Text = "";
            grupo.Text = "";
            Prix.Text = "";
            Nom.Focus();
        }
        
        private void profa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var coner = sender as ComboBox;
            string vali = coner.SelectedItem as string;
            switch (vali)
            {
                case "Rahab":
                    {
                        matira.SelectedIndex = 0;
                        nueva.SelectedIndex = 14;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Maatar":
                    {
                        matira.SelectedIndex = 2;
                        nueva.SelectedIndex = 12;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Saadi":
                    {
                        matira.SelectedIndex = 1;
                        nueva.SelectedIndex = 14;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Khemari":
                    {
                        matira.SelectedIndex = 6;
                        nueva.SelectedIndex = 14;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Baldia":
                    {
                        matira.SelectedIndex = 8;
                        nueva.SelectedIndex = 13;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Habta":
                    {
                        matira.SelectedIndex = 3;
                        nueva.SelectedIndex = 14;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Alaarbi":
                    {
                        matira.SelectedIndex = 0;
                        nueva.SelectedIndex = 13;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Guiroud":
                    {
                        matira.SelectedIndex = 1;
                        nueva.SelectedIndex = 12;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Benkharrour":
                    {
                        matira.SelectedIndex = 1;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Khaldi":
                    {
                        matira.SelectedIndex = 0;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Amouri":
                    {
                        matira.SelectedIndex = 0;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Kahlat":
                    {
                        matira.SelectedIndex = 3;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Chafei":
                    {
                        matira.SelectedIndex = 4;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Ziani":
                    {
                        matira.SelectedIndex = 2;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Zeroual":
                    {
                        matira.SelectedIndex = 5;
                        nueva.SelectedIndex = 8;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Aglef":
                    {
                        matira.SelectedIndex = 3;
                        nueva.SelectedIndex = 3;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Afofo":
                    {
                        matira.SelectedIndex = 3;
                        nueva.SelectedIndex = 3;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Brikat":
                    {
                        matira.SelectedIndex = 3;
                        nueva.SelectedIndex = 3;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Nasira":
                    {
                        matira.SelectedIndex = 4;
                        nueva.SelectedIndex = 3;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Boualam":
                    {
                        matira.SelectedIndex = 4;
                        nueva.SelectedIndex = 3;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Lebbier":
                    {
                        matira.SelectedIndex = 0;
                        nueva.SelectedIndex = 12;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "Khouatir":
                    {
                        matira.SelectedIndex = 9;
                        nueva.SelectedIndex = 14;
                        grupo.SelectedIndex = 0;
                        break;
                    }
                case "--- Standard ---":
                    {
                        matira.SelectedIndex = 10;
                        nueva.SelectedIndex = 0;
                        grupo.SelectedIndex = 0;
                        break;
                    }
            }
        }

        private void profa_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> lista = new List<string>();
            // fill the list 
            lista.Add("--- Standard ---");
            lista.Add("Rahab");
            lista.Add("Maatar");
            lista.Add("Saadi");
            lista.Add("Khemari");
            lista.Add("Baldia");
            lista.Add("Habta");
            lista.Add("Alaarbi");
            lista.Add("Guiroud");
            lista.Add("Benkharrour");
            lista.Add("Khaldi");
            lista.Add("Amouri");
            lista.Add("Kahlat");
            lista.Add("Chafei");
            lista.Add("Ziani");
            lista.Add("Zeroual");
            lista.Add("Aglef");
            lista.Add("Brikat");
            lista.Add("Afofo");
            lista.Add("Nasira");
            lista.Add("Boualam");
            lista.Add("Lebbier");
            lista.Add("Khouatir");

            // fill the combo
            var combobx = sender as ComboBox;
            combobx.ItemsSource = lista;
            combobx.SelectedIndex = 0;
        }

        private void matira_Loaded(object sender, RoutedEventArgs e)
        {
            List<String> lista = new List<string>();
            // fill the list 
            lista.Add("Maths");
            lista.Add("Physique");
            lista.Add("Science");
            lista.Add("Arabe");
            lista.Add("Francais");
            lista.Add("Anglais");
            lista.Add("Histoire et Geographie");
            lista.Add("Philosophie");
            lista.Add("Comptabilité");
            lista.Add("Allemand");
            lista.Add("Crèche");

            // fill the combo
            var comba = sender as ComboBox;
            comba.ItemsSource = lista;
            comba.SelectedIndex = 10;
        }

        private void matira_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
