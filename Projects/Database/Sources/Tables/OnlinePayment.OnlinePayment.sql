CREATE TABLE [OnlinePayment].[OnlinePayment](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [AccountId] [int] NOT NULL,
    [Amount] [decimal](19, 4) NOT NULL,
    [GatewayId] [int] NOT NULL,
    [CreationDateTime] [datetime] NOT NULL,
    [PaymentDateTime] [datetime] NULL,
    [PaidAmount] [decimal](19, 4) NULL,
    [StateId] [int] NOT NULL,
    [UniqueId] UNIQUEIDENTIFIER NULL,
    [RecordVersion] TIMESTAMP ,
    [RecordState] INT NOT NULL CONSTRAINT [DF_OnlinePayment_RecordState] DEFAULT 0,
    [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_OnlinePayment_RecordInsertDateTime] DEFAULT GETDATE(),
    [RecordUpdateDateTime] DATETIME NULL ,
    [RecordDeleteDateTime] DATETIME NULL ,
CONSTRAINT [PK_OnlinePayment] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]