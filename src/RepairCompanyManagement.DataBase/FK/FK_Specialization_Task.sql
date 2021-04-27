ALTER TABLE dbo.Task
ADD CONSTRAINT FK_Specialization_Task FOREIGN KEY (IdSpecialization)
	REFERENCES dbo.Specialization (Id)
