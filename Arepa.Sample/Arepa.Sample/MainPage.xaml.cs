using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Arepa.Sample.Controls;
using Arepa.Sample.DataAdapter;
using Arepa.Sample.Infrastructure.POCO;
using Arepa.Sample.Templates;
using Xamarin.Forms;

namespace Arepa.Sample
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
        }

        string _SelectedItemDescription = default;

        public string SelectedItemDescription
        {
            get => _SelectedItemDescription;
            set => SetProperty(ref _SelectedItemDescription, value);
        }

        string _SelectedImage = default;

        public string SelectedImage
        {
            get => _SelectedImage;
            set => SetProperty(ref _SelectedImage, value);
        }

        string _SelectionText = default;

        public string SelectionText
        {
            get => _SelectionText;
            set => SetProperty(ref _SelectionText, value);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var picker = new GenericPicker(new ArepaDataAdapter(), new DataTemplate( typeof(ArepaTemplateView)),
                (selectedItem) =>
                {
                    var arepa = selectedItem as ArepaItem;
                    this.SelectedImage = arepa.ImageName;
                    this.SelectedItemDescription =
                        $"Ingredients : {arepa.Ingredients} Region: {arepa.Region}";

                    SelectionText = arepa.Name;
                    Navigation.PopModalAsync(true);

                });
            Navigation.PushModalAsync(picker);

        }

        private void SelectTeam(object sender, EventArgs e)
        {
            var picker = new GenericPicker(new FootballTeamDataAdapter(),
                new DataTemplate(typeof(FootballTeamTemplateView)), (selectedItem) =>
                {
                    var team = selectedItem as FootballTeamItem;
                    this.SelectedImage = team.ImageName;
                    this.SelectedItemDescription =
                        $"Location : {team.Location} Stadium: {team.StadiumName}";
                    SelectionText = team.Name;
                    Navigation.PopModalAsync(true);
                });
            Navigation.PushModalAsync(picker);

        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
