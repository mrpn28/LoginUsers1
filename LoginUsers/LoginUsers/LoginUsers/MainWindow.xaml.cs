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

namespace LoginUsers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string captcha;
        public MainWindow()
        {
            InitializeComponent();
        }
        Windows.WindowTableware tableware = new Windows.WindowTableware();

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            //СПОСОБ 2
            try
            {
                if (string.IsNullOrEmpty(tbLogin.Text))
                {
                    MessageBox.Show("Введите логин!");
                    return;
                }

                if (string.IsNullOrEmpty(tbPassword.Text))
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }

                App.currentUser = App.Dbtableware.Users.Where(user => user.login == tbLogin.Text &&
                user.password == tbPassword.Text).First<Users>();

                if (App.currentUser.RoleWorker.role_worker == "Администратор")
                {
                    Visibility = Visibility.Hidden;
                    tableware.ShowDialog();
                    Visibility = Visibility.Visible;
                }
                if (App.currentUser.RoleWorker.role_worker == "Менеджер")
                {
                    MessageBox.Show("Вы менеджер");
                    Visibility = Visibility.Hidden;
                    tableware.btnAddProducts.Visibility = Visibility.Collapsed;
                    tableware.btnEditProducts.Visibility = Visibility.Collapsed;
                    tableware.btnDeleteProducts.Visibility = Visibility.Collapsed;
                    tableware.ShowDialog();
                    Visibility = Visibility.Visible;
                }
                if (App.currentUser.RoleWorker.role_worker == "Клиент")
                {
                    MessageBox.Show("Вы Клиент");
                    Visibility = Visibility.Hidden;
                    tableware.btnAddProducts.Visibility = Visibility.Collapsed;
                    tableware.btnEditProducts.Visibility = Visibility.Collapsed;
                    tableware.btnDeleteProducts.Visibility = Visibility.Collapsed;
                    tableware.ShowDialog();
                    Visibility = Visibility.Visible;
                }
            }
            catch
            {
                MessageBox.Show("Такого пользователя нет! Необходимо пройти проверку");
                btnEnter.IsEnabled = false;
                lbcaptcha.Visibility = Visibility.Visible;
                tbCaptcha.Visibility = Visibility.Visible;
                pnlCaptcha.Visibility = Visibility.Visible;
                GenerateCaptcha();
            }
        }

        private string GenerateCaptcha()
        {
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                int charType = random.Next(0, 3);
                int charValue = 0;

                if (charType == 0)
                {
                    charValue = random.Next(48, 58);
                }
                else
                {
                    charValue = random.Next(65, 91);

                    if (charType == 2)
                    {
                        charValue = charValue + 32;
                    }
                }

                captcha += (char)charValue;
            }
            tbCaptcha.Text = captcha;
            return captcha;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void tbCheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (tbCaptchaInput.Text.Equals(captcha))
            {
                captcha = null;
                MessageBox.Show("Успешно. Повторите авторизацию");
                tbLogin.Clear();
                tbCaptchaInput.Clear();
                tbPassword.Clear();
                tbCaptcha.Clear();
                lbcaptcha.Visibility = Visibility.Collapsed;
                tbCaptcha.Visibility = Visibility.Collapsed;
                pnlCaptcha.Visibility = Visibility.Collapsed;
                btnEnter.IsEnabled = true;
            }
            else
            {
                tbCaptcha.Clear();
                captcha = null;
                MessageBox.Show("Повторите попытку");
                GenerateCaptcha();
            }
        }

        private void tbGuest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы вошли как гость");
            Visibility = Visibility.Hidden;
            tableware.btnAddProducts.Visibility = Visibility.Collapsed;
            tableware.btnEditProducts.Visibility = Visibility.Collapsed;
            tableware.btnDeleteProducts.Visibility = Visibility.Collapsed;
            tableware.ShowDialog();
        }
    }
}
