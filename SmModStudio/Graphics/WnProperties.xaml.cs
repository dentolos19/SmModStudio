using System;
using System.Windows;
using SmModStudio.Core.Models;

namespace SmModStudio.Graphics
{

    public partial class WnProperties
    {

        private readonly ModDescription _description;

        public WnProperties(string path)
        {
            _description = ModDescription.Load(path);
            InitializeComponent();
            TitleBox.Text = _description.Title;
            TypeBox.Text = _description.Type;
            VersionBox.Value = _description.Version;
            IdBox.Text = _description.Id;
            DescriptionBox.Text = _description.Description;
        }

        private void SaveProperties(object sender, RoutedEventArgs args)
        {
            _description.Title = TitleBox.Text;
            _description.Type = TypeBox.Text;
            _description.Version = (int)VersionBox.Value;
            _description.Id = IdBox.Text;
            _description.Description = DescriptionBox.Text;
            _description.Save();
            Close();
        }

        private void RenewId(object sender, RoutedEventArgs args)
        {
            IdBox.Text = Guid.NewGuid().ToString();
        }

    }

}