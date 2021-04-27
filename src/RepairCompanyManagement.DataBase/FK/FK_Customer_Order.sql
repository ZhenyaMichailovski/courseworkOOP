ALTER TABLE dbo.[Order]
ADD CONSTRAINT FK_Customer_Order FOREIGN KEY (IdCustomers)
	REFERENCES dbo.[Customer] (Id)
