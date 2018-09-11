SET IDENTITY_INSERT [Accounts].[AccountState] ON 
INSERT [Accounts].[AccountState] ([Id], [Title], [TitleEn]) VALUES (1, N'فعال', 'Active')
INSERT [Accounts].[AccountState] ([Id], [Title], [TitleEn]) VALUES (2, N'غیر فعال', 'Deactive')
SET IDENTITY_INSERT [Accounts].[AccountState] OFF
