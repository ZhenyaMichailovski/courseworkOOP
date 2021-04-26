ALTER TABLE dbo.Brigade
ADD CONSTRAINT FK_Brigade_Order FOREIGN KEY (Id)
	REFERENCES dbo.[Order] (IdBrigade)
