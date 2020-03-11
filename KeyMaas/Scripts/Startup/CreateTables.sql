CREATE TABLE dbo.[Key]
(
  ID INT PRIMARY KEY IDENTITY(1, 1)
  ,Name VARCHAR(100)
  ,Description VARCHAR(MAX)
  ,PersonID INT NULL
  ,State INT
  ,Illuminated BIT
  ,Updated DATETIME
)

CREATE TABLE dbo.[KeyAudit]
(
  AuditID INT PRIMARY KEY IDENTITY(1, 1)
  ,AuditNote Varchar(100)
  ,ID INT 
  ,Name VARCHAR(100)
  ,Description VARCHAR(MAX)
  ,PersonID INT NULL
  ,State INT
  ,Illuminated BIT
  ,Updated DATETIME
)

--SELECT * FROM [key]
--SELECT * FROM KeyAudit
