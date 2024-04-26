-- Drop the database if it exists
DROP DATABASE IF EXISTS Bogreden;

-- Create the database
CREATE DATABASE Bogreden;

-- Switch to the new database
USE Bogreden;

-- Create a database user and grant privileges
DROP USER IF EXISTS 'BogredenUser'@'localhost';
CREATE USER 'BogredenUser'@'localhost' IDENTIFIED BY 'password';
GRANT ALL PRIVILEGES ON Bogreden.* TO 'BogredenUser'@'localhost';

-- Create the tables
CREATE TABLE ZipCode (
    ZipCode VARCHAR(4) PRIMARY KEY NOT NULL,
    City VARCHAR(100) NOT NULL
);

CREATE TABLE Genre (
    GenreID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name VARCHAR(30) NOT NULL,
    Description VARCHAR(100)
);

CREATE TABLE Author (
    AuthorID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE Book (
    BookID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Title VARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2),
    ISBN BIGINT(13),
    ReleaseDate DATETIME,
    Description VARCHAR(100),
    AuthorID INT,
    GenreID INT,
    FOREIGN KEY (AuthorID) REFERENCES Author(AuthorID),
    FOREIGN KEY (GenreID) REFERENCES Genre(GenreID)
);

CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50),
    Email VARCHAR(100),
    PhoneNumber VARCHAR(50),
    ZipCodeID VARCHAR(4),
    FOREIGN KEY (ZipCodeID) REFERENCES ZipCode(ZipCode)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    OrderDate DATETIME,
    TotalPrice DECIMAL(10, 2),
    CustomerID INT,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

CREATE TABLE OrderItem (
    OrderItemID INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    Quantity INT NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    OrderID INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);


-- Populate ZipCode table using LOAD DATA INFILE
LOAD DATA INFILE 'C:\\Users\\Bruger\\Downloads\\postnumre.csv'
INTO TABLE ZipCode
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n';

-- Create logging table
CREATE TABLE Bogreden_Log (
    LogID INT PRIMARY KEY AUTO_INCREMENT,
    LogMessage TEXT,
    LogDateTime DATETIME
);

-- Create triggers for logging
DELIMITER //

CREATE TRIGGER Book_Insert_Trigger
AFTER INSERT ON Book
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('New book inserted', NOW());
END; //

CREATE TRIGGER Book_Update_Trigger
AFTER UPDATE ON Book
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Book updated', NOW());
END; //

CREATE TRIGGER Book_Delete_Trigger
AFTER DELETE ON Book
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Book deleted', NOW());
END; //

-- Customer triggers
CREATE TRIGGER Customer_Insert_Trigger
AFTER INSERT ON Customer
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('New Customer inserted', NOW());
END; //

CREATE TRIGGER Customer_Update_Trigger
AFTER UPDATE ON Customer
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Customer updated', NOW());
END; //

CREATE TRIGGER Customer_Delete_Trigger
AFTER DELETE ON Customer
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Customer deleted', NOW());
END; //

-- Order triggers
CREATE TRIGGER Order_Insert_Trigger
AFTER INSERT ON Orders
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('New Order inserted', NOW());
END; //

CREATE TRIGGER Order_Update_Trigger
AFTER UPDATE ON Orders
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Order updated', NOW());
END; //

CREATE TRIGGER Order_Delete_Trigger
AFTER DELETE ON Orders
FOR EACH ROW
BEGIN
    INSERT INTO Bogreden_Log (LogMessage, LogDateTime)
    VALUES ('Order deleted', NOW());
END; //

DELIMITER ;

-- Create stored procedures
DELIMITER //

CREATE PROCEDURE GetBooks()
BEGIN
    SELECT * FROM Book;
END //

CREATE PROCEDURE GetCustomers()
BEGIN
    SELECT * FROM Customer;
END //


CREATE PROCEDURE GetBookByID(IN book_id INT)
BEGIN
    SELECT * FROM Book WHERE BookID = book_id;
END //

CREATE PROCEDURE GetOrdersByCustomerID(IN customer_id INT)
BEGIN
    SELECT * FROM Orders WHERE CustomerID = customer_id;
END //

CREATE PROCEDURE GetTotalOrdersByCustomerID(IN customer_id INT, OUT total_orders INT)
BEGIN
    SELECT COUNT(*) INTO total_orders FROM Orders WHERE CustomerID = customer_id;
END //

CREATE PROCEDURE AddNewBook(
    IN p_Title VARCHAR(100),
    IN p_Price DECIMAL(10,2),
    IN p_ISBN BIGINT(13),
    IN p_ReleaseDate DATETIME,
    IN p_Description VARCHAR(100),
    IN p_AuthorID INT,
    IN p_GenreID INT
)
BEGIN
    INSERT INTO Book (Title, Price, ISBN, ReleaseDate, Description, AuthorID, GenreID)
    VALUES (p_Title, p_Price, p_ISBN, p_ReleaseDate, p_Description, p_AuthorID, p_GenreID);
END //

CREATE PROCEDURE AddNewCustomer(
    IN p_FirstName VARCHAR(50),
    IN p_LastName VARCHAR(50),
    IN p_Email VARCHAR(100),
    IN p_PhoneNumber VARCHAR(50),
    IN p_ZipCodeID VARCHAR(4)
)
BEGIN
    INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, ZipCodeID)
    VALUES (p_FirstName, p_LastName, p_Email, p_PhoneNumber, p_ZipCodeID);
END //

CREATE PROCEDURE AddNewAuthor(
    IN p_Name VARCHAR(100)
)
BEGIN
    INSERT INTO Author (Name) VALUES (p_Name);
END //

CREATE PROCEDURE AddNewGenre(
    IN p_Name VARCHAR(30),
    IN p_Description VARCHAR(100)
)
BEGIN
    INSERT INTO Genre (Name, Description) VALUES (p_Name, p_Description);
END //
 
DELIMITER ;

-- Create indexes
CREATE INDEX IX_Book_AuthorID ON Book(AuthorID);
CREATE INDEX IX_Customer_CustomerID ON Customer(CustomerID);
CREATE INDEX IX_ZipCode_ZipCode ON ZipCode(ZipCode);


-- Insert Dummy Data


-- Insert dummy genres
INSERT INTO Genre (Name, Description) VALUES
('Fiction', 'Works of the imagination or prose.'),
('Non-fiction', 'Factual stories and true accounts.'),
('Science Fiction', 'Speculative fiction based on imagined future.');

-- Insert dummy authors
INSERT INTO Author (Name) VALUES
('HC Andersen'),
('Sten Stensen Blicher'),
('David Svarrer');

-- Insert dummy books
INSERT INTO Book (Title, Price, ISBN, ReleaseDate, Description, AuthorID, GenreID) VALUES
('Pride and Prejudice', 9.99, 9780141439518, '1813-01-28', 'Classic novel by Jane Austen', 1, 1),
('Great Expectations', 7.99, 9780141439563, '1861-08-01', 'Classic novel by Charles Dickens', 2, 1),
('War and Peace', 12.99, 9780143035008, '1869-01-01', 'Historical novel by Leo Tolstoy', 3, 2);

-- Insert dummy customers
INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, ZipCodeID) VALUES
('John', 'Doe', 'john.doe@example.com', '12345678', '4200'),
('Alice', 'Smith', 'alice.smith@example.com', '98765432', '4200'),
('Bob', 'Johnson', 'bob.johnson@example.com', '11112222', '4200');

-- Insert dummy orders
INSERT INTO Orders (OrderDate, TotalPrice, CustomerID) VALUES
('2024-04-25 10:00:00', 29.97, 1),
('2024-04-25 11:30:00', 7.99, 2),
('2024-04-26 09:45:00', 12.99, 3);

-- Insert dummy order items
INSERT INTO OrderItem (Quantity, TotalPrice, OrderID) VALUES
(1, 9.99, 1),
(1, 7.99, 2),
(1, 12.99, 3);