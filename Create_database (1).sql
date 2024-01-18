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






