

create table BANK_USER_STATUS(
	status_id int PRIMARY KEY IDENTITY(1,1),
	status_name nvarchar(30)  NOT NULL,
	status_describ nvarchar(MAX)  NOT NULL
);

create table BANK_USER(
	user_id int PRIMARY KEY IDENTITY(1,1),
	user_name nvarchar(50) NOT NULL,
    user_surname nvarchar(50) NOT NULL,
	user_patronymic nvarchar(50) NOT NULL,
	user_sex bit NOT NULL,
	user_login nvarchar(30) NOT NULL,
	user_password nvarchar(60) NOT NULL,
	user_age date  NOT NULL,
	user_status_to_system int FOREIGN KEY REFERENCES BANK_USER_STATUS(status_id) NOT NULL,
	user_register_data datetime default (getdate())
);


create table BANK_USER_ACCESS(
	access_id int PRIMARY KEY IDENTITY(1,1),
	access_user_status int FOREIGN KEY REFERENCES BANK_USER_STATUS(status_id) NOT NULL,
	access_name_table NVARCHAR(350) NOT  NULL,
	access_modification int default (1)

);


Insert into BANK_USER (user_name, user_surname, user_patronymic, user_sex, user_login, user_password, user_age, user_status_to_system)
Values (N'Маргарита', N'Глуханова', N'Игоревна', 'False', N'rita', N'qwe', N'2003-08-10', 3);