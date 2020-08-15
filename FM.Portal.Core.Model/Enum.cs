namespace FM.Portal.Core.Model
{

    public enum AttachmentType : byte
    {
        نامشخص = 0,
        اصلی = 1,
        ثانویه = 2
    }
    public enum PathType : byte
    {
        unknown = 0,
        pages = 1,
        banner = 2,
        product = 6,
        article = 4,
        news = 3,
        slider = 5,
        video = 7,
        events = 8,
        editor = 9,
        file = 10,
        gallery = 11
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
        نامشخص = 0,
        عدم_نمایش = 1,
        نمایش = 2
    }
    public enum CommentArticleType : byte
    {
        نامشخص = 0,
        بسته = 1,
        باز = 2
    }
    public enum PositionType : byte
    {
        Unknown = 0,
        رییس_امور = 5,
        کارشناس_امور = 10,
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
        نامشخص = 0,
        مبلغی = 1,
        درصدی = 2
    }
    public enum AttributeControlType : byte
    {
        نامشخص = 0,
        کشویی = 1
    }
    public enum SendType : byte
    {
        نامشخص = 0,
        آنلاین = 1,
        درب_منزل = 2
    }
    public enum EnableMenuType : byte
    {
        نامشخص = 0,
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
        نامشخص = 0,
        صفحات = 1,
        اخبار = 3,
        مقاله = 4,
        محصولات = 6,
        رویداد = 8
    }
    public enum BankName : byte
    {
        نامشخص = 0,
        بانک_ملی = 1
    }
    public enum PageType : byte
    {
        نامشخص = 0,
        داینامیک = 1,
        استاتیک = 2
    }
    public enum BannerType : byte
    {
        نامشخص = 0,
        تبلیغات = 1,
        صفحات = 2
    }

    public enum EmailStatusType : byte
    {
        unknown = 0,
        تحویل_داده_شده = 1,
        ناموفق = 2,
        تحویل_داده_نشده = 2
    }

    public enum DocumentType : byte
    {
        unknown = 0,
        محصولات = 1
    }
    public enum DocState : byte
    {
        نامشخص = 0,
        ثبت_درخواست = 1,
        بررسی_و_ارجاع_به_واحد_مالی = 10,
        بررسی_و_ارجاع_به_واحد_انبار = 15,
        آماده_بسته_بندی = 20,
        ارسال_محصول = 25

    }
    public enum SendDocumentType : byte
    {
        نامشخص = 0,
        تایید_ارسال = 1
    }
    public enum DepartmentType : byte
    {
        نامشخص = 0,
        سامانه_اصلی = 1,
        واحد_فروش = 2,
        واحد_مالی = 3
    }
    public enum ActionState : byte
    {
        نامشخص=0,
        موارد_در_دست_اقدام = 1
    }
}
