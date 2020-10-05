SELECT id ProjectId, name ProjectName, status ProjectStatusId, enabled Enabled, view_state ViewState, 
access_min AccessMin, file_path FilePath, description Description, category_id CategoryId, inherit_global InheritGlobal
FROM mantis_project_table
WHERE ID = '$projectId'