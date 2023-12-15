CREATE DATABASE LibraryDB

USE LibraryDB

CREATE TABLE Books 
(
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Author NVARCHAR(100),
    Genre NVARCHAR(50),
    Quantity INT
)
SELECT * FROM Books
