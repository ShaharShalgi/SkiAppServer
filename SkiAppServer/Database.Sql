
    
    USE master
    GO
    IF EXISTS (SELECT * FROM sys.databases WHERE name = N'Ski_DB')
    DROP DATABASE Ski_DB;
    END
    Go
    Create Database Ski_DB
    Go
    Use Ski_DB
    Go
    --יצירת טבלת מבקרים
    CREATE TABLE Visitors (
    UserId INT PRIMARY KEY, --מפתחראשי
    Username NVARCHAR(25),
    Pass NVARCHAR(25),
    Gender NVARCHAR(15),
    Email NVARCHAR(50)
    );

    --טבלת סוגים
    CREATE TABLE Type (
    TypeID INT PRIMARY KEY,
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
    Img VARBINARY(MAX),
    FOREIGN KEY (UserID) REFERENCES Visitors(UserID),
    FOREIGN KEY (TypeID) REFERENCES Type(TypeID)
    );
   
   --טבלת טיפים
   CREATE TABLE Tips (
   TipID INT PRIMARY KEY,
   Difficulty NVARCHAR(15),
   Topic NVARCHAR(15),
   Title NVARCHAR(50),
   Info NVARCHAR(100),
   VideoLink NVARCHAR(70)
   );

   --טבלת ביקורות
   CREATE TABLE REVIEWS (
   ReviewID INT PRIMARY KEY,
   RecieverID INT,
   IsPositive BOOLEAN,
   Txt



    

 

