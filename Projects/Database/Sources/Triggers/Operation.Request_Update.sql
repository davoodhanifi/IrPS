CREATE TRIGGER [Operation].[Request_Update] ON [Operation].[Request]
				FOR UPDATE
AS
BEGIN
        
    SET NOCOUNT ON;
        
    IF NOT (UPDATE ([RecordInsertDateTime]) OR UPDATE ([RecordUpdateDateTime]) OR UPDATE ([RecordDeleteDateTime]) OR UPDATE ([RecordState]))
    BEGIN
        IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
        BEGIN
        RAISERROR ('Cannot update deleted records',16,1)
        ROLLBACK TRANSACTION
        RETURN
        END
        
        UPDATE [Operation].[Request] 
        SET [RecordUpdateDateTime] = GETDATE(), [RecordState] = 1
        WHERE [Id] IN (SELECT [Id] FROM INSERTED)
    END
END