CREATE TRIGGER [Accounts].[LegalProfile_Insert] ON [Accounts].[LegalProfile]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[LegalProfileHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END