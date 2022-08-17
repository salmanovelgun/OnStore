using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lesson5WPFhomework
{
    public class Product : INotifyPropertyChanged
    {
        private string? name;
        private double? price;
        private int? countInStock;

        public Product(string? name, double? price, int? countInStock)
        {
            Name = name;
            Price = price;
            CountInStock = countInStock;
        }



        public string? Name 
        { 
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public double? Price
        { 
            get => price; 
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public int? CountInStock 
        { 
            get => countInStock; 
            set
            {
                countInStock = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
