DROP TABLE IF EXISTS SKU;
CREATE TABLE SKU (
	SkuID INT Primary Key Identity(1,1),
	SkuName NVARCHAR(50) NOT NULL,
	SkuCode NVARCHAR(50) NOT NULL,
	UnitPrice money NOT NULL,
	ImagePath NVARCHAR(MAX),
	IsActive bit NOT NULL,
	CreatedDate DateTime DEFAULT(GETDATE()),
	CreatedBy NVARCHAR(50) DEFAULT('User'),
	UpdatedDate DateTime,
	UpdatedBy NVARCHAR(50) DEFAULT('User')
);

GO
CREATE OR ALTER PROCEDURE GetSKU
	@SkuID INT
AS
BEGIN
	SELECT * FROM SKU WHERE SkuID = @SkuID
END;

GO
CREATE OR ALTER PROCEDURE GetAllSKU
AS
BEGIN
	SELECT * FROM SKU;
END;

GO
CREATE OR ALTER PROCEDURE ValidateSKUInput
	@SkuID INT = NULL,
    @Input NVARCHAR(50),
    @ValType NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
	
    IF @ValType = 'SKU Code'
    BEGIN
        IF EXISTS (SELECT 1 FROM SKU WHERE SkuCode = @Input AND SkuID <> @SkuID)
        BEGIN
            PRINT 'SKU Code already exists';
            SELECT 1;
			RETURN;
        END
    END

	IF @ValType = 'SKU Name'
    BEGIN
        IF EXISTS (SELECT 1 FROM SKU WHERE SkuName = @Input AND SkuID <> @SkuID)
        BEGIN
            PRINT 'SKU Name already exists';
            SELECT 1;
			RETURN;
        END
    END
	
    RETURN 0;
END;

GO
CREATE OR ALTER PROCEDURE UpdateSKUImage
    @SkuID INT,
    @FileName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ApiBaseUrl NVARCHAR(255) = 'https://localhost:7263';

    DECLARE @ImagePath NVARCHAR(500) = @ApiBaseUrl + '/uploads/sku' 
                                      + FORMAT(@SkuID, '000#') + '/' 
                                      + @FileName;    
    UPDATE SKU
    SET ImagePath = @ImagePath
    WHERE SKUID = @SkuID;
END;

GO
CREATE OR ALTER PROCEDURE CreateSKU
	@SkuName NVARCHAR(50),
	@SkuCode NVARCHAR(50),
	@UnitPrice money,
	@IsActive bit
AS
BEGIN
	INSERT INTO SKU(SkuName, SkuCode, UnitPrice, IsActive)
	VALUES(@SkuName, @SkuCode, @UnitPrice, @IsActive);

	DECLARE @SkuID AS INT = SCOPE_IDENTITY();

	SELECT * FROM SKU WHERE SkuID = @SkuID;
END;

GO
CREATE OR ALTER PROCEDURE [dbo].[UpdateSKU]
	@SkuID INT,
	@SkuName NVARCHAR(50),
	@SkuCode NVARCHAR(50),
	@UnitPrice money
AS
BEGIN
	UPDATE SKU
	SET SkuName = @SkuName,
	SkuCode = @SkuCode,
	UnitPrice = @UnitPrice,
	UpdatedDate = GETDATE()
	WHERE SkuID = @SkuID;

	SELECT * FROM SKU WHERE SkuID = @SkuID;
END;

GO
CREATE OR ALTER PROCEDURE DeleteSKU
	@SkuID INT
AS
BEGIN
	UPDATE SKU
	SET 
	IsActive = 0,
	UpdatedDate = GETDATE()
	WHERE SkuID = @SkuID
END;