using System;
using System.IO;

namespace SmModStudio.Core
{

    public static class Constants
    {

        public const uint GameId = 387990;

        public static readonly string UsersDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Axolot Games", "Scrap Mechanic", "User");

    }

}