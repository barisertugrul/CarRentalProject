USE [RentACar]
GO

/****** Object: Table [dbo].[Brands] Script Date: 28.2.2021 18:25:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Brands] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL
);


