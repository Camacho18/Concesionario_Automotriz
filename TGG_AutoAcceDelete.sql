-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter TRIGGER TGG_AutoAcceDelete
   ON  AutoAccesorio 
   AFTER DELETE
AS 
BEGIN  
	SET NOCOUNT ON;

	DECLARE @IdAcAu INT, @IdAcc INT, @IdAut INT, @PrecioAcc INT
BEGIN TRY	
	IF EXISTS(SELECT IdAutoAccesorio FROM DELETED)
		BEGIN			
			SET @IdAcc = (SELECT IdAccesorio FROM DELETED)
			SET @IdAut = (SELECT IdAutomovil FROM DELETED)
			SET @PrecioAcc = (SELECT Precio FROM Accesorio WHERE IdAccesorio=@IdAcc)
			UPDATE Automovil SET PrecioTotal=PrecioTotal-@PrecioAcc
		END
END TRY
BEGIN CATCH

	INSERT into eRROR values (ERROR_MESSAGE())
END CATCH
END