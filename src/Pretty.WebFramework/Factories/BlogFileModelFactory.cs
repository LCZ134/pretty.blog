using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Pretty.Core;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Helpers;
using Pretty.Services.Dto;
using Pretty.Services.Files;
using Pretty.Services.Settings;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public class BlogFileModelFactory : IBlogFileModelFacroty
    {
        private const string FILE_VIRTUAL_PATH = "./api/file";
        private IWorkContext _workContext;
        private IFileService _fileService;
        private ISettingService _settingServce;
        private IFileHelper _fileHelper;

        public BlogFileModelFactory(
            IWorkContext workContext,
            IFileService fileService,
            ISettingService settingServce,
            IFileHelper fileHelper)
        {
            _workContext = workContext;
            _fileService = fileService;
            _settingServce = settingServce;
            _fileHelper = fileHelper;
        }

        public Paged<BlogFile> GetBlogFiles(FilePagingFilter filter)
        {
            return _fileService.GetBlogFiles(filter.UserId,
                filter.PageIndex,
                filter.PageSize);
        }

        public ActResult<string> DeleteBlogFile(string fileId)
        {
            int result = _fileService.DeleteBlogFile(fileId);
            return ActResultFactory.GetActResult(result);
        }

        public IEnumerable<ActResult<BlogFile>> SaveFiles(IFormFileCollection files)
        {
            var saveResult = files.Select(SaveFile).ToList();
            var fileEntities = saveResult
                .Where(i => i.StatusCode == StatusCode.Success)
                .Select(i => i.Extends);
            _fileService.InsertFile(fileEntities);

            return saveResult;
        }

        private ActResult<BlogFile> SaveFile(IFormFile file)
        {
            var userId = _workContext.User?.Id;

            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            string savePath = Path.Combine(
                _settingServce.Get("pretty_file_path").Value,
                nowDate);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var suffix = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";

            string fileFullName = Path.Combine(savePath, fileName);

            try
            {
                using (FileStream fs = File.Create(fileFullName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            catch
            {
                return new ActResult<BlogFile>(StatusCode.Failed, "保存失败", null);
            }

            return new ActResult<BlogFile>(
                StatusCode.Success,
                "保存成功",
                new BlogFile
                {
                    FileSize = file.Length,
                    Name = fileName,
                    Path = $"{FILE_VIRTUAL_PATH}?date={nowDate}&fileName={fileName}",
                    Suffix = suffix,
                    UserId = userId
                });
        }

        public IEnumerable<BlogFile> GetBlogFiles()
        {
            throw new NotImplementedException();
        }

        public IActionResult GetFile(FetchFileArgs args)
        {
            var savePath = _settingServce.Get("pretty_file_path").Value;
            var filePath = Path.Combine(
                savePath,
                args.Date.ToString("yyyy-MM-dd"));
            var fileFullName = Path.Combine(
                filePath,
                args.FileName);

            if (!File.Exists(fileFullName)) return new StatusCodeResult(404);

            if (args.Flag > 0 && _fileHelper.IsImg(fileFullName))
            {
                var compressPath = Path.Combine(filePath, "_compress", args.FileName);
                if (File.Exists(compressPath) ||
                    _fileHelper.CompressionImage(
                        fileFullName,
                        Path.Combine(filePath, "_compress"),
                        args.FileName,
                        args.Flag))
                {
                    fileFullName = compressPath;
                }
            }
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileFullName).ToLower();
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            FileStream fs = new FileStream(fileFullName, FileMode.Open);
            var fsr = new FileStreamResult(fs, mimeType);
            return new FileStreamResult(fs, mimeType);
        }
    }
}
