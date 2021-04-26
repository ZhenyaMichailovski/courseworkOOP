ALTER TABLE dbo.Customer
ADD CONSTRAINT FK_Customer_Order FOREIGN KEY (Id)
	REFERENCES dbo.[Order] (IdCustomers)
