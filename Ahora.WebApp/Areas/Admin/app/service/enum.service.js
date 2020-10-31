(function () {
	angular
		.module('portal')
		.factory('enumService', enumService);

	function enumService() { var enumService={};

			//enums in dll: FM.Portal.Core.Model.dll
			enumService.AttachmentType = {
				'1': 'اصلی',
				'2': 'ثانویه'
			},
			enumService.PathType = {
				'1': 'pages',
				'2': 'banner',
				'3': 'news',
				'4': 'article',
				'5': 'slider',
				'6': 'product',
				'7': 'video',
				'8': 'events',
				'9': 'editor',
				'10': 'file',
				'11': 'gallery'
			},
			enumService.UserType = {
				'1': 'کاربر درون سازمانی',
				'2': 'کاربر برون سازمانی'
			},
			enumService.CommandsType = {
				'1': 'برنامه',
				'2': 'منو',
				'3': 'صفحه'
			},
			enumService.ViewStatusType = {
				'1': 'عدم نمایش',
				'2': 'نمایش'
			},
			enumService.CommentArticleType = {
				'1': 'بسته',
				'2': 'باز'
			},
			enumService.PositionType = {
				'5': 'رییس امور',
				'10': 'کارشناس امور',
				'100': 'راهبر'
			},
			enumService.CommentType = {
				'1': 'در حال بررسی',
				'2': 'تایید',
				'3': 'عدم تایید'
			},
			enumService.DiscountType = {
				'1': 'مبلغی',
				'2': 'درصدی'
			},
			enumService.AttributeControlType = {
				'1': 'کشویی'
			},
			enumService.SendType = {
				'1': 'آنلاین',
				'2': 'درب منزل'
			},
			enumService.EnableMenuType = {
				'1': 'فعال',
				'2': 'غیرفعال'
			},
			enumService.CommentForType = {
				'3': 'اخبار',
				'4': 'مقالات',
				'6': 'محصولات',
				'8': 'رویدادها'
			},
			enumService.DocumentTypeForTags = {
				'1': 'صفحات',
				'3': 'اخبار',
				'4': 'مقاله',
				'6': 'محصولات',
				'8': 'رویداد'
			},
			enumService.BankName = {
				'1': 'بانک ملی'
			},
			enumService.PageType = {
				'1': 'داینامیک',
				'2': 'استاتیک'
			},
			enumService.BannerType = {
				'1': 'تبلیغات',
				'2': 'صفحات'
			},
			enumService.EmailStatusType = {
				'1': 'تحویل داده شده',
				'2': 'ناموفق',
				'2': 'ناموفق'
			},
			enumService.DocumentType = {
				'1': 'محصولات'
			},
			enumService.DocState = {
				'100': 'تایید نهایی'
			},
			enumService.SalesDocState = {
				'1': 'ثبت درخواست',
				'10': 'بررسی و ارجاع به واحد مالی',
				'15': 'بررسی و ارجاع به واحد انبار',
				'20': 'آماده بسته بندی',
				'100': 'ارسال محصول'
			},
			enumService.SendDocumentType = {
				'1': 'تایید ارسال',
				'100': 'تایید نهایی'
			},
			enumService.DepartmentType = {
				'1': 'سامانه اصلی',
				'2': 'واحد فروش',
				'3': 'واحد مالی',
				'4': 'واحد انبار و لجستیک'
			},
			enumService.ActionState = {
				'1': 'موارد در دست اقدام',
				'2': 'موارد ارسال شده',
				'3': 'موارد نهایی'
			},
			enumService.YesOrNoType = {
				'1': 'بله',
				'2': 'خیر'
			},
			enumService.LanguageCultureType = {
				'1': 'English',
				'2': 'French',
				'3': 'German',
				'4': 'Turkish',
				'5': 'Spanish',
				'6': 'Persian',
				'7': 'Arabic',
				'8': 'Hindi',
				'9': 'Chinese'
			}

		return enumService;
	}
})();
