GO
DROP TABLE IF EXISTS PurchaseOrder;
CREATE TABLE PurchaseOrder (
	PurchaseOrderID INT PRIMARY KEY IDENTITY(1,1),
	CustomerID INT NOT NULL,
	DateOfDelivery DateTime Not Null,
	Status NVARCHAR(50) NOT NULL,
	AmountDue money NOT NULL,
	IsActive bit NOT NULL,
	CreatedDate DateTime DEFAULT(GETDATE()),
	CreatedBy NVARCHAR(50) DEFAULT('User'),
	UpdatedDate DateTime,
	UpdatedBy NVARCHAR(50) DEFAULT('User')
);

GO
DROP TABLE IF EXISTS PurchaseItem;
CREATE TABLE PurchaseItem (
	PurchaseItem INT PRIMARY KEY IDENTITY(1,1),
	PurchaseOrderID INT NOT NULL,
	SKUID INT NOT NULL,
	Quantity INT NOT NULL,
	TotalPrice money NOT NULL,
	IsActive bit NOT NULL,
	CreatedDate DateTime DEFAULT(GETDATE()),
	CreatedBy NVARCHAR(50) DEFAULT('User'),
);

GO
CREATE OR ALTER VIEW vwPurchaseOrdersGVM AS
SELECT AZ.*, CONCAT(B.LastName, ', ', B.FirstName) customerName,
    (SELECT A.*, B.SkuName, B.UnitPrice
     FROM PurchaseItem A
     LEFT JOIN SKU B ON A.SkuID = B.SkuID
     WHERE A.PurchaseOrderID = AZ.PurchaseOrderID
     FOR JSON PATH) AS PurchaseItemJson
FROM PurchaseOrder AZ
LEFT JOIN Customer B ON AZ.CustomerID = B.CustomerID;

GO
CREATE OR ALTER PROCEDURE GetPurchaseOrder
	@PurchaseOrderID INT
AS
BEGIN
	SELECT * FROM vwPurchaseOrdersGVM WHERE PurchaseOrderID = @PurchaseOrderID;
END;

GO
CREATE OR ALTER PROCEDURE GetAllPurchaseOrder
AS
BEGIN
	SELECT * FROM vwPurchaseOrdersGVM;
END;

GO
CREATE OR ALTER PROCEDURE GetPurchaseOrderDetail
	@PurchaseOrderID INT
AS
BEGIN
	SELECT * FROM vwPurchaseOrdersGVM WHERE PurchaseOrderID = @PurchaseOrderID;
END;

GO
CREATE OR ALTER PROCEDURE CreateNewPurchaseOrder
	@CustomerID INT,
	@DateOfDelivery DateTime,
	@Status NVARCHAR(50),
	@AmountDue money,
	@IsActive bit,
	@PurchaseItemJson NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO PurchaseOrder(CustomerID, DateOfDelivery, Status, AmountDue, IsActive)
	VALUES(@CustomerID, @DateOfDelivery, @Status, @AmountDue, @IsActive);

	DECLARE @PurchaseOrderID AS INT = SCOPE_IDENTITY();

	INSERT INTO PurchaseItem(PurchaseOrderID, SKUID, Quantity, TotalPrice, IsActive)
	SELECT @PurchaseOrderID, * 
	FROM OPENJSON(@PurchaseItemJson)
	WITH (
		skuId INT,
		quantity INT,
		totalPrice money,
		isActive bit
	);


	SELECT * FROM vwPurchaseOrdersGVM WHERE PurchaseOrderID = @PurchaseOrderID;
END;

GO
CREATE OR ALTER PROCEDURE UpdatePurchaseOrder
	@PurchaseOrderID INT,
	@CustomerID INT,
	@DateOfDelivery DateTime,
	@Status NVARCHAR(50),
	@AmountDue money,
	@IsActive bit,
	@PurchaseItemJson NVARCHAR(MAX)
AS
BEGIN
	UPDATE PurchaseOrder
	SET CustomerID = @CustomerID,
		DateOfDelivery = @DateOfDelivery,
		Status = @Status,
		AmountDue = @AmountDue,
		IsActive = @IsActive
	WHERE PurchaseOrderID = @PurchaseOrderID;

	MERGE INTO PurchaseItem AS t
	USING OPENJSON(@PurchaseItemJson)
	WITH (
		skuId INT,
		quantity INT,
		totalPrice money,
		isActive bit
	) AS s
	ON t.SKUID = s.SKUID AND t.PurchaseOrderID = @PurchaseOrderID
	WHEN MATCHED THEN
	UPDATE SET 
		t.Quantity = s.Quantity,
		t.TotalPrice = s.TotalPrice,
		t.IsActive = s.IsActive
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (PurchaseOrderID, SKUID, Quantity, TotalPrice, IsActive)
	VALUES (@PurchaseOrderID, s.SKUID, s.Quantity, s.TotalPrice, s.IsActive);

	DELETE FROM PurchaseItem
	WHERE SKUID NOT IN (SELECT SKUID FROM OPENJSON(@PurchaseItemJson) 
						WITH (SKUID INT))
	AND PurchaseOrderID = @PurchaseOrderID;

END;