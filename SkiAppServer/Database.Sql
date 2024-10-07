
    
        USE master
        GO
        IF EXISTS (SELECT * FROM sys.databases WHERE name = N'Ski_DB')
        BEGIN
        DROP DATABASE Ski_DB;
   END
   Go
   Create Database Ski_DB
   Go
   Use Ski_DB
   Go


    --יצירת טבלת מבקרים
    CREATE TABLE Visitors (
    UserId INT PRIMARY KEY IDENTITY, --מפתחראשי
    Username NVARCHAR(25),
    Pass NVARCHAR(25),
    Gender NVARCHAR(15),
    Email NVARCHAR(50)
    );

    --טבלת סוגים
    CREATE TABLE TypeUser (
    TypeID INT PRIMARY KEY IDENTITY,
    TypeName NVARCHAR(15)
    );
    --יצירת טבלת מנהלים\מדריכים
    CREATE TABLE Proffesionals (
    UserID INT PRIMARY KEY,
    Rating FLOAT,
    TypeID INT,
    Loc NVARCHAR(40),
    Price FLOAT,
    Txt NVARCHAR(300),
    --Img VARBINARY(MAX),
    FOREIGN KEY (UserID) REFERENCES Visitors(UserID),
    FOREIGN KEY (TypeID) REFERENCES TypeUser(TypeID)
    );
   
   --טבלת טיפים
   CREATE TABLE Tips (
   TipID INT PRIMARY KEY IDENTITY,
   Difficulty NVARCHAR(15),
   Topic NVARCHAR(15),
   Title NVARCHAR(100),
   Info NVARCHAR(100),
   VideoLink NVARCHAR(70)
   );

   --טבלת ביקורות
   CREATE TABLE REVIEWS (
   ReviewID INT PRIMARY KEY IDENTITY,
   RecieverID INT,
   SenderID int,
   IsPositive BIT,
   Txt NVARCHAR(150),
   --Img VARBINARY(MAX),
   FOREIGN KEY (RecieverID) REFERENCES Visitors(UserID),
   FOREIGN KEY (SenderID) REFERENCES Visitors(UserID)
   );

   --טבלת סטטוסים
   CREATE TABLE Condition(
   StatusID INT PRIMARY KEY IDENTITY,
   StatusName NVARCHAR(10),
   
   );


   --טבלת בקשות
   CREATE TABLE Requests (
   RequestID INT PRIMARY KEY IDENTITY,
   SenderID INT,
   RecieverID INT,
   StatusID INT, 
   FOREIGN KEY (RecieverID) REFERENCES Visitors(UserID),
   FOREIGN KEY (SenderID) REFERENCES Visitors(UserID),
   FOREIGN KEY (StatusID) REFERENCES Condition(StatusID)

   );
   


   CREATE LOGIN [AdminLogin] WITH PASSWORD = 'shalgon101';
Go

-- Create a user in the SkiDB database for the login
CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go
INSERT INTO Visitors Values('Shahar Shalgi', 'shalgon202', 'Male', 'shshalgi@gmail.com')
INSERT INTO Visitors Values('Tami Freiman', 'tami123', 'Female', 'freimantami@gmail.com')
INSERT into TypeUser Values('Coach')
INSERT into TypeUser Values('Manager')
insert into Proffesionals Values(1, 0, 1, 'France', 50, 'Birthday ski call it ski BD')
insert into Tips Values('Hard', 'Curling', 'HOW TO CURL IN 30 MINUTES (99% WILL NOT SURVIVE) (GONE WRONG)', 'idc', 'https://www.youtube.com/watch?v=Jxs8Mt3M8nA')
insert into REVIEWS Values(1, 2, 1, 'BEST COACH EVER!!!!111!')
insert into Condition Values('Pending')
insert into Condition Values('Approved')
insert into Condition Values('Declined')
insert into Requests Values(2, 1, 2)
SELECT * from Tips

--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=Ski_DB;User ID=AdminLogin;Password=shalgon101;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context SkiDBContext -DataAnnotations -force
*/







  


    

 

