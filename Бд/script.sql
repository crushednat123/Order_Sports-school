ALTER DATABASE [SportSchool] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SportSchool].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO﻿
ALTER DATABASE [SportSchool] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SportSchool] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SportSchool] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SportSchool] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SportSchool] SET ARITHABORT OFF 
GO
ALTER DATABASE [SportSchool] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SportSchool] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SportSchool] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SportSchool] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SportSchool] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SportSchool] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SportSchool] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SportSchool] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SportSchool] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SportSchool] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SportSchool] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SportSchool] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SportSchool] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SportSchool] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SportSchool] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SportSchool] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SportSchool] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SportSchool] SET RECOVERY FULL 
GO
ALTER DATABASE [SportSchool] SET  MULTI_USER 
GO
ALTER DATABASE [SportSchool] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SportSchool] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SportSchool] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SportSchool] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SportSchool] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SportSchool] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SportSchool', N'ON'
GO
ALTER DATABASE [SportSchool] SET QUERY_STORE = ON
GO
ALTER DATABASE [SportSchool] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SportSchool]
GO
/****** Object:  Table [dbo].[Coach]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[SurName] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Telephone] [nvarchar](12) NULL,
 CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[SurName] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Telephone] [nvarchar](18) NULL,
 CONSTRAINT [PK_Parents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecordingsOffClasses]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordingsOffClasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdStudent] [int] NULL,
	[IdSession] [int] NULL,
 CONSTRAINT [PK_RecordingsOffClasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] NOT NULL,
	[DayOfWeek] [nvarchar](12) NULL,
	[TimeSlot] [time](7) NULL,
	[IdCoach] [int] NULL,
	[IdSession] [int] NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSport] [int] NULL,
	[Date] [date] NULL,
	[Time] [datetime] NULL,
	[Location] [nvarchar](50) NULL,
	[IdCoach] [int] NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sports]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [money] NULL,
 CONSTRAINT [PK_Sports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SportsAndCoach]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SportsAndCoach](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCoach] [int] NOT NULL,
	[IdSports] [int] NOT NULL,
 CONSTRAINT [PK_SportsAndCoach_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SportsStudents]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SportsStudents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSport] [int] NOT NULL,
	[IdStudent] [int] NOT NULL,
 CONSTRAINT [PK_SportsStudents_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 12.04.2025 21:35:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[SurName] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[IdParents] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Coach] ON 

INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (1, N'Иван', N' Иванов', N' Иванович', N'12345678901')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (2, N'Сергей', N' Петров', N' Сергеевич', N'12345678902')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (3, N'Александр', N' Сидоров', N' Алексеевич', N'12345678903')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (4, N'Михаил', N' Кузнецов', N' Михайлович', N'12345678904')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (5, N'Николай', N' Фёдоров', N' Николаевич', N'12345678905')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (6, N'Дмитрий', N' Орлов', N' Дмитриевич', N'12345678906')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (7, N'Василий', N' Павлов', N' Васильевич', N'12345678907')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (8, N'Андрей', N' Морозов', N' Андреевич', N'12345678908')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (9, N'Петр', N' Воробьев', N' Петрович', N'12345678909')
INSERT [dbo].[Coach] ([Id], [Name], [SurName], [Patronymic], [Telephone]) VALUES (10, N'Григорий', N' Соловьев', N' Григорьевич', N'12345678910')
SET IDENTITY_INSERT [dbo].[Coach] OFF
GO
SET IDENTITY_INSERT [dbo].[Parents] ON 

INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (1, N'Мария', N'Иванова', N'Петровна', N'г. Москва, ул. Ленина, д. 12', N'+7 (495) 123-45-67')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (2, N'Алексей', N'Петров', N'Иванович', N'г. Санкт-Петербург, пр. Невский, д. 45', N'+7 (812) 234-56-78')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (3, N'Анна', N'Сидорова', N'Владимировна', N'г. Новосибирск, ул. Красная, д. 18', N'+7 (383) 345-67-89')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (4, N'Дмитрий', N'Кузнецов', N'Алексеевич', N'г. Екатеринбург, ул. Победы, д. 3', N'+7 (343) 456-78-90')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (5, N'Елена', N'Орлова', N'Сергеевна', N'г. Казань, ул. Московская, д. 7', N'+7 (843) 567-89-01')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (6, N'Игорь', N'Федоров', N'Михайлович', N'г. Ростов-на-Дону, ул. Зелёная, д. 25', N'+7 (863) 678-90-12')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (7, N'Светлана', N'Павлова', N'Константиновна', N'г. Омск, ул. Солнечная, д. 8', N'+7 (381) 789-01-23')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (8, N'Сергей', N'Николаев', N'Васильевич', N'г. Челябинск, ул. Южная, д. 22', N'+7 (351) 890-12-34')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (9, N'Екатерина', N'Тихонова', N'Аркадьевна', N'г. Самара, ул. Гагарина, д. 15', N'+7 (846) 901-23-45')
INSERT [dbo].[Parents] ([Id], [Name], [SurName], [Patronymic], [Address], [Telephone]) VALUES (10, N'Николай', N'Васильев', N'Игоревич', N'г. Уфа, ул. Центральная, д. 30', N'+7 (347) 012-34-56')
SET IDENTITY_INSERT [dbo].[Parents] OFF
GO
SET IDENTITY_INSERT [dbo].[RecordingsOffClasses] ON 

INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (1, 1, 1)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (2, 2, 2)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (3, 3, 3)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (4, 4, 4)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (5, 5, 5)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (6, 6, 6)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (7, 7, 7)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (8, 8, 8)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (9, 9, 9)
INSERT [dbo].[RecordingsOffClasses] ([Id], [IdStudent], [IdSession]) VALUES (10, 10, 10)
SET IDENTITY_INSERT [dbo].[RecordingsOffClasses] OFF
GO
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (1, N'Понедельник', CAST(N'10:00:00' AS Time), 1, 1)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (2, N'Вторник', CAST(N'12:30:00' AS Time), 2, 2)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (3, N'Среда', CAST(N'09:00:00' AS Time), 3, 3)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (4, N'Четверг', CAST(N'14:00:00' AS Time), 4, 4)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (5, N'Пятница', CAST(N'17:00:00' AS Time), 5, 5)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (6, N'Суббота', CAST(N'08:30:00' AS Time), 6, 6)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (7, N'Воскресенье', CAST(N'11:00:00' AS Time), 7, 7)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (8, N'Понедельник', CAST(N'16:00:00' AS Time), 8, 8)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (9, N'Вторник', CAST(N'18:00:00' AS Time), 9, 9)
INSERT [dbo].[Schedules] ([Id], [DayOfWeek], [TimeSlot], [IdCoach], [IdSession]) VALUES (10, N'Среда', CAST(N'07:30:00' AS Time), 10, 10)
GO
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (1, 2, CAST(N'2025-04-24' AS Date), CAST(N'1900-01-01T14:20:00.000' AS DateTime), N' Рига', 7)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (2, 2, CAST(N'2025-04-05' AS Date), CAST(N'1900-01-01T11:00:00.000' AS DateTime), N' Москва', 2)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (3, 3, CAST(N'2025-04-06' AS Date), CAST(N'1900-01-01T12:00:00.000' AS DateTime), N' Лондон', 3)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (4, 4, CAST(N'2025-04-07' AS Date), CAST(N'1900-01-01T13:00:00.000' AS DateTime), N' Берлин', 4)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (5, 5, CAST(N'2025-04-08' AS Date), CAST(N'1900-01-01T14:00:00.000' AS DateTime), N' Париж', 5)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (6, 6, CAST(N'2025-04-09' AS Date), CAST(N'1900-01-01T15:00:00.000' AS DateTime), N' Токио', 6)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (7, 7, CAST(N'2025-04-10' AS Date), CAST(N'1900-01-01T16:00:00.000' AS DateTime), N' Нью-Йорк', 7)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (8, 8, CAST(N'2025-04-11' AS Date), CAST(N'1900-01-01T17:00:00.000' AS DateTime), N' Сидней', 8)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (9, 9, CAST(N'2025-04-12' AS Date), CAST(N'1900-01-01T18:00:00.000' AS DateTime), N' Рим', 9)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (10, 9, CAST(N'2025-04-24' AS Date), CAST(N'1900-01-01T19:45:00.000' AS DateTime), N'Пекин190', 7)
INSERT [dbo].[Sessions] ([Id], [IdSport], [Date], [Time], [Location], [IdCoach]) VALUES (11, 10, CAST(N'2025-04-05' AS Date), CAST(N'1900-01-01T03:25:00.000' AS DateTime), N'Краснодар', 9)
SET IDENTITY_INSERT [dbo].[Sessions] OFF
GO
SET IDENTITY_INSERT [dbo].[Sports] ON 

INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (1, N' Футбол', 20000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (2, N' Тяжелая атлетика', 21000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (3, N' Гимнастика', 22000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (4, N' Баскетбол', 23000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (5, N' Хоккей', 50000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (6, N' Лыжный спорт', 40000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (7, N' Велоспорт', 30000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (8, N' Теннис', 25000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (9, N' Плавание', 3000.0000)
INSERT [dbo].[Sports] ([Id], [Name], [Price]) VALUES (10, N' Легкая атлетика', 5000.0000)
SET IDENTITY_INSERT [dbo].[Sports] OFF
GO
SET IDENTITY_INSERT [dbo].[SportsAndCoach] ON 

INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (1, 1, 1)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (2, 2, 2)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (3, 3, 3)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (4, 4, 4)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (5, 5, 5)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (6, 6, 6)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (7, 7, 7)
INSERT [dbo].[SportsAndCoach] ([Id], [IdCoach], [IdSports]) VALUES (8, 7, 6)
SET IDENTITY_INSERT [dbo].[SportsAndCoach] OFF
GO
SET IDENTITY_INSERT [dbo].[SportsStudents] ON 

INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (1, 1, 2)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (2, 2, 2)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (3, 3, 3)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (4, 3, 4)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (5, 4, 1)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (6, 5, 1)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (7, 6, 5)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (8, 7, 6)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (9, 8, 9)
INSERT [dbo].[SportsStudents] ([Id], [IdSport], [IdStudent]) VALUES (22, 9, 10)
SET IDENTITY_INSERT [dbo].[SportsStudents] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (1, N'Алексей', N'Иванов', N'Петрович', CAST(N'2001-05-12' AS Date), 1)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (2, N'Анна', N'Петрова', N'Сергеевна', CAST(N'1999-09-18' AS Date), 2)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (3, N'Максим', N'Сидоров', N'Андреевич', CAST(N'2002-01-22' AS Date), 3)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (4, N'Ольга', N'Кузнецова', N'Ивановна', CAST(N'2000-03-03' AS Date), 4)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (5, N'Виктор', N'Смирнов', N'Дмитриевич', CAST(N'2003-07-15' AS Date), 5)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (6, N'Елена', N'Орлова', N'Владимировна', CAST(N'1998-12-10' AS Date), 6)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (7, N'Артем', N'Тихонов', N'Николаевич', CAST(N'1997-04-25' AS Date), 7)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (8, N'Мария', N'Павлова', N'Алексеевна', CAST(N'1996-06-08' AS Date), 8)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (9, N'Сергей', N'Николаев', N'Владимирович', CAST(N'1995-08-19' AS Date), 9)
INSERT [dbo].[Student] ([Id], [Name], [SurName], [Patronymic], [BirthDate], [IdParents]) VALUES (10, N'Ирина', N'Федорова', N'Константиновна', CAST(N'2001-10-30' AS Date), 10)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[RecordingsOffClasses]  WITH CHECK ADD  CONSTRAINT [FK_RecordingsOffClasses_Sessions] FOREIGN KEY([IdSession])
REFERENCES [dbo].[Sessions] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecordingsOffClasses] CHECK CONSTRAINT [FK_RecordingsOffClasses_Sessions]
GO
ALTER TABLE [dbo].[RecordingsOffClasses]  WITH CHECK ADD  CONSTRAINT [FK_RecordingsOffClasses_Student] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecordingsOffClasses] CHECK CONSTRAINT [FK_RecordingsOffClasses_Student]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Coach] FOREIGN KEY([IdCoach])
REFERENCES [dbo].[Coach] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Coach]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Coach] FOREIGN KEY([IdCoach])
REFERENCES [dbo].[Coach] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Coach]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Sports] FOREIGN KEY([IdSport])
REFERENCES [dbo].[Sports] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Sports]
GO
ALTER TABLE [dbo].[SportsAndCoach]  WITH CHECK ADD  CONSTRAINT [FK_SportsAndCoach_Coach] FOREIGN KEY([IdCoach])
REFERENCES [dbo].[Coach] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SportsAndCoach] CHECK CONSTRAINT [FK_SportsAndCoach_Coach]
GO
ALTER TABLE [dbo].[SportsAndCoach]  WITH CHECK ADD  CONSTRAINT [FK_SportsAndCoach_Sports] FOREIGN KEY([IdSports])
REFERENCES [dbo].[Sports] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SportsAndCoach] CHECK CONSTRAINT [FK_SportsAndCoach_Sports]
GO
ALTER TABLE [dbo].[SportsStudents]  WITH CHECK ADD  CONSTRAINT [FK_SportsStudents_Sports] FOREIGN KEY([IdSport])
REFERENCES [dbo].[Sports] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SportsStudents] CHECK CONSTRAINT [FK_SportsStudents_Sports]
GO
ALTER TABLE [dbo].[SportsStudents]  WITH CHECK ADD  CONSTRAINT [FK_SportsStudents_Student] FOREIGN KEY([IdStudent])
REFERENCES [dbo].[Student] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SportsStudents] CHECK CONSTRAINT [FK_SportsStudents_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Parents] FOREIGN KEY([IdParents])
REFERENCES [dbo].[Parents] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Parents]
GO
USE [master]
GO
ALTER DATABASE [SportSchool] SET  READ_WRITE 
GO
