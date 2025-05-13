-- Create the database
CREATE DATABASE EventEaseDB;

-- Use the database
USE 
EventEaseDB;
-- Create the Venues table
CREATE TABLE Venues (
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl NVARCHAR(255) -- Placeholder image URL
);

-- Insert sample venue
INSERT INTO Venues (Name, Location, Capacity, ImageUrl)
VALUES ('Grand Hall', '123 Main St, Cape Town', 300, 'https://via.placeholder.com/150');

SELECT * FROM Venues;

-- Create the Events table
CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description NVARCHAR(500),
    ImageUrl NVARCHAR(255), -- Placeholder image URL
    VenueId INT NOT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venues(VenueId) ON DELETE CASCADE
);

-- Insert sample event
INSERT INTO Events (EventName, EventDate, Description, ImageUrl, VenueId)
VALUES ('Tech Conference 2025', '2025-06-15 10:00:00', 'A technology-focused conference.', 'https://via.placeholder.com/150', 1);

SELECT * FROM Events;

-- Create the Bookings table
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    NumberOfTickets INT NOT NULL,
    BookingDate DATETIME DEFAULT GETDATE(),
    EventId INT NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Events(EventId) ON DELETE CASCADE
);

-- Insert sample booking
INSERT INTO Bookings (FullName, Email, NumberOfTickets, EventId)
VALUES ('Jane Doe', 'jane.doe@example.com', 2, 1);

SELECT * FROM Bookings;

--Part 2 STarts here
CREATE VIEW vw_BookingDetails AS
SELECT 
    b.BookingId,
    b.FullName,
    b.Email,
    b.NumberOfTickets,
    b.BookingDate,
    e.EventName,
    e.EventDate,
    v.Name AS VenueName,
    v.Location
FROM Bookings b
INNER JOIN Events e ON b.EventId = e.EventId
INNER JOIN Venues v ON e.VenueId = v.VenueId;

SELECT * FROM vw_BookingDetails;

-- Step 1: Drop the table if it already exists (for clean testing)
IF OBJECT_ID('dbo.BookingDetails', 'U') IS NOT NULL
    DROP TABLE dbo.BookingDetails;
GO

-- Step 2: Create the BookingDetails table
CREATE TABLE BookingDetails (
    BookingDetailsId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    NumberOfTickets INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    VenueName NVARCHAR(100) NOT NULL,
    VenueLocation NVARCHAR(200) NOT NULL
);
GO

-- Step 3: Insert joined data from Bookings, Events, and Venues
INSERT INTO BookingDetails (
    BookingId, FullName, Email, NumberOfTickets, BookingDate,
    EventName, EventDate, VenueName, VenueLocation
)
SELECT 
    b.BookingId,
    b.FullName,
    b.Email,
    b.NumberOfTickets,
    b.BookingDate,
    e.EventName,
    e.EventDate,
    v.Name AS VenueName,
    v.Location AS VenueLocation
FROM 
    Bookings b
JOIN Events e ON b.EventId = e.EventId
JOIN Venues v ON e.VenueId = v.VenueId;
GO

-- Step 4: View the results
SELECT * FROM BookingDetails;