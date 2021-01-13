using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.Core.Security;
using FM.Portal.DataSource;
using FM.Portal.Core.Owin;
using FM.Portal.Core;
using System.Collections.Generic;
using FM.Portal.Core.Common;
using System.Linq;
using System.Web.Mvc;
using FM.Portal.Core.Infrastructure;

namespace FM.Portal.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserDataSource _dataSource;
        private readonly IRequestInfo _requestInfo;
        private ILocaleStringResourceService _localeStringResourceService;
        private readonly IActivityLogService _activityLogService;
        private readonly IWebHelper _webHelper;
        public UserService(IUserDataSource dataSource
                         , IRequestInfo requestInfo
                         , IActivityLogService activityLogService
                         , IWebHelper webHelper)
        {
            _dataSource = dataSource;
            _requestInfo = requestInfo;
            _activityLogService = activityLogService;
            _webHelper = webHelper;
        }
        public Result<User> Add(User model)
        {
            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<User>.Failure(message: validateResult.Message);

            model.ID = Guid.NewGuid();
            model.Password = model.Password.HashText();
            model.PasswordExpireDate = DateTime.Now.AddMonths(6);

            var result = _dataSource.Insert(model);

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.AddUser").Data ?? "افزودن کاربر",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "AddUser"
            });

            return result;
        }

        public Result<User> Get(Guid ID)
        {
            return _dataSource.Get(ID, null, null, null, UserType.Unknown);
        }

        public Result<User> Get(string Username, string Password, string NationalCode, UserType userType)
        {
            if (Password == null || Password == "")
                return _dataSource.Get(null, Username, null, NationalCode, userType);

            return _dataSource.Get(null, Username, Password.HashText(), NationalCode, userType);
        }

        public Result<List<User>> List()
        {
            var table = ConvertDataTableToList.BindList<User>(_dataSource.List());
            if (table.Count > 0)
                return Result<List<User>>.Successful(data: table);
            return Result<List<User>>.Failure();
        }

        public Result ResetPassword(Guid UserID)
        {
            var userResult = Get(UserID);
            if (!userResult.Success)
                return Result.Failure(message: "کاربر یافت نشد");
            var user = userResult.Data;
            var result = _dataSource.SetPassword(new SetPasswordVM { NewPassword = user.NationalCode.HashText(), UserID = user.ID });

            if (result.Success)
            {
                _activityLogService.Add(new ActivityLog()
                {
                    Comment = _localeStringResourceService.GetResource("ActivityLog.ResetPasswordUser").Data ?? "ریست کردن رمز عبور کاربر",
                    EntityID = UserID,
                    EntityName = "User",
                    IpAddress = _webHelper.GetCurrentIpAddress(),
                    SystemKeyword = "ResetPasswordUser"
                });
            }
            return result;
        }

        public Result SetPassword(SetPasswordVM model)
        {
            if (model.UserName == model.NewPassword)
                return Result.Failure(message: "نام کاربری و رمز عبور یکسان است");

            if (model.OldPassword == model.NewPassword)
                return Result.Failure(message: "کلمه عبور جدید نباید با کلمه عبور قبلی یکسان باشد");

            var userID = Guid.Empty;

            if (_requestInfo.UserId != null)
                userID = (Guid)_requestInfo.UserId;
            else
                userID = model.UserID;

            var userResult = _dataSource.Get(userID, null, model.OldPassword.HashText(), null, UserType.Unknown);
            if (!userResult.Success)
                return Result<User>.Failure(message: userResult.Message);

            if (userResult.Data == null)
                return Result.Failure(message: "کلمه عبور فعلی اشتباه است");

            model.NewPassword = model.NewPassword.HashText();
            var result = _dataSource.SetPassword(model);

            if (result.Success)
            {
                _activityLogService.Add(new ActivityLog()
                {
                    Comment = _localeStringResourceService.GetResource("ActivityLog.ChangePasswordUser").Data ?? "تغییر رمز عبور کاربر",
                    EntityID = userResult.Data.ID,
                    EntityName = model.GetType().Name,
                    IpAddress = _webHelper.GetCurrentIpAddress(),
                    SystemKeyword = "ChangePasswordUser"
                });
            }

            return result;
        }

        public Result<User> Edit(User model)
        {
            if (_localeStringResourceService == null)
                _localeStringResourceService = (ILocaleStringResourceService)DependencyResolver.Current.GetService(typeof(ILocaleStringResourceService));


            var validateResult = ValidateModel(model);
            if (!validateResult.Success)
                return Result<User>.Failure(message: validateResult.Message);

            var userResult = Get(model.ID);
            if (!userResult.Success || userResult.Data == null)
                return Result<User>.Failure(message: userResult.Message);

            var user = userResult.Data;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.NationalCode = model.NationalCode;
            user.CellPhone = model.CellPhone;

            var result = _dataSource.Update(user);

            _activityLogService.Add(new ActivityLog()
            {
                Comment = _localeStringResourceService.GetResource("ActivityLog.UpdateUser").Data ?? "ویرایش کاربر",
                EntityID = model.ID,
                EntityName = model.GetType().Name,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                SystemKeyword = "UpdateUser"
            });

            return result;

        }

        private Result ValidateModel(User model)
        {
            if (_localeStringResourceService == null)
                _localeStringResourceService = (ILocaleStringResourceService)DependencyResolver.Current.GetService(typeof(ILocaleStringResourceService));

            var errors = new List<string>();
            if (string.IsNullOrEmpty(model.FirstName))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.Firstname.ErrorMessage").Data ?? "نام راوارد نمایید");

            if (string.IsNullOrEmpty(model.LastName))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.LastName.ErrorMessage").Data ?? "نام خانوادگی را وارد نمایید");

            if (string.IsNullOrEmpty(model.NationalCode))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.NationalCode.ErrorMessage").Data ?? "کد ملی را وارد نمایید");

            if (!string.IsNullOrEmpty(model.NationalCode) && (model.NationalCode.Trim().Length < 10 || model.NationalCode.Trim().Length > 10))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.NationalCode.Format.ErrorMessage").Data ?? "کد ملی را صحیح وارد نمایید");

            if (string.IsNullOrEmpty(model.CellPhone))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.CellPhone.ErrorMessage").Data ?? "شماره همراه را وارد نمایید");

            if (!string.IsNullOrEmpty(model.CellPhone) && (model.CellPhone.Trim().Length < 11 || model.CellPhone.Trim().Length > 11))
                errors.Add(_localeStringResourceService.GetResource("Account.Register.Field.CellPhone.Format.ErrorMessage").Data ?? "شماره همراه را صحیح وارد نمایید");

            if (errors.Any())
                return Result.Failure(message: string.Join("&&", errors));

            return Result.Successful();
        }
    }
}
