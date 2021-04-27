ALTER TABLE dbo.[OrderTask]
ADD CONSTRAINT FK_OrderTask_Order FOREIGN KEY (IdOrder)
	REFERENCES dbo.[Order](Id)

