using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace SmModStudio.Core.Models
{

    public class InventoryDescriptionModel : INotifyPropertyChanged
    {

        [JsonProperty("title")] private string _title;
        [JsonProperty("description")] private string _description;

        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonIgnore]
        public string Title
        { 
            get => _title;
            set
            {
                if(_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}