USE [FlexiTask]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityChange]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityChange](
	[EntityChangeID] [int] IDENTITY(1,1) NOT NULL,
	[Entity] [varchar](50) NOT NULL,
	[EntityField] [varchar](50) NOT NULL,
	[FieldType] [varchar](50) NOT NULL,
	[OldValue] [varchar](50) NOT NULL,
	[NewValue] [varchar](50) NOT NULL,
	[ChangedAt] [datetimeoffset](7) NOT NULL,
	[ChangedBy] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_EntityChange] PRIMARY KEY CLUSTERED 
(
	[EntityChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JsonSchemaTemplate]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JsonSchemaTemplate](
	[JsonSchemaTemplateID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [varchar](50) NOT NULL,
	[JsonSchema] [nvarchar](max) NOT NULL,
	[Version] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_JsonSchemaTemplate] PRIMARY KEY CLUSTERED 
(
	[JsonSchemaTemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoadingTask]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoadingTask](
	[TaskHeaderID] [int] NOT NULL,
	[TaskItemsID] [int] NOT NULL,
	[Product] [varchar](50) NOT NULL,
	[Qty] [float] NOT NULL,
	[Support] [varchar](50) NULL,
	[AreaForLoading] [int] NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_LoadingTask] PRIMARY KEY CLUSTERED 
(
	[TaskHeaderID] ASC,
	[TaskItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationID] [varchar](10) NOT NULL,
	[Label] [varchar](20) NOT NULL,
	[WorkAreaId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lot]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lot](
	[LocationID] [varchar](10) NOT NULL,
	[StorageID] [varchar](10) NOT NULL,
	[LotID] [varchar](10) NOT NULL,
	[Blocked] [bit] NOT NULL,
	[PositionStorage] [int] NULL,
	[ProductID] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Lot] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[StorageID] ASC,
	[LotID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plant]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plant](
	[PlantID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[Code] [nvarchar](4) NOT NULL,
	[CommonName] [nvarchar](50) NULL,
	[LanguageEnum] [nvarchar](50) NOT NULL,
	[Active] [bit] NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Plant] PRIMARY KEY CLUSTERED 
(
	[PlantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantIDentity]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantIDentity](
	[PlantID] [int] NOT NULL,
	[Id_AspnetIdentity] [nvarchar](450) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantIDentity] PRIMARY KEY CLUSTERED 
(
	[PlantID] ASC,
	[Id_AspnetIdentity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Storage]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Storage](
	[LocationID] [varchar](10) NOT NULL,
	[StorageID] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[LengthInMillimeters] [float] NOT NULL,
	[Empty] [bit] NOT NULL,
	[Id] [nvarchar](max) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Storage] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC,
	[StorageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskData]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskData](
	[TaskDataID] [int] IDENTITY(1,1) NOT NULL,
	[JsonData] [varchar](max) NOT NULL,
	[ExternalLink] [varchar](50) NULL,
	[JsonSchemaTemplateId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaskData] PRIMARY KEY CLUSTERED 
(
	[TaskDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskHeader]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskHeader](
	[TaskHeaderID] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaskHeader] PRIMARY KEY CLUSTERED 
(
	[TaskHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskItemDependency]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItemDependency](
	[TaskHeaderID] [int] NOT NULL,
	[TaskItemsID] [int] NOT NULL,
	[TaskHeaderID_DependOn] [int] NOT NULL,
	[TaskItemsID_DependOn] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TaskItemDependency] PRIMARY KEY CLUSTERED 
(
	[TaskHeaderID_DependOn] ASC,
	[TaskItemsID_DependOn] ASC,
	[TaskHeaderID] ASC,
	[TaskItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskItems]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItems](
	[TaskHeaderID] [int] NOT NULL,
	[TaskItemsID] [int] IDENTITY(1,1) NOT NULL,
	[StartingTask] [bit] NOT NULL,
	[EndingTask] [bit] NOT NULL,
	[LinkedWorkArea] [int] NULL,
	[TaskDataId] [int] NOT NULL,
	[TaskItemTypeId] [varchar](4) NULL,
	[TaskStatusId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaskItems] PRIMARY KEY CLUSTERED 
(
	[TaskHeaderID] ASC,
	[TaskItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskItemType]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskItemType](
	[TaskItemType_ID] [varchar](4) NOT NULL,
	[InstructionDescription] [varchar](20) NOT NULL,
	[JsonSchemaTemplateID] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaskItemType] PRIMARY KEY CLUSTERED 
(
	[TaskItemType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[TaskStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](5) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED 
(
	[TaskStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransportTask]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportTask](
	[TaskHeaderID] [int] NOT NULL,
	[TaskItemsID] [int] NOT NULL,
	[Support] [varchar](20) NULL,
	[DestinationArea] [int] NOT NULL,
	[SourceArea] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_TransportTask] PRIMARY KEY CLUSTERED 
(
	[TaskHeaderID] ASC,
	[TaskItemsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkArea]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkArea](
	[WorkAreaID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NOT NULL,
	[CommonName] [nvarchar](50) NOT NULL,
	[PlantId] [int] NOT NULL,
	[WorkAreaTypeId] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_WorkArea] PRIMARY KEY CLUSTERED 
(
	[WorkAreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkAreaType]    Script Date: 25-01-26 17:23:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkAreaType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Label] [nvarchar](max) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetimeoffset](7) NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_WorkAreaType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e18aaac3-494c-46ea-acf5-b115a02ddead', N'Users', N'USERS', N'a7e2b615-23d0-405c-8f66-1018d24912c7')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e805d68a-28b2-48d5-bd5b-495a1cfe9f95', N'Administrator', N'ADMINISTRATOR', N'7e3466b4-2749-4b86-990e-559715c06d93')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'964ff4c8-2abe-4cf0-a931-d2c56c6e18c2', N'e18aaac3-494c-46ea-acf5-b115a02ddead')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e0ec8f75-c420-45a6-b167-d0db6acd0081', N'e805d68a-28b2-48d5-bd5b-495a1cfe9f95')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', N'e805d68a-28b2-48d5-bd5b-495a1cfe9f95')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'964ff4c8-2abe-4cf0-a931-d2c56c6e18c2', N'nicolas@localhost', N'NICOLAS@LOCALHOST', N'user@localhost', N'USER@LOCALHOST', 0, N'AQAAAAIAAYagAAAAEA+/Fxhhik9DiKp7bC2BhgT2UyBbcZCv2gy1fh+jUvCLNDT/4O3zEpJr7hOEZhBFww==', N'PFBZHJ6NXEWKKENDRUUHDCS7YPYNQNLE', N'2a428e97-ecf1-46a4-bcb9-182313402d39', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e0ec8f75-c420-45a6-b167-d0db6acd0081', N'administrator@local', N'ADMINISTRATOR@LOCAL', N'administrator@local', N'ADMINISTRATOR@LOCAL', 0, N'AQAAAAIAAYagAAAAEI+uAtD772ayvasD3U/0+DejADjtYAdleaWsaElB4AMOkaLf2CaA1GpV+7bj/Os6Yw==', N'APFEAML26SN43DIMII4B5ZNN7CLM5XWI', N'6d58bbfe-67ba-4b39-8819-aeac385feaee', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', N'administrator@localhost', N'ADMINISTRATOR@LOCALHOST', N'administrator@localhost', N'ADMINISTRATOR@LOCALHOST', 0, N'AQAAAAIAAYagAAAAEEf1X9CY7XEuwEnlWNTiCJB2DAFoEF+2EJx4wT3kLdbo82M/m01L9TakF7C+VKWX/g==', N'HHL2ABCHL3DH7RX5ABA2RBXUZA3C6LGC', N'b356b7e9-3525-4ee1-9a04-997220e2b819', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Plant] ON 
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'OST1', N'OSTWERDINGEN', N'DE', 1, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2026-01-25T11:53:35.4530147+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (2, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'MOU1', N'Moustier', N'FR', 1, CAST(N'2026-01-24T21:25:17.4886869+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:12.7387379+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (7, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'SAG1', N'Sagunto', N'ES', 0, CAST(N'2026-01-24T21:28:16.7183086+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:25.3905518+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (8, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'FOS', N'Fosses', N'FR', 1, CAST(N'2026-01-24T21:32:06.3139539+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:10.1298069+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (9, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'aa', N'aa', N'IT', 1, CAST(N'2026-01-24T21:32:49.4218981+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:53:42.2702459+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (10, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'MOUA', N'AAA', N'FR', 1, CAST(N'2026-01-24T21:42:06.7626013+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:04.3860167+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (11, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'MOUC', N'AAA', N'FR', 0, CAST(N'2026-01-24T21:42:26.9244581+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:22.5761944+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (12, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'MO', N'AAA', N'FR', 0, CAST(N'2026-01-24T21:44:02.2117670+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:18.9802269+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (13, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'BXL', N'Bruxelles', N'IT', 1, CAST(N'2026-01-24T22:30:20.8746888+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:53:45.6833381+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (14, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'FO01', N'Pierre', N'ES', 0, CAST(N'2026-01-25T11:43:10.7119739+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:58:39.1753361+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (15, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'F', N'dsd', N'FR', 0, CAST(N'2026-01-25T11:46:04.9357461+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T13:32:53.2169404+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (16, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'sdsd', N'dsdsds', N'FR', 0, CAST(N'2026-01-25T11:46:20.6274134+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:54:28.7343843+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (17, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'zz', N'z', N'FR', 1, CAST(N'2026-01-25T11:46:31.1915242+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:46:31.1915242+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
INSERT [dbo].[Plant] ([PlantID], [CreatedAt], [Code], [CommonName], [LanguageEnum], [Active], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (18, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), N'AX01', N'aa', N'FR', 0, CAST(N'2026-01-25T11:47:35.3379592+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T11:53:15.2768636+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78')
GO
SET IDENTITY_INSERT [dbo].[Plant] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkArea] ON 
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (1, N'WA10', N'T69', 11, 1, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, CAST(N'2026-01-25T10:52:31.0490450+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (2, N'WA02', N'T28 : Command expedition', 1, 1, CAST(N'2026-01-24T18:13:12.2191599+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T10:52:24.9092425+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (3, N'AAA', N'T28 : Command expedition', 1, 1, CAST(N'2026-01-24T18:13:52.5584705+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-24T23:29:18.5928034+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (9, N'WA100', N'T28 : Command expedition', 1, 1, CAST(N'2026-01-24T18:21:13.0576577+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T12:28:15.3147081+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (10, N'WA010', N'Mon AREA', 1, 1, CAST(N'2026-01-24T19:57:32.0666077+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T12:28:09.9849065+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (11, N'a', N'dsdsdsd', 1, 1, CAST(N'2026-01-24T20:03:49.1495228+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-24T22:34:49.8181171+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 0)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (12, N'OST01', N'Laminated', 1, 1, CAST(N'2026-01-24T20:08:22.1351826+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-24T22:39:04.3527783+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 0)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (13, N'FO01', N'Melissa', 11, 1, CAST(N'2026-01-24T21:49:11.7199158+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T12:26:56.1564556+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (14, N'T69', N'rr', 1, 1, CAST(N'2026-01-24T22:28:10.2449137+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-24T22:35:02.1261113+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 0)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (15, N'T69e', N'aaa', 1, 1, CAST(N'2026-01-24T22:28:32.2932202+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T10:57:27.8701632+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (16, N'TEST', N'TEST2', 1, 1, CAST(N'2026-01-25T10:56:41.6349276+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T10:56:41.6349276+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
INSERT [dbo].[WorkArea] ([WorkAreaID], [Code], [CommonName], [PlantId], [WorkAreaTypeId], [Created], [CreatedBy], [LastModified], [LastModifiedBy], [Active]) VALUES (17, N'aa', N'ddd', 2, 1, CAST(N'2026-01-25T12:28:35.2955808+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', CAST(N'2026-01-25T12:28:35.2955808+00:00' AS DateTimeOffset), N'faaa8267-b1cf-40cb-a401-ddf6ace65c78', 1)
GO
SET IDENTITY_INSERT [dbo].[WorkArea] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkAreaType] ON 
GO
INSERT [dbo].[WorkAreaType] ([Id], [Code], [Label], [Created], [CreatedBy], [LastModified], [LastModifiedBy]) VALUES (1, N'PROD', NULL, CAST(N'0001-01-01T00:00:00.0000000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[WorkAreaType] OFF
GO
ALTER TABLE [dbo].[Plant] ADD  CONSTRAINT [DF_Plant_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[WorkArea] ADD  CONSTRAINT [DF_WorkArea_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[EntityChange]  WITH CHECK ADD  CONSTRAINT [FK_EntityChange_AspNetUsers_ChangedBy] FOREIGN KEY([ChangedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[EntityChange] CHECK CONSTRAINT [FK_EntityChange_AspNetUsers_ChangedBy]
GO
ALTER TABLE [dbo].[LoadingTask]  WITH CHECK ADD  CONSTRAINT [FK_LoadingTask_TaskItems_TaskHeaderID_TaskItemsID] FOREIGN KEY([TaskHeaderID], [TaskItemsID])
REFERENCES [dbo].[TaskItems] ([TaskHeaderID], [TaskItemsID])
GO
ALTER TABLE [dbo].[LoadingTask] CHECK CONSTRAINT [FK_LoadingTask_TaskItems_TaskHeaderID_TaskItemsID]
GO
ALTER TABLE [dbo].[LoadingTask]  WITH CHECK ADD  CONSTRAINT [FK_LoadingTask_WorkArea_AreaForLoading] FOREIGN KEY([AreaForLoading])
REFERENCES [dbo].[WorkArea] ([WorkAreaID])
GO
ALTER TABLE [dbo].[LoadingTask] CHECK CONSTRAINT [FK_LoadingTask_WorkArea_AreaForLoading]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_WorkArea_WorkAreaId] FOREIGN KEY([WorkAreaId])
REFERENCES [dbo].[WorkArea] ([WorkAreaID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_WorkArea_WorkAreaId]
GO
ALTER TABLE [dbo].[Lot]  WITH CHECK ADD  CONSTRAINT [FK_Lot_Product_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Lot] CHECK CONSTRAINT [FK_Lot_Product_ProductID]
GO
ALTER TABLE [dbo].[Lot]  WITH CHECK ADD  CONSTRAINT [FK_Lot_Storage_LocationID_StorageID] FOREIGN KEY([LocationID], [StorageID])
REFERENCES [dbo].[Storage] ([LocationID], [StorageID])
GO
ALTER TABLE [dbo].[Lot] CHECK CONSTRAINT [FK_Lot_Storage_LocationID_StorageID]
GO
ALTER TABLE [dbo].[PlantIDentity]  WITH CHECK ADD  CONSTRAINT [FK_PlantIDentity_Plant_PlantID] FOREIGN KEY([PlantID])
REFERENCES [dbo].[Plant] ([PlantID])
GO
ALTER TABLE [dbo].[PlantIDentity] CHECK CONSTRAINT [FK_PlantIDentity_Plant_PlantID]
GO
ALTER TABLE [dbo].[Storage]  WITH CHECK ADD  CONSTRAINT [FK_Storage_Location_LocationID] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Location] ([LocationID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Storage] CHECK CONSTRAINT [FK_Storage_Location_LocationID]
GO
ALTER TABLE [dbo].[TaskData]  WITH CHECK ADD  CONSTRAINT [FK_TaskData_JsonSchemaTemplate_JsonSchemaTemplateId] FOREIGN KEY([JsonSchemaTemplateId])
REFERENCES [dbo].[JsonSchemaTemplate] ([JsonSchemaTemplateID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskData] CHECK CONSTRAINT [FK_TaskData_JsonSchemaTemplate_JsonSchemaTemplateId]
GO
ALTER TABLE [dbo].[TaskItemDependency]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemDependency_TaskItems_TaskHeaderID_DependOn_TaskItemsID_DependOn] FOREIGN KEY([TaskHeaderID_DependOn], [TaskItemsID_DependOn])
REFERENCES [dbo].[TaskItems] ([TaskHeaderID], [TaskItemsID])
GO
ALTER TABLE [dbo].[TaskItemDependency] CHECK CONSTRAINT [FK_TaskItemDependency_TaskItems_TaskHeaderID_DependOn_TaskItemsID_DependOn]
GO
ALTER TABLE [dbo].[TaskItemDependency]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemDependency_TaskItems_TaskHeaderID_TaskItemsID] FOREIGN KEY([TaskHeaderID], [TaskItemsID])
REFERENCES [dbo].[TaskItems] ([TaskHeaderID], [TaskItemsID])
GO
ALTER TABLE [dbo].[TaskItemDependency] CHECK CONSTRAINT [FK_TaskItemDependency_TaskItems_TaskHeaderID_TaskItemsID]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_TaskData_TaskDataId] FOREIGN KEY([TaskDataId])
REFERENCES [dbo].[TaskData] ([TaskDataID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_TaskData_TaskDataId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_TaskHeader_TaskHeaderID] FOREIGN KEY([TaskHeaderID])
REFERENCES [dbo].[TaskHeader] ([TaskHeaderID])
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_TaskHeader_TaskHeaderID]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_TaskItemType_TaskItemTypeId] FOREIGN KEY([TaskItemTypeId])
REFERENCES [dbo].[TaskItemType] ([TaskItemType_ID])
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_TaskItemType_TaskItemTypeId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_TaskStatus_TaskStatusId] FOREIGN KEY([TaskStatusId])
REFERENCES [dbo].[TaskStatus] ([TaskStatusID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_TaskStatus_TaskStatusId]
GO
ALTER TABLE [dbo].[TaskItems]  WITH CHECK ADD  CONSTRAINT [FK_TaskItems_WorkArea_LinkedWorkArea] FOREIGN KEY([LinkedWorkArea])
REFERENCES [dbo].[WorkArea] ([WorkAreaID])
GO
ALTER TABLE [dbo].[TaskItems] CHECK CONSTRAINT [FK_TaskItems_WorkArea_LinkedWorkArea]
GO
ALTER TABLE [dbo].[TaskItemType]  WITH CHECK ADD  CONSTRAINT [FK_TaskItemType_JsonSchemaTemplate_JsonSchemaTemplateID] FOREIGN KEY([JsonSchemaTemplateID])
REFERENCES [dbo].[JsonSchemaTemplate] ([JsonSchemaTemplateID])
GO
ALTER TABLE [dbo].[TaskItemType] CHECK CONSTRAINT [FK_TaskItemType_JsonSchemaTemplate_JsonSchemaTemplateID]
GO
ALTER TABLE [dbo].[TransportTask]  WITH CHECK ADD  CONSTRAINT [FK_TransportTask_TaskItems_TaskHeaderID_TaskItemsID] FOREIGN KEY([TaskHeaderID], [TaskItemsID])
REFERENCES [dbo].[TaskItems] ([TaskHeaderID], [TaskItemsID])
GO
ALTER TABLE [dbo].[TransportTask] CHECK CONSTRAINT [FK_TransportTask_TaskItems_TaskHeaderID_TaskItemsID]
GO
ALTER TABLE [dbo].[TransportTask]  WITH CHECK ADD  CONSTRAINT [FK_TransportTask_WorkArea_DestinationArea] FOREIGN KEY([DestinationArea])
REFERENCES [dbo].[WorkArea] ([WorkAreaID])
GO
ALTER TABLE [dbo].[TransportTask] CHECK CONSTRAINT [FK_TransportTask_WorkArea_DestinationArea]
GO
ALTER TABLE [dbo].[TransportTask]  WITH CHECK ADD  CONSTRAINT [FK_TransportTask_WorkArea_SourceArea] FOREIGN KEY([SourceArea])
REFERENCES [dbo].[WorkArea] ([WorkAreaID])
GO
ALTER TABLE [dbo].[TransportTask] CHECK CONSTRAINT [FK_TransportTask_WorkArea_SourceArea]
GO
ALTER TABLE [dbo].[WorkArea]  WITH CHECK ADD  CONSTRAINT [FK_WorkArea_Plant_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plant] ([PlantID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkArea] CHECK CONSTRAINT [FK_WorkArea_Plant_PlantId]
GO
ALTER TABLE [dbo].[WorkArea]  WITH CHECK ADD  CONSTRAINT [FK_WorkArea_WorkAreaType_WorkAreaTypeId] FOREIGN KEY([WorkAreaTypeId])
REFERENCES [dbo].[WorkAreaType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkArea] CHECK CONSTRAINT [FK_WorkArea_WorkAreaType_WorkAreaTypeId]
GO
ALTER TABLE [dbo].[JsonSchemaTemplate]  WITH CHECK ADD  CONSTRAINT [CK_JsonSchemaTemplate_ValidJson] CHECK  ((isjson([JsonSchema])=(1)))
GO
ALTER TABLE [dbo].[JsonSchemaTemplate] CHECK CONSTRAINT [CK_JsonSchemaTemplate_ValidJson]
GO
ALTER TABLE [dbo].[TaskData]  WITH CHECK ADD  CONSTRAINT [CK_JsonTaskData_ValidJson] CHECK  ((isjson([JsonData])=(1)))
GO
ALTER TABLE [dbo].[TaskData] CHECK CONSTRAINT [CK_JsonTaskData_ValidJson]
GO
