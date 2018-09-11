SET IDENTITY_INSERT [Accounts].[SessionType] ON

INSERT INTO [Accounts].[SessionType] (Id, Title, TitleEn)
VALUES
(1, N'عادی', 'Normal'),
(2, N'دائمی', 'Permanent')

SET IDENTITY_INSERT [Accounts].[SessionType] OFF