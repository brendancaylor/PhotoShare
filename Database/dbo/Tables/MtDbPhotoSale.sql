CREATE TABLE [dbo].[MtDbPhotoSale] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [MtDbPhotoId] UNIQUEIDENTIFIER NOT NULL,
    [SaleCode]    NVARCHAR (20)    NOT NULL,
    [PricePaid]   DECIMAL (18, 2)  NOT NULL,
    [DatePaid]    DATETIME         NOT NULL,
    [IpnMessage]  NVARCHAR (MAX)   NOT NULL,
    [BuyersEmail] NVARCHAR(250) NOT NULL, 
    [Txnid] NVARCHAR(250) NOT NULL DEFAULT '', 
    CONSTRAINT [PK_MtDbPhotoSale] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MtDbPhotoSale_MtDbPhoto] FOREIGN KEY ([MtDbPhotoId]) REFERENCES [dbo].[MtDbPhoto] ([Id])
);

