using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.FrameWork.MVC.Helpers.Files;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FM.Portal.Domain
{
    public class BannerService : IBannerService
    {
        private readonly IBannerDataSource _dataSource;
        private readonly IAttachmentService _attachmentService;
        public BannerService(IBannerDataSource dataSource
                            , IAttachmentService attachmentService)
        {
            _dataSource = dataSource;
            _attachmentService = attachmentService;
        }

        public Result<Banner> Add(Banner model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Banner>.Failure(message: validateResult.Message);
            model.ID = Guid.NewGuid();
            return _dataSource.Insert(model);
        }

        public Result Delete(Guid ID)
        {
            try
            {
                var attachment = _attachmentService.List(ID);
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

        public Result<Banner> Edit(Banner model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<Banner>.Failure(message: validateResult.Message);
            return _dataSource.Update(model);
        }

        public Result<Banner> Get(Guid ID)
        => _dataSource.Get(ID);

        public Result<List<Banner>> List(BannerListVM listVM)
        {
            var table = ConvertDataTableToList.BindList<Banner>(_dataSource.List(listVM));
            if (table.Count > 0 || table.Count == 0)
                return Result<List<Banner>>.Successful(data: table);
            return Result<List<Banner>>.Failure();
        }

        private Result ValidateModel(Banner model)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.Description))
                errors.Add("توضیحات کوتاه را وارد نمایید");
            if (string.IsNullOrEmpty(model.Name))
                errors.Add("نام بنر وارد نمایید");
            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));
            return Result.Successful();
        }
    }
}
