CREATE PROCEDURE [dbo].[spSale_SaleReport]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT		Sale.SaleDate, 
				Sale.SubTotal, 
				Sale.Tax, 
				Sale.Total,
				[User].FirstName, 
				[user].LastName, 
				[user].EmailAddress
	FROM		[dbo].[Sale]
	INNER JOIN	[dbo].[User]
	ON			[User].Id = Sale.CashierId;
END
