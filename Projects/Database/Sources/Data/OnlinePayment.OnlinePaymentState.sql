SET IDENTITY_INSERT [OnlinePayment].[OnlinePaymentState] ON 
INSERT INTO [OnlinePayment].[OnlinePaymentState] (Id, Title, TitleEn)
VALUES (1, N'ساخته شده', 'Created'), (2, N'پرداخت شده', 'Paid'), (3, N'ناموفق', 'Failed'), (4, N'رد شده', 'Rejected'), (5, N'تایید شده', 'Verified')
SET IDENTITY_INSERT [OnlinePayment].[OnlinePaymentState] OFF