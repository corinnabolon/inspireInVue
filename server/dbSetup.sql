-- Active: 1701191390666@@SG-harsh-omelet-6782-8000-mysql-master.servers.mongodirector.com@3306@FirstDatabase

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS todolists(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        UNIQUE(accountId)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS todolistitems(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        completed BOOLEAN NOT NULL DEFAULT false,
        description VARCHAR(1000) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        todolistId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (todolistId) REFERENCES todolists(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE todolists;

SELECT * FROM todolists WHERE id = 1;

INSERT INTO
    todolistitems (
        description,
        creatorId,
        todoListId
    )
VALUES (
        "Test",
        "652ef11083c134acc79f1229",
        1
    );