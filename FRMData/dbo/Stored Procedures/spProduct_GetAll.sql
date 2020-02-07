CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		Product.Id,
				Product.ProductName,
				Product.Description,
				Product.RetailPrice,
				Product.QuantityInStock,
				Product.IsTaxable
	FROM		[dbo].[Product]
	ORDER BY	Product.ProductName
END
