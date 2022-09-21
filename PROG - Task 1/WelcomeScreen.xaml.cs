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

namespace PROG___Task_1
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Window
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void btnReplacingBooks_Click(object sender, RoutedEventArgs e)
        {
            //Used to open new window and close Welcome screen
            Instructions instructions = new Instructions();
            instructions.Show();
            this.Close();
        }

        private void btnIdentifyArea_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Feature is Under Development \n\tWatch this Space ;)");
        }

        private void btnFindingCallNums_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This Feature is Under Development \n\tWatch this Space ;)");
        }
    }
}
