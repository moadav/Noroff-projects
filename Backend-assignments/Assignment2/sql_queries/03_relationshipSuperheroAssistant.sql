ALTER TABLE Superhero 
ADD AssistantId int null 


ALTER TABLE Superhero
ADD FOREIGN KEY (AssistantId) REFERENCES Assistant(Id) ON DELETE SET NULL
