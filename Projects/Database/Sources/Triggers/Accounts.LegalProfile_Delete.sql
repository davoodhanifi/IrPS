CREATE TRIGGER [Accounts].[LegalProfile_Delete] ON [Accounts].[LegalProfile]
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
                
    UPDATE  [Accounts].[LegalProfile]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[LegalProfileHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END