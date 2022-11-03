CREATE TABLE SuperheroPowerRelation(
	SuperheroId int,
	PowerId int,

	FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id),
	FOREIGN KEY (PowerId) REFERENCES Power(Id),
	UNIQUE (SuperheroId, PowerId)
);