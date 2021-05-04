CREATE TABLE BANK_PASSIVE_AUTHORIZED_CAPITAL
(
	apc_id INT PRIMARY KEY IDENTITY(1,1),
	apc_name_transactions NVARCHAR(250) NOT NULL,
	apc_describtion_transactions NVARCHAR(MAX) NOT NULL,
	apc_debit DECIMAL(10, 2),
	apc_credit DECIMAL,
	apc_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id)
);

CREATE TABLE BANK_PASSIVE_CAMP
(
	pcamp_id INT PRIMARY KEY IDENTITY(1,1),
	pcamp_name NVARCHAR(250) NOT NULL,
	pcamp_quantity DECIMAL(10,2) NOT NULL,
	pcamp_type INT FOREIGN KEY REFERENCES BANK_CURRENCY(currency_id)
);

