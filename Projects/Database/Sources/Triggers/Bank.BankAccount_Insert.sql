CREATE TRIGGER [Bank].[BankAccount_Insert] ON [Bank].[BankAccount]
				FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [Bank].[BankAccountHistory]
    SELECT GETDATE(), 0, * FROM INSERTED
END