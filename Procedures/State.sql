--State--

--Insert--

ALTER PROCEDURE [dbo].[PR_State_Insert]
@StateName varchar(100),
@CountryID int,
@StateCode varchar(50)
AS
INSERT INTO [dbo].[LOC_State]
(
	[StateName],
	[CountryID],
	[StateCode],
	[Created],
	[Modified]
)
VALUES
(
	@StateName,
	@CountryID,
	@StateCode,
	GETDATE(),
	GETDATE()
)

EXEC [dbo].[PR_State_Insert] 'Gujarat',1,15;
EXEC [dbo].[PR_State_Insert] 'Maharastra',4,14;
EXEC [dbo].[PR_State_Insert] 'Panjab',5,13;
EXEC [dbo].[PR_State_Insert] 'Delhi',3,17;


SELECT * FROM LOC_STATE; 

--Select All--

ALTER PROCEDURE [dbo].[PR_State_SelectAll]
AS
BEGIN
SELECT 
	[dbo].[LOC_State].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateCode],
	[dbo].[LOC_State].[Created],
	[dbo].[LOC_State].[Modified]

FROM [dbo].[LOC_State]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]

ORDER BY 
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName]

END

EXEC [dbo].[PR_State_SelectAll]

-- Search State
ALTER PROCEDURE PR_State_Search
@CountryID int,
@StateName varchar(100),
@StateCode varchar(50)
AS
BEGIN
SELECT 
	[dbo].[LOC_State].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateCode],
	[dbo].[LOC_State].[Created],
	[dbo].[LOC_State].[Modified]

FROM [dbo].[LOC_State]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]

WHERE  
	(@CountryID = 0 OR [dbo].[LOC_State].CountryID = @CountryID)
	AND
	(@StateName IS NULL OR StateName LIKE CONCAT('%',@StateName,'%'))
	AND
	(@StateCode IS NULL OR StateCode LIKE CONCAT('%',@StateCode,'%'))
ORDER BY 
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName]

END


--Select By Primary Key--
ALTER PROCEDURE [dbo].[PR_State_SelectByPK]
@StateID int
AS
BEGIN
SELECT
	[dbo].[LOC_State].[StateID],
	[dbo].[LOC_State].[StateName],
	[dbo].[LOC_State].[StateCode],
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[Created]

FROM [dbo].[LOC_State]

INNER JOIN [dbo].[LOC_Country]
ON [dbo].[LOC_Country].[CountryID] = [dbo].[LOC_State].[CountryID]

WHERE [dbo].[LOC_State].[StateID] = @StateID

ORDER BY
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_State].[StateName]
END

EXEC [dbo].[PR_State_SelectByPK] 1


--Update--
ALTER PROCEDURE [dbo].[PR_State_UpdateByPK]
@StateID		int,
@StateName		varchar(100),
@StateCode		varchar(50),
@CountryID		int
AS
BEGIN
UPDATE [dbo].[LOC_State]
 SET
	[StateName] = @StateName,
	[StateCode] = @StateCode,
	[CountryID] = @CountryID,
	[Modified] = GETDATE()

WHERE [dbo].[LOC_State].[StateID] = @StateID
END

EXEC [dbo].[PR_State_UpdateByPK] 2,'Goa',14,5;

--Delete--
ALTER PROCEDURE [dbo].[PR_State_DeleteByPK]
@StateID int
AS
BEGIN

DELETE
FROM [dbo].[LOC_State]

WHERE [dbo].[LOC_State].[StateID] = @StateID

END

EXEC [dbo].[PR_State_DeleteByPK] 4

-- Select State List Country Wise
CREATE PROCEDURE PR_State_Country_wise
@CountryID int
AS
BEGIN
	SELECT
	[dbo].[LOC_State].[StateID],
	[dbo].[LOC_State].[StateName]

	FROM [dbo].[LOC_State]

	WHERE [dbo].[LOC_State].[CountryID] = @CountryID
END
EXEC PR_State_Country_wise 13