SET IDENTITY_INSERT [Operation].[RequestStatus] ON
INSERT INTO [Operation].[RequestStatus] (Id, Title, TitleEn)
VALUES (1, N'در انتظار اقدام', 'Pending'),
       (2, N'در حال اقدام', 'In Progress'),
       (3, N'انجام شده', 'Completed'),
       (4, N'لغو شده', 'Canceled'),
       (5, N'رد شده', 'Rejected')
SET IDENTITY_INSERT [Operation].[RequestStatus] OFF