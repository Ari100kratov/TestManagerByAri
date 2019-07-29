/****** Object:  Table [dbo].[Department]    Script Date: 29.07.2019 3:33:49 ******/
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
/****** Object:  Table [dbo].[Worker]    Script Date: 29.07.2019 3:33:49 ******/
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
 CONSTRAINT [PK_Worker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (22, N'Development', 0)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (23, N'Web', 22)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (24, N'Desktop', 22)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (25, N'WebTesting', 23)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (26, N'DescTesting', 24)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (27, N'SoftwareTesting', 0)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (28, N'UnitTesting', 27)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (29, N'ModulTesting', 27)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (30, N'IntegratedTesting', 27)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (31, N'Production', 0)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (32, N'Managment', 0)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (34, N'MainTeam', 25)
INSERT [dbo].[Department] ([Id], [NameDepartment], [ParentId]) VALUES (35, N'Team', 25)
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Worker] ON 

INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (40, N'Иван', N'Иванов', CAST(N'1989-10-10' AS Date), N'(473) 588-5373', 25)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (41, N'Андрей', N'Макаревич', CAST(N'1996-12-02' AS Date), N'(649) 468-4575', 25)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (43, N'Сергей', N'Куприянов', CAST(N'1989-09-22' AS Date), N'(462) 467-2452', 24)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (44, N'Егор', N'Куликов', CAST(N'1993-09-17' AS Date), N'(437) 346-3463', 24)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (45, N'Артем', N'Шаповалов', CAST(N'1999-06-24' AS Date), N'(437) 373-4734', 26)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (46, N'Никита', N'Валеров', CAST(N'2000-02-08' AS Date), N'(473) 478-3473', 28)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (47, N'Алексей', N'Сазонов', CAST(N'1999-06-16' AS Date), N'(437) 347-3467', 29)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (48, N'Игорь', N'Гнедов', CAST(N'1988-06-25' AS Date), N'(362) 362-3623', 30)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (49, N'Андрей', N'Голубев', CAST(N'1988-10-27' AS Date), N'(377) 548-4563', 30)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (50, N'Артем', N'Морозов', CAST(N'1966-07-21' AS Date), N'(627) 569-5676', 32)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (51, N'Анатолий', N'Буров', CAST(N'1991-03-14' AS Date), N'(623) 623-6234', 31)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (53, N'Антон', N'Бурков', CAST(N'1989-01-03' AS Date), N'(365) 236-2362', 35)
INSERT [dbo].[Worker] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [DepartmentId]) VALUES (54, N'Сергей', N'Гнездов', CAST(N'1958-10-30' AS Date), N'(362) 362-3523', 31)
SET IDENTITY_INSERT [dbo].[Worker] OFF
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_Department]
GO
USE [master]
GO
ALTER DATABASE [TestManagerDB] SET  READ_WRITE 
GO
