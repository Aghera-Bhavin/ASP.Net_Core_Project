-- MST_branch --

-- Insert --
-- EXEC PR_Branch_Insert 'MCA','210MS3'
ALTER PROCEDURE [dbo].[PR_Branch_Insert]
@BranchName varchar(100),
@BranchCode varchar(100)
AS
BEGIN
	INSERT INTO [dbo].[MST_Branch]
	(
		 [BranchName]
		,[BranchCode]
		,[Created]
		,[Modified]
	) 
	VALUES(
		 @BranchName
		,@BranchCode
		,GETDATE()
		,GETDATE()
	);
END
SELECT * FROM [dbo].[MST_Branch]

-- Search Procedure 

CREATE PROCEDURE PR_Branch_Search
@BranchName varchar(100),
@BranchCode varchar(100)
AS
BEGIN
	Select 
		 [BranchID]
		,[BranchName]
		,[BranchCode]
		,[Created]
		,[Modified]
	FROM [dbo].[MST_Branch]
	WHERE 
		(@BranchName IS NULL OR BranchName LIKE CONCAT('%',@BranchName,'%'))
		AND
		(@BranchCode IS NULL OR BranchCode LIKE CONCAT('%',@BranchCode,'%'))
	ORDER BY
		[dbo].[MST_Branch].[BranchName]
END	


-- Select All --
-- EXEC [dbo].[PR_Branch_SelectAll]
ALTER PROCEDURE [dbo].[PR_Branch_SelectAll]
AS
BEGIN
	Select
		 [BranchID]
		,[BranchName]
		,[BranchCode]
		,[Created]
		,[Modified]
	FROM [dbo].[MST_Branch]

	ORDER BY 
		[dbo].[MST_Branch].[BranchName]
END

-- Select By Pk --
-- EXEC [dbo].[PR_Branch_SelectByPk] 1
ALTER PROCEDURE [dbo].[PR_Branch_SelectByPk]
	@BranchID int
AS
BEGIN
	Select
		 [BranchID]
		,[BranchName]
		,[BranchCode]
		,[Created]
		,[Modified]
	FROM [dbo].[MST_Branch]

	WHERE [dbo].[MST_Branch].[BranchID] = @BranchID

	ORDER BY 
		[dbo].[MST_Branch].[BranchName]
END

-- Update By Pk --
-- EXEC [dbo].[PR_Branch_UpdateByPk] 1,'Mechanical','22331ES'
ALTER PROCEDURE [dbo].[PR_Branch_UpdateByPk]
	@BranchID int,
	@BranchName varchar(100),
	@BranchCode varchar(100)
AS
BEGIN
	UPDATE [dbo].[MST_Branch]
	SET 
		[BranchName] = @BranchName
		,[BranchCode] = @BranchCode
		,[Modified] = GETDATE()

	WHERE [dbo].[MST_Branch].[BranchID] = @BranchID
END

-- Delete By Pk --
-- EXEC [dbo].[PR_Branch_DeleteByPk] 1
ALTER PROCEDURE [dbo].[PR_Branch_DeleteByPk]
	@BranchID int
AS
BEGIN
	DELETE FROM [dbo].[MST_Branch]
	
	WHERE [dbo].[MST_Branch].[BranchID] = @BranchID
END