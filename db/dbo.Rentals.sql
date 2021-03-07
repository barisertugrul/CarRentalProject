USE [RentACar]
GO

/****** Object: Table [dbo].[Rentals] Script Date: 7.3.2021 03:04:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rentals] (
    [Id]         INT  IDENTITY (1, 1) NOT NULL,
    [CarId]      INT  NOT NULL,
    [CustomerId] INT  NOT NULL,
    [RentDate]   DATE NOT NULL,
    [ReturnDate] DATE NULL
);


