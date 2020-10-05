SELECT username Username, realname RealName, email Email, id UserId
FROM bugtracker.mantis_user_table
WHERE username = '$username'