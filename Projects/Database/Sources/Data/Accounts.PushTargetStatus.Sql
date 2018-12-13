SET IDENTITY_INSERT [Accounts].[PushTargetStatus] ON 
INSERT [Accounts].[PushTargetStatus] ([Id], [Title], [TitleEn]) VALUES (1, N'فعال', 'Active')
INSERT [Accounts].[PushTargetStatus] ([Id], [Title], [TitleEn]) VALUES (2, N'غیر فعال', 'Deactive')
SET IDENTITY_INSERT [Accounts].[PushTargetStatus] OFF
