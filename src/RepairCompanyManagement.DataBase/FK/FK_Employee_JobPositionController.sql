ALTER TABLE dbo.Employee
ADD CONSTRAINT FK_Employee_JobPositionController FOREIGN KEY (IdJobPosition)
	REFERENCES dbo.JobPositionController(Id)

