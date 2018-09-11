CREATE TRIGGER [Accounts].[PersonProfile_Insert] ON [Accounts].[PersonProfile]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[PersonProfileHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END