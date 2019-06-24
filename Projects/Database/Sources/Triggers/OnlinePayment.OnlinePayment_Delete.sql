CREATE TRIGGER [OnlinePayment].[OnlinePayment_Delete] ON [OnlinePayment].[OnlinePayment]
INSTEAD OF DELETE
AS
BEGIN

  SET NOCOUNT ON;

  IF EXISTS (SELECT * FROM DELETED WHERE RecordState > 1)
  BEGIN
    RAISERROR ('Cannot update deleted records',16,1)
    ROLLBACK TRANSACTION
    RETURN
  END

  UPDATE [OnlinePayment].[OnlinePayment]
  SET [RecordDeleteDateTime] = GETDATE(), [RecordState] = 2
  WHERE [Id] IN (SELECT [Id] FROM DELETED)
  
END