ALTER TABLE dbo.Employee
ADD CONSTRAINT FK_Brigade_Employee FOREIGN KEY (IdBrigade)
	REFERENCES dbo.[Brigade] (Id)
