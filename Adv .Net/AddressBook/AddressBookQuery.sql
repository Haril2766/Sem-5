create table [dbo].[AddressBook_Country](
		CountryID		Int Primary Key Identity(1,1),
		CountryName		varchar(100) Unique Not Null,
		CountryCode		NVARCHAR(10) NOT NULL,
		CreatedDate		DATETIME NOT NULL DEFAULT GETDATE(),
		ModifiedDate	DATETIME NULL
);

SELECT * FROM AddressBook_Country
create table [dbo].[AddressBook_State](
		StateID			Int Primary Key Identity(1,1),
		CountryID		Int Not Null,
		StateName		Varchar(100) Unique Not Null,
		StateCode		Varchar(50) Not Null,
);
INSERT INTO [dbo].[AddressBook_State](StateName, StateCode, CountryID) VALUES
('California', 'CA', 1),
('Texas', 'TX', 1),
('Gujarat', 'GJ', 2),
('Maharashtra', 'MH', 2),
('New South Wales', 'NSW', 3),
('Victoria', 'VIC', 3),
('Ontario', 'ON', 4),
('Quebec', 'QC', 4),
('England', 'ENG', 5),
('Scotland', 'SCT', 5);

drop table AddressBook_City

create table [dbo].[AddressBook_City](
		CityID			Int Primary Key Identity(1,1),
		StateID			Int Not Null,
		CountryId		Int Not Null,
		CityName		Varchar(100) Unique Not Null,
		PinCode			Varchar(6),
);

INSERT INTO AddressBook_City (CityName,StateID, CountryID) VALUES
('Los Angeles',  1, 1),
('Houston', 2, 1),
('Ahmedabad',  3, 2),
('Mumbai',  4, 2),
('Sydney',  5, 3),
('Melbourne', 6, 3),
('Toronto',  7, 4),
('Montreal', 8, 4),
('London', 9, 5),
('Edinburgh', 10, 5);
INSERT INTO [dbo].[AddressBook_City] (CityName, StateID, PinCode) VALUES
('Los Angeles', 1, 265423),
('Houston',2,546518),
('Ahmedabad', 3, 531648),
('Mumbai',4, 315845),
('Sydney',5, 846585),
('Melbourne',  6, 416811),
('Toronto', 7, 516551),
('Montreal',  8, 165118),
('London', 9,158687),
('Edinburgh', 10,148485);
-----------------------------Data----------------------------------
INSERT INTO [dbo].[AddressBook_Country] (CountryName, CountryCode, CreatedDate) VALUES
('United States', 'US', GETDATE()),
('India', 'IN', GETDATE()),
('Australia', 'AU', GETDATE()),
('Canada', 'CA', GETDATE()),
('United Kingdom', 'UK', GETDATE()),
('Germany', 'DE', GETDATE()),
('France', 'FR', GETDATE()),
('Japan', 'JP', GETDATE()),
('China', 'CN', GETDATE()),
('Brazil', 'BR', GETDATE());

SELECT CountryId, CountryName, CountryCode FROM AddressBook_Country WHERE CountryId = 2

---------------------------------------------------------Country----------------------------------------------------------------
----------------------Insert-------------------------

Create or alter PROCEDURE PR_Country_Insert
    @CountryName	VARCHAR(100),
	@CountryCode	VARCHAR(10)

AS
BEGIN
    INSERT INTO [dbo].[AddressBook_Country] (
		CountryName,
		CountryCode,
		CreatedDate
	)
    VALUES (
		@CountryName,
		@CountryCode,
		GETDATE()
	);
END;
-----------------------Update-----------------------------
Create or Alter PROCEDURE PR_Country_Update
    @CountryID		INT,
    @CountryName	VARCHAR(100),
	@CountryCode	VARCHAR(10)
AS
BEGIN
    UPDATE [dbo].[AddressBook_Country]
    SET	
		[dbo].[AddressBook_Country].[CountryName]	=	@CountryName,
		[dbo].[AddressBook_Country].[CountryCode]   =	@CountryCode,
		[dbo].[AddressBook_Country].[ModifiedDate]  =	GETDATE()
		WHERE [dbo].[AddressBook_Country].[CountryID]	=	@CountryID;
END;


------------Delete---------------------
Create or Alter PROCEDURE PR_Country_Delete
    @CountryID	INT
AS
BEGIN
    DELETE FROM [dbo].[AddressBook_Country]
    WHERE 
		[dbo].[AddressBook_Country].[CountryID]  =  @CountryID;
END;

------------Select Alll ----------------------
Create or Alter PROCEDURE PR_Country_SelectAll
AS
BEGIN
    SELECT
		[dbo].[AddressBook_Country].[CountryID],
		[dbo].[AddressBook_Country].[CountryName],
		[dbo].[AddressBook_Country].[CountryCode],
		[dbo].[AddressBook_Country].[CreatedDate]
	    FROM [dbo].[AddressBook_Country]
END;

---------------Select By ID ----------------------------
Create or alter PROCEDURE PR_Country_SelectByID
       @CountryID	INT
AS
BEGIN
	SELECT 
		[dbo].[AddressBook_Country].[CountryID],
		[dbo].[AddressBook_Country].[CountryName],
		[dbo].[AddressBook_Country].[CountryCode]
	    FROM [dbo].[AddressBook_Country]
		WHERE [dbo].[AddressBook_Country].[CountryID]	=	@CountryID;
END;


---------------------------------------------------------State----------------------------------------------------------------

--------------------------Insert---------------------------------------------------------
Create or Alter PROCEDURE PR_State_Insert
    @CountryID	INT,
    @StateName	VARCHAR(100),
    @StateCode	VARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[AddressBook_State] (
		CountryID,
		StateName, 
		StateCode
	)
    VALUES (
		@CountryID, 
		@StateName, 
		@StateCode
	);
END;

--------------------------Update---------------------------------------

Create or Alter PROCEDURE PR_State_Update
    @StateID	INT,
    @CountryID	INT,
    @StateName	VARCHAR(100),
    @StateCode	VARCHAR(50)
AS
BEGIN
    UPDATE [dbo].[AddressBook_State]
    SET
		[dbo].[AddressBook_State].[CountryID]	= @CountryID, 
		[dbo].[AddressBook_State].[StateName]	= @StateName, 
		[dbo].[AddressBook_State].[StateCode]	= @StateCode
    WHERE [dbo].[AddressBook_State].[StateID]	= @StateID;
END;

---------------------Delete--------------------------
CREATE OR ALTER PROCEDURE PR_State_Delete
    @StateID INT
AS
BEGIN
    DELETE FROM [dbo].[AddressBook_State]
    WHERE [dbo].[AddressBook_State].[StateID] = @StateID;
END;


-----------------SelectAll--------------------

CREATE OR ALTER PROCEDURE PR_State_SelectAll
AS
BEGIN
    SELECT 
		[dbo].[AddressBook_State].[StateID],
		[dbo].[AddressBook_Country].[CountryID],
		[dbo].[AddressBook_State].[StateName],
		[dbo].[AddressBook_State].[StateCode]
    FROM [dbo].[AddressBook_State] 
	inner join [dbo].[AddressBook_Country] On [dbo].[AddressBook_Country].[CountryID] = [dbo].[AddressBook_State].[CountryID]
END;

---------------------Select By ID -------------------------------
CREATE OR ALTER PROCEDURE PR_State_SelectById
    @StateID INT
AS
BEGIN
    SELECT 
		[dbo].[AddressBook_State].[CountryID],
		[dbo].[AddressBook_State].[CountryID],
		[dbo].[AddressBook_State].[StateName],
		[dbo].[AddressBook_State].[StateCode]
    FROM [dbo].[AddressBook_State] 
    WHERE [dbo].[AddressBook_State].[StateID] = @StateID;
END;

------------------------------------------------City------------------------------------------

----------Insert------------
CREATE OR ALTER PROCEDURE PR_City_Insert
    @StateID	INT,
    @CityName	VARCHAR(100),
    @PinCode	VARCHAR(6)
AS
BEGIN
    INSERT INTO [dbo].[AddressBook_City]
	(
		StateID,
		CityName,
		PinCode
	)
    VALUES
	(
		@StateID, 
		@CityName, 
		@PinCode 
	);
END;

-----------------------Update---------------------------

CREATE OR ALTER PROCEDURE PR_City_Update
    @CityID		INT,
	@countryId	INT,
    @StateID	INT,
    @CityName	VARCHAR(100),
    @PinCode	VARCHAR(6)
AS
BEGIN
    UPDATE [dbo].[AddressBook_City]
    SET 
		[dbo].[AddressBook_City].[StateID]		=	@StateID,
		[dbo].[AddressBook_City].[CountryId]		=	@countryId,
		[dbo].[AddressBook_City].[CityName]		=	@CityName, 
		[dbo].[AddressBook_City].[PinCode]		=	@PinCode 
    WHERE [dbo].[AddressBook_City].[CityID] = @CityID;
END;

-------------------Delete------------------------

CREATE OR ALTER PROCEDURE PR_City_Delete
    @CityID INT
AS
BEGIN
    DELETE FROM [dbo].[AddressBook_City]
    WHERE [dbo].[AddressBook_City].[CityID] = @CityID;
END;

-----------------Select All-------------------------
CREATE OR ALTER PROCEDURE PR_City_SelectAll
AS
BEGIN
    SELECT 
		[dbo].[AddressBook_City].[CityID],
		[dbo].[AddressBook_City].[CountryId],
		[dbo].[AddressBook_City].[StateID],
		[dbo].[AddressBook_City].[CityName],
		[dbo].[AddressBook_City].[PinCode]
    FROM [dbo].[AddressBook_City] 
END;


--------------Select BY Id------------------------

CREATE OR ALTER PROCEDURE PR_City_SelectById
    @CityID INT
AS
BEGIN
    SELECT	[dbo].[AddressBook_City].[CityID],
			[dbo].[AddressBook_City].[StateID],
			[dbo].[AddressBook_City].[CountryId],
			[dbo].[AddressBook_City].[CityName],
			[dbo].[AddressBook_City].[PinCode]
    FROM [dbo].[AddressBook_City] 
    WHERE [dbo].[AddressBook_City].[CityID] = @CityID;
END;


CREATE PROCEDURE [dbo].[PR_LOC_Country_SelectComboBox]
AS 
SELECT
    COUNTRYID,
    COUNTRYNAME
FROM AddressBook_Country
ORDER BY COUNTRYNAME
CREATE or alter PROCEDURE PR_LOC_State_SelectComboBoxByCountryID
    @CountryID INT
AS
BEGIN
    SELECT StateID, StateName
    FROM AddressBook_State
    WHERE CountryID = @CountryID
END
