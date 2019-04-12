SET IDENTITY_INSERT [Operation].[RequestType] ON
INSERT INTO [Operation].[RequestType] (Id, Title)
VALUES (1, N'درخواست پرداخت وجه'),
	   (2, N'درخواست کپی اطلاعات کاربری'),
       (3, N'درخواست مراجعه حضوری احراز هویت')
SET IDENTITY_INSERT [Operation].[RequestType] OFF