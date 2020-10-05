SELECT id BugId FROM mantis_bug_table 
WHERE project_id = '$projectId'
AND summary = '$summary'