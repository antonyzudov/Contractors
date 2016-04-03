USE [testbase]
GO

/****** Object:  Table [dbo].[CONTRACTORS]    Script Date: 03.04.2016 12:20:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CONTRACTORS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](max) NULL,
	[INN] [nvarchar](max) NULL,
	[KPP] [nvarchar](max) NULL,
	[settlement_account] [nvarchar](max) NULL,
	[bank] [nvarchar](max) NULL,
	[city] [nvarchar](max) NULL,
	[corr_account] [nvarchar](max) NULL,
	[BIK] [nvarchar](max) NULL,
	[full_name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


