using System.IO;

namespace SmModStudio.Bindings
{

    public class ProjectListItem
    {

        public ProjectListItem(string path)
        {
            Path = path;
            Name = new DirectoryInfo(path).Name;
        }
        
        public string Name { get; }
        public string Path { get; }

    }

}