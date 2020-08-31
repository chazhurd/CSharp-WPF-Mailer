using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace FinalHolidayMailer
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public bool infoEntered = false;
        public AddPerson()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text != "" && txtFirst.Text != "" && txtLast.Text != "" && txtEmail.Text.Contains("@"))
            {
                infoEntered = true;
                this.Close();
            }
            if(txtEmail.Text == "")
            {
                lblDetails.Content = "Please Enter Email";
                txtEmail.Focus();
                SystemSounds.Beep.Play();
            }
            if(txtFirst.Text == "")
            {
                lblDetails.Content = "Please Enter First Name";
                txtFirst.Focus();
                SystemSounds.Beep.Play();
            }
            if(txtLast.Text == "")
            {
                lblDetails.Content = "Please Enter Last Name";
                txtLast.Focus();
                SystemSounds.Beep.Play();
            }
            if (txtEmail.Text.Contains("@")) { } else
            {
                txtEmail.Focus();
                lblDetails.Content = "Please Enter Valid Email";
                SystemSounds.Beep.Play();
            }
        }
    }
}
