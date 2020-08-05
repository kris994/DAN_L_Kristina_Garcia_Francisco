-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblSong', 'U') IS NOT NULL DROP TABLE tblSong;
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AudioDB')
CREATE DATABASE AudioDB;
GO

USE AudioDB
CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	Username VARCHAR (40) UNIQUE			NOT NULL,
	UserPassword VARCHAR (40)				NOT NULL,
);

USE AudioDB
CREATE TABLE tblSong(
	SongID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	SongName VARCHAR (40) UNIQUE			NOT NULL,
	SongAuthor VARCHAR (40)					NOT NULL,
	SongLength VARCHAR (10)					NOT NULL,
);

