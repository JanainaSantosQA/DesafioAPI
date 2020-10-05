SELECT a.*, b.note FROM mantis_bugnote_table a
INNER JOIN mantis_bugnote_text_table b
ON a.id = b.id
WHERE bug_id = '$bugId'
AND note = '$note'