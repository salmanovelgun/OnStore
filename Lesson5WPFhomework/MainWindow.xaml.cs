using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lesson5WPFhomework
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool StateClosed = true;
        public ObservableCollection<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Products = new ObservableCollection<Product>()
            {
                new Product("Coco-Cola", 1, 30),
                new Product("Fanta", 1, 30),
                new Product("Lays", 3, 20),
                new Product("Chipsim", 2, 10)
            };
            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CartIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (StateClosed)
            {
                BasketBoard.Width = 200;
                CartIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowLeftBold;
                TerminalPanel.Visibility = Visibility.Visible;
                piCart.Visibility = Visibility.Visible;
                Paynment.Visibility = Visibility.Visible;
            }
            else
            {
                BasketBoard.Width = 50;
                CartIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cart;
                TerminalPanel.Visibility = Visibility.Hidden;
                piCart.Visibility = Visibility.Hidden;
                Paynment.Visibility = Visibility.Hidden;
            }

            StateClosed = !StateClosed;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FinalPrice.Content = double.Parse(FinalPrice.Content.ToString()) + Products[lbProducts.SelectedIndex].Price;
            Products[lbProducts.SelectedIndex].CountInStock--;
        }

        private void EnteredMoney_TextChanged(object sender, TextChangedEventArgs e)
        {
            double number;

            bool success = double.TryParse(EnteredMoney.Text, out number);
            if (success)
            {
                if (number > double.Parse(FinalPrice.Content.ToString()))
                    Resiude.Content = number - double.Parse(FinalPrice.Content.ToString());
                else
                    Resiude.Content = 0;
            }
            else
                Resiude.Content = 0;
        }

        private void Paynment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double number;

            bool success = double.TryParse(EnteredMoney.Text, out number);
            if (success)
            {
                if (number > double.Parse(FinalPrice.Content.ToString()))
                {
                    MessageBox.Show($"{Resiude.Content}$ were returned to you.");
                    EnteredMoney.Text = "";
                    FinalPrice.Content = 0;
                    Resiude.Content = 0;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Add
            var addWD = new AddEdit(new Product(null, null, null));
            addWD.AddEditIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.BurgerAdd;
            addWD.ShowDialog();
            Products.Add(addWD.TProduct);
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Edit
            var addWD = new AddEdit(Products[lbProducts.SelectedIndex]);
            addWD.AddEditIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.EditOutline;
            addWD.ShowDialog();
            Products[lbProducts.SelectedIndex] = addWD.TProduct;
        }

        private void HelpBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Click to three point, then \nselect \"Logout\" to shutdown.", "Help", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}
