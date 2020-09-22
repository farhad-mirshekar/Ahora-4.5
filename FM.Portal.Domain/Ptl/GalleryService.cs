using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Result;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using System;
using System.Collections.Generic;

namespace FM.Portal.Domain
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryDataSource _dataSource;
        private readonly ITagsService _tagsService;
        private readonly IAttachmentService _attachmentService;
        public GalleryService(IGalleryDataSource dataSource
                              , ITagsService tagsService
                              , IAttachmentService attachmentService)
        {
            _dataSource = dataSource;
            _tagsService = tagsService;
            _attachmentService = attachmentService;
        }
        public Result<Gallery> Add(Gallery model)
        {
            model.ID = Guid.NewGuid();
            if (model.Tags != null && model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        {
            try
            {
                var attachment = _attachmentService.List(ID);
                _tagsService.Delete(ID);
                if (attachment.Data.Count > 0)
                {
                    foreach (var item in attachment.Data)
                    {
                        string path = $"{Enum.GetName(typeof(PathType), item.PathType)}/{item.FileName}";
                        _attachmentService.Delete(item.ID);
                        FileHelper.DeleteFile(path);
                    }
                }
                return _dataSource.Delete(ID);
            }
            catch(Exception e)
            {
                return _dataSource.Delete(ID);
            }
        }

        public Result<Gallery> Edit(Gallery model)
        {
            if (model.Tags != null && model.Tags.Count > 0)
            {
                var tags = new List<Tags>();
                foreach (var item in model.Tags)
                {
                    tags.Add(new Tags { Name = item });
                }
                _tagsService.Insert(tags, model.ID);
            }
            else
            {
                _tagsService.Delete(model.ID);
            }
            return _dataSource.Update(model);
        }

        public Result<Gallery> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<Gallery> Get(string TrackingCode)
        => _dataSource.Get(TrackingCode);

        public Result<List<Gallery>> List(GalleryListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Gallery>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Gallery>>.Successful(data: table);
            return Result<List<Gallery>>.Failure();
        }
    }
}
