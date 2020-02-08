CREATE PROCEDURE [dbo].[spProduct_GetById]
	@Id int
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
	WHERE		Product.Id = @Id
END
