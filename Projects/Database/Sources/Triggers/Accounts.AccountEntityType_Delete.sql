CREATE TRIGGER [Accounts].[AccountEntityType_Delete] ON [Accounts].[AccountEntityType]
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
                
    UPDATE  [Accounts].[AccountEntityType]
    SET     [RecordDeleteDateTime] = GETDATE() ,
            [RecordState] = 2
    WHERE   [Id] IN ( SELECT    [Id]
                        FROM      DELETED )
END