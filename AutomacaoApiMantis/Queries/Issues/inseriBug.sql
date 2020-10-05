INSERT INTO mantis_bug_table (project_id, summary)
VALUES ('$projectId','$summary');
            
SET @idBug = (SELECT MAX(Id) FROM mantis_bug_table);

UPDATE mantis_bug_table 
SET bug_text_id  = @idBug 
WHERE id = @idBug;

INSERT INTO mantis_bug_text_table
(id, description, steps_to_reproduce, additional_information)
VALUES(@idBug, 'Description for sample REST issue.', 'teste', 'More info about the issue');

SELECT id BugId
FROM mantis_bug_table
WHERE ID = @idBug;