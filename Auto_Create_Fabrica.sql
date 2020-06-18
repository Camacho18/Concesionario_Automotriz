-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Automovil_Create_Fabrica	
	@IdFabrica INT,
	@NumeroF VARCHAR(30),
	@IdUsuario INT,
	@NumeroA VARCHAR(30),
	@IdAnio INT,
	@IdAutoModelo INT,
	@IdAutoColor INT,
	@PrecioCompra MONEY,
	@PrecionVenta MONEY,
	@Fecha date,
	@IdSuc int, 
	@Bandera INT OUTPUT
AS
BEGIN
	BEGIN TRAN
		BEGIN TRY
			INSERT INTO Automovil (Numero,IdAnios,IdAutoModelo,IdAutoColor,IdAutoEstado,PrecioCompra,PrecioVenta,FechaIngreso,IdConcesinaria,PrecioTotal,PrecioPromo) VALUES 
								  (@NumeroA,@IdAnio,@IdAutoModelo,@IdAutoColor,1,@PrecioCompra,@PrecionVenta,@Fecha,@IdSuc,@PrecionVenta,@PrecionVenta)
			DECLARE @IdAuto INT
			SET @IdAuto = SCOPE_IDENTITY()

			INSERT INTO Origen_Fabrica (Numero,IdFabrica,IdUsuario,IdAutomovil,IdOrigenEstado) VALUES
										(@NumeroF,@IdFabrica,@IdUsuario,@IdAuto,2)
			SET @Bandera=1

			COMMIT TRAN
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN
			SET @Bandera=1
		END CATCH
END