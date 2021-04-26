ALTER TABLE dbo.Specialization
ADD CONSTRAINT FK_Specialization_Brigade FOREIGN KEY (Id)
	REFERENCES dbo.Brigade (IdSpecialization)
