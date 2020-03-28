

using System;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.Core.Security;
using FM.Portal.DataSource;
using FM.Portal.Core.Owin;
using FM.Portal.Core.Result;

namespace FM.Portal.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserDataSource _dataSource;
        private readonly IRequestInfo _requestInfo;
        public UserService(IUserDataSource dataSource, IRequestInfo requestInfo)
        {
            _dataSource = dataSource;
            _requestInfo = requestInfo;
        }
        public Result<User> Add(User model)
        {
            try
            {
                model.ID = Guid.NewGuid();
                model.Password = model.Password.HashText();
                model.PasswordExpireDate = DateTime.Now.AddMonths(6);

                return _dataSource.Insert(model);
            }
            catch (Exception e) { throw; }
        }

        public Result<User> Get(Guid ID)
        {
            return _dataSource.Get(ID, null, null, null);
        }

        public Result<User> Get(string Username, string Password, string NationalCode)
        {
            if (Password == null || Password == "")
                return _dataSource.Get(null, Username, null, NationalCode);

            return _dataSource.Get(null, Username, Password.HashText(), NationalCode);
        }

        public Result SetPassword(SetPasswordVM model)
        {
            if (_requestInfo.UserName == model.NewPassword)
                return Result.Failure(message: "نام کاربری و رمز عبور یکسان است");

            if (model.OldPassword == model.NewPassword)
                return Result.Failure(message: "کلمه عبور جدید نباید با کلمه عبور قبلی یکسان باشد");

            var userID = Guid.Empty;

            if (_requestInfo.UserId != null)
                userID = (Guid)_requestInfo.UserId;
            else
                userID = model.UserID;
            var userResult = _dataSource.Get(userID, null, model.OldPassword.HashText(), null);
            if (!userResult.Success)
                return Result<User>.Failure();
            if (userResult.Data.ID == Guid.Empty)
                return Result.Failure(message: "کلمه عبور فعلی اشتباه است");
            model.NewPassword = model.NewPassword.HashText();
            return _dataSource.SetPassword(model);
        }

        public Result<User> Update(User model)
        {
            return _dataSource.Update(model);
        }
    }
}
