--CREATE DATABASE DataCaptureExperts;
--GO

USE DataCaptureExperts

GO

CREATE TABLE Customer (
    userId UNIQUEIDENTIFIER  NOT NULL ,
    userName NVARCHAR(30)  NOT NULL,
    email NVARCHAR(20)  NOT NULL,
    firstName NVARCHAR(20)  NOT NULL,
    lastName NVARCHAR(20)  NOT NULL,
	createOn DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	isActive BIT,
	CONSTRAINT customer_ak_1 UNIQUE (userName),
    CONSTRAINT customer_pk PRIMARY KEY  (userId)
);


CREATE TABLE [Order] (
    orderId UNIQUEIDENTIFIER NOT NULL  ,
    productId UNIQUEIDENTIFIER NOT NULL,
    orderStatus SMALLINT NOT NULL,
    orderType SMALLINT NOT NULL,
    orderBy UNIQUEIDENTIFIER NOT NULL,
	orderedOn DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	shippedOn DATETIME NOT NULL,
	isActive BIT,
    CONSTRAINT order_pk PRIMARY KEY  (orderId)
);


CREATE TABLE Product (
    productId UNIQUEIDENTIFIER NOT NULL,
    productName NVARCHAR(50)  NOT NULL,
    unitPrice DECIMAL  NOT NULL,
    supplierId UNIQUEIDENTIFIER NOT NULL,
	createdOn DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	isActive BIT,
    CONSTRAINT product_pk PRIMARY KEY  (productId)
);


CREATE TABLE Supplier (
    supplierId UNIQUEIDENTIFIER NOT NULL  ,
    supplierName NVARCHAR(50)  NOT NULL,
	createdOn DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	isActive BIT,
    CONSTRAINT supplier_pk PRIMARY KEY  (supplierId)
);


ALTER TABLE "Order" ADD CONSTRAINT order_product
    FOREIGN KEY (productId)
    REFERENCES Product (productId);


ALTER TABLE "Order" ADD CONSTRAINT order_customer
    FOREIGN KEY (orderBy)
    REFERENCES Customer (userId);


ALTER TABLE Product ADD CONSTRAINT product_supplier
    FOREIGN KEY (supplierId)
    REFERENCES Supplier (supplierId);


GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pramod Madushan
-- Create date: 02/07
-- Description:	Get Active Orders by Customer Id
-- =============================================
CREATE PROCEDURE ActiveOrdersByCustomerId
	-- Add the parameters for the stored procedure here
	 @UserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM 
	[Order] where orderBy = @UserId  and isActive = 1
	
	 
END
GO
