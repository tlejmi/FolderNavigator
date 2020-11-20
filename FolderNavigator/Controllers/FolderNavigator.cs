using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FolderNavigator.Controllers
{
    public class FolderNavigator
    {
        public FolderNavigator(string[] folders, string[] files, string currentPath)
        {
            Folders = folders;
            Files = files;
            CurrentPath = currentPath;
        }
        public string CurrentPath { get; set; }
        public string[] Folders { get; set; }

        public string[] Files { get; set; }
    }



    public class Items
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
