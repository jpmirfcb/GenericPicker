using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Arepa.Sample.DataAdapter;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Arepa.Sample.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericPicker : ContentPage
    {
        public ICommand SearchTextCommand { get; set; }
        public ICommand LoadItemsCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }

        private int currentOffset = 0;
        private Action<object> OnItemPickedAction;

        public GenericPicker(IPickerDataAdapter adapter, DataTemplate itemsTemplate, Action<object> onItemPickedAction)
        {
            InitializeComponent();
            DataAdapter = adapter;
            SearchTextCommand = new Command(ExecuteSearchCommand);
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            SelectedItemCommand = new Command(ExecuteSelectedItemCommand);
            ItemTemplate = itemsTemplate;
            OnItemPickedAction = onItemPickedAction;
            LoadData();
        }



        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(GenericPicker), null, BindingMode.Default, null, ItemTemplatePropertyChangeHandler);

        private static void ItemTemplatePropertyChangeHandler(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is GenericPicker control)) return;
            if (!(newvalue is DataTemplate template)) return;

            control.CollectionViewControl.ItemTemplate = template;
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set
            {
                SetValue(ItemTemplateProperty, value);
                OnPropertyChanged("ItemTemplate");
            }
        }


        public static readonly BindableProperty SearchProperty =
            BindableProperty.Create("Search", typeof(string), typeof(GenericPicker), null, BindingMode.Default, null, null);

        public string Search
        {
            get => (string)GetValue(SearchProperty);
            set
            {
                SetValue(SearchProperty, value);
                OnPropertyChanged("Search");
            }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(ObservableRangeCollection<object>), typeof(GenericPicker), null, BindingMode.Default, null, null);

        public ObservableRangeCollection<object> ItemsSource
        {
            get => (ObservableRangeCollection<object>)GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                OnPropertyChanged("ItemsSource");
            }
        }

        public static readonly BindableProperty DataAdapterProperty =
            BindableProperty.Create("DataAdapter", typeof(IPickerDataAdapter), typeof(GenericPicker), null, BindingMode.Default, null, null);

        public IPickerDataAdapter DataAdapter
        {
            get => (IPickerDataAdapter)GetValue(DataAdapterProperty);
            set
            {
                SetValue(DataAdapterProperty, value);
                OnPropertyChanged("DataAdapter");
            }
        }

        private void LoadData()
        {
            if (DataAdapter == null) throw new Exception("You must define a data adapter before using this control");

            SetBusyState(true);

            Task.Run(async () =>
            {
                try
                {
                    var response = await DataAdapter.LoadDataAsync(currentOffset, Search);
                    if (response.Success)
                    {
                        if (ItemsSource == null) ItemsSource = new ObservableRangeCollection<object>();
                        ItemsSource.AddRange(response.Results.Cast<object>());
                        currentOffset = ItemsSource.Count;
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", e.Message, "OK");
                }
            })
                .ContinueWith((t) =>
                {
                    SetBusyState(false);
                });
        }


        private void SetBusyState(bool state)
        {
            Device.BeginInvokeOnMainThread(() => { IsBusy = state; });
        }

        private void ExecuteSearchCommand()
        {
            currentOffset = 0;
            ItemsSource.Clear();
            LoadData();
        }

        private void ExecuteLoadItemsCommand()
        {
            LoadData();
        }

        private void ExecuteSelectedItemCommand(object selection)
        {
            if (selection == null) return;
            //DisplayAlert(selection.ToString(), "SELECTED", "OK");
            OnItemPickedAction?.Invoke(selection);
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            ExecuteSearchCommand();
        }
    }
}