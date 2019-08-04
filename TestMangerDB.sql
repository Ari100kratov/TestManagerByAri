USE [master]
GO
/****** Object:  Database [TestManagerDB]    Script Date: 04.08.2019 23:51:51 ******/
CREATE DATABASE [TestManagerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestManagerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TestManagerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestManagerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TestManagerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TestManagerDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestManagerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestManagerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestManagerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestManagerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestManagerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestManagerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestManagerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestManagerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestManagerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestManagerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestManagerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestManagerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestManagerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestManagerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestManagerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestManagerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestManagerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestManagerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestManagerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestManagerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestManagerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestManagerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestManagerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestManagerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TestManagerDB] SET  MULTI_USER 
GO
ALTER DATABASE [TestManagerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestManagerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestManagerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestManagerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestManagerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestManagerDB] SET QUERY_STORE = OFF
GO
USE [TestManagerDB]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 04.08.2019 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameDepartment] [varchar](50) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worker]    Script Date: 04.08.2019 23:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NULL,
	[LastName] [varchar](30) NULL,
	[DateOfBirth] [date] NULL,
	[PhoneNumber] [varchar](20) NULL,
	[DepartmentId] [int] NULL,
	[SexId] [int] NULL,
 CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (68, N'Development', NULL)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (69, N'Testing', NULL)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (70, N'Advertising', NULL)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (71, N'Production', NULL)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (76, N'Unit testing', 69)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (77, N'Integrate testing', 69)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (78, N'Social media', 70)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (79, N'Television', 78)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (80, N'WEB ', 68)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (81, N'Desktop', 68)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (82, N'Net. Core', 81)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (83, N'Net. Framework', 81)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (104, N'Social network', 78)
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Worker] ON 

INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1069, N'Инна', N'Леева', CAST(N'1988-08-04' AS Date), N'(534) 634-7347', 83, 1)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1070, N'Алексей', N'Воробьев', CAST(N'1999-08-04' AS Date), N'(523) 634-7347', 83, 0)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1071, N'Англелина', N'Петрова', CAST(N'1989-01-03' AS Date), N'(363) 463-4734', 82, 1)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1072, N'Евгений', N'Баженов', CAST(N'1998-08-04' AS Date), N'(523) 562-3634', 81, 0)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1073, N'Алексей', N'Леонидов', CAST(N'1988-08-04' AS Date), N'(352) 363-4634', 80, 0)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1087, N'Андрей', N'Гаврилов', CAST(N'1994-06-06' AS Date), N'(634) 734-7574', 69, 0)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1089, N'Кристина', N'Павлова', CAST(N'1977-09-22' AS Date), N'(362) 363-4634', 77, 1)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1090, N'Александр', N'Валерьев', CAST(N'1965-07-29' AS Date), N'(363) 467-3474', 78, 0)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1091, N'Инна', N'Голубева', CAST(N'1978-08-04' AS Date), N'(574) 574-5745', 78, 1)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId], [SexId]) VALUES (1092, N'Иван', N'Иванов', CAST(N'1989-11-21' AS Date), N'(463) 473-4734', 79, 0)
SET IDENTITY_INSERT [dbo].[Worker] OFF
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_Department]
GO
USE [master]
GO
ALTER DATABASE [TestManagerDB] SET  READ_WRITE 
GO
