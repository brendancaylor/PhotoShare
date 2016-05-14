CREATE TABLE [dbo].[MtIpnTest]
(
	[Id]            UNIQUEIDENTIFIER NOT NULL,
	[IpnMessage]  NVARCHAR (MAX)   NOT NULL,	
    [DateCreated] DATETIME2 NOT NULL DEFAULT getdate(), 
    CONSTRAINT [PK_MtIpnTest] PRIMARY KEY CLUSTERED ([Id] ASC)
)
