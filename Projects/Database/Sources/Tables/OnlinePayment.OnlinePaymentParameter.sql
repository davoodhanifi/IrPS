CREATE TABLE [OnlinePayment].[OnlinePaymentParameter](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [OnlinePaymentId] [int] NOT NULL,
    [Key] [nvarchar](max) NOT NULL,
    [BooleanValue] [bit] NULL,
    [IntegerValue] [bigint] NULL,
    [DecimalValue] [decimal](19, 4) NULL,
    [TextValue] [nvarchar](max) NULL,
    [DateTimeValue] [datetime] NULL,
    [BinaryValue] [varbinary](max) NULL,
    [RecordVersion] TIMESTAMP ,
    [RecordState] INT NOT NULL CONSTRAINT [DF_OnlinePaymentParameter_RecordState] DEFAULT 0,
    [RecordInsertDateTime] DATETIME NULL CONSTRAINT [DF_OnlinePaymentParameter_RecordInsertDateTime] DEFAULT GETDATE(),
    [RecordUpdateDateTime] DATETIME NULL ,
    [RecordDeleteDateTime] DATETIME NULL ,
CONSTRAINT [PK_OnlinePaymentParameter] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]