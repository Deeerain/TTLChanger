using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TTLChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly TTLManager TTLManager;

        public MainWindow()
        {   
            InitializeComponent();

            try
            {
                TTLManager = new TTLManager();

                v4TTLTextBox.Text = TTLManager.V4TTL.ToString();
                v6TTLTextBox.Text = TTLManager.V6TTL.ToString();
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("Попробуйте запустить программу от имени администратора");
                App.Current.Shutdown();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string client_v4 = v4TTLTextBox.Text;
                string client_v6 = v6TTLTextBox.Text;

                var dialogResult = MessageBox.Show("Вы используете эту программу на свой страх и риск. Вы уверены, что хотите сохранить изменения?", "", MessageBoxButton.YesNo);

                if(dialogResult == MessageBoxResult.Yes)
                {
                    if (v4TTLTextBox.Text != "-1")
                        TTLManager.V4TTL = v4TTLTextBox.Text;
                    if (v6TTLTextBox.Text != "-1")
                        TTLManager.V6TTL = v6TTLTextBox.Text;
                }

                if (TTLManager.IsChanged)
                {
                    var rebootDialog = MessageBox.Show("После изменений, рекомендуется перезагрузка. Перезагрузить компьютер?", "", MessageBoxButton.YesNo);

                    if (rebootDialog == MessageBoxResult.Yes)
                        RebootPC();

                    TTLManager.IsChanged = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void RebootPC()
        {
            Process.Start("shutdown", "/r /t 0");
        }
    }
}
