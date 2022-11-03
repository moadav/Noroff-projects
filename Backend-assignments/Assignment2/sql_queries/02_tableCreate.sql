CREATE TABLE Superhero(
	Id int Primary Key Identity(1,1),
	SuperheroName varchar(50),
	Alias varchar(50),
	Origin varchar(50)
);

CREATE TABLE Assistant(
	Id int Primary Key Identity(1,1),
	AssistantName varchar(50),
);

CREATE TABLE Power(
	Id int Primary Key Identity(1,1),
	PowerName varchar(50),
	PowerDesc varchar(50),
);