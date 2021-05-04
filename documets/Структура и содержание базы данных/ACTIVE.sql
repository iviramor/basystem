CREATE TABLE BANK_ACTIVE_AUTHORIZED_CAPITAL 
(
	aac_id INT PRIMARY KEY IDENTITY(1, 1),
	aac_name_transactions NVARCHAR(500) NOT NULL,
	aac_describtion_transactions NVARCHAR(MAX) NOT NULL,
	aac_debit DECIMAL(5, 2) NOT NULL,
	aac_credit DECIMAL(5, 2) NOT NULL,
	aac_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id)
);

CREATE TABLE BANK_ACTIVE_CAMP 
(
	acamp_id INT PRIMARY KEY IDENTITY(1,1),
	acamp_name NVARCHAR(250) NOT NULL,
	acamp_quantity DECIMAL(10, 2) NOT NULL,
	acamp_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id)
);

CREATE TABLE BANK_ACTIVE_DOCS 
(
	docs_id INT PRIMARY KEY IDENTITY(1, 1),
	docs_name NVARCHAR(350) NOT NULL,
	docs_type_doc NVARCHAR(50) NOT NULL,
	docs_cash DECIMAL(10, 2) NOT NULL,
	docs_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id)
);

CREATE TABLE BANK_ACTIVE_CREDITS_OUT
(
	co_id INT PRIMARY KEY IDENTITY(1, 1),
	co_name NVARCHAR(250) NOT NULL,
	co_describ NVARCHAR(MAX) NOT NULL,
	co_debtor INT FOREIGN KEY REFERENCES BANK_CLIENT(client_id) NOT NULL,
	co_cash DECIMAL(10, 2) NOT NULL,
	co_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id) NOT NULL
);