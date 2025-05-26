--REPLACE YOUR DATABASE LOGIN AND PASSWORD IN THE SCRIPT BELOW

Use master
Go

-- Create a login for the admin user
CREATE LOGIN [AdminLogin] WITH PASSWORD = 'shalgon101';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [AdminLogin];
Go

Create Database Ski_DB
Go



Use master
Go


USE master;
ALTER DATABASE Ski_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE Ski_DB FROM DISK = 'D:\Projects\ShaharShalgi\SkiAppServer\SkiAppServer\DBScripts\backup.bak' WITH REPLACE,     --להחליף את זה לנתיב של קובץ הגיבוי
  MOVE 'Ski_DB' TO 'C:\Users\User\Ski_DB.mdf', --להחליף לנתיב שנמצא על המחשב שלך
  MOVE 'Ski_DB_log' TO 'C:\Users\User\Ski_DB_log.ldf';
ALTER DATABASE Ski_DB SET MULTI_USER;