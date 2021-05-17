ALTER TABLE dbo.[Task]
ADD CONSTRAINT FK_Brigade_Task FOREIGN KEY (IdBrigade)
	REFERENCES dbo.[Brigade] (Id)
