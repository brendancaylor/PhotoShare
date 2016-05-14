CREATE TABLE [dbo].[MtSettings] (
    [ID]                 UNIQUEIDENTIFIER NOT NULL,
    [DropboxAccessToken] NVARCHAR (255)   NOT NULL,
    [DropboxRootFolder]  NVARCHAR (50)    NOT NULL,
    [PayPalId]           NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_MtSettings] PRIMARY KEY CLUSTERED ([ID] ASC)
);

