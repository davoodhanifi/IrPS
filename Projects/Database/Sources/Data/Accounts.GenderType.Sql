SET IDENTITY_INSERT [Accounts].[GenderType] ON 
INSERT [Accounts].[GenderType] ([Id], [Title], [TitleEn]) VALUES (1, N'مرد', 'Male')
INSERT [Accounts].[GenderType] ([Id], [Title], [TitleEn]) VALUES (2, N'زن', 'Female')
SET IDENTITY_INSERT [Accounts].[GenderType] OFF
