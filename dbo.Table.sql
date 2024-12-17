CREATE TABLE VegetablesandFruits
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] nvarchar (50) not null check ([name]<>'') unique,
	color nvarchar (50) not null check ([name]<>''),
	caloricity int not null,
	type nvarchar (50) NOT NULL,
)
