USE [MaidanVault]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[VenueId] [int] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[Time] [time](7) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venues]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VenueAddress] [nvarchar](255) NOT NULL,
	[OpenHoursFrom] [nvarchar](10) NOT NULL,
	[OpenHoursTo] [nvarchar](10) NOT NULL,
	[Sport] [nvarchar](50) NOT NULL,
	[VenueType] [nvarchar](50) NOT NULL,
	[ParkingAvailable] [bit] NOT NULL,
	[PricePerHour] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[name] [nvarchar](20) NULL,
	[Ratings] [decimal](3, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[UpcomingBookings]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UpcomingBookings] AS
SELECT 
    b.BookingId, 
    b.UserId, 
    v.Name AS VenueName, 
    b.BookingDate, 
    b.Time, 
    b.Status
FROM Bookings b
JOIN Venues v ON b.venueid = v.id
WHERE b.BookingDate >= GETDATE();
GO
/****** Object:  Table [dbo].[Amenities]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Amenities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VenueId] [int] NOT NULL,
	[Amenity] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Announcements]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[AnnouncementId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[AnnouncementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VenueId] [int] NOT NULL,
	[FilePath] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leaderboard]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leaderboard](
	[UserId] [int] NOT NULL,
	[Points] [int] NULL,
	[Ranking] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SportId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sports]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sports](
	[SportId] [int] IDENTITY(1,1) NOT NULL,
	[SportName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournaments]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournaments](
	[TournamentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[VenueId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TournamentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/24/2025 1:24:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[IsPlayer] [bit] NOT NULL,
	[ProfilePicUrl] [nvarchar](255) NULL,
	[Points] [int] NULL,
	[Ranking] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Amenities] ON 
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (1, 1, N'Changing Rooms')
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (2, 1, N'Parking')
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (3, 2, N'Showers')
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (4, 3, N'Locker Facilities')
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (5, 4, N'Seating Areas')
GO
INSERT [dbo].[Amenities] ([Id], [VenueId], [Amenity]) VALUES (6, 5, N'First Aid')
GO
SET IDENTITY_INSERT [dbo].[Amenities] OFF
GO
SET IDENTITY_INSERT [dbo].[Announcements] ON 
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (1, N'New Booking Discounts!', N'Get 20% off on bookings made this weekend.')
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (2, N'Champions Cup Registration', N'Registrations for the Champions Cup tournament are now open.')
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (3, N'Sky League Kickoff', N'The Sky League tournament begins next month!')
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (4, N'Facility Upgrade', N'New courts and seating areas added at Smash Arena.')
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (5, N'Loyalty Rewards', N'Earn double points for every booking this month.')
GO
INSERT [dbo].[Announcements] ([AnnouncementId], [Title], [Message]) VALUES (6, N'Table Tennis Grand Slam', N'Top players from around the world are joining!')
GO
SET IDENTITY_INSERT [dbo].[Announcements] OFF
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (4, 6, 1, CAST(N'2025-02-28T00:00:00.000' AS DateTime), CAST(N'18:00:00' AS Time), N'Confirmed')
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (5, 7, 3, CAST(N'2025-03-02T00:00:00.000' AS DateTime), CAST(N'16:30:00' AS Time), N'Pending')
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (6, 8, 2, CAST(N'2025-03-05T00:00:00.000' AS DateTime), CAST(N'17:00:00' AS Time), N'Cancelled')
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (7, 9, 5, CAST(N'2025-03-07T00:00:00.000' AS DateTime), CAST(N'19:00:00' AS Time), N'Confirmed')
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (8, 10, 4, CAST(N'2025-03-09T00:00:00.000' AS DateTime), CAST(N'14:00:00' AS Time), N'Pending')
GO
INSERT [dbo].[Bookings] ([BookingId], [UserId], [VenueId], [BookingDate], [Time], [Status]) VALUES (9, 11, 6, CAST(N'2025-03-12T00:00:00.000' AS DateTime), CAST(N'20:00:00' AS Time), N'Confirmed')
GO
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (6, 2000, 1)
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (7, 1600, 2)
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (8, 1400, 3)
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (9, 1200, 5)
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (10, 1000, 7)
GO
INSERT [dbo].[Leaderboard] ([UserId], [Points], [Ranking]) VALUES (11, 800, 10)
GO
SET IDENTITY_INSERT [dbo].[Players] ON 
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (4, 6, 1)
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (5, 7, 2)
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (6, 8, 1)
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (7, 9, 2)
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (8, 10, 1)
GO
INSERT [dbo].[Players] ([Id], [UserId], [SportId]) VALUES (9, 11, 2)
GO
SET IDENTITY_INSERT [dbo].[Players] OFF
GO
SET IDENTITY_INSERT [dbo].[Sports] ON 
GO
INSERT [dbo].[Sports] ([SportId], [SportName]) VALUES (2, N'Basketball')
GO
INSERT [dbo].[Sports] ([SportId], [SportName]) VALUES (1, N'Football')
GO
SET IDENTITY_INSERT [dbo].[Sports] OFF
GO
SET IDENTITY_INSERT [dbo].[Tournaments] ON 
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (1, N'Champions Cup', CAST(N'2025-03-15T00:00:00.000' AS DateTime), N'Registration Open', 1)
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (2, N'Sky League', CAST(N'2025-04-10T00:00:00.000' AS DateTime), N'Ongoing', 2)
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (3, N'Ace Tennis Tournament', CAST(N'2025-05-05T00:00:00.000' AS DateTime), N'Upcoming', 3)
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (4, N'Badminton Blitz', CAST(N'2025-06-12T00:00:00.000' AS DateTime), N'Upcoming', 4)
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (5, N'Cricket World Series', CAST(N'2025-07-20T00:00:00.000' AS DateTime), N'Upcoming', 5)
GO
INSERT [dbo].[Tournaments] ([TournamentId], [Name], [Date], [Status], [VenueId]) VALUES (6, N'Table Tennis Grand Slam', CAST(N'2025-08-25T00:00:00.000' AS DateTime), N'Upcoming', 6)
GO
SET IDENTITY_INSERT [dbo].[Tournaments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (5, N'Abhishek Krishna Shrestha', N'9869664448', N'abhishek.shrestha16@gmail.com', N'$2a$11$wKae3GkaB8m/bnjfEShuvu2nBt6Iq7onim5RQLg1QrSfw.4o.gNMm', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (6, N'John Doe', N'+1234567890', N'john@example.com', N'hashed_password_1', 1, N'https://example.com/john.jpg', 1200, 5)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (7, N'Jane Smith', N'+9876543210', N'jane@example.com', N'hashed_password_2', 0, N'https://example.com/jane.jpg', 1400, 3)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (8, N'Alex Johnson', N'+1122334455', N'alex@example.com', N'hashed_password_3', 1, N'https://example.com/alex.jpg', 1000, 7)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (9, N'Chris Evans', N'+6655443322', N'chris@example.com', N'hashed_password_4', 1, N'https://example.com/chris.jpg', 1600, 2)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (10, N'Emma Watson', N'+9988776655', N'emma@example.com', N'hashed_password_5', 0, N'https://example.com/emma.jpg', 800, 10)
GO
INSERT [dbo].[Users] ([Id], [FullName], [PhoneNumber], [Email], [PasswordHash], [IsPlayer], [ProfilePicUrl], [Points], [Ranking]) VALUES (11, N'David Beckham', N'+5566778899', N'david@example.com', N'hashed_password_6', 1, N'https://example.com/david.jpg', 2000, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Venues] ON 
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (1, N'Downtown Arena, NYC', N'08:00 AM', N'10:00 PM', N'Football', N'Outdoor', 1, CAST(50.00 AS Decimal(10, 2)), N'Spacious football ground.', N'Downtown Arena', CAST(4.70 AS Decimal(3, 2)))
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (2, N'Sky Court, LA', N'09:00 AM', N'09:00 PM', N'Basketball', N'Indoor', 1, CAST(40.00 AS Decimal(10, 2)), N'State-of-the-art basketball court.', N'Sky Court', CAST(4.80 AS Decimal(3, 2)))
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (3, N'Ace Tennis Club, TX', N'07:00 AM', N'08:00 PM', N'Tennis', N'Outdoor', 0, CAST(30.00 AS Decimal(10, 2)), N'Professional tennis courts.', N'Ace Tennis Club', CAST(4.60 AS Decimal(3, 2)))
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (4, N'Smash Arena, SF', N'06:00 AM', N'11:00 PM', N'Badminton', N'Indoor', 1, CAST(25.00 AS Decimal(10, 2)), N'Top-notch badminton facility.', N'Smash Arena', CAST(4.50 AS Decimal(3, 2)))
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (5, N'Legends Cricket Ground, Mumbai', N'06:00 AM', N'10:00 PM', N'Cricket', N'Outdoor', 1, CAST(75.00 AS Decimal(10, 2)), N'Massive cricket stadium.', N'Legends Ground', CAST(4.90 AS Decimal(3, 2)))
GO
INSERT [dbo].[Venues] ([Id], [VenueAddress], [OpenHoursFrom], [OpenHoursTo], [Sport], [VenueType], [ParkingAvailable], [PricePerHour], [Description], [name], [Ratings]) VALUES (6, N'Ping Pong Hall, Chicago', N'08:00 AM', N'09:00 PM', N'Table Tennis', N'Indoor', 0, CAST(20.00 AS Decimal(10, 2)), N'Best ping pong tables in town.', N'Ping Pong Hall', CAST(4.40 AS Decimal(3, 2)))
GO
SET IDENTITY_INSERT [dbo].[Venues] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Sports__14A9DBB039C97DA9]    Script Date: 2/24/2025 1:24:36 PM ******/
ALTER TABLE [dbo].[Sports] ADD UNIQUE NONCLUSTERED 
(
	[SportName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534C820F2D6]    Script Date: 2/24/2025 1:24:36 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Leaderboard] ADD  DEFAULT ((0)) FOR [Points]
GO
ALTER TABLE [dbo].[Leaderboard] ADD  DEFAULT ((0)) FOR [Ranking]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Points]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Ranking]
GO
ALTER TABLE [dbo].[Venues] ADD  DEFAULT ((0)) FOR [ParkingAvailable]
GO
ALTER TABLE [dbo].[Amenities]  WITH CHECK ADD FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Leaderboard]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_Sports] FOREIGN KEY([SportId])
REFERENCES [dbo].[Sports] ([SportId])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_Sports]
GO
ALTER TABLE [dbo].[Tournaments]  WITH CHECK ADD  CONSTRAINT [FK_Tournaments_Venues] FOREIGN KEY([VenueId])
REFERENCES [dbo].[Venues] ([Id])
GO
ALTER TABLE [dbo].[Tournaments] CHECK CONSTRAINT [FK_Tournaments_Venues]
GO
