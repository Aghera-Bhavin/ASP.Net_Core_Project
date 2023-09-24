CREATE TABLE [dbo].[LOC_City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](100) NOT NULL,
	[StateID] [int] NOT NULL,
	[CountryID] [int] NOT NULL,
	[Citycode] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_LOC_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LOC_Country]    Script Date: 24-Jul-23 9:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOC_Country](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_LOC_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LOC_State]    Script Date: 24-Jul-23 9:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LOC_State](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](100) NOT NULL,
	[CountryID] [int] NOT NULL,
	[StateCode] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_LOC_State] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MST_Branch]    Script Date: 24-Jul-23 9:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MST_Branch](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [varchar](100) NOT NULL,
	[BranchCode] [varchar](100) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_MST_Branch] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MST_Student]    Script Date: 24-Jul-23 9:49:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MST_Student](
	[StudentID] [int] NOT NULL,
	[BranchID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[StudentName] [varchar](100) NOT NULL,
	[MobileNoStudent] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[MobileNoFather] [varchar](100) NULL,
	[Address] [varchar](500) NULL,
	[BirthDate] [datetime] NULL,
	[Age] [int] NULL,
	[IsActive] [bit] NULL,
	[Gender] [varchar](50) NULL,
	[Password] [varchar](100) NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL
	
 CONSTRAINT [PK_MST_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[LOC_City] ADD  CONSTRAINT [DF_LOC_City_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[LOC_City] ADD  CONSTRAINT [DF_LOC_City_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[LOC_Country] ADD  CONSTRAINT [DF_LOC_Country_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[LOC_Country] ADD  CONSTRAINT [DF_LOC_Country_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[LOC_State] ADD  CONSTRAINT [DF_LOC_State_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[LOC_State] ADD  CONSTRAINT [DF_LOC_State_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[MST_Branch] ADD  CONSTRAINT [DF_MST_Branch_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[MST_Branch] ADD  CONSTRAINT [DF_MST_Branch_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[MST_Student] ADD  CONSTRAINT [DF_MST_Student_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[MST_Student] ADD  CONSTRAINT [DF_MST_Student_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[LOC_City]  WITH CHECK ADD  CONSTRAINT [FK_LOC_City_LOC_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[LOC_Country] ([CountryID])
GO
ALTER TABLE [dbo].[LOC_City] CHECK CONSTRAINT [FK_LOC_City_LOC_Country]
GO
ALTER TABLE [dbo].[LOC_City]  WITH CHECK ADD  CONSTRAINT [FK_LOC_City_LOC_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[LOC_State] ([StateID])
GO
ALTER TABLE [dbo].[LOC_City] CHECK CONSTRAINT [FK_LOC_City_LOC_State]
GO
ALTER TABLE [dbo].[LOC_State]  WITH CHECK ADD  CONSTRAINT [FK_LOC_State_LOC_Country] FOREIGN KEY([CountryID])
REFERENCES [dbo].[LOC_Country] ([CountryID])
GO
ALTER TABLE [dbo].[LOC_State] CHECK CONSTRAINT [FK_LOC_State_LOC_Country]
GO
ALTER TABLE [dbo].[MST_Student]  WITH CHECK ADD  CONSTRAINT [FK_MST_Student_LOC_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[LOC_City] ([CityID])
GO
ALTER TABLE [dbo].[MST_Student] CHECK CONSTRAINT [FK_MST_Student_LOC_City]
GO
ALTER TABLE [dbo].[MST_Student]  WITH CHECK ADD  CONSTRAINT [FK_MST_Student_MST_Branch] FOREIGN KEY([BranchID])
REFERENCES [dbo].[MST_Branch] ([BranchID])
GO
ALTER TABLE [dbo].[MST_Student] CHECK CONSTRAINT [FK_MST_Student_MST_Branch]

