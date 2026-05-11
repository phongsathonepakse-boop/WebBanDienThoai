-- This creates the missing column manually
ALTER TABLE Phones ADD Category nvarchar(MAX) NULL;