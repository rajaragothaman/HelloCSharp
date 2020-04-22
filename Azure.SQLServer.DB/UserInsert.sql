CREATE PROCEDURE [dbo].[UserInsert]
	@EmployeeName varchar(50),
	@DOB Date
AS
	Insert into Employee values(@EmployeeName,@DOB)
RETURN 0
