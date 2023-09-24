-- MST_branch --

-- Insert --
-- EXEC [dbo].[PR_Student_Insert] 7,1,'Bhavin','7046074960','iambhavinaghera@gmail.com','9723246469','Rama Prasad, New Nehru Nagar','21-OCT-2001',22,1,'Male','123456';

CREATE PROCEDURE PR_Student_Insert
 @BranchID int
,@CityID int
,@StudentName varchar(100)
,@MobileNoStudent varchar(100)
,@Email varchar(100)
,@MobileNoFather varchar(100)
,@Address varchar(500)
,@BirthDate datetime
,@Age int
,@IsActive bit
,@Gender varchar(50)
,@Password varchar(1000)
AS
BEGIN
	INSERT INTO [dbo].[MST_Student]
	(
		[BranchID]
		,[CityID]
		,[StudentName]
		,[MobileNoStudent]
		,[Email]
		,[MobileNoFather]
		,[Address]
		,[BirthDate]
		,[Age]
		,[IsActive]
		,[Gender]
		,[Password]
		,[Created]
		,[Modified]
	) 
	VALUES(
		   @BranchID
		  ,@CityID
		  ,@StudentName
		  ,@MobileNoStudent
		  ,@Email
		  ,@MobileNoFather
		  ,@Address
		  ,@BirthDate
		  ,@Age
		  ,@IsActive
		  ,@Gender
		  ,@Password  
		  ,GETDATE()
		  ,GETDATE()
	);
END


SELECT * FROM [dbo].[MST_Student]

-- Select All --
-- EXEC PR_Student_SelectAll
ALTER PROCEDURE PR_Student_SelectAll
AS
BEGIN
	Select
	   [dbo].[MST_Student].[StudentID] 
	  ,[dbo].[MST_Branch].[BranchName]
      ,[dbo].[LOC_City].[CityName]
      ,[dbo].[MST_Student].[StudentName]
      ,[dbo].[MST_Student].[MobileNoStudent]
      ,[dbo].[MST_Student].[Email]
      ,[dbo].[MST_Student].[MobileNoFather]
      ,[dbo].[MST_Student].[Address]
      ,[dbo].[MST_Student].[BirthDate]
      ,[dbo].[MST_Student].[Age]
      ,[dbo].[MST_Student].[IsActive]
      ,[dbo].[MST_Student].[Gender]
      ,[dbo].[MST_Student].[Password]
      ,[dbo].[MST_Student].[Created]
      ,[dbo].[MST_Student].[Modified]
	FROM [dbo].[MST_Student]

	INNER JOIN [dbo].[MST_Branch]
	ON [dbo].[MST_Student].[BranchID] = [dbo].[MST_Branch].[BranchID]

	INNER JOIN [dbo].[LOC_City]
	ON [dbo].[MST_Student].[CityID] = [dbo].[LOC_City].[CityID]

	ORDER BY 
		[dbo].[MST_Student].[StudentName]
END

-- Search Student
CREATE PROCEDURE PR_Student_Search
 @BranchID int
,@CityID int
,@StudentName varchar(100)
AS
BEGIN
	Select
	   [dbo].[MST_Student].[StudentID] 
	  ,[dbo].[MST_Branch].[BranchName]
      ,[dbo].[LOC_City].[CityName]
      ,[dbo].[MST_Student].[StudentName]
      ,[dbo].[MST_Student].[MobileNoStudent]
      ,[dbo].[MST_Student].[Email]
      ,[dbo].[MST_Student].[MobileNoFather]
      ,[dbo].[MST_Student].[Address]
      ,[dbo].[MST_Student].[BirthDate]
      ,[dbo].[MST_Student].[Age]
      ,[dbo].[MST_Student].[IsActive]
      ,[dbo].[MST_Student].[Gender]
      ,[dbo].[MST_Student].[Password]
      ,[dbo].[MST_Student].[Created]
      ,[dbo].[MST_Student].[Modified]
	FROM [dbo].[MST_Student]

	INNER JOIN [dbo].[MST_Branch]
	ON [dbo].[MST_Student].[BranchID] = [dbo].[MST_Branch].[BranchID]

	INNER JOIN [dbo].[LOC_City]
	ON [dbo].[MST_Student].[CityID] = [dbo].[LOC_City].[CityID]

	WHERE 
		(@BranchID = 0 OR [dbo].[MST_Student].BranchID = @BranchID)
		AND
		(@CityID = 0 OR [dbo].[MST_Student].CityID = @CityID)
		AND
		(@StudentName IS NULL OR StudentName LIKE CONCAT('%',@StudentName,'%'))

	ORDER BY 
		[dbo].[MST_Student].[StudentName]
END



-- Select By Pk --
-- EXEC [dbo].[PR_MST_Student_SelectByPk] 1
CREATE PROCEDURE PR_Student_SelectByPk
	@StudentID int
AS
BEGIN
	Select
	   [StudentID]
	  ,[BranchID]
      ,[CityID]
      ,[StudentName]
      ,[MobileNoStudent]
      ,[Email]
      ,[MobileNoFather]
      ,[Address]
      ,[BirthDate]
      ,[Age]
      ,[IsActive]
      ,[Gender]
      ,[Password]
      ,[Created]
      ,[Modified]
	FROM [dbo].[MST_Student]

	WHERE [dbo].[MST_Student].[StudentID] = @StudentID
END

-- Update By Pk --
-- EXEC [dbo].[PR_MST_Student_UpdateByPk] 1,7,1,'Vivek','7046074960','iambhavinaghera@gmail.com','9723246469','Rama Prasad, New Nehru Nagar','21-OCT-2001',22,1,'Male','123456';
CREATE PROCEDURE PR_Student_UpdateByPk
 @StudentID int
,@BranchID int
,@CityID int
,@StudentName varchar(100)
,@MobileNoStudent varchar(100)
,@Email varchar(100)
,@MobileNoFather varchar(100)
,@Address varchar(500)
,@BirthDate datetime
,@Age int
,@IsActive bit
,@Gender varchar(50)
,@Password varchar(1000)
AS
BEGIN
	UPDATE [dbo].[MST_Student]
	SET 
	   [StudentName] = @StudentName
      ,[CityID] = @CityID
      ,[BranchID] = @BranchID
      ,[MobileNoStudent] = @MobileNoStudent
      ,[Email] = @Email
      ,[MobileNoFather] = @MobileNoFather
      ,[Address] = @Address
      ,[BirthDate] = @BirthDate
      ,[Age] = @Age
      ,[IsActive] = @IsActive
      ,[Gender] = @Gender
      ,[Password] = @Password
      ,[Modified] = GETDATE()

	WHERE [dbo].[MST_Student].[StudentID] = @StudentID
END

-- Delete By Pk --
-- EXEC [dbo].[PR_Student_DeleteByPk] 1
CREATE PROCEDURE PR_Student_DeleteByPk
	@StudentID int
AS
BEGIN
	DELETE FROM [dbo].[MST_Student]
	
	WHERE [dbo].[MST_Student].[StudentID] = @StudentID
END
