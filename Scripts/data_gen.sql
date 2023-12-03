DELETE FROM Logins;
DELETE FROM Users;
SELECT *
FROM Users;
SELECT *
FROM Logins
INSERT INTO Users (
        Name,
        Address,
        Phone,
        SecondaryPhone,
        Contact,
        Email,
        GOVID,
        FISCALID,
        CORPORATEID,
        UserType,
        CreationDate,
        UpdateDate
    )
VALUES (
        'Usuario De Teste',
        'Av maria Antonieta, 100',
        '11 912345678',
        '11 912345678',
        'Maria',
        'usuario.teste@app.com.br',
        '23.456.789-5',
        '111.222.333.09',
        '0',
        1,
        NOW(),
        NOW()
    );
INSERT INTO Logins(
        UserId,
        Username,
        Password,
        CreationDate,
        UpdateDate,
        IsActive
    )
VALUES (
        LAST_INSERT_ID(),
        'usuario.teste@app.com.br',
        '123@456',
        NOW(),
        NOW(),
        true
    )