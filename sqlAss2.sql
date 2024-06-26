USE [master]
GO
/****** Object:  Database [Ass2PRN231]    Script Date: 18/04/2024 15:25:54 ******/
CREATE DATABASE [Ass2PRN231]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ass2PRN231', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.EVE\MSSQL\DATA\Ass2PRN231.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ass2PRN231_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.EVE\MSSQL\DATA\Ass2PRN231_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Ass2PRN231] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ass2PRN231].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ass2PRN231] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ass2PRN231] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ass2PRN231] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ass2PRN231] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ass2PRN231] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ass2PRN231] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Ass2PRN231] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ass2PRN231] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ass2PRN231] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ass2PRN231] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ass2PRN231] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ass2PRN231] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ass2PRN231] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ass2PRN231] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ass2PRN231] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Ass2PRN231] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ass2PRN231] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ass2PRN231] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ass2PRN231] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ass2PRN231] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ass2PRN231] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ass2PRN231] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ass2PRN231] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Ass2PRN231] SET  MULTI_USER 
GO
ALTER DATABASE [Ass2PRN231] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ass2PRN231] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ass2PRN231] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ass2PRN231] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ass2PRN231] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ass2PRN231] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Ass2PRN231] SET QUERY_STORE = ON
GO
ALTER DATABASE [Ass2PRN231] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Ass2PRN231]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [varchar](255) NULL,
	[first_name] [varchar](255) NULL,
	[phone] [varchar](20) NULL,
	[address] [varchar](255) NULL,
	[city] [varchar](255) NULL,
	[state] [varchar](255) NULL,
	[zip] [varchar](20) NULL,
	[email] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[type] [varchar](255) NULL,
	[pubId] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[advance] [decimal](10, 2) NULL,
	[royalty] [decimal](5, 2) NULL,
	[ytd_sales] [int] NULL,
	[notes] [varchar](255) NULL,
	[date] [date] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookAuthor]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookId] [int] NULL,
	[authorId] [int] NULL,
	[author_order] [int] NULL,
	[royalty_per] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[city] [varchar](255) NULL,
	[state] [varchar](255) NULL,
	[country] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[role_desc] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 18/04/2024 15:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[source] [varchar](255) NULL,
	[first_name] [varchar](255) NULL,
	[last_name] [varchar](255) NULL,
	[roleId] [int] NULL,
	[pubId] [int] NULL,
	[hire_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email]) VALUES (1, N'Nguy?n', N'Nam', N'1234567890', N'123 Ðu?ng ABC', N'Thành ph? HCM', N'HCM', N'700000', N'nam.nguyen@email.com')
INSERT [dbo].[Author] ([id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email]) VALUES (2, N'Tr?n', N'Huong', N'0987654321', N'456 Ðu?ng XYZ', N'Thành ph? Hà N?i', N'HN', N'100000', N'huong.tran@email.com')
INSERT [dbo].[Author] ([id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email]) VALUES (3, N'Lê', N'Ð?c', N'0123456789', N'789 Ðu?ng DEF', N'Thành ph? Ðà N?ng', N'DN', N'500000', N'duc.le@email.com')
INSERT [dbo].[Author] ([id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email]) VALUES (4, N'Ph?m', N'Lan', N'0909090909', N'987 Ðu?ng GHI', N'Thành ph? C?n Tho', N'CT', N'900000', N'lan.pham@email.com')
INSERT [dbo].[Author] ([id], [last_name], [first_name], [phone], [address], [city], [state], [zip], [email]) VALUES (5, N'Hoàng', N'Hà', N'0808080808', N'654 Ðu?ng KLM', N'Thành ph? H?i Phòng', N'HP', N'200000', N'ha.hoang@email.com')
SET IDENTITY_INSERT [dbo].[Author] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([id], [title], [type], [pubId], [price], [advance], [royalty], [ytd_sales], [notes], [date], [IsActive]) VALUES (1, N'Book 1', N'Fiction', 1, CAST(29.99 AS Decimal(10, 2)), CAST(1000.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(5, 2)), 500, N'Lorem ipsum dolor sit amet', CAST(N'2023-01-01' AS Date), 1)
INSERT [dbo].[Book] ([id], [title], [type], [pubId], [price], [advance], [royalty], [ytd_sales], [notes], [date], [IsActive]) VALUES (2, N'Book 2', N'Non-Fiction', 2, CAST(39.99 AS Decimal(10, 2)), CAST(1500.00 AS Decimal(10, 2)), CAST(15.00 AS Decimal(5, 2)), 750, N'Lorem ipsum dolor sit amet', CAST(N'2023-02-01' AS Date), 1)
INSERT [dbo].[Book] ([id], [title], [type], [pubId], [price], [advance], [royalty], [ytd_sales], [notes], [date], [IsActive]) VALUES (3, N'Book 3', N'Fiction', 3, CAST(24.99 AS Decimal(10, 2)), CAST(800.00 AS Decimal(10, 2)), CAST(8.00 AS Decimal(5, 2)), 400, N'Lorem ipsum dolor sit amet', CAST(N'2023-03-01' AS Date), 1)
INSERT [dbo].[Book] ([id], [title], [type], [pubId], [price], [advance], [royalty], [ytd_sales], [notes], [date], [IsActive]) VALUES (4, N'Book 4', N'Non-Fiction', 1, CAST(34.99 AS Decimal(10, 2)), CAST(1200.00 AS Decimal(10, 2)), CAST(12.00 AS Decimal(5, 2)), 600, N'Lorem ipsum dolor sit amet', CAST(N'2023-04-01' AS Date), 1)
INSERT [dbo].[Book] ([id], [title], [type], [pubId], [price], [advance], [royalty], [ytd_sales], [notes], [date], [IsActive]) VALUES (5, N'Book 5', N'Fiction', 2, CAST(19.99 AS Decimal(10, 2)), CAST(600.00 AS Decimal(10, 2)), CAST(6.00 AS Decimal(5, 2)), 300, N'Lorem ipsum dolor sit amet', CAST(N'2023-05-01' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[BookAuthor] ON 

INSERT [dbo].[BookAuthor] ([id], [bookId], [authorId], [author_order], [royalty_per]) VALUES (1, 1, 1, 1, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[BookAuthor] ([id], [bookId], [authorId], [author_order], [royalty_per]) VALUES (2, 1, 2, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[BookAuthor] ([id], [bookId], [authorId], [author_order], [royalty_per]) VALUES (3, 2, 3, 1, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[BookAuthor] ([id], [bookId], [authorId], [author_order], [royalty_per]) VALUES (4, 2, 4, 2, CAST(25.00 AS Decimal(5, 2)))
INSERT [dbo].[BookAuthor] ([id], [bookId], [authorId], [author_order], [royalty_per]) VALUES (5, 3, 5, 1, CAST(12.00 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[BookAuthor] OFF
GO
SET IDENTITY_INSERT [dbo].[Publisher] ON 

INSERT [dbo].[Publisher] ([id], [name], [city], [state], [country]) VALUES (1, N'Publisher A', N'Hanoi', N'HN', N'Vietnam')
INSERT [dbo].[Publisher] ([id], [name], [city], [state], [country]) VALUES (2, N'Publisher B', N'Ho Chi Minh City', N'HCM', N'Vietnam')
INSERT [dbo].[Publisher] ([id], [name], [city], [state], [country]) VALUES (3, N'Publisher C', N'Da Nang', N'DN', N'Vietnam')
INSERT [dbo].[Publisher] ([id], [name], [city], [state], [country]) VALUES (4, N'Publisher D', N'New York', N'NY', N'USA')
INSERT [dbo].[Publisher] ([id], [name], [city], [state], [country]) VALUES (5, N'Publisher E', N'London', NULL, N'UK')
SET IDENTITY_INSERT [dbo].[Publisher] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [role_desc]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([id], [role_desc]) VALUES (2, N'staff')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([id], [email], [password], [source], [first_name], [last_name], [roleId], [pubId], [hire_date]) VALUES (1, N'user1@example.com', N'password1', N'source1', N'John', N'Doe', 1, 1, CAST(N'2023-01-01' AS Date))
INSERT [dbo].[User] ([id], [email], [password], [source], [first_name], [last_name], [roleId], [pubId], [hire_date]) VALUES (2, N'user2@example.com', N'password2', N'source2', N'Jane', N'Smith', 2, 2, CAST(N'2023-02-01' AS Date))
INSERT [dbo].[User] ([id], [email], [password], [source], [first_name], [last_name], [roleId], [pubId], [hire_date]) VALUES (3, N'user3@example.com', N'password3', N'source3', N'Alice', N'Johnson', 2, 3, CAST(N'2023-03-01' AS Date))
INSERT [dbo].[User] ([id], [email], [password], [source], [first_name], [last_name], [roleId], [pubId], [hire_date]) VALUES (4, N'user4@example.com', N'password4', N'source4', N'Bob', N'Williams', 1, 4, CAST(N'2023-04-01' AS Date))
INSERT [dbo].[User] ([id], [email], [password], [source], [first_name], [last_name], [roleId], [pubId], [hire_date]) VALUES (5, N'user5@example.com', N'password5', N'source5', N'Emily', N'Brown', 1, 5, CAST(N'2023-05-01' AS Date))
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__AB6E61648F6EE085]    Script Date: 18/04/2024 15:25:54 ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD FOREIGN KEY([pubId])
REFERENCES [dbo].[Publisher] ([id])
GO
ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD FOREIGN KEY([authorId])
REFERENCES [dbo].[Author] ([id])
GO
ALTER TABLE [dbo].[BookAuthor]  WITH CHECK ADD FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([pubId])
REFERENCES [dbo].[Publisher] ([id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([id])
GO
USE [master]
GO
ALTER DATABASE [Ass2PRN231] SET  READ_WRITE 
GO
