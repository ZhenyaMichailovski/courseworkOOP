ALTER TABLE dbo.Manager
ADD CONSTRAINT FK_Manager_Order FOREIGN KEY (Id)
	REFERENCES dbo.[Order] (IdManager)