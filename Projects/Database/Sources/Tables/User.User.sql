CREATE TABLE [User].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[PhoneNumber] [varchar](16) NOT NULL,
	[Username] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[UserCode] [nvarchar](16) NOT NULL,
	[PasswordHash] [binary](32) NULL,
	[PasswordSalt] [binary](32) NULL,
	[FingerprintEnabled] [bit] NOT NULL,
	[Image] [varbinary](max) NULL,
	[ImageMimeType] [nvarchar](max) NULL,
	[Barcode] [varbinary](max) NULL,
	[BarcodeMimeType] [nvarchar](max) NULL,
	[RegistrationDateTime] [datetime] NOT NULL,
	[RegistrationCode] [varchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]