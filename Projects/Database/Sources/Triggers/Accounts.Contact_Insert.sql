CREATE TRIGGER [Accounts].[Contact_Insert] ON [Accounts].[Contact]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[ContactHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END