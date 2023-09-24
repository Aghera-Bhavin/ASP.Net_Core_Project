-- Country Procedures --
-- 1. Insert 
-- EXEC [dbo].[PR_Country_Insert] 'UK',095

ALTER PROCEDURE [dbo].[PR_Country_Insert]
@CountryName varchar(100),
@CountryCode varchar(6)
AS
BEGIN
INSERT INTO [dbo].[LOC_Country]
(
 [CountryName]
,[CountryCode]
,[Created]
,[Modified]
)
VALUES
(
@CountryName,
@CountryCode,
GETDATE(),
GETDATE()
)
END 
	
select * from LOC_Country

-- 2. Select All
-- EXEC [dbo].[PR_Country_SelectAll]

ALTER PROCEDURE [dbo].[PR_Country_SelectAll]
AS
BEGIN
SELECT
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_Country].[CountryCode],
	[dbo].[LOC_Country].[Created],
	[dbo].[LOC_Country].[Modified]

FROM [dbo].[LOC_Country]

ORDER BY 
	[dbo].[LOC_Country].[CountryName]
END

-- 3. Select By Primary Key
-- EXEC [dbo].[PR_Country_SelectByPk] 3

ALTER PROCEDURE [dbo].[PR_Country_SelectByPk]
@CountryID int
AS
BEGIN
SELECT
	[dbo].[LOC_Country].[CountryID],
	[dbo].[LOC_Country].[CountryName],
	[dbo].[LOC_Country].[CountryCode],
	[dbo].[LOC_Country].[Created]

FROM [dbo].[LOC_Country]

WHERE [dbo].[LOC_Country].[CountryID] = @CountryID

ORDER BY 
	[dbo].[LOC_Country].[CountryName]
END

--Update [dbo].[PR_Country_UpdateByPk] 
--EXEC [dbo].[PR_Country_UpdateByPk] 1,96,'India'
ALTER PROCEDURE PR_Country_UpdateByPk
@CountryID int,
@CountryName VARCHAR(100),
@CountryCode VARCHAR(100)
AS
BEGIN
UPDATE [dbo].[LOC_Country]
SET 
	[CountryName] = @CountryName,
	[CountryCode] = @CountryCode,
	[Modified] = GETDATE()
WHERE
	[CountryID] = @CountryID
END 


-- 5. Delete
-- EXEC [dbo].[PR_Country_DeleteByPk] 3

ALTER PROCEDURE [dbo].[PR_Country_DeleteByPk]
@CountryID	INT
AS
BEGIN

DELETE 
FROM [dbo].[LOC_Country]

WHERE [dbo].[LOC_Country].[CountryID] = @CountryID
END


--6. Search Country 

CREATE PROCEDURE PR_Country_Search
@CountryName varchar(50),
@CountryCode varchar(50)
AS
BEGIN
Select 
	[CountryID]
	,[CountryName]
	,[CountryCode]
	,[Created]
	,[Modified]
from [dbo].[LOC_Country]
WHERE 
	(@CountryName IS NULL OR CountryName LIKE CONCAT('%',@CountryName,'%'))
	AND
	(@CountryCode IS NULL OR CountryCode LIKE CONCAT('%',@CountryCode,'%'))
END	
