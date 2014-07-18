USE [Dotnetnuke]
GO

/****** Object:  Table [dbo].[MiniGallery_Images]    Script Date: 12/10/2013 11:06:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MiniGallery_Images](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[Alt] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[URL] [nvarchar](255) NULL,
	[ThumbFileID] [int] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[SortIndex] [int] NOT NULL,
	[IsPublished] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedByUserID] [int] NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[LastModifiedByUserID] [int] NULL,
 CONSTRAINT [PK_MiniGallery_Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MiniGallery_Images]  WITH CHECK ADD  CONSTRAINT [FK_MiniGallery_Images_Files] FOREIGN KEY([ThumbFileID])
REFERENCES [dbo].[Files] ([FileId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MiniGallery_Images] CHECK CONSTRAINT [FK_MiniGallery_Images_Files]
GO

ALTER TABLE [dbo].[MiniGallery_Images]  WITH CHECK ADD  CONSTRAINT [FK_MiniGallery_Images_Modules] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Modules] ([ModuleID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MiniGallery_Images] CHECK CONSTRAINT [FK_MiniGallery_Images_Modules]
GO

ALTER TABLE [dbo].[MiniGallery_Images] ADD  CONSTRAINT [DF_MiniGallery_Images_SortOrder]  DEFAULT ((1)) FOR [SortIndex]
GO

ALTER TABLE [dbo].[MiniGallery_Images] ADD  CONSTRAINT [DF_MiniGallery_Images_IsEnabled]  DEFAULT ((1)) FOR [IsPublished]
GO


