--Create Schema org
--Create Schema app
--Create Schema pbl
--CREATE SCHEMA ptl
USE [Ahora]
GO
/****** Object:  Table [app].[Bank]    Script Date: 10/29/2020 6:32:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Bank](
	[ID] [uniqueidentifier] NOT NULL,
	[BankName] [tinyint] NOT NULL,
	[UserName] [nvarchar](3000) NOT NULL,
	[Password] [nvarchar](3000) NOT NULL,
	[MerchantID] [nvarchar](3000) NOT NULL,
	[MerchantKey] [nvarchar](3000) NOT NULL,
	[TerminalID] [nvarchar](3000) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[RedirectUrl] [nvarchar](max) NOT NULL,
	[Default] [bit] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Category]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Category](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[IncludeInTopMenu] [bit] NULL,
	[IncludeInLeftMenu] [bit] NULL,
	[HasDiscountsApplied] [bit] NULL,
	[CreationDate] [smalldatetime] NULL,
	[Node] [hierarchyid] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[Category_Discount_Mapping]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Category_Discount_Mapping](
	[ID] [uniqueidentifier] NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[DiscountID] [uniqueidentifier] NOT NULL,
	[Active] [bit] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[Comment]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Comment](
	[ID] [uniqueidentifier] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[CommentType] [tinyint] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[LikeCount] [int] NULL,
	[DisLikeCount] [int] NULL,
	[ParentID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NULL,
	[DocumentID] [uniqueidentifier] NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[CommentForType] [tinyint] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Comment_User_Mapping]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Comment_User_Mapping](
	[CommentID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [app].[DeliveryDate]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[DeliveryDate](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Enabled] [tinyint] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_DeliveryDate] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[Discount]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Discount](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DiscountType] [tinyint] NOT NULL,
	[DiscountAmount] [decimal](18, 3) NULL,
	[CreationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[FAQ]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [app].[FAQGroup]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[FAQGroup](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[CreatorID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_FAQGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[Order]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Order](
	[ID] [uniqueidentifier] NOT NULL,
	[ShoppingID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[SendType] [tinyint] NOT NULL,
	[BankID] [uniqueidentifier] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[AddressID] [uniqueidentifier] NOT NULL,
	[Price] [decimal](18, 3) NOT NULL,
	[TrackingCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[OrderDetail]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[OrderDetail](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[ProductJson] [nvarchar](max) NOT NULL,
	[UserJson] [nvarchar](max) NOT NULL,
	[AttributeJson] [nvarchar](max) NULL,
	[ShoppingCartJson] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Payment]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Payment](
	[ID] [uniqueidentifier] NOT NULL,
	[OrderID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[TransactionStatusMessage] [nvarchar](max) NULL,
	[Price] [decimal](18, 3) NOT NULL,
	[RetrivalRefNo] [nvarchar](max) NULL,
	[SystemTraceNo] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[TransactionStatus] [int] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Product]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Product](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[ShortDescription] [nvarchar](max) NULL,
	[FullDescription] [nvarchar](max) NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](400) NULL,
	[AllowCustomerReviews] [bit] NULL,
	[ApprovedRatingSum] [int] NOT NULL,
	[NotApprovedRatingSum] [int] NULL,
	[ApprovedTotalReviews] [int] NULL,
	[NotApprovedTotalReviews] [int] NULL,
	[CallForPrice] [bit] NULL,
	[Price] [decimal](18, 3) NULL,
	[Weight] [decimal](18, 4) NOT NULL,
	[Length] [decimal](18, 4) NOT NULL,
	[Width] [decimal](18, 4) NOT NULL,
	[Height] [decimal](18, 4) NOT NULL,
	[Published] [bit] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[TrackingCode] [nvarchar](20) NULL,
	[StockQuantity] [int] NULL,
	[Discount] [decimal](18, 3) NULL,
	[DiscountType] [tinyint] NULL,
	[SpecialOffer] [bit] NULL,
	[HasDiscount] [bit] NULL,
	[IsDownload] [bit] NULL,
	[ShippingCostID] [uniqueidentifier] NULL,
	[DeliveryDateID] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Product_ProductAttribute_Mapping]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Product_ProductAttribute_Mapping](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[ProductAttributeID] [uniqueidentifier] NOT NULL,
	[TextPrompt] [nvarchar](max) NULL,
	[IsRequired] [bit] NOT NULL,
	[CreationDate] [smalldatetime] NULL,
	[AttributeControlType] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[Product_SpecificationAttribute_Mapping]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Product_SpecificationAttribute_Mapping](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[SpecificationAttributeOptionId] [uniqueidentifier] NOT NULL,
	[CustomValue] [nvarchar](4000) NULL,
	[ShowOnProductPage] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[ProductAttribute]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[ProductAttribute](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[ProductVariantAttributeValue]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[ProductVariantAttributeValue](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductVariantAttributeID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](400) NOT NULL,
	[IsPreSelected] [bit] NOT NULL,
	[CreationDate] [smalldatetime] NULL,
	[Price] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[RelatedProduct]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[RelatedProduct](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID1] [uniqueidentifier] NOT NULL,
	[ProductID2] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_RelatedProduct] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[Sales]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[Sales](
	[ID] [uniqueidentifier] NOT NULL,
	[PaymentID] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[ShippingCost]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[ShippingCost](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Price] [decimal](18, 3) NULL,
	[Enabled] [tinyint] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_ShippingCost] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [app].[ShoppingCartItem]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[ShoppingCartItem](
	[ID] [uniqueidentifier] NOT NULL,
	[ShoppingID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NULL,
	[AttributeJson] [nvarchar](max) NULL,
	[CreationDate] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[SpecificationAttribute]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[SpecificationAttribute](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [app].[SpecificationAttributeOption]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [app].[SpecificationAttributeOption](
	[ID] [uniqueidentifier] NOT NULL,
	[SpecificationAttributeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [org].[Application]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [org].[Command]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [org].[Department]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [org].[Department](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[RemoverDate] [smalldatetime] NULL,
	[Address] [nvarchar](1000) NULL,
	[PostalCode] [char](10) NULL,
	[Phone] [nvarchar](11) NULL,
	[CodePhone] [nvarchar](3) NULL,
	[Node] [hierarchyid] NULL,
	[Type] [tinyint] NULL,
 CONSTRAINT [PK_orgDepartments] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [org].[Position]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
 CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [org].[PositionRole]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [org].[RefreshToken]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [org].[Role]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [org].[RolePermission]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [org].[User]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [org].[UserAddress]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Attachment]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Attachment](
	[ID] [uniqueidentifier] NOT NULL,
	[ParentID] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[CreationDate] [smalldatetime] NULL,
	[PathType] [tinyint] NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [pbl].[BaseDocument]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[BaseDocument](
	[ID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[RemoveDate] [smalldatetime] NULL,
 CONSTRAINT [PK_BaseDocument] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Contact]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Contact](
	[ID] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](1000) NOT NULL,
	[LastName] [nvarchar](1000) NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[DocumentFlow]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[DocumentFlow](
	[ID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
	[FromPositionID] [uniqueidentifier] NOT NULL,
	[FromUserID] [uniqueidentifier] NOT NULL,
	[FromDocState] [smallint] NOT NULL,
	[ToPositionID] [uniqueidentifier] NOT NULL,
	[ToDocState] [tinyint] NOT NULL,
	[SendType] [tinyint] NOT NULL,
	[Comment] [nvarchar](4000) NULL,
	[ReadDate] [datetime] NULL,
	[ActionDate] [datetime] NULL,
 CONSTRAINT [PK_DocumentFlow] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Download]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [pbl].[Download](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[Data] [varbinary](max) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ExpireDate] [smalldatetime] NOT NULL,
	[PaymentID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Download] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [pbl].[EmailLogs]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[EmailLogs](
	[ID] [uniqueidentifier] NOT NULL,
	[From] [nvarchar](1000) NOT NULL,
	[To] [nvarchar](1000) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[IP] [nvarchar](15) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[EmailStatusType] [tinyint] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_EmailLogs] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[GeneralSetting]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[GeneralSetting](
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Language]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Language](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[LanguageCultureType] [tinyint] NOT NULL,
	[UniqueSeoCode] [nvarchar](100) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Enabled] [tinyint] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Link]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Link](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IconText] [nvarchar](100) NULL,
	[ShowFooter] [bit] NULL,
	[Priority] [int] NULL,
	[CreationDate] [smalldatetime] NULL,
	[Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[LocaleStringResource]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[LocaleStringResource](
	[ID] [uniqueidentifier] NOT NULL,
	[LanguageID] [uniqueidentifier] NOT NULL,
	[ResourceName] [nvarchar](3000) NULL,
	[ResourceValue] [nvarchar](3000) NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_LocaleStringResource] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Menu]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Menu](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[Enabled] [tinyint] NULL,
	[Url] [nvarchar](max) NOT NULL,
	[IconText] [nvarchar](256) NULL,
	[Priority] [int] NULL,
	[Parameters] [nvarchar](max) NULL,
	[RemoverDate] [smalldatetime] NULL,
	[ForeignLink] [tinyint] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Notification]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Notification](
	[ID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](3000) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ReadDate] [smalldatetime] NULL,
	[CreationDate] [smalldatetime] NULL,
	[PositionID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Pages]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Pages](
	[ID] [uniqueidentifier] NOT NULL,
	[Node] [hierarchyid] NOT NULL,
	[ControllerName] [nvarchar](200) NULL,
	[ActionName] [nvarchar](200) NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[Enabled] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreationDate] [smalldatetime] NULL,
	[RouteUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Tags]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Tags](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [pbl].[Tags_Mapping]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Tags_Mapping](
	[TagID] [uniqueidentifier] NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Article]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[Article](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[Description] [nvarchar](max) NULL,
	[VisitedCount] [int] NOT NULL,
	[LikeCount] [int] NULL,
	[DisLikeCount] [int] NULL,
	[UrlDesc] [nvarchar](max) NULL,
	[IsShow] [bit] NOT NULL,
	[UserID] [uniqueidentifier] NULL,
	[RemoverID] [uniqueidentifier] NULL,
	[TrackingCode] [nvarchar](100) NULL,
	[CommentStatus] [tinyint] NULL,
	[CategoryID] [uniqueidentifier] NULL,
	[ReadingTime] [nvarchar](200) NULL,
	[LanguageID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Banner]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[Banner](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[BannerType] [tinyint] NULL,
	[Enabled] [tinyint] NULL,
	[UserID] [uniqueidentifier] NULL,
	[CreationDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Category]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

GO
/****** Object:  Table [ptl].[DynamicPage]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[DynamicPage](
	[ID] [uniqueidentifier] NOT NULL,
	[TrackingCode] [nvarchar](100) NULL,
	[Name] [nvarchar](2000) NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[PageID] [uniqueidentifier] NOT NULL,
	[MetaKeywords] [nvarchar](1000) NULL,
	[VisitedCount] [int] NULL,
	[IsShow] [tinyint] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[UrlDesc] [nvarchar](1000) NOT NULL,
	[CreationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_DynamicPage] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Events]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	[ReadingTime] [nvarchar](200) NULL,
	[LanguageID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Gallery]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[Gallery](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[VisitedCount] [int] NULL,
	[Enabled] [tinyint] NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[TrackingCode] [nvarchar](20) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[MetaKeywords] [nvarchar](1000) NULL,
	[UrlDesc] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[News]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[News](
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
	[ReadingTime] [nvarchar](200) NULL,
	[LanguageID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Pages]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[Pages](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[UrlDesc] [nvarchar](1000) NULL,
	[PageType] [tinyint] NULL,
	[UserID] [uniqueidentifier] NULL,
	[Enabled] [tinyint] NULL,
	[CreationDate] [smalldatetime] NULL,
	[TrackingCode] [nvarchar](100) NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [ptl].[Slider]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[Slider](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreationDate] [smalldatetime] NOT NULL,
	[Priority] [int] NOT NULL,
	[Enabled] [tinyint] NOT NULL,
	[Deleted] [bit] NULL,
	[Url] [nvarchar](max) NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [ptl].[StaticPage]    Script Date: 10/29/2020 6:32:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ptl].[StaticPage](
	[ID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[AttachmentID] [uniqueidentifier] NULL,
	[MetaKeywords] [nvarchar](1000) NULL,
	[VisitedCount] [int] NULL,
	[BannerShow] [tinyint] NULL,
	[Body] [nvarchar](max) NULL,
 CONSTRAINT [PK_StaticPage] PRIMARY KEY CLUSTERED 
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [app].[Bank] ADD  DEFAULT ((0)) FOR [Default]
GO
ALTER TABLE [app].[Product] ADD  DEFAULT ((0)) FOR [IsDownload]
GO
ALTER TABLE [app].[RelatedProduct] ADD  DEFAULT ((1)) FOR [Priority]
GO
ALTER TABLE [app].[ShoppingCartItem] ADD  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [pbl].[Language] ADD  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [pbl].[Link] ADD  DEFAULT ((1)) FOR [ShowFooter]
GO
ALTER TABLE [pbl].[Menu] ADD  CONSTRAINT [DF_Menu_ForeignLink]  DEFAULT ((2)) FOR [ForeignLink]
GO
ALTER TABLE [pbl].[Pages] ADD  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [pbl].[Pages] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [ptl].[Banner] ADD  DEFAULT ((0)) FOR [Enabled]
GO
ALTER TABLE [ptl].[DynamicPage] ADD  DEFAULT ((0)) FOR [VisitedCount]
GO
ALTER TABLE [ptl].[Gallery] ADD  DEFAULT ((0)) FOR [VisitedCount]
GO
ALTER TABLE [ptl].[Gallery] ADD  DEFAULT ((0)) FOR [Enabled]
GO
ALTER TABLE [ptl].[Slider] ADD  DEFAULT ((1)) FOR [Priority]
GO
ALTER TABLE [ptl].[Slider] ADD  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [ptl].[Slider] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [ptl].[StaticPage] ADD  DEFAULT ((0)) FOR [VisitedCount]
GO
ALTER TABLE [ptl].[StaticPage] ADD  DEFAULT ((0)) FOR [BannerShow]
GO
ALTER TABLE [app].[Comment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [app].[Comment_User_Mapping]  WITH CHECK ADD  CONSTRAINT [Comment_User_Mapping_CommentID] FOREIGN KEY([CommentID])
REFERENCES [app].[Comment] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [app].[Comment_User_Mapping] CHECK CONSTRAINT [Comment_User_Mapping_CommentID]
GO
ALTER TABLE [app].[Comment_User_Mapping]  WITH CHECK ADD  CONSTRAINT [Comment_User_Mapping_UserID] FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [app].[Comment_User_Mapping] CHECK CONSTRAINT [Comment_User_Mapping_UserID]
GO
ALTER TABLE [app].[DeliveryDate]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [app].[FAQ]  WITH CHECK ADD FOREIGN KEY([FAQGroupID])
REFERENCES [app].[FAQGroup] ([ID])
GO
ALTER TABLE [app].[Order]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [app].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [app].[Order] ([ID])
GO
ALTER TABLE [app].[Payment]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [app].[Order] ([ID])
GO
ALTER TABLE [app].[Payment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
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
ALTER TABLE [app].[Product_SpecificationAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductSpecificationAttribute_Product] FOREIGN KEY([ProductId])
REFERENCES [app].[Product] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [app].[Product_SpecificationAttribute_Mapping] CHECK CONSTRAINT [ProductSpecificationAttribute_Product]
GO
ALTER TABLE [app].[Product_SpecificationAttribute_Mapping]  WITH CHECK ADD  CONSTRAINT [ProductSpecificationAttribute_SpecificationAttributeOption] FOREIGN KEY([SpecificationAttributeOptionId])
REFERENCES [app].[SpecificationAttributeOption] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [app].[Product_SpecificationAttribute_Mapping] CHECK CONSTRAINT [ProductSpecificationAttribute_SpecificationAttributeOption]
GO
ALTER TABLE [app].[RelatedProduct]  WITH CHECK ADD FOREIGN KEY([ProductID1])
REFERENCES [app].[Product] ([ID])
GO
ALTER TABLE [app].[RelatedProduct]  WITH CHECK ADD FOREIGN KEY([ProductID2])
REFERENCES [app].[Product] ([ID])
GO
ALTER TABLE [app].[Sales]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [app].[Payment] ([ID])
GO
ALTER TABLE [app].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_BaseDocument_Sales] FOREIGN KEY([ID])
REFERENCES [pbl].[BaseDocument] ([ID])
GO
ALTER TABLE [app].[Sales] CHECK CONSTRAINT [FK_BaseDocument_Sales]
GO
ALTER TABLE [app].[ShippingCost]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [app].[ShoppingCartItem]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [app].[Product] ([ID])
GO
ALTER TABLE [app].[ShoppingCartItem]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [app].[SpecificationAttributeOption]  WITH CHECK ADD  CONSTRAINT [SpecificationAttributeOption_SpecificationAttribute] FOREIGN KEY([SpecificationAttributeId])
REFERENCES [app].[SpecificationAttribute] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [app].[SpecificationAttributeOption] CHECK CONSTRAINT [SpecificationAttributeOption_SpecificationAttribute]
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
ALTER TABLE [org].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_User] FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [org].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_User]
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
ALTER TABLE [org].[UserAddress]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[DocumentFlow]  WITH CHECK ADD  CONSTRAINT [FK_DocumentFlow_BaseDocument] FOREIGN KEY([DocumentID])
REFERENCES [pbl].[BaseDocument] ([ID])
GO
ALTER TABLE [pbl].[DocumentFlow] CHECK CONSTRAINT [FK_DocumentFlow_BaseDocument]
GO
ALTER TABLE [pbl].[Download]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [app].[Payment] ([ID])
GO
ALTER TABLE [pbl].[Download]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[EmailLogs]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[Language]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[Link]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[LocaleStringResource]  WITH CHECK ADD FOREIGN KEY([LanguageID])
REFERENCES [pbl].[Language] ([ID])
GO
ALTER TABLE [pbl].[LocaleStringResource]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [pbl].[Tags_Mapping]  WITH CHECK ADD  CONSTRAINT [Tags_Mapping_Tag] FOREIGN KEY([TagID])
REFERENCES [pbl].[Tags] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [pbl].[Tags_Mapping] CHECK CONSTRAINT [Tags_Mapping_Tag]
GO
ALTER TABLE [ptl].[Article]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [ptl].[Category] ([ID])
GO
ALTER TABLE [ptl].[Article]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[Banner]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[DynamicPage]  WITH CHECK ADD FOREIGN KEY([PageID])
REFERENCES [ptl].[Pages] ([ID])
GO
ALTER TABLE [ptl].[DynamicPage]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[Events]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[Gallery]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[News]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [ptl].[Category] ([ID])
GO
ALTER TABLE [ptl].[News]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[Pages]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [org].[User] ([ID])
GO
ALTER TABLE [ptl].[Article] WITH CHECK ADD FOREIGN KEY ([LanguageID])
REFERENCES [pbl].[Language] ([ID])
GO
ALTER TABLE [ptl].[Events] WITH CHECK ADD FOREIGN KEY ([LanguageID])
REFERENCES [pbl].[Language] ([ID])
GO
ALTER TABLE [ptl].[News] WITH CHECK ADD FOREIGN KEY ([LanguageID])
REFERENCES [pbl].[Language] ([ID])

