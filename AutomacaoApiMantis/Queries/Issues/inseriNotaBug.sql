INSERT INTO mantis_bugnote_table (bug_id) VALUES ('$bugId');
INSERT INTO mantis_bugnote_text_table VALUES (LAST_INSERT_ID(), '$note');
SELECT LAST_INSERT_ID();