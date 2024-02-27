-- Active: 1702937297947@@34.211.60.37@3306@inspire

CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key', createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', name varchar(255) COMMENT 'User Name', email varchar(255) COMMENT 'User Email', picture varchar(255) COMMENT 'User Picture', wantsCelsius BOOLEAN NOT NULL DEFAULT false, wantsTwentyFourClock BOOLEAN NOT NULL DEFAULT false, preferredImageTypes VARCHAR(5000)
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS todolists (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', accountId VARCHAR(255) NOT NULL, FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE, UNIQUE (accountId)
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS todolistitems (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', completed BOOLEAN NOT NULL DEFAULT false, description VARCHAR(1000) NOT NULL, creatorId VARCHAR(255) NOT NULL, todolistId INT NOT NULL, FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE, FOREIGN KEY (todolistId) REFERENCES todolists (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS preferredimagetypes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT, createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created', updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update', ImageType VARCHAR(255), AccountId VARCHAR(255) NOT NULL, FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE preferredimagetypes;

DROP TABLE todolists;

DROP TABLE todolistitems;

SELECT * FROM todolists WHERE id = 1;

DROP TABLE accounts;

INSERT INTO
    todolistitems (
        description, creatorId, todoListId
    )
VALUES (
        "Test", "652ef11083c134acc79f1229", 1
    );