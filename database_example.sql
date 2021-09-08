CREATE TABLE dbo.Birds
	(
	Id uniqueidentifier NOT NULL Primary Key,
	Type int NOT NULL,
	Color varchar(25) NOT NULL,
	Size varchar(10) NOT NULL,
	Name varchar(50) NULL
	)

create Table dbo.Hats
	(
	Id uniqueidentifier NOT NULL Primary Key default(newid()),
	Designer varchar(200) null,
	Color varchar(25) NOT NULL,
	Style int
	)

create table dbo.Orders 
	(
	 Id uniqueidentifier NOT NULL Primary Key default(newid()),
	 BirdId uniqueidentifier not null,
	 HatId uniqueidentifier not null,
	 Price decimal(18,2),
	 CONSTRAINT FK_BirdId_BirdsID FOREIGN KEY (BirdId)
		REFERENCES DBO.Birds (Id),
	 CONSTRAINT FK_HatId_HatsID FOREIGN KEY (HatId)
		REFERENCES DBO.Hats (Id)
	)