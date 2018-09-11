CREATE TRIGGER [Accounts].[Account_Insert] ON [Accounts].[Account]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Accounts].[AccountHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END