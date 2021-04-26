ALTER TABLE dbo.Specialization
ADD CONSTRAINT FK_Specialization_Task FOREIGN KEY (Id)
	REFERENCES dbo.[Task] (IdSpecialization)
