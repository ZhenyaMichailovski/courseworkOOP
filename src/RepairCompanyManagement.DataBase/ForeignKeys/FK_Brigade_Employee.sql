ALTER TABLE dbo.Brigade
ADD CONSTRAINT FK_Brigade_Employee FOREIGN KEY (Id)
	REFERENCES dbo.[Employee] (IdBrigade)
