using System.IO;

namespace SmModStudio.Core.Bindings
{

    public class ProjectItemBinding
    {

        public ProjectItemBinding(string path)
        {
            Path = path;
            Name = new DirectoryInfo(path).Name;
        }

        public string Name { get; }
        public string Path { get; }

    }

}