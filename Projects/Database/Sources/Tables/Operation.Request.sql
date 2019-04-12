CREATE TABLE [Operation].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
    [DateTime] [DateTime] NOT NULL,
	[Parameters] [NVARCHAR] (MAX) NULL,
	[Comments] [NVARCHAR] (MAX) NULL,
	[RecordVersion] [timestamp] NOT NULL,
	[RecordState] [int] NOT NULL CONSTRAINT [DF_Request_RecordState] DEFAULT 0,
	[RecordInsertDateTime] [datetime] NOT NULL CONSTRAINT [DF_Request_RecordInsertDateTime] DEFAULT GETDATE(),
	[RecordUpdateDateTime] [datetime] NULL,
	[RecordDeleteDateTime] [datetime] NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]