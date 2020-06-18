-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER TGG_AutoAcceInsert
   ON  AutoAccesorio 
   AFTER INSERT
AS 
BEGIN  
	SET NOCOUNT ON;

	DECLARE @IdAcAu INT, @IdAcc INT, @IdAut INT, @PrecioAcc INT

BEGIN TRY
	IF EXISTS(SELECT IdAutoAccesorio FROM INSERTED)
		BEGIN
			SET @IdAcAu = (SELECT IdAutoAccesorio FROM INSERTED)
			SET @IdAcc = (SELECT IdAccesorio FROM AutoAccesorio WHERE IdAutoAccesorio=@IdAcAu)
			SET @IdAut = (SELECT IdAutomovil FROM AutoAccesorio WHERE IdAccesorio=@IdAut)
			SET @PrecioAcc = (SELECT Precio FROM Accesorio WHERE IdAccesorio=@IdAcc )
			UPDATE Automovil SET PrecioTotal=PrecioTotal+@PrecioAcc
		END	
END TRY
BEGIN CATCH
	INSERT into eRROR values (ERROR_MESSAGE())
END CATCH
END