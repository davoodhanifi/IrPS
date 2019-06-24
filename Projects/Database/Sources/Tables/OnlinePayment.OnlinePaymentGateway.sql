CREATE TABLE [OnlinePayment].[OnlinePaymentGateway]
(
    [Id] [INT] IDENTITY(1, 1) NOT NULL,
    [Title] NVARCHAR(MAX),
    [TitleEn] VARCHAR(MAX),
    [RecordVersion] [timestamp] NOT NULL,
    [RecordState] INT NOT NULL CONSTRAINT [DF_OnlinePaymentGateway_RecordState] DEFAULT 0,
    [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_OnlinePaymentGateway_RecordInsertDateTime] DEFAULT GETDATE(),
    [RecordUpdateDateTime] DATETIME NULL ,
    [RecordDeleteDateTime] DATETIME NULL ,
CONSTRAINT [PK_OnlinePaymentGateway] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]