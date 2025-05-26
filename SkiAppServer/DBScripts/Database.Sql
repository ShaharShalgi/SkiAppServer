
    
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
    Email NVARCHAR(50),
    IsPro BIT,
    IsAdmin BIT
    );

    --טבלת סוגים
    CREATE TABLE TypeUser (
    TypeID INT PRIMARY KEY IDENTITY,
    TypeName NVARCHAR(15)
    );
    --יצירת טבלת מנהלים\מדריכים
    CREATE TABLE Professionals (
    UserID INT PRIMARY KEY,
    Rating FLOAT,
    TypeID INT,
    Loc NVARCHAR(40),
    Price FLOAT,
    Txt NVARCHAR(300),
    RaterNum INT,
    Post BIT,
    Link NVARCHAR(150)
    --Img VARBINARY(MAX),
    FOREIGN KEY (UserID) REFERENCES Visitors(UserId),
    FOREIGN KEY (TypeID) REFERENCES TypeUser(TypeID)
    );
   
   --טבלת טיפים
   CREATE TABLE Tips (
   TipID INT PRIMARY KEY IDENTITY,
   Difficulty INT,
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
   Rating int,
   Txt NVARCHAR(150),
   Title NVARCHAR(25),
   --Img VARBINARY(MAX),
   FOREIGN KEY (RecieverID) REFERENCES Visitors(UserID),
   FOREIGN KEY (SenderID) REFERENCES Visitors(UserID)
   );

  
   CREATE TABLE PostPhotos (
    PhotoId INT PRIMARY KEY identity,        -- מפתח ראשי
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Visitors(UserID),       -- מפתח משני
    

)
 CREATE TABLE ReviewPhotos (
    PhotoId INT PRIMARY KEY identity,        -- מפתח ראשי
    ReviewID INT,
    FOREIGN KEY (ReviewID) REFERENCES REVIEWS(ReviewID),       -- מפתח משני
    

)
   


   CREATE LOGIN [AdminLogin] WITH PASSWORD = 'shalgon101';
Go

-- Create a user in the SkiDB database for the login
CREATE USER [AdminUser] FOR LOGIN [AdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [AdminUser];
Go
INSERT INTO Visitors Values('Shahar Shalgi', 'shalgon202', 'Male', 'shshalgi@gmail.com', 1, 1)
INSERT INTO Visitors Values('Tami Freiman', 'tami123', 'Female', 'freimantami@gmail.com', 0, 0)
INSERT into TypeUser Values('Coach')
INSERT into TypeUser Values('Manager')
insert into Professionals Values(1, 0, 1, 'France', 50, 'Birthday ski call it ski BD',0,0,'www.google.com')
insert into Tips Values('3', 'Curling', 'HOW TO CURL IN 30 MINUTES (99% WILL NOT SURVIVE) (GONE WRONG)', 'idc', 'https://www.youtube.com/embed/Jxs8Mt3M8nA')
insert into Tips Values('4', 'Falling', 'HOW TO CRACK YOUR BUTT', 'BOOM SHAKALAK BOOM BOOM', 'https://www.youtube.com/embed/dQw4w9WgXcQ')
INSERT INTO Visitors Values('1', '1', 'Male', 'shacharshalgi@gmail.com', 0, 0)


SELECT * from Professionals
select * from visitors
Select * from Tips
Select * from TypeUser
select * from Reviews



--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=Ski_DB;User ID=AdminLogin;Password=shalgon101;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context SkiDBContext -DataAnnotations -force
*/







  


    

 

