DROP TABLE IF EXISTS Customer;
CREATE TABLE Customer (
	CustomerID INT Primary Key Identity(1,1),
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	FullName NVARCHAR(50) NOT NULL UNIQUE,
	MobileNumber NVARCHAR(10) NOT NULL UNIQUE,
	City NVARCHAR(255) NOT NULL,
	IsActive bit NOT NULL,
	CreatedDate DateTime DEFAULT(GETDATE()),
	UpdatedDate DateTime
);

CREATE OR ALTER PROCEDURE GetCustomer
	@CustomerID INT
AS
BEGIN
	SELECT * FROM Customer WHERE CustomerID = @CustomerID
END;

CREATE OR ALTER PROCEDURE GetAllCustomer
AS
BEGIN
	SELECT * FROM Customer;
END;

CREATE OR ALTER PROCEDURE ValidateInput
	@CustomerID INT = NULL,
    @Input NVARCHAR(50),
    @ValType NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF @ValType = 'First Name'
    BEGIN
        IF EXISTS (SELECT 1 FROM Customer WHERE FirstName = @Input AND CustomerID <> @CustomerID)
        BEGIN
            PRINT 'First Name already exists';
            RETURN 1;
        END
    END

	IF @ValType = 'Last Name'
    BEGIN
        IF EXISTS (SELECT 1 FROM Customer WHERE LastName = @Input AND CustomerID <> @CustomerID)
        BEGIN
            PRINT 'Last Name already exists';
            RETURN 1;
        END
    END

	IF @ValType = 'Mobile Number'
    BEGIN
        IF EXISTS (SELECT 1 FROM Customer WHERE MobileNumber = @Input AND CustomerID <> @CustomerID)
        BEGIN
            PRINT 'Mobile Number already exists';
            RETURN 1;
        END
    END

    RETURN 0;
END


CREATE OR ALTER PROCEDURE CreateCustomer
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@FullName NVARCHAR(50),
	@MobileNumber NVARCHAR(10),
	@City NVARCHAR(255),
	@IsActive bit
AS
BEGIN
	INSERT INTO Customer(FirstName, LastName, FullName, MobileNumber, City, IsActive)
	VALUES(@FirstName, @LastName, @FullName, @MobileNumber, @City, @IsActive);

	DECLARE @CustomerID AS INT = SCOPE_IDENTITY();

	SELECT * FROM Customer WHERE CustomerID = @CustomerID;
END;

CREATE OR ALTER PROCEDURE UpdateCustomer
	@CustomerID INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@FullName NVARCHAR(50),
	@MobileNumber NVARCHAR(10),
	@City NVARCHAR(255),
	@IsActive bit
AS
BEGIN
	UPDATE Customer
	SET FirstName = @FirstName,
	LastName = @LastName,
	FullName = @FullName,
	MobileNumber = @MobileNumber,
	City = @City,
	IsActive = @IsActive,
	UpdatedDate = GETDATE()
	WHERE CustomerID = @CustomerID
END;

CREATE OR ALTER PROCEDURE DeleteCustomer
	@CustomerID INT,
	@IsActive bit
AS
BEGIN
	UPDATE Customer
	SET 
	IsActive = @IsActive,
	UpdatedDate = GETDATE()
	WHERE CustomerID = @CustomerID
END;