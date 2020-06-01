using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.VisualBasic;

namespace WpfAppExceptionDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public class CheckboxItem : INotifyPropertyChanged
    {
        public CheckboxItem(bool isChecked)
        {
            IsChecked = isChecked;
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get => _isChecked;
            set => SetField(ref _isChecked, value);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public partial class MainWindow : Window
    {
        public NotifyCatalogViewModel<CheckboxItem> ComboBoxCatalog { get; set; }

        public MainWindow()
        {
            DataContext = this;

            ComboBoxCatalog = new NotifyCatalogViewModel<CheckboxItem>();

            ComboBoxCatalog.Collection.Add(new CheckboxItem(false));
            ComboBoxCatalog.Collection.Add(new CheckboxItem(false));
            ComboBoxCatalog.Collection.Add(new CheckboxItem(true));
            ComboBoxCatalog.Collection.Add(new CheckboxItem(true));
            ComboBoxCatalog.Collection.Add(new CheckboxItem(true));
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ComboBoxCatalog.Collection.First().IsChecked = true;
        }
    }

    public class NotifyCatalogViewModel<TItem> where TItem : class, INotifyPropertyChanged
    {
        public ObservableCollection<TItem> Collection { get; set; } = new ObservableCollection<TItem>();

        public NotifyCatalogViewModel()
        {
            Collection = new ObservableCollection<TItem>();
            Collection.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= ItemPropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += ItemPropertyChanged;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException("ToDo");
            //PropertyItemChanged(sender, e);
        }

        public event PropertyChangedEventHandler PropertyItemChanged = delegate { };
    }
}