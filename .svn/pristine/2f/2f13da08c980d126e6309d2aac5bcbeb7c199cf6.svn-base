CREATE TABLE [dbo].[MtDbPhoto] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [MtDbFolderId] UNIQUEIDENTIFIER NOT NULL,
    [DbName]       NVARCHAR (255)   NOT NULL,
    [DbPath]       NVARCHAR (255)   NOT NULL,
    [DbShareUrl]   NVARCHAR (255)   NOT NULL,
    [ViewingCode]  NVARCHAR (20)    NOT NULL,
    [TotalSold]    INT              NOT NULL,
    [TotalSales]   DECIMAL (18, 2)  NOT NULL,
    [LargeImage]   IMAGE            NOT NULL,
    [SmallImage]   IMAGE            NOT NULL,
    [PayPalId] NVARCHAR(20) NOT NULL, 
    [Width] INT NOT NULL DEFAULT 100, 
    CONSTRAINT [PK_MtDbPhoto] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MtDbPhoto_MtDbFolder] FOREIGN KEY ([MtDbFolderId]) REFERENCES [dbo].[MtDbFolder] ([Id])
);

