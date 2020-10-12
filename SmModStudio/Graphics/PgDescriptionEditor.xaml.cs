using System.Windows;
using SmModStudio.Core.Enums;
using SmModStudio.Core.Models;

namespace SmModStudio.Graphics
{

    public partial class PgDescriptionEditor
    {

        private readonly string _descriptionPath;
        private readonly ModDescriptionModel _descriptionModel;

        #region Methods

        public PgDescriptionEditor(string descriptionPath)
        {
            InitializeComponent();
            _descriptionPath = descriptionPath;
            _descriptionModel = ModDescriptionModel.Load(_descriptionPath);
            ProjectNameBox.Text =  _descriptionModel.Name;
            ProjectTypeBox.SelectedIndex =  _descriptionModel.Type switch
            {
                ModType.TerrainAssets => 1,
                _ => 0
            };
            ProjectDescriptionBox.Text =  _descriptionModel.Description;
        }

        #endregion

        #region Events

        public void Save(object sender, RoutedEventArgs args)
        {
            _descriptionModel.Name = ProjectNameBox.Text;
            _descriptionModel.Type = ProjectTypeBox.SelectedIndex switch
            {
                1 => ModType.TerrainAssets,
                _ => ModType.BlocksAndParts
            };
            _descriptionModel.Description = ProjectDescriptionBox.Text;
        }

        #endregion

    }

}