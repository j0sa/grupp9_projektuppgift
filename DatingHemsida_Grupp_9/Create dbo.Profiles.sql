USE [DatingDb]
GO

/****** Object: Table [dbo].[Profiles] Script Date: 2020-12-23 09:56:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Profiles] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Firstname]   NVARCHAR (MAX)  NULL,
    [Lastname]    NVARCHAR (MAX)  NULL,
    [Gender]      NVARCHAR (MAX)  NULL,
    [UserPicture] VARBINARY (MAX) NULL
);


Insert into [dbo].[Profiles] values('Adam', 'Test', 'kvinna', 100)