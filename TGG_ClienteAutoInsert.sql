-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER TGG_ClienteAutoInsert
   ON   AutoCliente
   AFTER INSERT
AS 
BEGIN  
	SET NOCOUNT ON;

	DECLARE @IdAutoCli INT, @IdVentaAuto INT, @IdAuto INT, @IdConce INT

	DECLARE  @IdAni INT, @IdMode INT, @IdPromo INT, @CantidadAuto INT, @Descuento MONEY, @Tipo bit

	DECLARE @TablaAutos AS TABLE (IdAutoCliT INT)


	BEGIN TRY
		SELECT @IdAutoCli=IdAutoCliente,@IdVentaAuto=IdVentaAuto,@IdAuto=IdAutomovil FROM INSERTED
		
		SELECT @IdAni=a.IdAnios,@IdMode=a.IdAutoModelo,@IdConce=a.IdConcesinaria FROM Automovil AS a		
		WHERE a.IdAutomovil=@IdAuto

		SET @IdPromo = (SELECT IdPromocion FROM Promocion_Auto WHERE IdAnios=@IdAni AND IdAutoModelo=@IdMode AND IdConcesinaria=@IdConce AND Vigente=1)
		
		
		
		IF(@IdPromo IS NOT NULL)
			BEGIN
				SELECT @CantidadAuto=Cantidad_Auto,@Descuento=Descuento,@Tipo=Tipo FROM PromocionList WHERE IdPromocion=@IdPromo			
				DECLARE @IAut INT				
				IF(@Tipo=1)--TRUE Flotilla
					BEGIN
					-- Consulta los autos de la tabla autocliente que pertenescan a la venta y sean del mismo modelo y año y no tengan promoción aplicada
					INSERT INTO @TablaAutos (IdAutoCliT) SELECT ac.IdAutomovil FROM AutoCliente AS ac
						INNER JOIN Automovil AS a ON ac.IdAutomovil = A.IdAutomovil
						WHERE AC.IdVentaAuto=@IdVentaAuto AND a.IdAnios=@IdAni AND a.IdAutoModelo=@IdMode AND (ac.Promo is Null OR ac.Promo=0)
						-- Verifica si la cantidad de vehiculos a vender es la misma de la promoción
						IF(@CantidadAuto = (SELECT COUNT(IdAutoCliT) FROM @TablaAutos))
							BEGIN
								WHILE((SELECT COUNT(IdAutoCliT) FROM @TablaAutos)!=0)--Foreach
									BEGIN
										SET @IdAuto = (SELECT TOP 1(IdAutoCliT) FROM @TablaAutos)
										UPDATE AutoCliente SET IdPromocion_Auto=@IdPromo, Promo=1
										UPDATE Automovil SET PrecioPromo=PrecioVenta*(1-(@Descuento/100)) WHERE IdAutomovil=@IdAuto
										UPDATE Automovil SET PrecioTotal=(PrecioTotal-PrecioVenta)+PrecioPromo WHERE IdAutomovil=@IdAuto
										DELETE @TablaAutos WHERE IdAutoCliT=@IdAuto-- Elimina el registro ya modificado
									END
							END
					END
				ELSE
				IF(@Tipo=0)
					BEGIN -- False Normal					
						DECLARE @TablaPromo AS TABLE (IdAniT INT, IdModelT INT)
						-- Obtenemos los modelos y años que aplican a la promocion
						INSERT INTO @TablaPromo (IdModelT,IdAniT) SELECT IdAutoModelo,IdAnios  FROM Promocion_Auto WHERE IdPromocion=@IdPromo

						
							-- Consulta los autos de la tabla autocliente que pertenescan a la venta y apliquen para la promocion
						INSERT INTO @TablaAutos (IdAutoCliT) SELECT ac.IdAutomovil FROM AutoCliente AS ac
							INNER JOIN Automovil AS a ON ac.IdAutomovil = A.IdAutomovil
							WHERE AC.IdVentaAuto=@IdVentaAuto AND (ac.Promo is Null OR ac.Promo=0) AND a.IdAutoModelo in(SELECT IdModelT FROM @TablaPromo) AND a.IdAnios in(SELECT IdAniT FROM @TablaPromo)

							--INSERT into eRROR (DESCR) select (IdAutoCliT) from @TablaAutos
							-- Verifica si la cantidad de vehiculos a vender es la misma de la promoción
						IF(@CantidadAuto = (SELECT COUNT(IdAutoCliT) FROM @TablaAutos))
							BEGIN
								WHILE((SELECT COUNT(IdAutoCliT) FROM @TablaAutos)!=0)--Foreach
									BEGIN
										SET @IdAuto = (SELECT TOP 1(IdAutoCliT) FROM @TablaAutos)
										UPDATE AutoCliente SET IdPromocion_Auto=@IdPromo, Promo=1
										UPDATE Automovil SET PrecioPromo=PrecioVenta*(1-(@Descuento/100)) WHERE IdAutomovil=@IdAuto
										UPDATE Automovil SET PrecioTotal=(PrecioTotal-PrecioVenta)+PrecioPromo WHERE IdAutomovil=@IdAuto
										DELETE @TablaAutos WHERE IdAutoCliT=@IdAuto-- Elimina el registro ya modificado
									END
							END
					END
				
				DECLARE @SumaPre MONEY
				--Consulta todos los autos preteneciantes a la venta y suma los recios total para sacar la suma de los precios de autos sumado
						SET @SumaPre=(SELECT SUM(a.PrecioTotal) FROM AutoCliente AS ac
						INNER JOIN Automovil AS a ON ac.IdAutomovil = A.IdAutomovil
						WHERE AC.IdVentaAuto=@IdVentaAuto)

				--ACTUALIZAMOS PRECION FINAL EN TABLA VENTAAUTO
					UPDATE VentaAuto SET PrecioFinal=@SumaPre WHERE IdVentaAuto=@IdVentaAuto

			END
		
	END TRY
	BEGIN CATCH
		INSERT into eRROR values ( ERROR_MESSAGE())
	END CATCH
END