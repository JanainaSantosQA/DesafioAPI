INSERT INTO mantis_project_table (
name, status, enabled, view_state, access_min, file_path, description, category_id, inherit_global)
VALUES('$projectName', 10, 1, 10, 10, '/tmp/', 'Teste Automatizado - Criando um novo projeto.', 1, 1);

SELECT id ProjectId, name ProjectName, status ProjectStatusId, enabled Enabled , view_state ViewState, 
access_min AccessMin, file_path FilePath, description Description, category_id CategoryId, inherit_global InheritGlobal
FROM mantis_project_table
WHERE ID = (SELECT MAX(id) FROM mantis_project_table);