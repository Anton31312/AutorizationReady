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
using System.IO;

namespace StrakhFed
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();
        bool truecapcha = false;
        

        public MainWindow()
        {
            for (int i = 1; i <= 5; i++)
            {
                people.Add(new Person
                {
                    id = i,
                    Name = $"User{i}",
                    Login = $"Log{i}",
                    Password = $"Passw{i}"
                }
                ); 
            }

            InitializeComponent();
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string[] LogPas = streamReader.ReadToEnd().Split(' ');
                    tbLog.Text = LogPas[0].Trim();
                    tbPass.Text = LogPas[1].Trim();
                }
            }
        }

        string path = @"C:\Users\WSR\Desktop\LogPas.txt";


       
        

        private void btnCapch_Click(object sender, RoutedEventArgs e)
        {
            txtCapch.Text = GetString.GetCapcha();
        }


        private void btnSign_Click(object sender, RoutedEventArgs e)
        {

            var user = people.Where(i => i.Login == tbLog.Text && i.Password == tbPass.Text).FirstOrDefault();

            if (user != null && !truecapcha)
            {

                if (chbRememb.IsChecked == true)
                {
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        streamWriter.Write(tbLog.Text + " " + tbPass.Text);
                        streamWriter.Close();
                    }
                }
                WindowSing windowSing = new WindowSing(user);
                windowSing.ShowDialog();


            }

            else if (user != null && truecapcha)
            {
                if (txtCapch.Text == tbCapch.Text)
                {
                    WindowSing windowSing = new WindowSing(user);
                    windowSing.ShowDialog();

                } 
            }

            else
            {
                MessageBox.Show("Пользователь не найден.");

                txtCapch.Text = GetString.GetCapcha();
                tbCapch.Visibility = Visibility.Visible;
                txtCapch1.Visibility = Visibility.Visible;
                txtCapch.Visibility = Visibility.Visible;
                imgCapcha.Visibility = Visibility.Visible;
                btnCapch.Visibility = Visibility.Visible;
                
                truecapcha = true;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void chbRememb_Checked(object sender, RoutedEventArgs e)
        {
            StreamReader streamReader = new StreamReader(path);
           

        }
    }
}
