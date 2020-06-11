Create Schema org
CREATE TABLE [org].[Position](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[DepartmentID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[Type] [tinyint] NOT NULL,
	[Default] [bit] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[RemoveDate] [smalldatetime] NULL,
	[Enabled] [bit] NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [org].[Role](
	[ID] [uniqueidentifier] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
 CONSTRAINT [PK_orgRoles] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [org].[User](
	[ID] [uniqueidentifier] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](1000) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[NationalCode] [varchar](18) NULL,
	[Email] [varchar](256) NULL,
	[EmailVerified] [bit] NOT NULL,
	[CellPhone] [varchar](20) NULL,
	[CellPhoneVerified] [bit] NOT NULL,
	[PasswordExpireDate] [smalldatetime] NULL,
	[UserType] [tinyint] NULL,
 CONSTRAINT [PK_orgUsers] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [org].[Application](
	[ID] [uniqueidentifier] NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Enabled] [bit] NOT NULL,
	[Comment] [nvarchar](256) NULL,
 CONSTRAINT [PK_orgApplications] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE app.Comment
ADD CommentForType Tinyint
GO
CREATE TABLE [org].[Department](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[RemoverDate] [smalldatetime] NULL,
	[Address] [nvarchar](1000) NULL,
	[Phone] [nvarchar](11) NULL,
	[CodePhone] [nvarchar](3) NULL,
 CONSTRAINT [PK_orgDepartments] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [org].[Command](
	[ID] [uniqueidentifier] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Type] [tinyint] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[FullName] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_orgApplicationCommands] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [org].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Command_Application] FOREIGN KEY([ApplicationID])
REFERENCES [org].[Application] ([ID])
GO

ALTER TABLE [org].[Command] CHECK CONSTRAINT [FK_Command_Application]
GO
ALTER TABLE [org].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Application] FOREIGN KEY([ApplicationID])
REFERENCES [org].[Application] ([ID])
GO

ALTER TABLE [org].[Position] CHECK CONSTRAINT [FK_Position_Application]
GO

ALTER TABLE [org].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Department] FOREIGN KEY([DepartmentID])
REFERENCES [org].[Department] ([ID])
GO

ALTER TABLE [org].[Position] CHECK CONSTRAINT [FK_Position_Department]
GO

ALTER TABLE [org].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User] FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO

ALTER TABLE [org].[Position] CHECK CONSTRAINT [FK_Position_User]
GO

ALTER TABLE [org].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_User1] FOREIGN KEY([RemoverID])
REFERENCES [org].[User] ([ID])
GO

ALTER TABLE [org].[Position] CHECK CONSTRAINT [FK_Position_User1]
GO

ALTER TABLE [org].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_Users1] FOREIGN KEY([RemoverID])
REFERENCES [org].[User] ([ID])
GO

ALTER TABLE [org].[Position] CHECK CONSTRAINT [FK_Position_Users1]
GO
CREATE TABLE [org].[PositionRole](
	[ID] [uniqueidentifier] NOT NULL,
	[PositionID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_UserRole] UNIQUE NONCLUSTERED 
(
	[PositionID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [org].[PositionRole]  WITH CHECK ADD  CONSTRAINT [FK_PositionRole_Position] FOREIGN KEY([PositionID])
REFERENCES [org].[Position] ([ID])
GO

ALTER TABLE [org].[PositionRole] CHECK CONSTRAINT [FK_PositionRole_Position]
GO

ALTER TABLE [org].[PositionRole]  WITH CHECK ADD  CONSTRAINT [FK_PositionRole_Role] FOREIGN KEY([RoleID])
REFERENCES [org].[Role] ([ID])
GO

ALTER TABLE [org].[PositionRole] CHECK CONSTRAINT [FK_PositionRole_Role]
GO

CREATE TABLE [org].[RolePermission](
	[ID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NOT NULL,
	[CommandID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_orgRolePermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_orgRolePermission] UNIQUE NONCLUSTERED 
(
	[RoleID] ASC,
	[CommandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [org].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_orgRolePermission_ApplicationCommands] FOREIGN KEY([CommandID])
REFERENCES [org].[Command] ([ID])
GO

ALTER TABLE [org].[RolePermission] CHECK CONSTRAINT [FK_orgRolePermission_ApplicationCommands]
GO

ALTER TABLE [org].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_orgRolePermission_Roles] FOREIGN KEY([RoleID])
REFERENCES [org].[Role] ([ID])
GO

ALTER TABLE [org].[RolePermission] CHECK CONSTRAINT [FK_orgRolePermission_Roles]
GO


CREATE TABLE [org].[UserAddress](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[PostalCode] [nvarchar](20) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_UserAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [org].[UserAddress]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
CREATE TABLE [org].[RefreshToken](
	[ID] [uniqueidentifier] NOT NULL,
	[IssuedDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](4000) NOT NULL,
	[UserID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [org].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO

ALTER TABLE [org].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_User]
GO
-------------------------------------------------------------------------------------------------------------------------
Create Schema app
CREATE TABLE [app].[FAQGroup](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[CreatorID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL
 CONSTRAINT [PK_FAQGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [app].[FAQ](
	[ID] [uniqueidentifier] NOT NULL,
	[FAQGroupID] [uniqueidentifier] NOT NULL,
	[Question] [nvarchar](500) NOT NULL,
	[Answer] [nvarchar](500) NOT NULL,
	[CreatorID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [app].[FAQ]  WITH CHECK ADD FOREIGN KEY([FAQGroupID])
REFERENCES [app].[FAQGroup] ([ID])
GO
USE [Ahora]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [app].[Product](
	[ID] [uniqueidentifier]  NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[FullDescription] [nvarchar](max) NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](400) NULL,
	[AllowCustomerReviews] [bit] NOT NULL,
	[ApprovedRatingSum] [int] NOT NULL,
	[NotApprovedRatingSum] [int] NOT NULL,
	[ApprovedTotalReviews] [int] NOT NULL,
	[NotApprovedTotalReviews] [int] NOT NULL,
	[CallForPrice] [bit] NOT NULL,
	[Price] [decimal](18, 3) NOT NULL,
	[OldPrice] [decimal](18, 3) NOT NULL,
	[SpecialPrice] [decimal](18, 3) NULL,
	[Weight] [decimal](18, 4) NOT NULL,
	[Length] [decimal](18, 4) NOT NULL,
	[Width] [decimal](18, 4) NOT NULL,
	[Height] [decimal](18, 4) NOT NULL,
	[Published] [bit] NOT NULL,
	[RemoverID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [app].[SpecificationAttribute](
	[ID] uniqueidentifier NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [app].[SpecificationAttributeOption](
	[ID] uniqueidentifier NOT NULL,
	[SpecificationAttributeId] uniqueidentifier NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [app].[SpecificationAttributeOption]  WITH CHECK ADD  CONSTRAINT [SpecificationAttributeOption_SpecificationAttribute] FOREIGN KEY([SpecificationAttributeID])
REFERENCES [app].[SpecificationAttribute] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[SpecificationAttributeOption] CHECK CONSTRAINT [SpecificationAttributeOption_SpecificationAttribute]
GO


CREATE TABLE [app].[Product_SpecificationAttribute_Mapping](
	[ID] uniqueidentifier NOT NULL,
	[ProductId] uniqueidentifier NOT NULL,
	[SpecificationAttributeOptionId] uniqueidentifier NOT NULL,
	[CustomValue] [nvarchar](4000) NULL,
	[ShowOnProductPage] [bit] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [app].[Product_SpecificationAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductSpecificationAttribute_Product] FOREIGN KEY([ProductID])
REFERENCES [app].[Product] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[Product_SpecificationAttribute_Mapping] CHECK CONSTRAINT [ProductSpecificationAttribute_Product]
GO

ALTER TABLE [app].[Product_SpecificationAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductSpecificationAttribute_SpecificationAttributeOption] FOREIGN KEY([SpecificationAttributeOptionID])
REFERENCES [app].[SpecificationAttributeOption] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[Product_SpecificationAttribute_Mapping] CHECK CONSTRAINT [ProductSpecificationAttribute_SpecificationAttributeOption]
GO
CREATE TABLE [app].[ProductAttribute](
	[ID] uniqueidentifier NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] smalldatetime 
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [app].[Product_ProductAttribute_Mapping](
	[ID] uniqueidentifier NOT NULL,
	[ProductID] uniqueidentifier NOT NULL,
	[ProductAttributeID] uniqueidentifier NOT NULL,
	[TextPrompt] [nvarchar](max) NULL,
	[IsRequired] [bit] NOT NULL,
	[AttributeControlType] [int] NOT NULL,
	[CreationDate] smalldatetime 
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [app].[Product_ProductAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductVariantAttribute_Product] FOREIGN KEY([ProductID])
REFERENCES [app].[Product] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[Product_ProductAttribute_Mapping] CHECK CONSTRAINT [ProductVariantAttribute_Product]
GO

ALTER TABLE [app].[Product_ProductAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductVariantAttribute_ProductAttribute] FOREIGN KEY([ProductAttributeID])
REFERENCES [app].[ProductAttribute] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[Product_ProductAttribute_Mapping] CHECK CONSTRAINT [ProductVariantAttribute_ProductAttribute]
GO
CREATE TABLE [app].[ProductVariantAttributeValue](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductVariantAttributeID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[IsPreSelected] [bit] NOT NULL,
	[CreationDate] smalldatetime 
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [app].[Category](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[IncludeInTopMenu] [bit] NULL,
	[IncludeInLeftMenu] [bit] NULL,
	[HasDiscountsApplied] [bit] NULL,
	[CreationDate] [smalldatetime] NULL
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [app].[Discount](
	[ID] uniqueidentifier NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DiscountType] tinyint NOT NULL,
	[DiscountPercentage] int NULL,
	[DiscountAmount] [decimal](18, 3) NULL,
	[CreationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [app].[ShoppingCartItem](
	[ID] uniqueidentifier NOT NULL,
	[ShoppingID] uniqueidentifier NOT NULL,
	[UserID] uniqueidentifier NOT NULL,
	[ProductID] uniqueidentifier NOT NULL,
	[Quantity] int default(1),
	[AttributeJson] Nvarchar(max),
	CreationDate SmallDateTime
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [app].[ShoppingCartItem]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])

ALTER TABLE [app].[ShoppingCartItem]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [app].[Product] ([ID])
GO
CREATE TABLE [app].[Comment] (
    [ID] uniqueidentifier,
    [Body]  NVARCHAR (MAX) NOT NULL,
	[CommentType] TINYINT NULL,
    [CreationDate] SmallDATETIME NOT NULL,
    [LikeCount] INT NULL,
	[DisLikeCount] INT NULL,
    [ParentID] uniqueidentifier  NULL,
	[UserID] uniqueidentifier  NULL,
	[DocumentID] uniqueidentifier  NULL,
	[RemoverID] uniqueidentifier  NULL

 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [app].[Comment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
USE [Ahora]
GO

/****** Object:  Table [app].[Product_ProductAttribute_Mapping]    Script Date: 12/27/2019 6:12:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [app].[Comment_Product_Mapping](
	[ProductID] [uniqueidentifier] NOT NULL,
	[CommentID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NULL
)
GO

CREATE TABLE [app].[Comment_User_Mapping](
	[CommentID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NULL
)
GO

ALTER TABLE [app].[Comment_User_Mapping]  WITH CHECK ADD  CONSTRAINT [Comment_User_Mapping_CommentID] FOREIGN KEY([CommentID])
REFERENCES [app].[Comment] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [app].[Comment_User_Mapping]  WITH CHECK ADD  CONSTRAINT [Comment_User_Mapping_UserID] FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
ON DELETE CASCADE
GO

CREATE TABLE [app].[Order](
	[ID] [uniqueidentifier] NOT NULL,
	[ShoppingID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[SendType] [tinyint] NOT NULL,
	[AddressID] [uniqueidentifier] NOT NULL,
	[Price] decimal(18,3) NOT NULL,
	[BankID] [uniqueidentifier]  NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[TrackingCode] [int] NOT NULL
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [app].[Order]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
CREATE TABLE [app].[OrderDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[ProductJson] [nvarchar](max) NOT NULL,
	[UserJson] [nvarchar](max) NOT NULL,
	[AttributeJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [app].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [app].[Order] ([ID])
GO

CREATE TABLE [app].[Payment](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[TransactionStatus] [tinyint]  NULL,
	[TransactionStatusMessage] [nvarchar](max) NULL,
	[Price] decimal(18,3) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[RetrivalRefNo] nvarchar(max) NULL,
	[SystemTraceNo] nvarchar(max) NULL,
	[CreationDate] DateTime NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [app].[Payment]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [app].[Order] ([ID])
GO
ALTER TABLE [app].[Payment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
CREATE TABLE [app].[Category_Discount_Mapping](
	[ID] uniqueidentifier NOT NULL,
	[CategoryID] uniqueidentifier NOT NULL,
	[DiscountID] uniqueidentifier NOT NULL,
	[Active] [BIT] NULL,
	[CreationDate] [SMALLDATETIME] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE app.product
ADD StockQuantity INT
GO
ALTER TABLE app.product
drop column OldPrice
ALTER TABLE app.product
drop column SpecialPrice
GO
ALTER TABLE app.product
ADD Discount DECIMAL(18,3) NULL
ALTER TABLE app.product
ADD DiscountType TINYINT NULL
ALTER TABLE app.product
ADD SpecialOffer BIT NULL
ALTER TABLE app.product
ADD HasDiscount BIT NULL

GO
ALTER TABLE app.Product
ADD IsDownload BIT DEFAULT 0
GO
CREATE TABLE [app].[Bank](
	[ID] [uniqueidentifier] NOT NULL,
	[BankName] [Tinyint] NOT NULL,
	[UserName] [Nvarchar](3000) NOT NULL,
	[Password] [Nvarchar](3000) NOT NULL,
	[MerchantID] [Nvarchar](3000) NOT NULL,
	[MerchantKey] [Nvarchar](3000) NOT NULL,
	[TerminalID] [Nvarchar](3000) NOT NULL,
	[Url] [Nvarchar](Max) NOT NULL,
	[RedirectUrl] [Nvarchar](Max) NOT NULL,
	[Default] [Bit] NOT NULL Default 0,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
Insert into app.bank
values(NEWID() , 1 , '0' ,'0' , '1694' ,'YjdjYmZhNGY4NDQ5NTI3NThhNzY1MTI5' , 'tC4Nkmug' ,'http://banktest.ir/gateway/melli' , 'http://localhost:61837/redirect/melli',1,GETDATE())
GO
ALTER TABLE app.Payment
DROP COLUMN TransactionStatus

ALTER TABLE app.Payment
ADD TransactionStatus int
GO
ALTER TABLE app.[Order] 
ALTER COLUMN TrackingCode NVARCHAR(Max)
GO
ALTER TABLE app.[OrderDetail]
ADD ShoppingCartJson Nvarchar(MAX)
GO
ALTER TABLE app.OrderDetail
ADD Quantity INT
GO
ALTER TABLE app.ProductVariantAttributeValue
ADD Price MONEY NULL
GO
ALTER TABLE app.Category
ADD [Node] [hierarchyid] NULL
ALTER TABLE app.Category
DROP COLUMN ParentID
GO
CREATE TABLE [app].[ShippingCost](
	[ID] uniqueidentifier NOT NULL,
	[Name] NVARCHAR(1000) NOT NULL,
	[Description] NVARCHAR(1000) NOT NULL,
	[Price] Decimal(18,3) NULL,
	[Enabled] [Tinyint] NULL,
	[UserID] uniqueidentifier NOT NULL,
	[CreationDate] [SMALLDATETIME] NOT NULL
 CONSTRAINT [PK_ShippingCost] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [app].[ShippingCost] ADD FOREIGN KEY ([UserID])
REFERENCES org.[User] (ID)
GO
ALTER TABLE app.Product
ADD ShippingCostID UNIQUEIDENTIFIER

ALTER TABLE app.ShippingCost
ADD [Priority] INT 
GO
CREATE TABLE [app].[DeliveryDate](
	[ID] uniqueidentifier NOT NULL,
	[Name] NVARCHAR(1000) NOT NULL,
	[Description] NVARCHAR(1000) NOT NULL,
	[Enabled] [Tinyint] NULL,
	[UserID] uniqueidentifier NOT NULL,
	[CreationDate] [SMALLDATETIME] NOT NULL,
	[Priority] [INT] NULL,
 CONSTRAINT [PK_DeliveryDate] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [app].[DeliveryDate] ADD FOREIGN KEY ([UserID])
REFERENCES org.[User] (ID)
GO
ALTER TABLE app.Product
ADD DeliveryDateID UNIQUEIDENTIFIER
GO
----------------------------------------------------------------------------------
Create Schema pbl
GO
CREATE TABLE [pbl].[Attachment](
	[ID] [uniqueidentifier]  NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[Data] [varbinary](max) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
ALTER TABLE [pbl].[Attachment]
ADD PathType TINYINT
GO

CREATE TABLE [pbl].[Pages](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[ControllerName] [Nvarchar](200)  NULL,
	[ActionName] [Nvarchar](200) NULL,
	[Title] [Nvarchar](200) NULL,
	[Description] [Nvarchar](max) NULL,
	[Enabled] [bit] NULL Default 1,
	[Deleted] [bit] NUll Default 0 ,
	[CreationDate] SmallDateTime
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE pbl.pages
ADD RouteUrl nvarchar(max)
go

CREATE TABLE [pbl].[Menu](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[Name] [Nvarchar](256) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[RemoverDate] [smalldatetime] NULL,
	[RemoverID] [Uniqueidentifier] NULL,
	[Enabled] [Tinyint] NULL,
	[ForeignLink] [Tinyint] NULL,
	[Url] [Nvarchar] NOT NULL
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [pbl].[Menu]
ADD IconText Nvarchar(256) NULL
ALTER TABLE [pbl].[Menu]
ADD Priority int NULL
GO
CREATE TABLE [pbl].[GeneralSetting](
	[Name] [Nvarchar](Max) NOT NULL,
	[Value] [Nvarchar](Max) NULL
)
GO
INSERT INTO pbl.GeneralSetting
VALUES('SiteMetaTag','')
INSERT INTO pbl.GeneralSetting
VALUES('FacebookUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('InstagramUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('TelegramUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('TwitterUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('WhatsUpUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('Phone','')
INSERT INTO pbl.GeneralSetting
VALUES('Fax','')
INSERT INTO pbl.GeneralSetting
VALUES('Address','')
INSERT INTO pbl.GeneralSetting
VALUES('Mobile','')
INSERT INTO pbl.GeneralSetting
VALUES('CountShowSlider','')
INSERT INTO pbl.GeneralSetting
VALUES('CountShowArticle','')
INSERT INTO pbl.GeneralSetting
VALUES('CountShowNews','')
INSERT INTO pbl.GeneralSetting
VALUES('CountShowProduct','')
INSERT INTO pbl.GeneralSetting
VALUES('SiteUrl','')
INSERT INTO pbl.GeneralSetting
VALUES('SiteKeyword','')
INSERT INTO pbl.GeneralSetting
VALUES('SiteDescription','')
INSERT INTO pbl.GeneralSetting (Name,Value)
VALUES('CountShowEvents','')
INSERT INTO pbl.GeneralSetting
VALUES('ShoppingCartRate','')

INSERT INTO pbl.GeneralSetting
VALUES('ShippingCosts','')
Go
CREATE TABLE [pbl].[Tags](
	[ID] [uniqueidentifier]  NOT NULL,
	[Name] [Nvarchar](MAX) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [pbl].[Tags_Mapping](
	[TagID] [uniqueidentifier]  NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [pbl].[Tags_Mapping]  WITH CHECK ADD  CONSTRAINT [Tags_Mapping_Tag] FOREIGN KEY([TagID])
REFERENCES [pbl].[Tags] ([ID])
ON DELETE CASCADE
GO
CREATE TABLE [pbl].[Notification](
	[ID] [uniqueidentifier]  NOT NULL,
	[UserID] [uniqueidentifier]  NOT NULL,
	[Title] [Nvarchar](3000) NOT NULL,
	[Description][Nvarchar](MAX) NOT NULL,
	[ReadDate] [SmallDateTime] NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [pbl].[Download](
	[ID] [uniqueidentifier]  NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[Data] [varbinary](max) NOT NULL,
	[PaymentID] UNIQUEIDENTIFIER NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ExpireDate] [smalldatetime] NOT NULL,
CONSTRAINT [PK_Download] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [pbl].[Download] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])

ALTER TABLE [pbl].[Download] WITH CHECK ADD FOREIGN KEY ([PaymentID])
REFERENCES [app].[Payment] ([ID])
GO
ALTER Table pbl.Menu
ADD [Parameters] Nvarchar(Max) NULL
GO
CREATE TABLE [pbl].[Contact](
	[ID] [uniqueidentifier]  NOT NULL,
	[FirstName] [nvarchar](1000) NOT NULL,
	[LastName] [nvarchar](1000) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](MAX) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [pbl].[Link](
	[ID] [uniqueidentifier]  NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IconText] [nvarchar](100) NULL,
	[ShowFooter] [bit] NULL DEFAULT 1,
	[Priority] [Int] NULL,
	[CreationDate] [smalldatetime] NULL,
CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [pbl].[Link] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])
GO
-----------------------------------------------------------------------------------
CREATE SCHEMA ptl
GO
CREATE TABLE [ptl].[Article] (
    [ID] uniqueidentifier,
    [Title]  NVARCHAR (MAX) NOT NULL,
    [CreationDate] SmallDATETIME NOT NULL,
    [ModifiedDate] DATETIME NOT NULL,
    [Body] NVARCHAR (MAX) NOT NULL,
    [MetaKeywords] NVARCHAR (400) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CommentStatus] TINYINT NOT NULL,
    [VisitedCount] INT NOT NULL,
    [LikeCount] INT NULL,
	[DisLikeCount] INT NULL,
    [UrlDesc] NVARCHAR (MAX) NULL,
    [IsShow] BIT NOT NULL,
    --[CategoryID] uniqueidentifier  NULL,
	[UserID] uniqueidentifier  NULL,
	[RemoverID] uniqueidentifier  NULL,
	[TrackingCode] nvarchar(100),

 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[Article]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[user] ([ID])
GO

CREATE TABLE [ptl].[Category](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[ParentID] [uniqueidentifier] NULL,
	[IncludeInTopMenu] [bit] NULL,
	[IncludeInLeftMenu] [bit] NULL,
	[HasDiscountsApplied] [bit] NULL,
	[CreationDate] [smalldatetime] NULL
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [ptl].[Article]
ADD [CategoryID] uniqueidentifier  NULL
GO
ALTER TABLE [ptl].[Article]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [ptl].[Category] ([ID])
---------------------------------------------------------------------
Go
CREATE TABLE [ptl].[News] (
    [ID] uniqueidentifier,
    [Title]  NVARCHAR (MAX) NOT NULL,
    [CreationDate] SmallDATETIME NOT NULL,
    [ModifiedDate] DATETIME NOT NULL,
    [Body] NVARCHAR (MAX) NOT NULL,
    [MetaKeywords] NVARCHAR (400) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [CommentStatus] TINYINT NOT NULL,
    [VisitedCount] INT NOT NULL,
    [LikeCount] INT NULL,
	[DisLikeCount] INT NULL,
    [UrlDesc] NVARCHAR (MAX) NULL,
    [IsShow] BIT NOT NULL,
    [CategoryID] uniqueidentifier  NULL,
	[UserID] uniqueidentifier  NULL,
	[RemoverID] uniqueidentifier  NULL,
	[TrackingCode] nvarchar(100),

 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[News]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[user] ([ID])
GO



ALTER TABLE [ptl].[News]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [ptl].[Category] ([ID])
Go

ALTER TABLE ptl.news
DROP COLUMN IsShow 
GO
ALTER TABLE ptl.news
add IsShow TINYINT
------------------------------------------------------------------------------------------
Go
CREATE TABLE [ptl].[Slider] (
    [ID] uniqueidentifier,
    [Title]  NVARCHAR (MAX) NOT NULL,
    [CreationDate] SmallDATETIME NOT NULL,
	[Priority] int NOT NULL DEFAULT 1,
	[Enabled] Tinyint NOT NULL DEFAULT 1,
	[Deleted] bit NULL DEFAULT 0,
	[Url] Nvarchar(max) NULL
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


-----------------------------------------------------------------------------------------

GO

CREATE TABLE [ptl].[Events](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[Description] [nvarchar](max) NULL,
	[CommentStatus] [tinyint] NOT NULL,
	[VisitedCount] [int] NOT NULL,
	[LikeCount] [int] NULL,
	[DisLikeCount] [int] NULL,
	[UrlDesc] [nvarchar](max) NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[TrackingCode] [nvarchar](100) NULL,
	[IsShow] [tinyint] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [ptl].[Events]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [ptl].[Category] ([ID])
GO

ALTER TABLE [ptl].[Events]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE ptl.Events  
DROP CONSTRAINT FK__Events__Category__473C8FC7;  
GO
ALTER TABLE [ptl].[Events]  
ADD ReadingTime NVARCHAR(200)
ALTER TABLE [ptl].[Article]  
ADD ReadingTime NVARCHAR(200)
ALTER TABLE [ptl].[News]  
ADD ReadingTime NVARCHAR(200)


CREATE TABLE [ptl].[Pages](
	[ID] [uniqueidentifier] NOT NULL,
	[TrackingCode] [Nvarchar](100) NULL,
	[Name] [Nvarchar](1000) NULL,
	[UrlDesc] [Nvarchar](1000) NULL,
	[PageType] [Tinyint] NULL,
	[UserID] [UniqueIdentifier] NULL,
	[Enabled] [Tinyint] NULL,
	[CreationDate] SmallDateTime
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[Pages] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])
GO

CREATE TABLE [ptl].[DynamicPage](
	[ID] [uniqueidentifier] NOT NULL,
	[TrackingCode] [Nvarchar](100) NULL,
	[Name] [Nvarchar](2000) NULL,
	[Body] [Nvarchar](MAX) NOT NULL,
	[Description] [Nvarchar](1000) NOT NULL,
	[PageID] [uniqueidentifier] NOT NULL,
	[MetaKeywords] [Nvarchar](1000) NULL,
	[VisitedCount] [Int] NULL Default 0,
	[IsShow] [Tinyint] NULL,
	[UserID] [UniqueIdentifier] NOT NULL,
	[UrlDesc] [Nvarchar](1000) NOT NULL,
	[CreationDate] SmallDateTime
 CONSTRAINT [PK_DynamicPage] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[DynamicPage] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])
ALTER TABLE [ptl].[DynamicPage] WITH CHECK ADD FOREIGN KEY ([PageID])
REFERENCES [ptl].[Pages] ([ID])
GO
CREATE TABLE [ptl].[Category](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[IncludeInTopMenu] [bit] NULL,
	[IncludeInLeftMenu] [bit] NULL,
	[CreationDate] [smalldatetime] NULL,
	[Node] [hierarchyid] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [ptl].[StaticPage](
	[ID] [uniqueidentifier] NOT NULL,
	[TrackingCode] [Nvarchar](100) NOT NULL,
	[Description] [Nvarchar](1000) NULL,
	[AttachmentID] [uniqueidentifier] NULL,
	[MetaKeywords] [Nvarchar](1000) NULL,
	[VisitedCount] [Int] NULL Default 0,
	[BannerShow] [Tinyint] NULL Default 0,
	[Body] [Nvarchar](MAX) NULL,
 CONSTRAINT [PK_StaticPage] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [ptl].[Banner](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [Nvarchar](1000) NOT NULL,
	[Description] [Nvarchar](1000) NULL,
	[BannerType] [Tinyint] NULL,
	[Enabled] [Tinyint] NULL Default 0,
	[UserID] [Uniqueidentifier] NULL,
	[CreationDate] [SmalldateTime] NOT NULL
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[Banner] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])
GO
CREATE TABLE [ptl].[Gallery](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [Nvarchar](1000) NOT NULL,
	[Description] [Nvarchar](max) NOT NULL,
	[VisitedCount] [Int] NULL Default 0,
	[Enabled] [Tinyint] NULL Default 0,
	[UserID] [Uniqueidentifier] NOT NULL,
	[TrackingCode] [Nvarchar](20) NOT NULL,
	[CreationDate] [SmalldateTime] NOT NULL,
	[MetaKeywords] NVARCHAR(1000) NULL,
	[UrlDesc] NVARCHAR(1000) NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [ptl].[Gallery] WITH CHECK ADD FOREIGN KEY ([UserID])
REFERENCES [org].[User] ([ID])