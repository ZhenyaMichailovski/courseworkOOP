ALTER TABLE dbo.[Order]
ADD CONSTRAINT FK_Brigade_Order FOREIGN KEY (IdBrigade)
	REFERENCES dbo.[Brigade] (Id)
