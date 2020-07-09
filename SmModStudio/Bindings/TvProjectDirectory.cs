using System.Collections.ObjectModel;

namespace SmModStudio.Bindings
{
    
    public class TvProjectDirectory
    {
        
        public string Name { get; set; }
        public string Path { get; set; }
        
        public ObservableCollection<TvProjectDirectory> Directories { get; set; }
        public ObservableCollection<TvProjectFile> Files { get; set; }
        
    }
    
}