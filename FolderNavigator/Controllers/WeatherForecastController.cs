using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderNavigator.Controllers
{
    [ApiController]
    [Route("/api/navigator")]
    public class NavigatorController : ControllerBase
    {
        private readonly ILogger<NavigatorController> _logger;

        public NavigatorController(ILogger<NavigatorController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("/api/navigator/exists")]
        public IActionResult Exists(string path)
        {
            var exists = Directory.Exists(path);
            var fileexists = System.IO.File.Exists(path);

            if (exists)
            {
                return Ok("Is A Directory"); 
            }
            else if(fileexists)
            {
                return Ok("Is A File");
            }
            else
            {
                return NotFound(); 
            }
        }


        [HttpGet]
        [Route("/api/navigator/rootpath")]
        public IActionResult GetRootPath()
        {
            var root = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());

            return Ok(root); 
        }


        [HttpGet]
        [Route("/api/navigator/info")]
        public IActionResult GetSubFolderandFiles(string path)
        {
            var exists = Directory.Exists(path);
            var fileexists = System.IO.File.Exists(path); 
            if (exists == false)
            {
                if (fileexists == true)
                {
                    return NotFound("Can not navigate a file");
                }
                return NotFound(); 
            }
            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path,"*", SearchOption.TopDirectoryOnly);

            return Ok(new FolderNavigator(directories, files,path));
        }

    }



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
}
