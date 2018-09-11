CREATE TRIGGER [Bank].[BankAccount_Delete] ON [Bank].[BankAccount]
            INSTEAD OF DELETE
AS
BEGIN
                
    SET NOCOUNT ON;
                
    IF EXISTS ( SELECT  *
                FROM    DELETED
                WHERE   RecordState > 1 )
        BEGIN
            RAISERROR ('Cannot update deleted records',16,1)
            ROLLBACK TRANSACTION
            RETURN
        END
                
    UPDATE  [Bank].[BankAccount]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Bank].[BankAccountHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END