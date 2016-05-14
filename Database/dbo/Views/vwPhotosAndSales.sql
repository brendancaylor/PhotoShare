CREATE VIEW [dbo].[vwPhotosAndSales]
	AS 


	SELECT
	
	NEWID() AS Id
	,  MtDbPhoto.Id AS MtDbPhotoId
	,  MtDbPhoto.DbName
	,  MtDbPhoto.ViewingCode
	,  MtDbPhoto.LargeImage
	,  MtDbPhoto.SmallImage
	,  MtDbPhoto.PayPalId
	,  MtDbPhoto.Width
	,  MtDbPhoto.MtDbFolderId
	,  MtDbFolder.PricePerPhoto
	,  MtDbPhoto.DbPath
	,  MtDbPhoto.DbShareUrl
	,  MtDbPhoto.TotalSold
	,  MtDbPhoto.TotalSales
	,  MtDbPhotoSale.SaleCode


	FROM MtDbPhoto
	INNER JOIN MtDbFolder ON MtDbFolder.Id = MtDbPhoto.MtDbFolderId
	LEFT OUTER JOIN MtDbPhotoSale ON MtDbPhoto.Id = MtDbPhotoSale.MtDbPhotoId
