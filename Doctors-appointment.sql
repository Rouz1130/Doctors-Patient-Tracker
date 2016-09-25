USE [doctor_appointment]
GO
/****** Object:  Table [dbo].[doctors]    Script Date: 9/25/2016 4:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[doctors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[patients]    Script Date: 9/25/2016 4:13:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[patients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[doctor_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[doctors] ON 

INSERT [dbo].[doctors] ([id], [name]) VALUES (13, N'Dr. Majlessi')
INSERT [dbo].[doctors] ([id], [name]) VALUES (14, N'jj')
SET IDENTITY_INSERT [dbo].[doctors] OFF
SET IDENTITY_INSERT [dbo].[patients] ON 

INSERT [dbo].[patients] ([id], [name], [doctor_id]) VALUES (15, N'fff', 14)
SET IDENTITY_INSERT [dbo].[patients] OFF
