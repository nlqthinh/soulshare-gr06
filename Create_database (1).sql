Create database SoulShare
use SoulShare

create table Customer (
	customer_id int IDENTITY(1,1) primary key not null,
	customer_name varchar(255) not null,
	customer_email varchar(255) not null,
	customer_address varchar(255) not null,
	account_password varchar(255) not null,
	account_phone varchar(10) not null
)

-- Alter Customer table to add customer_role column
ALTER TABLE Customer
ADD customer_role int;


-- Create Room table
CREATE TABLE Room (
    room_id int IDENTITY(1,1) primary key not null,
    user1 int not null,
    user2 int not null,
    room_status int,
    FOREIGN KEY (user1) REFERENCES Customer(customer_id),
    FOREIGN KEY (user2) REFERENCES Customer(customer_id)
);
-- Alter Room table to allow NULL for user1 and user2
ALTER TABLE Room
ALTER COLUMN user1 int NULL;

ALTER TABLE Room
ALTER COLUMN user2 int NULL;

-- Drop foreign key constraints
ALTER TABLE Room
DROP CONSTRAINT FK__Room__user1__4F7CD00D;

ALTER TABLE Room
DROP CONSTRAINT FK__Room__user2__5070F446;
