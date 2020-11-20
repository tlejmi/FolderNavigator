using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        [HttpGet]
        [Route("/api/navigator/info")]
        public IActionResult GetSubFolder([FromForm] string path)
        {
            if (path == "/")
            {
                path = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
            }

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

            var pathitem = new List<Items>();

            var root = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());

            if (path != root)
            {
                pathitem.Add(new Items()
                {
                    Name = "..",
                    Type = "folder"
                });
            }

            var directories = Directory.GetDirectories(path);

            foreach (var dir in directories)
            {
                pathitem.Add(new Items()
                {
                    Name = Path.GetFileName(dir),
                    Type = "folder"
                });
            }

            var content = new Dictionary<string, List<Items>>(); 


            var files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                var fileinfo = Path.GetExtension(file);
                var filename = Path.GetFileName(file);

                pathitem.Add(new Items()
                {
                    Name = filename,
                    Type = fileinfo
                });
            }

            return Ok(pathitem);
        }
    }
}