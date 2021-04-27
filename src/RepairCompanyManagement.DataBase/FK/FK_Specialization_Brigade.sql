ALTER TABLE dbo.Brigade
ADD CONSTRAINT FK_Specialization_Brigade FOREIGN KEY (IdSpecialization)
	REFERENCES dbo.Specialization (Id)
