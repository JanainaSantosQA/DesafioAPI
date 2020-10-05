SELECT id VersionId, project_id ProjectId, version VersionName, description VersionDescription, released VersionReleased, obsolete VersionObsolete
FROM mantis_project_version_table
WHERE id = '$versionId'