DECLARE @FromVersion NVARCHAR(MAX)
DECLARE @ToVersion NVARCHAR(MAX)

DECLARE @Product NVARCHAR(MAX)
SELECT @Product = [TextValue] FROM [System].[Configuration] WHERE [Key] = 'System.Product'

SELECT @FromVersion = '0.1'
SELECT @ToVersion = '0.2'

IF ((SELECT [TextValue] FROM [System].[Configuration] WHERE [Key] = 'System.Version') = @FromVersion)
BEGIN
	BEGIN TRY
		BEGIN TRAN

			UPDATE [System].[Configuration] SET [TextValue] = @ToVersion WHERE [Key] = 'System.Version'
			COMMIT
			PRINT 'Updated to version ' + @ToVersion
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK

		PRINT 'Error on line ' + CAST(ERROR_LINE() AS VARCHAR(MAX)) + ': ' + ERROR_MESSAGE()
	END CATCH
END

