CREATE TRIGGER [Accounts].[PersonProfile_Delete] ON [Accounts].[PersonProfile]
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
                
    UPDATE  [Accounts].[PersonProfile]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )

	INSERT INTO [Accounts].[PersonProfileHistory]
	SELECT GETDATE(), 2, * FROM [Deleted]
END