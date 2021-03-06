USE [master]
GO
/****** Object:  Database [SaleManagementDB]    Script Date: 24-11-2021 11:34:04 PM ******/
CREATE DATABASE [SaleManagementDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SaleManagementDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SaleManagementDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SaleManagementDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SaleManagementDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SaleManagementDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SaleManagementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SaleManagementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SaleManagementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SaleManagementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SaleManagementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SaleManagementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SaleManagementDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SaleManagementDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SaleManagementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SaleManagementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SaleManagementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SaleManagementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SaleManagementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SaleManagementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SaleManagementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SaleManagementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SaleManagementDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SaleManagementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SaleManagementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SaleManagementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SaleManagementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SaleManagementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SaleManagementDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SaleManagementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SaleManagementDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SaleManagementDB] SET  MULTI_USER 
GO
ALTER DATABASE [SaleManagementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SaleManagementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SaleManagementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SaleManagementDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SaleManagementDB]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 24-11-2021 11:34:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Price] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SalesItem]    Script Date: 24-11-2021 11:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesList]    Script Date: 24-11-2021 11:34:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SalesList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Total] [int] NULL,
	[Paid] [int] NULL,
	[Due] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (1, N'7up 500ml', 30)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (2, N'Cocacola 1ltr', 50)
INSERT [dbo].[Items] ([Id], [Name], [Price]) VALUES (3, N'Cocacola 2ltr', 110)
SET IDENTITY_INSERT [dbo].[Items] OFF
SET IDENTITY_INSERT [dbo].[SalesItem] ON 

INSERT [dbo].[SalesItem] ([Id], [ItemId], [Price], [Quantity], [Total]) VALUES (13, 0, 0, 0, 0)
INSERT [dbo].[SalesItem] ([Id], [ItemId], [Price], [Quantity], [Total]) VALUES (14, 1, 30, 10, 300)
INSERT [dbo].[SalesItem] ([Id], [ItemId], [Price], [Quantity], [Total]) VALUES (15, 2, 50, 15, 750)
SET IDENTITY_INSERT [dbo].[SalesItem] OFF
SET IDENTITY_INSERT [dbo].[SalesList] ON 

INSERT [dbo].[SalesList] ([Id], [CustomerName], [Phone], [Address], [Total], [Paid], [Due]) VALUES (1, N'Tarequl', N'09876543', N'Cox''s bazar', 1000, 200, 800)
INSERT [dbo].[SalesList] ([Id], [CustomerName], [Phone], [Address], [Total], [Paid], [Due]) VALUES (2, N'Ruhul Kuddus', N'01234567890', N'chattogram', 1600, 500, 1100)
INSERT [dbo].[SalesList] ([Id], [CustomerName], [Phone], [Address], [Total], [Paid], [Due]) VALUES (3, N'Sonjoy paul', N'01234567890', N'Potenga', 610, 300, 310)
SET IDENTITY_INSERT [dbo].[SalesList] OFF
USE [master]
GO
ALTER DATABASE [SaleManagementDB] SET  READ_WRITE 
GO
