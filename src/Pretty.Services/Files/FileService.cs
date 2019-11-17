using System;
using System.Collections.Generic;
using System.Linq;
using Pretty.Core.Data;
using Pretty.Core.Domain.Blogs;
using Pretty.Services.Dto;
using Pretty.Services.Extends;

namespace Pretty.Services.Files
{
    public class FileService : IFileService
    {
        private IRepository<BlogFile> _fileRepository;

        public FileService(IRepository<BlogFile> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public int InsertFile(IEnumerable<BlogFile> files)
        {
            return _fileRepository.Insert(files);
        }

        public int DeleteBlogFile(string blogFileId)
        {
            return _fileRepository.Delete(_fileRepository.GetById(blogFileId));
        }

        public Paged<BlogFile> GetBlogFiles(
            string userId = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _fileRepository.Table;

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(i => i.UserId == userId);
            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);

            query = query.OrderByDescending(i => i.CreateOn);

            var files = new PagedList<BlogFile>(
                query,
                pageIndex,
                pageSize
                );

            return files.GetDto();
        }

        public BlogFile GetBlogFile(string blogFileId)
        {
            return _fileRepository.GetById(blogFileId);
        }
    }
}
