--City--

--1. Insert 
-- EXEC [dbo].[PR_City_Insert]

ALTER PROCEDURE [dbo].[PR_City_Insert]
@CityName	VARCHAR(100),
@StateID	INT,
@CountryID	INT,
@CityCode	VARCHAR(50)
AS
BEGIN
INSERT INTO [dbo].[LOC_City]
(
	[CityName],
	[StateID],
	[CountryID],
	[Citycode],
	[Created],
	[Modified]
)
VALUES
(
	@CityName,
	@StateID,
	@CountryID,
	@CityCode,
	GETDATE(),
	GETDATE()
);
END

SELECT * FROM LOC_CITY;

-- 2. Select All--
-- EXEC [dbo].[PR_City_SelectAll]

ALTER PROCEDURE PR_City_SelectAll
AS
BEGIN
SELECT 
	[dbo].[LOC_City].[CityID],
	[dbo].[LOC_City].[CityName],
	[dbo].[LOC_City].[Citycode],
	[dbo].[LOC_City].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_City].[Created],
	[dbo].[LOC_City].[Modified]

FROM [dbo].[LOC_City]

INNER JOIN [dbo].[LOC_State]
ON [dbo].[LOC_State].[StateID] = [dbo].[LOC_City].[StateID]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_City].[CountryID]

ORDER BY 
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_City].[CityName]
END

-- Select City
CREATE PROCEDURE PR_City_Search
@CountryID	INT,
@StateID	INT,
@CityName	VARCHAR(100),
@CityCode	VARCHAR(50)
AS
BEGIN
SELECT 
	[dbo].[LOC_City].[CityID],
	[dbo].[LOC_City].[CityName],
	[dbo].[LOC_City].[Citycode],
	[dbo].[LOC_City].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_City].[Created],
	[dbo].[LOC_City].[Modified]

FROM [dbo].[LOC_City]

INNER JOIN [dbo].[LOC_State]
ON [dbo].[LOC_State].[StateID] = [dbo].[LOC_City].[StateID]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_City].[CountryID]

WHERE 
	(@CountryID = 0 OR [dbo].[LOC_City].CountryID = @CountryID)
	AND
	(@StateID = 0 OR [dbo].[LOC_City].StateID = @StateID)
	AND
	(@CityName IS NULL OR CityName LIKE CONCAT('%',@CityName,'%'))
	AND
	(@CityCode IS NULL OR CityCode LIKE CONCAT('%',@CityCode,'%'))

ORDER BY 
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_City].[CityName]
END


--Select By Primary Key--

ALTER PROCEDURE PR_City_SelectByPK
@CityID INT
AS
BEGIN
SELECT 
	[dbo].[LOC_City].[CityID],
	[dbo].[LOC_City].[CityName],
	[dbo].[LOC_City].[Citycode],
	[dbo].[LOC_City].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_City].[Created]

FROM [dbo].[LOC_City]

INNER JOIN [dbo].[LOC_State]
ON [dbo].[LOC_State].[StateID] = [dbo].[LOC_City].[StateID]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]

WHERE [dbo].[LOC_City].[CityID] = @CityID

ORDER BY 
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_City].[CityName]

END

EXEC [dbo].[PR_City_SelectByPK] 1



--Update--

ALTER PROCEDURE PR_City_UpdateByPK
	@CityID		int,
	@CityName	varchar(100),
	@CityCode	varchar(50),
	@StateID	int,
	@CountryID	int
AS
BEGIN

UPDATE [dbo].[LOC_City]

 SET 
	[CityName] = @CityName,
	[CityCode] = @CityCode,
	[StateID] = @StateID,
	[CountryID] = @CountryID,
	[Modified] = GETDATE()

WHERE [dbo].[LOC_City].[CityID] = @CityID
END 

EXEC [dbo].[PR_City_UpdateByPK] 2,'Gandinagar',1,5,1,'6-6-2003','7-5-2004';


--Delete--


CREATE PROCEDURE [dbo].[PR_City_DeleteByPK]
@CityID int

AS
BEGIN

DELETE
FROM [dbo].[LOC_City]

WHERE [dbo].[LOC_City].[CityID] = @CityID

END

EXEC [dbo].[PR_City_DeleteByPK] 4