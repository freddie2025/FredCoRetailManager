CREATE PROCEDURE [dbo].[spInventory_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	Inventory.Id, 
			Inventory.ProductId, 
			Inventory.Quantity,
			Inventory.PurchasePrice,
			Inventory.PurchaseDate
	FROM	[dbo].[Inventory]
END