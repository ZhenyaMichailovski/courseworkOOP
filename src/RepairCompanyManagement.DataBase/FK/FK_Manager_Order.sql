ALTER TABLE dbo.[Order]
ADD CONSTRAINT FK_Manager_Order FOREIGN KEY (IdManager)
	REFERENCES dbo.Manager(Id)
