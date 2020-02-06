﻿CREATE DATABASE TEST

USE TEST;
GO

CREATE TABLE R_CURRENCY
(
    ID INT IDENTITY NOT NULL
PRIMARY KEY,
    TITLE VARCHAR(60) NOT NULL,
    CODE VARCHAR(3) NOT NULL,
    VALUE NUMERIC(18, 2) NOT NULL,
    A_DATE DATE NOT NULL

)

CREATE PROCEDURE sp_GetRates AS
BEGIN
SELECT ID, TITLE, CODE, VALUE, A_DATE
FROM R_CURRENCY
END;