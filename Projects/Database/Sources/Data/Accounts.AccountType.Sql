SET IDENTITY_INSERT [Accounts].[AccountType] ON 
INSERT [Accounts].[AccountType] ([Id], [Title], [TitleEn]) VALUES (1, N'کاربر', 'User')
INSERT [Accounts].[AccountType] ([Id], [Title], [TitleEn]) VALUES (2, N'مشتری', 'Customer')
SET IDENTITY_INSERT [Accounts].[AccountType] OFF
