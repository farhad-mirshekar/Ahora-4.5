namespace FM.Portal.Core.Model
{

    public enum AttachmentType : byte
    {
        نامشخص = 0,
        اصلی = 1,
        ثانویه = 2,
        اخبار = 3,
        مقاله = 4,
        اسلایدر = 5,
        محصولات = 6,
        ویدیو = 7,
        رویداد=8,
        ویرایشگر = 9,
        فایل=10
    }
    public enum PathType : byte
    {
        unknown = 0,
        product = 6,
        article = 4,
        news = 3,
        slider = 5,
        video=7,
        events=8,
        editor=9,
        file=10
    }
    public enum UserType : byte
    {
        Unknown = 0,
        کاربر_درون_سازمانی = 1,
        کاربر_برون_سازمانی = 2
    }
    public enum CommandsType : byte
    {
        نامشخص = 0,

        برنامه = 1,
        منو = 2,
        صفحه = 3
    }
    public enum ShowArticleType : byte
    {
        عدم_نمایش = 0,
        نمایش = 1
    }
    public enum CommentArticleType : byte
    {
        بسته = 0,
        باز = 1
    }
    public enum PositionType : byte
    {
        Unknown = 0,
        راهبر = 100
    }
    public enum CommentType : byte
    {
        نامشخص = 0,
        در_حال_بررسی = 1,
        تایید = 2,
        عدم_تایید = 3
    }
    public enum DiscountType : byte
    {
        مبلغی = 1,
        درصدی = 2
    }
    public enum AttributeControlType : byte
    {
        کشویی = 1
    }
    public enum SendType : byte
    {
        آنلاین = 1,
        درب_منزل = 2
    }
    public enum EnableMenuType : byte
    {
        فعال = 1,
        غیرفعال = 2
    }
    public enum CommentForType : byte
    {
        unknown = 0,
        محصولات = 6,
        مقالات = 4,
        اخبار = 3,
        رویدادها = 8
    }
    public enum DocumentTypeForTags : byte
    {
        اخبار = 3,
        مقاله = 4,
        محصولات = 6,
        رویداد = 8
    }
}
