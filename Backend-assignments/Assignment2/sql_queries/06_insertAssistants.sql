INSERT INTO Assistant (AssistantName)
VALUES ('Assistent1'), ('Assistent2'), ('Assistent3')

UPDATE Superhero
SET AssistantId = (SELECT Id from Assistant where AssistantName = 'Assistent2')
WHERE SuperheroName = 'Mohammed'

UPDATE Superhero
SET AssistantId = (SELECT Id from Assistant where AssistantName = 'Assistent1')
WHERE SuperheroName = 'Simon'

UPDATE Superhero
SET AssistantId = (SELECT Id from Assistant where AssistantName = 'Assistent3')
WHERE SuperheroName = 'Johanna'