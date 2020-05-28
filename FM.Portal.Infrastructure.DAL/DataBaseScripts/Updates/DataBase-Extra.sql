Create Schema pbl
CREATE TABLE [pbl].[Attachment](
	[ID] [UNIQUEIDENTIFIER] NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[Data] [varbinary](max) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO