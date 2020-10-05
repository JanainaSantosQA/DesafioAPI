SELECT id FilterId, user_id UserId, project_id ProjectId, is_public IsPublic, NAME FilterName, filter_string FilterString 
FROM mantis_filters_table 
WHERE id = '$filterId'