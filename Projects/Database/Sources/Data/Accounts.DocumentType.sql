SET IDENTITY_INSERT	 [Accounts].[DocumentType] ON

INSERT INTO [Accounts].[DocumentType]
(
    [Id],
    [Title]
)
VALUES
(1, N'شناسنامه'),
(2, N'کارت ملی'),
(3, N'آواتار'),
(4, N'کارت ماشین'),
(5, N'گواهینامه'),
(6, N'مدرک تاکسیرانی')
SET IDENTITY_INSERT	 [Accounts].[DocumentType] OFF