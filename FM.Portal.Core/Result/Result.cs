namespace FM.Portal.Core.Result
{
   public class Result
    {
        public bool Success { get; set; }

        public bool OK => Success;

        public int Code { get; set; }

        public string Message { get; set; }
        public static Result Failure(int code = -1, string message = "")
            => new Result { Success = false, Code = code, Message = message };
        public static Result Successful(int code = 0, string message = "")
            => new Result { Success = true, Code = code, Message = message };
        public string Description => Message;
        protected object _data;
    }
    public class Result<T> : Result
    {
        public T Data
        {
            get { return (T)_data; }
            set { _data = value; }
        }
        public static Result<T> Failure(int code = -1, string message = "", T data = default(T))
          => new Result<T> { Success = false, Code = code, Message = message, Data = data};
        public static Result<T> Successful(int code = 0, string message = "", T data = default(T))
           => new Result<T> { Success = true, Code = code, Message = message, Data = data };
    }
}
