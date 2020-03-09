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
    /// Logique d'interaction pour PayerAn.xaml
    /// </summary>
    public partial class PayerAn : UserControl
    {
        public PayerAn()
        {
            InitializeComponent();
        }

        private void CreatePdf_Click(object sender, RoutedEventArgs e)
        {
            if (Nom.Text == "" || Prena.Text == "" || nueva.Text == "")
            {
                ErrMsg.Text = "Veuillez Remplir les Champs vides !";
            }
            else
            {
                MainWindow mainee = new MainWindow();
                mainee.CreateCharges(Nom.Text, Prena.Text, nueva.Text);
            }

        }

        private void CancelB_Click(object sender, RoutedEventArgs e)
        {
            Nom.Text = "";
            Prena.Text = "";
            nueva.Text = "";
            Nom.Focus();
        }
    }
}
