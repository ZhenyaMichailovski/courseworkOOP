ALTER TABLE dbo.[OrderTask]
ADD CONSTRAINT FK_Task_OrderTask FOREIGN KEY (IdTask)
	REFERENCES dbo.Task(Id)

