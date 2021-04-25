ALTER TABLE dbo.Customer
ADD CONSTRAINT FK_AspNetUsers_Customrs FOREIGN KEY (IdentityUserID)
	REFERENCES dbo.AspNetUSers(Id)
