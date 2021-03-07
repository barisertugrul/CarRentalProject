USE [RentACar]
GO

/****** Object: Table [dbo].[Users] Script Date: 7.3.2021 03:04:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Email]     VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (MAX) NOT NULL
);


