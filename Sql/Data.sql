USE [master]
GO
/****** Object:  Database [Gym]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE DATABASE [Gym]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gym', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Gym.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Gym_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Gym_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Gym] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gym].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gym] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gym] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gym] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gym] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gym] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gym] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Gym] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gym] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gym] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gym] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gym] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gym] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gym] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gym] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gym] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Gym] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gym] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gym] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gym] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gym] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gym] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Gym] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gym] SET RECOVERY FULL 
GO
ALTER DATABASE [Gym] SET  MULTI_USER 
GO
ALTER DATABASE [Gym] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gym] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gym] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gym] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Gym] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Gym] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Gym', N'ON'
GO
ALTER DATABASE [Gym] SET QUERY_STORE = ON
GO
ALTER DATABASE [Gym] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Gym]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountCode] [varchar](10) NOT NULL,
	[PhoneNumber] [char](10) NOT NULL,
	[IdNumber] [char](12) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Age] [int] NOT NULL,
	[LivingAt] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[UpdateBy] [varchar](10) NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[SalaryCode] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountCode] ASC,
	[PhoneNumber] ASC,
	[IdNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchCode] [varchar](6) NOT NULL,
	[Address] [nvarchar](450) NOT NULL,
	[BranchName] [nvarchar](max) NOT NULL,
	[QuantityOfStaffs] [int] NOT NULL,
	[QuantityOfPTs] [int] NOT NULL,
	[QuantityOfWorkers] [int] NOT NULL,
	[AdminUpdate] [varchar](10) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[BranchCode] ASC,
	[Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerCode] [varchar](12) NOT NULL,
	[PhoneNumber] [char](10) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[IsBanned] [bit] NOT NULL,
	[BannedReason] [nvarchar](max) NULL,
	[BranchCode] [varchar](6) NOT NULL,
	[UpdateBy] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerCode] ASC,
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomersVouchers]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomersVouchers](
	[OrderNumber] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [varchar](12) NOT NULL,
	[PackageCode] [varchar](5) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateBy] [varchar](10) NOT NULL,
 CONSTRAINT [PK_CustomersVouchers] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSalaries]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSalaries](
	[EmpSalCode] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[BranchName] [nvarchar](max) NOT NULL,
	[Month] [date] NOT NULL,
	[WorkDays] [int] NOT NULL,
	[PriceTotals] [money] NOT NULL,
	[Note] [nvarchar](max) NULL,
	[IsPaid] [bit] NOT NULL,
	[ProofImage] [varbinary](max) NULL,
	[AccountCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_EmployeeSalaries] PRIMARY KEY CLUSTERED 
(
	[EmpSalCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[EquipCode] [varchar](10) NOT NULL,
	[BranchCode] [varchar](6) NOT NULL,
	[EquipName] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[StaffUpdate] [varchar](10) NULL,
	[AdminUpdate] [varchar](10) NOT NULL,
	[IsReceived] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[EquipCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fines]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fines](
	[FineCode] [uniqueidentifier] NOT NULL,
	[CustomerCode] [varchar](12) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[IsCompensated] [bit] NOT NULL,
	[AdminNote] [nvarchar](max) NULL,
	[StaffNote] [nvarchar](max) NULL,
	[UpdateBy] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Fines] PRIMARY KEY CLUSTERED 
(
	[FineCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[OrderNumber] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salaries]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salaries](
	[SalaryCode] [varchar](5) NOT NULL,
	[SalaryType] [nvarchar](450) NOT NULL,
	[PricesApply] [money] NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Salaries] PRIMARY KEY CLUSTERED 
(
	[SalaryCode] ASC,
	[SalaryType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicePackages]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePackages](
	[PackageCode] [varchar](5) NOT NULL,
	[PackageName] [nvarchar](450) NOT NULL,
	[Price] [money] NOT NULL,
	[MemberQuantity] [int] NOT NULL,
	[NumberOfDays] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[AdminUpdate] [varchar](10) NOT NULL,
 CONSTRAINT [PK_ServicePackages] PRIMARY KEY CLUSTERED 
(
	[PackageCode] ASC,
	[PackageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkingChecks]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkingChecks](
	[OrderNumber] [int] IDENTITY(1,1) NOT NULL,
	[CheckDate] [datetime2](7) NOT NULL,
	[CheckOf] [varchar](10) NOT NULL,
	[IsCheckIn] [bit] NOT NULL,
 CONSTRAINT [PK_WorkingChecks] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250306033958_GymlgDB', N'9.0.1')
GO
INSERT [dbo].[Accounts] ([AccountCode], [PhoneNumber], [IdNumber], [FullName], [Age], [LivingAt], [Password], [UpdateBy], [RoleId], [IsDeleted], [SalaryCode]) VALUES (N'AD00000001', N'7898562026', N'374657523710', N'Tran Gia Hao', 21, N'Cong vien phan mem Quang Trung', N'dGdoMTIz', N'AD00000001', 1, 0, N'SA001')
INSERT [dbo].[Accounts] ([AccountCode], [PhoneNumber], [IdNumber], [FullName], [Age], [LivingAt], [Password], [UpdateBy], [RoleId], [IsDeleted], [SalaryCode]) VALUES (N'AD00000002', N'0958476534', N'456372635432', N'Tran Thanh Phong', 30, N'HCM city', N'dHRwMTIz', N'AD00000001', 1, 0, N'SA001')
INSERT [dbo].[Accounts] ([AccountCode], [PhoneNumber], [IdNumber], [FullName], [Age], [LivingAt], [Password], [UpdateBy], [RoleId], [IsDeleted], [SalaryCode]) VALUES (N'ST00000001', N'0847348576', N'123456789012', N'Nguyen Van A', 18, N'Quan 12', N'bnZhMTIz', N'AD00000001', 2, 0, N'SA002')
INSERT [dbo].[Accounts] ([AccountCode], [PhoneNumber], [IdNumber], [FullName], [Age], [LivingAt], [Password], [UpdateBy], [RoleId], [IsDeleted], [SalaryCode]) VALUES (N'WO00000003', N'0384756382', N'456372635432', N'Nguyen Van B', 23, N'Quan 1, Ben Nghe', N'bnZiMTIz', N'AD00000001', 3, 0, N'SA002')
GO
INSERT [dbo].[Branches] ([BranchCode], [Address], [BranchName], [QuantityOfStaffs], [QuantityOfPTs], [QuantityOfWorkers], [AdminUpdate], [IsDeleted]) VALUES (N'BR0001', N'Ha Thi Khiem street, Trung My Tay ward', N'Ha Thi Khiem Gym', 0, 0, 0, N'AD00000001', 0)
INSERT [dbo].[Branches] ([BranchCode], [Address], [BranchName], [QuantityOfStaffs], [QuantityOfPTs], [QuantityOfWorkers], [AdminUpdate], [IsDeleted]) VALUES (N'BR0002', N'Quang trung street, Ward 12', N'Quang Trung Gym', 0, 0, 0, N'AD00000001', 0)
GO
INSERT [dbo].[Customers] ([CustomerCode], [PhoneNumber], [CustomerName], [IsBanned], [BannedReason], [BranchCode], [UpdateBy]) VALUES (N'CT0000000001', N'0384576853', N'Doan Ba Bach', 0, NULL, N'BR0001', N'ST00000001')
GO
INSERT [dbo].[Equipment] ([EquipCode], [BranchCode], [EquipName], [Status], [Note], [StaffUpdate], [AdminUpdate], [IsReceived], [IsDeleted]) VALUES (N'EQ00000001', N'BR0001', N'Lift weight', N'quantity: 9, status: new', NULL, NULL, N'AD00000001', 0, 0)
GO
INSERT [dbo].[Fines] ([FineCode], [CustomerCode], [Date], [Reason], [IsCompensated], [AdminNote], [StaffNote], [UpdateBy]) VALUES (N'd6f23ce4-a1be-4a4c-a705-08dd771bb331', N'CT0000000001', CAST(N'2025-04-09T00:00:00.0000000' AS DateTime2), N'Talk less', 1, NULL, N'', N'ST00000001')
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([OrderNumber], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([OrderNumber], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Roles] ([OrderNumber], [RoleName]) VALUES (3, N'Worker')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[Salaries] ([SalaryCode], [SalaryType], [PricesApply], [UpdateDate]) VALUES (N'SA001', N'Admin', 300000.0000, CAST(N'2025-04-09T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Salaries] ([SalaryCode], [SalaryType], [PricesApply], [UpdateDate]) VALUES (N'SA002', N'Staff', 250000.0000, CAST(N'2025-04-09T03:23:51.7517380' AS DateTime2))
GO
INSERT [dbo].[ServicePackages] ([PackageCode], [PackageName], [Price], [MemberQuantity], [NumberOfDays], [IsDeleted], [AdminUpdate]) VALUES (N'SP001', N'Alone - A day', 100000.0000, 1, 1, 0, N'AD00000001')
INSERT [dbo].[ServicePackages] ([PackageCode], [PackageName], [Price], [MemberQuantity], [NumberOfDays], [IsDeleted], [AdminUpdate]) VALUES (N'SP002', N'Family - A month', 2100000.0000, 4, 31, 0, N'AD00000001')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Accounts_AccountCode]    Script Date: 09/04/2025 11:14:27 SA ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [AK_Accounts_AccountCode] UNIQUE NONCLUSTERED 
(
	[AccountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_RoleId]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_RoleId] ON [dbo].[Accounts]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Accounts_SalaryCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_SalaryCode] ON [dbo].[Accounts]
(
	[SalaryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Branches_BranchCode]    Script Date: 09/04/2025 11:14:27 SA ******/
ALTER TABLE [dbo].[Branches] ADD  CONSTRAINT [AK_Branches_BranchCode] UNIQUE NONCLUSTERED 
(
	[BranchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Branches_AdminUpdate]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Branches_AdminUpdate] ON [dbo].[Branches]
(
	[AdminUpdate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Customers_CustomerCode]    Script Date: 09/04/2025 11:14:27 SA ******/
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [AK_Customers_CustomerCode] UNIQUE NONCLUSTERED 
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customers_BranchCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Customers_BranchCode] ON [dbo].[Customers]
(
	[BranchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CustomersVouchers_CustomerCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_CustomersVouchers_CustomerCode] ON [dbo].[CustomersVouchers]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CustomersVouchers_PackageCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_CustomersVouchers_PackageCode] ON [dbo].[CustomersVouchers]
(
	[PackageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_EmployeeSalaries_AccountCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_EmployeeSalaries_AccountCode] ON [dbo].[EmployeeSalaries]
(
	[AccountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Equipment_BranchCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Equipment_BranchCode] ON [dbo].[Equipment]
(
	[BranchCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Fines_CustomerCode]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_Fines_CustomerCode] ON [dbo].[Fines]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_Salaries_SalaryCode]    Script Date: 09/04/2025 11:14:27 SA ******/
ALTER TABLE [dbo].[Salaries] ADD  CONSTRAINT [AK_Salaries_SalaryCode] UNIQUE NONCLUSTERED 
(
	[SalaryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_ServicePackages_PackageCode]    Script Date: 09/04/2025 11:14:27 SA ******/
ALTER TABLE [dbo].[ServicePackages] ADD  CONSTRAINT [AK_ServicePackages_PackageCode] UNIQUE NONCLUSTERED 
(
	[PackageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_WorkingChecks_CheckOf]    Script Date: 09/04/2025 11:14:27 SA ******/
CREATE NONCLUSTERED INDEX [IX_WorkingChecks_CheckOf] ON [dbo].[WorkingChecks]
(
	[CheckOf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([OrderNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles_RoleId]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Salaries_SalaryCode] FOREIGN KEY([SalaryCode])
REFERENCES [dbo].[Salaries] ([SalaryCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Salaries_SalaryCode]
GO
ALTER TABLE [dbo].[Branches]  WITH CHECK ADD  CONSTRAINT [FK_Branches_Accounts_AdminUpdate] FOREIGN KEY([AdminUpdate])
REFERENCES [dbo].[Accounts] ([AccountCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Branches] CHECK CONSTRAINT [FK_Branches_Accounts_AdminUpdate]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Branches_BranchCode] FOREIGN KEY([BranchCode])
REFERENCES [dbo].[Branches] ([BranchCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Branches_BranchCode]
GO
ALTER TABLE [dbo].[CustomersVouchers]  WITH CHECK ADD  CONSTRAINT [FK_CustomersVouchers_Customers_CustomerCode] FOREIGN KEY([CustomerCode])
REFERENCES [dbo].[Customers] ([CustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomersVouchers] CHECK CONSTRAINT [FK_CustomersVouchers_Customers_CustomerCode]
GO
ALTER TABLE [dbo].[CustomersVouchers]  WITH CHECK ADD  CONSTRAINT [FK_CustomersVouchers_ServicePackages_PackageCode] FOREIGN KEY([PackageCode])
REFERENCES [dbo].[ServicePackages] ([PackageCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomersVouchers] CHECK CONSTRAINT [FK_CustomersVouchers_ServicePackages_PackageCode]
GO
ALTER TABLE [dbo].[EmployeeSalaries]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSalaries_Accounts_AccountCode] FOREIGN KEY([AccountCode])
REFERENCES [dbo].[Accounts] ([AccountCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeSalaries] CHECK CONSTRAINT [FK_EmployeeSalaries_Accounts_AccountCode]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FK_Equipment_Branches_BranchCode] FOREIGN KEY([BranchCode])
REFERENCES [dbo].[Branches] ([BranchCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FK_Equipment_Branches_BranchCode]
GO
ALTER TABLE [dbo].[Fines]  WITH CHECK ADD  CONSTRAINT [FK_Fines_Customers_CustomerCode] FOREIGN KEY([CustomerCode])
REFERENCES [dbo].[Customers] ([CustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Fines] CHECK CONSTRAINT [FK_Fines_Customers_CustomerCode]
GO
ALTER TABLE [dbo].[WorkingChecks]  WITH CHECK ADD  CONSTRAINT [FK_WorkingChecks_Accounts_CheckOf] FOREIGN KEY([CheckOf])
REFERENCES [dbo].[Accounts] ([AccountCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkingChecks] CHECK CONSTRAINT [FK_WorkingChecks_Accounts_CheckOf]
GO
/****** Object:  Trigger [dbo].[add_new_accounts]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_accounts]
On [dbo].[Accounts]
Instead of insert
As
Begin
	Declare @AccountCode Varchar(10), @PhoneNumber Char(10), @IdNumber Char(12), @FullName Nvarchar(max), @Age Int,
		@LivingAt Nvarchar(max), @Password Nvarchar(max), @UpdateBy Varchar(10), @RoleId Int, @IsDeleted Bit, @SalaryCode Varchar(5);
	
	Declare @CodeSetString Varchar(8);
	Set @CodeSetString = '00000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Accounts Where AccountCode Like (Select AccountCode from inserted) COLLATE SQL_Latin1_General_CP1_CI_AS) + 1;

	While exists (Select AccountCode from Accounts where AccountCode = (Select AccountCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @AccountCode = (Select AccountCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PhoneNumber = (Select PhoneNumber from inserted);
	Set @IdNumber = (Select IdNumber from inserted);
	Set @FullName = (Select FullName from inserted);
	Set @Age = (Select Age from inserted);
	Set @LivingAt = (Select LivingAt from inserted);
	Set @Password = (Select Password from inserted);
	Set @UpdateBy = (Select UpdateBy from inserted);
	Set @RoleId = (Select RoleId from inserted);
	Set @IsDeleted = 0;
	Set @SalaryCode = (Select SalaryCode from inserted);

	Insert into Accounts (AccountCode, PhoneNumber, IdNumber, FullName, Age, LivingAt, Password, UpdateBy, RoleId, IsDeleted, SalaryCode) Values
		(@AccountCode, @PhoneNumber, @IdNumber, @FullName, @Age, @LivingAt, @Password, @UpdateBy, @RoleId, @IsDeleted, @SalaryCode);
End
GO
ALTER TABLE [dbo].[Accounts] ENABLE TRIGGER [add_new_accounts]
GO
/****** Object:  Trigger [dbo].[add_new_branches]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_branches]
On [dbo].[Branches]
Instead of insert
As
Begin
	Declare @BranchCode Varchar(6), @Address Nvarchar(450), @BranchName Nvarchar(max), @QuantityOfStaffs Int, @QuantityOfPTs Int,
		@QuantityOfWorkers Int, @AdminUpdate Varchar(10), @IsDeleted Bit;
	
	Declare @CodeSetString Varchar(4);
	Set @CodeSetString = '0000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Branches) + 1;

	While exists (Select BranchCode from Branches where BranchCode = (Select BranchCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @BranchCode = (Select BranchCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @Address = (Select Address from inserted);
	Set @BranchName = (Select BranchName from inserted);
	Set @QuantityOfPTs = 0;
	Set @QuantityOfStaffs = 0;
	Set @QuantityOfWorkers = 0;
	Set @AdminUpdate = (Select AdminUpdate from inserted);
	Set @IsDeleted = 0;

	Insert into Branches (BranchCode, Address, BranchName, QuantityOfStaffs, QuantityOfPTs, QuantityOfWorkers, AdminUpdate, IsDeleted) Values
		(@BranchCode, @Address, @BranchName, @QuantityOfStaffs, @QuantityOfPTs, @QuantityOfWorkers, @AdminUpdate, @IsDeleted);
End
GO
ALTER TABLE [dbo].[Branches] ENABLE TRIGGER [add_new_branches]
GO
/****** Object:  Trigger [dbo].[add_new_customers]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_customers]
On [dbo].[Customers]
Instead of insert
As
Begin
	Declare @CustomerCode Varchar(12), @PhoneNumber Char(10), @CustomerName Nvarchar(max),
		@IsBanned bit, @BannedReason Nvarchar(max), @BranchCode Varchar(6), @UpdateBy Varchar(10);
	
	Declare @CodeSetString Varchar(10);
	Set @CodeSetString = '0000000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Customers) + 1;

	While exists (Select CustomerCode from Customers where CustomerCode = (Select CustomerCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @CustomerCode = (Select CustomerCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PhoneNumber = (Select PhoneNumber from inserted);
	Set @CustomerName = (Select CustomerName from inserted);
	Set @IsBanned = 0;
	Set @BannedReason = null;
	Set @BranchCode = (Select BranchCode from inserted);
	Set @UpdateBy = (Select UpdateBy from inserted);

	Insert into Customers (CustomerCode, PhoneNumber, CustomerName, IsBanned, BannedReason, BranchCode, UpdateBy) values
	(@CustomerCode, @PhoneNumber, @CustomerName, @IsBanned, @BannedReason, @BranchCode, @UpdateBy);
End
GO
ALTER TABLE [dbo].[Customers] ENABLE TRIGGER [add_new_customers]
GO
/****** Object:  Trigger [dbo].[add_new_equipments]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_equipments]
On [dbo].[Equipment]
Instead of insert
As
Begin
	Declare @EquipCode Varchar(10), @BranchCode Varchar(6), @EquipName Nvarchar(max), @Status Nvarchar(max), @Note Nvarchar(max),
		@StaffUpdate Varchar(10), @AdminUpdate Varchar(10), @IsReceived Bit, @IsDeleted Bit;
	
	Declare @CodeSetString Varchar(8);
	Set @CodeSetString = '00000000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Equipment) + 1;

	While exists (Select EquipCode from Equipment where EquipCode = (Select EquipCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @EquipCode = (Select EquipCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @BranchCode = (Select BranchCode from inserted);
	Set @EquipName = (Select EquipName from inserted);
	Set @Status = (Select Status from inserted);
	Set @Note = (Select Note from inserted);
	Set @StaffUpdate = (Select StaffUpdate from inserted);
	Set @AdminUpdate = (Select AdminUpdate from inserted);
	Set @IsReceived = (Select IsReceived from inserted);
	Set @IsDeleted = 0;

	Insert into Equipment (EquipCode, BranchCode, EquipName, Status, Note, StaffUpdate, AdminUpdate, IsReceived, IsDeleted) Values
		(@EquipCode, @BranchCode, @EquipName, @Status, @Note, @StaffUpdate, @AdminUpdate, @IsReceived, @IsDeleted);
End
GO
ALTER TABLE [dbo].[Equipment] ENABLE TRIGGER [add_new_equipments]
GO
/****** Object:  Trigger [dbo].[add_new_salaries]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_salaries]
On [dbo].[Salaries]
Instead of insert
As
Begin
	Declare @SalaryCode Varchar(5), @SalaryType Nvarchar(450), @PricesApply Money, @UpdateDate Datetime2(7);
	
	Declare @CodeSetString Varchar(3);
	Set @CodeSetString = '000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from Salaries) + 1;

	While exists (Select SalaryCode from Salaries where SalaryCode = (Select SalaryCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @SalaryCode = (Select SalaryCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @SalaryType = (Select SalaryType from inserted);
	Set @PricesApply = (Select PricesApply from inserted);
	Set @UpdateDate = (Select UpdateDate from inserted);

	Insert into Salaries (SalaryCode, SalaryType, PricesApply, UpdateDate) Values
		(@SalaryCode, @SalaryType, @PricesApply, @UpdateDate);
End
GO
ALTER TABLE [dbo].[Salaries] ENABLE TRIGGER [add_new_salaries]
GO
/****** Object:  Trigger [dbo].[add_new_service_packages]    Script Date: 09/04/2025 11:14:27 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   trigger [dbo].[add_new_service_packages]
On [dbo].[ServicePackages]
Instead of insert
As
Begin
	Declare @PackageCode Varchar(5), @PackageName Nvarchar(450), @Price Money, @MemberQuantity Int, 
		@NumberOfDays Int, @IsDeleted Bit, @AdminUpdate Varchar(10);
	
	Declare @CodeSetString Varchar(3);
	Set @CodeSetString = '000';

	Declare @Count int;
	Set @Count = (Select COUNT(*) from ServicePackages) + 1;

	While exists (Select PackageCode from ServicePackages where PackageCode = (Select PackageCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count))
		Begin
			Set @Count = @Count + 1;
		End

	Set @PackageCode = (Select PackageCode from inserted) + SUBSTRING(@CodeSetString, 1, Len(@CodeSetString) - Len(CONVERT(varchar(max), @Count))) + CONVERT(varchar(max), @Count);
	Set @PackageName = (Select PackageName from inserted);
	Set @Price = (Select Price from inserted);
	Set @MemberQuantity = (Select MemberQuantity from inserted);
	Set @NumberOfDays = (Select NumberOfdays from inserted);
	Set @IsDeleted = 0;
	Set @AdminUpdate = (Select AdminUpdate from inserted);

	Insert into ServicePackages (PackageCode, PackageName, Price, MemberQuantity, NumberOfDays, IsDeleted, AdminUpdate) Values
		(@PackageCode, @PackageName, @Price, @MemberQuantity, @NumberOfDays, @IsDeleted, @AdminUpdate);
End
GO
ALTER TABLE [dbo].[ServicePackages] ENABLE TRIGGER [add_new_service_packages]
GO
USE [master]
GO
ALTER DATABASE [Gym] SET  READ_WRITE 
GO
