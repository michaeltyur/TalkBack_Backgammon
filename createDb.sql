use DB_A4A6EE_BackgamonChatDb
go


CREATE TABLE Users (
    UserId NOT NULL AUTO_INCREMENT,
    UserName nvarchar(255),
    Password nvarchar(30),
    FirstName nvarchar(255),
    LastName nvarchar(255) 
);

CREATE TABLE Messages (
    MessageId int,
    UserName nvarchar(255),
    [Content] nvarchar(30),
    Date datetime,
);


INSERT INTO Users(UserId,UserName,Password,FirstName,LastName)
VALUES
    (1,'dror',123,'Dror','Rabinovich'),
   (2,'yakov',123,'Yakov','Abrramovich'),
   (2,'afek',123,'Afek','Keglefich'),
   (2,'pazit',123,'Pazit','Berdishev'),
   (2,'niv',123,'Niv','Pupkovich'),
   (2,'david',123,'David','Shlakbaum'),
   (2,'misha',123,'Michael','Krasavcheg')

   update Users
   set UserId=7
   where FirstName='Michael'
select * from Users
