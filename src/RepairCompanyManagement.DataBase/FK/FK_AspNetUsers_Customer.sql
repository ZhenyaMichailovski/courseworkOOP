ALTER TABLE dbo.Customer
ADD CONSTRAINT FK_AspNetUsers_Customers FOREIGN KEY (IdentityUserID)
	REFERENCES dbo.AspNetUsers(Id)
