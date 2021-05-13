ALTER TABLE dbo.[Feedback]
ADD CONSTRAINT FK_Feedback_Order FOREIGN KEY (IdOrder)
	REFERENCES dbo.[Order](Id)