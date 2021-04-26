ALTER TABLE dbo.Manager
ADD CONSTRAINT FK_AspNetUsers_Manager FOREIGN KEY (IdentityUserID)
	REFERENCES dbo.AspNetUsers(Id)
