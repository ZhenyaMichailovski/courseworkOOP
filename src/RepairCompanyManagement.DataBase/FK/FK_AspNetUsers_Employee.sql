ALTER TABLE dbo.Employee
ADD CONSTRAINT FK_AspNetUsers_Employee FOREIGN KEY (IdentityUserID)
	REFERENCES dbo.AspNetUsers(Id)
