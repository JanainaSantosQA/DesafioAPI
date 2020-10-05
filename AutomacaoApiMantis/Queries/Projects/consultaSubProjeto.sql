SELECT child_id ChildId, parent_id ParentId, inherit_parent InheritParent
FROM mantis_project_hierarchy_table 
WHERE child_id = '$childId'
AND parent_id = '$parentId'