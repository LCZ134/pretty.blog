using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Blogs;
using Pretty.Services.Dto;
using Pretty.Services.Settings;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;

namespace Pretty.Web.Controllers
{    
    public class FileController : PrettyController
    {
        private IBlogFileModelFacroty _blogfileModelFactory;

        public FileController(IBlogFileModelFacroty blogfileModelFactory)
        {
            _blogfileModelFactory = blogfileModelFactory;
        }

        [HttpGet]
        [Route("api/[Controller]")]
        public IActionResult Get(FetchFileArgs args)
        {
            Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") });
            return _blogfileModelFactory.GetFile(args);
        }

        [HttpPost]
        [Route("api/[Controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public IEnumerable<ActResult<BlogFile>> Post()
        {
            var files = Request.Form.Files;            
            return _blogfileModelFactory.SaveFiles(files);
        }

        [HttpPost]
        [Route("api/[Controller]/imgUpload")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public WangEditorUploadModel UploadImg()
        {
            var files = Request.Form.Files;
            return new WangEditorUploadModel
            {
                Errno = 0,
                Data = _blogfileModelFactory.SaveFiles(files).Select(i=>i.Extends.Path).ToArray()
            };
        }
    }
}