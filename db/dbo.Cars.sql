CREATE TABLE [dbo].[Cars] (
    [CarId]       INT           IDENTITY (1, 1) NOT NULL,
    [BrandId]     INT           NOT NULL,
    [ColorId]     INT           NOT NULL,
    [CarName]        NCHAR (25)    NOT NULL,
    [ModelYear]   SMALLINT      NOT NULL,
    [DailyPrice]  MONEY         NOT NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([CarId] ASC),
    CONSTRAINT [FK_Cars_Colors] FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Colors] ([Id]),
    CONSTRAINT [FK_Cars_Brands] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([Id])
);

