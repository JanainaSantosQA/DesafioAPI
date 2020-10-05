INSERT INTO mantis_project_version_table (
project_id, version, description, released, obsolete, date_order) 
VALUES ('$projectId', 'v1.0.0', 'Major new version', 1, 0, 1);

SELECT id VersionId, project_id ProjectId, version VersionName, description VersionDescription, released VersionReleased, obsolete VersionObsolete
FROM mantis_project_version_table
WHERE id = (SELECT MAX(id) FROM mantis_project_version_table);