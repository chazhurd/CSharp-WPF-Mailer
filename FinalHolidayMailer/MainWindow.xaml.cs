using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Threading;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;

namespace FinalHolidayMailer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public class Person
        {
            public string First { get; set; }
            public string Last { get; set; }
            public string Email { get; set; }
            public string Received { get; set; }

            public override string ToString()
            {
                return "Found" + base.ToString();
            }
        }

        static SQLiteConnection sqlite_conn;
        static SQLiteCommand sqlite_cmd;
        static SQLiteDataReader sqlite_datareader;

        private DispatcherTimer mailTimer = new DispatcherTimer();
        private static int y = 0;
        private string mypwd = "SorryCharlie";
        private string mypath = @"";
        private bool flashAttach = false;
        private bool fileAttached = false;
        private bool searched = false;

        public MainWindow()
        {
            InitializeComponent();
            startSQL();
            readDatabase();
            mailTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            mailTimer.Tick += mailTimer_Tick;
            mailTimer.IsEnabled = true;
            lblHelper.Content = "Mailer Toolbar";
            
        }

        private void mailTimer_Tick(object sender, EventArgs e)
        {
            string checker = txtbxFind.Text;
            if (txtbxFind.Text != "" && txtbxFind.Text != "Enter Last Name Here")
            {
                GridDatabse.Items.Clear();
                    btnFindPerson();
                    searched = true;
            }


            if(searched == true && txtbxFind.Text == ""  && txtbxFind.Text != "Enter Last Name Here")
            {
                GridDatabse.Items.Clear();
                readDatabase();
                searched = false;
            }


            if(flashAttach == true)
            {
                lblAttached.Visibility = Visibility.Visible;
                y++;
            }

        }

        private void btnFindPerson()
        {
            string personsName = txtbxFind.Text.ToUpper();
            sqlite_cmd.CommandText = "SELECT * FROM EmailInfo ORDER BY lname ASC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) 
            {

                string namecheck = sqlite_datareader["lname"].ToString().ToUpper();
                if (personsName.Length <= namecheck.Length)
                {
                    if (personsName == namecheck.Substring(0, personsName.Length))
                    {

                        var data = new Person { First = sqlite_datareader["fname"] + "", Last = sqlite_datareader["lname"] + "", Email = sqlite_datareader["email"] + "", Received = sqlite_datareader["rec"] + "" };
                        GridDatabse.Items.Clear();
                        GridDatabse.Items.Add(data);
                    }
                }

            }
            sqlite_datareader.Close();
        }


        private void readDatabase()
        {
            sqlite_conn = new SQLiteConnection("Data Source=mailInfo.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM EmailInfo ORDER BY lname ASC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) 
            {
                if (sqlite_datareader["fname"] != null)
                {
                    var data = new Person { First = sqlite_datareader["fname"] + "", Last = sqlite_datareader["lname"] + "", Email = sqlite_datareader["email"] + "", Received = sqlite_datareader["rec"] + "" };
                    GridDatabse.Items.Add(data);
                }
            }

            sqlite_datareader.Close();
        }

        private void startSQL()
        { 

            try
            {
                sqlite_conn = new SQLiteConnection("Data Source= mailInfo.db;Version=3;New=True;Compress=True;");
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "CREATE TABLE if not exists EmailInfo (id integer, fname varchar(100), lname varchar(100), email varchar(100), rec integer);";
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Creating Database " + e.Message);
            }


        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {

            AddPerson personAdded = new AddPerson();
            personAdded.ShowDialog();
            if (personAdded.infoEntered == true)
            {
                string email = "'" + personAdded.txtEmail.Text + "'";
                string first = "'" + personAdded.txtFirst.Text + "'";
                string last = "'" + personAdded.txtLast.Text + "'";
                bool rec = (bool)personAdded.chkRec.IsChecked;
                int received = 0;
                if (rec == true)
                {
                    received = 1;
                }

                y++;
                sqlite_cmd.CommandText = "INSERT INTO EmailInfo (id, fname, lname, email, rec) VALUES (" + y + ", " + first + ", " + last + ", " + email + ", " + received + ");";
                sqlite_cmd.ExecuteNonQuery();
                clearLabels();
                readDatabase();

            }



        }

        private void clearLabels()
        {
            GridDatabse.Items.Clear();
        }

        private void txtbxFind_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxFind.Text == "Enter Last Name Here")
            {
                txtbxFind.Text = "";
            }

        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("You Will Lose All Data", "Confirmation", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "DROP TABLE if exists EmailInfo";
                sqlite_cmd.ExecuteNonQuery();
                GridDatabse.Items.Clear();
                startSQL();
            }

        }

        private void btnEmailAll_Click(object sender, RoutedEventArgs e)
        {
            sqlite_conn = new SQLiteConnection("Data Source=mailInfo.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM EmailInfo ORDER BY lname ASC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read()) 
            {
                bool sent = false;
                string sendTo = sqlite_datareader["email"].ToString();
                try
                {
                    var client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("youremail@gmail.com", mypwd),
                        EnableSsl = true
                    };
                    MailMessage message = new MailMessage("youremail@gmail.com", sendTo, "Happy Holidays", "Happy Holidays from us to you");
                    if (fileAttached == true)
                    {
                        Attachment attach = new Attachment(mypath);
                        message.Attachments.Add(attach);
                    }
                    client.Send(message);
                    sent = true;
                    

                }
                catch (Exception err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }

                if(sent == true)
                {
                    MessageBox.Show("Emails Succesfully Sent");
                }
            }
            sqlite_datareader.Close();
        }

        private void btnAddPerson_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHelper.Visibility = Visibility.Visible;
            lblHelper.Content = "Add Person";
        }

        private void btnRecOnly_Click(object sender, RoutedEventArgs e)
        {

            sqlite_conn = new SQLiteConnection("Data Source=mailInfo.db;Version=3;New=True;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM EmailInfo ORDER BY lname ASC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            bool sent = false;

                while (sqlite_datareader.Read())
                {
                   
                    if (sqlite_datareader["rec"].ToString() == "1")
                    {
                        string sendTo = sqlite_datareader["email"].ToString();
                        try
                        {
                            var client = new SmtpClient("smtp.gmail.com", 587)
                            {
                                Credentials = new NetworkCredential("youremail@gmail.com", mypwd),
                                EnableSsl = true
                            };
                            MailMessage message = new MailMessage("youremail@gmail.com", sendTo, "Happy Holidays", "Happy Holidays from us to you");
                        if (fileAttached == true)
                        {
                            Attachment attach = new Attachment(mypath);
                            message.Attachments.Add(attach);
                        }
                            client.Send(message);
                            sent = true;
                            

                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Error: " + err.Message);
                        }
                    }

                    
                }
            sqlite_datareader.Close();
            if(sent == true)
            {
                MessageBox.Show("Emails Succesfully Sent");
            }
        }
    

        private void txtbxAttachment_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtbxAttachment.Text == "Enter File Path Here")
            {
                txtbxAttachment.Text = "";
            }
        }

        private void btnAttach_Click(object sender, RoutedEventArgs e)
        {
            string path = @txtbxAttachment.Text;
            if (File.Exists(path))
            {
                mypath = path;
                fileAttached = true;
                flashAttach = true;
                lblHelper.Content = "File Attached";
            }
            else
            {
                flashAttach = false;
                fileAttached = false;
                lblAttached.Visibility = Visibility.Hidden;
            }
        }

        private void btnsendOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("youremail@gmail.com", mypwd),
                    EnableSsl = true
                };
                MailMessage message = new MailMessage("youremail@gmail.com", "anotheremail@hotmail.com", "Happy Holidays", "Happy Holidays from us to you");
                if (fileAttached == true)
                {
                    Attachment attach = new Attachment(mypath);
                    message.Attachments.Add(attach);
                }
                client.Send(message);
                MessageBox.Show("Sent");

            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }

        private void btnEmailAll_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHelper.Visibility = Visibility.Visible;
            lblHelper.Content = "Email All";
        }

        private void btnRecOnly_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHelper.Visibility = Visibility.Visible;
            lblHelper.Content = "Email Received";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnReset_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHelper.Content = "Reset Database";
        }

        private void btnExit_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHelper.Content = "Exit Application";
        }
    }
}
