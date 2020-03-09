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
using System.Windows.Shapes;

namespace PrintexV
{
    /// <summary>
    /// Logique d'interaction pour Account.xaml
    /// </summary>
    public partial class Account 
    {
        public Account()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            var t = new MainWindow();
            t.affectUserName(Clerker.Text);
            t.CreateExcels();
            this.Close();
        }

        private void cancela_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
