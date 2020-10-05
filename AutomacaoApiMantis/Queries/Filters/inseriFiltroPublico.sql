INSERT INTO mantis_filters_table (is_public, name, filter_string) 
VALUES (1, '$filterName', 'Teste');

SELECT id FilterId, user_id UserId, project_id ProjectId, is_public IsPublic, NAME FilterName, filter_string FilterString 
FROM mantis_filters_table 
WHERE id = (SELECT MAX(id) FROM mantis_filters_table);