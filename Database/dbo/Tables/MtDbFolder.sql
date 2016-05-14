CREATE TABLE [dbo].[MtDbFolder] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [AppId]         INT              NOT NULL,
    [DbName]        NVARCHAR (255)   NOT NULL,
    [DbPath]        NVARCHAR (255)   NOT NULL,
    [IsIncluded]    BIT              NOT NULL,
    [ViewingCode]   NVARCHAR (20)    NULL,
    [PricePerPhoto] DECIMAL (18, 2)  NULL,
    [TotalSold]     INT              NULL,
    [TotalSales]    DECIMAL (18, 2)  NULL,
    [SetsOf] INT NULL, 
    CONSTRAINT [PK_MtDbFolder] PRIMARY KEY CLUSTERED ([Id] ASC)
);

