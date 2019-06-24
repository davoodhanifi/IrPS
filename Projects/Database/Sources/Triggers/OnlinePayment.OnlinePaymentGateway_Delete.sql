CREATE TRIGGER [OnlinePayment].[OnlinePaymentGateway_Delete] ON [OnlinePayment].[OnlinePaymentGateway]
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

  UPDATE [OnlinePayment].[OnlinePaymentGateway]
  SET [RecordDeleteDateTime] = GETDATE(), [RecordState] = 2
  WHERE [Id] IN (SELECT [Id] FROM DELETED)
  
END