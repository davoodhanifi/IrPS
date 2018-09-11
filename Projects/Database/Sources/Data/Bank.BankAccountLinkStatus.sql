SET IDENTITY_INSERT [Bank].[BankAccountOnlineLinkStatus] ON

INSERT INTO [Bank].[BankAccountOnlineLinkStatus]
(Id, Title, TitleEn) Values
(1, N'وصل نشده', 'Not Linked'),
(2, N'وصل شده', 'Linked'),
(3, N'قطع شده', 'Unlinked')

SET IDENTITY_INSERT [Bank].[BankAccountOnlineLinkStatus] OFF