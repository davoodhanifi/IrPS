SET IDENTITY_INSERT	 [Accounts].[DocumentType] ON

INSERT INTO [Accounts].[DocumentType]
(
    [Id],
    [Title]
)
VALUES
(1, N'شناسنامه'),
(2, N'کارت ملی'),
(3, N'مدرک تحصیلی'),
(4, N'پاسپورت'),
(5, N'آگهی ثبت'),
(6, N'آگهی تغییرات'),
(7, N'وکالتنامه'),
(8, N'نمونه امضا'),
(9, N'عکس'),
(10, N'قرارداد اعتباری'),
(11, N'قرارداد آنلاین'),
(12, N'فرم افتتاح حساب'),
(13, N'فرم‌های پیشخوان')

SET IDENTITY_INSERT	 [Accounts].[DocumentType] OFF