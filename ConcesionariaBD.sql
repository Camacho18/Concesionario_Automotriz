DROP DATABASE CONCESIONARIA
CREATE DATABASE Concesionaria	
USE Concesionaria

-- Tablas varias (necesarias)
create table Pais(
	IdPais int identity(1,1) primary key,
	Nombre varchar(50)
	)

create table Estado(
	IdEstado int identity(1,1) primary key,
	Nombre varchar(50),
	IdPais int foreign key(IdPais) references Pais(IdPais)
	)

create table Municipio(
	IdMunicipio int identity(1,1) primary key,
	Nombre varchar(50),
	IdEstado int foreign key(IdEstado) references Estado(IdEstado)
	)
	
-- __________________________________________________________________ Concesionaria
create table Concesinaria(
	IdConcesinaria int identity(1,1) primary key,
	Nombre varchar(50),
	Clave VARCHAR(50),
	Direccion varchar(50),
	IdMunicipio int foreign key(IdMunicipio) references Municipio(IdMunicipio)
	)
-- __________________________________________________________________ Usuario

create table TipoEmpleado(
	IdTipoEmpleado int identity(1,1) primary key,
	Nombre varchar(50),
	_Estado BIT DEFAULT 1
	)

create table Empleado(
	IdEmpleado int identity(1,1) primary key,
	Numero VARCHAR(30),
	Nombre varchar(50),	
	Telefono int,
	_Estado BIT DEFAULT 1,
	IdTipoEmpleado int foreign key(IdTipoEmpleado) references TipoEmpleado(IdTipoEmpleado),
	IdConcesinaria int foreign key(IdConcesinaria) references Concesinaria(IdConcesinaria)
	)

create table Usuario( 
	IdUsuario int identity(1,1) primary key,
	NomUsuario varchar(50),
	Contrasena varchar(50),
	IdEmpleado int foreign key(IdEmpleado) references Empleado(IdEmpleado)
	)

-- __________________________________________________________________ Promoción

create table PromocionList(
	IdPromocion int identity(1,1) primary key,
	Numero VARCHAR(30),
	Cantidad_Auto INT,
	Descuento INT
)

-- __________________________________________________________________ Automovil

create table AutoMarca(
	IdAutoMarca int identity(1,1) primary key,
	Nombre varchar(40)
	)

create table AutoModelo(
	IdAutoModelo int identity(1,1) primary key,
	Nombre varchar(40),
	IdAutoMarca int foreign key (IdAutoMarca) references AutoMarca(IdAutoMarca),
	)

CREATE TABLE Promocion_Auto(
	IdPromocion_Auto INT IDENTITY(1,1),
	IdAutoModelo int foreign key (IdAutoModelo) references AutoModelo(IdAutoModelo),
	IdPromocion int foreign key (IdPromocion) references PromocionList(IdPromocion),
	IdConcesinaria int foreign key (IdConcesinaria) references Concesinaria(IdConcesinaria),
	Vigente bit default 1
)

CREATE TABLE AutoEstadoList(
	IdAutoEstado INT IDENTITY(1,1) PRIMARY KEY,
	Numero VARCHAR(30),
	Nombre VARCHAR(40)
	)

CREATE TABLE AutoColorList(
	IdAutoColor INT IDENTITY(1,1) PRIMARY KEY,
	Numero VARCHAR(30),
	Nombre VARCHAR(40)
)

create table Automovil(
	IdAutomovil int identity(1,1) primary key,
	Numero VARCHAR(30),
	Anio int,	
	IdAutoModelo int foreign key (IdAutoModelo) references AutoModelo(IdAutoModelo),
	IdAutoColor int foreign key (IdAutoColor) references AutoColorList(IdAutoColor),
	IdAutoEstado int foreign key (IdAutoEstado) references AutoEstadoList(IdAutoEstado)
	)

-- __________________________________________________________________ Accesorio

create table Accesorio(
	IdAccesorio int identity(1,1) primary key,
	Nombre varchar(50),
	CantidadExistencia INT,
	CantidadVendido INT,
	IdConcesinaria int foreign key(IdConcesinaria) references Concesinaria(IdConcesinaria)
	)

create table AutoAccesorio(
	IdAutoAccesorio int identity(1,1) primary key,
	IdAccesorio int foreign key(IdAccesorio) references Accesorio(IdAccesorio),
	IdAutomovil int foreign key(IdAutomovil) references Automovil(IdAutomovil)
	)

-- __________________________________________________________________ Fabrica

create table Fabrica(
	IdFabrica int identity(1,1) primary key,
	Numero VARCHAR(30),
	Nombre varchar(50),
	)
-- __________________________________________________________________ Origen 

CREATE TABLE OrigenEstado(
	IdOrigenEstado INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(50)
)

create table Origen_Fabrica(
	IdOrigen_Fabrica int identity(1,1) primary key,
	Numero VARCHAR(30),
	IdFabrica int foreign key(IdFabrica) references Fabrica(IdFabrica),
	IdUsuario int foreign key(IdUsuario) references Usuario(IdUsuario),
	IdAutomovil int foreign key(IdAutomovil) references Automovil(IdAutomovil),
	IdOrigenEstado INT FOREIGN KEY(IdOrigenEstado) REFERENCES OrigenEstado(IdOrigenEstado)
	)

create table Origen_Traspaso(
	IdOrigen_Traspaso int identity(1,1) primary key,
	Numero INT,
	IdVendedor int foreign key(IdVendedor) references Usuario(IdUsuario),
	IdComprador int foreign key(IdComprador) references Usuario(IdUsuario),
	IdConcesinaria int foreign key(IdConcesinaria) references Concesinaria(IdConcesinaria), -- Concesionaria que va recibir el automovil
	IdAutomovil int foreign key(IdAutomovil) references Automovil(IdAutomovil),
	IdOrigenEstado INT FOREIGN KEY(IdOrigenEstado) REFERENCES OrigenEstado(IdOrigenEstado)
	)

-- __________________________________________________________________ Cliente
CREATE TABLE Estado_Cliente(
	IdEstado_Cliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(30)
)

create table Cliente (
	IdCliente int identity(1,1) primary key,
	Numero VARCHAR(30),
	Nombre varchar(50),
	Edad int,
	Direccion varchar(60),
	FechaNac date,
	Sexo varchar(10),
	TelCasa int,
	TelCel int,
	Correo varchar(50),
	RFC varchar(13),
	IdMunicipio int foreign key(IdMunicipio) references Municipio(IdMunicipio),
	IdEstado_Cliente int foreign key(IdEstado_Cliente) references Estado_Cliente(IdEstado_Cliente)
	)

create table Referencias(
	IdReferencia int identity(1,1) primary key,
	Numero VARCHAR(30),
	Nombre varchar(50),
	TelCel int,
	IdCliente int foreign key(IdCliente) references Cliente(IdCliente)
	)

-- __________________________________________________________________ Venta

CREATE TABLE VentaAuto(
	IdVentaAuto INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	IdUsuario int foreign key(IdUsuario) references Usuario(IdUsuario),
	IdPromocion int foreign key (IdPromocion) references PromocionList(IdPromocion),
	IdCliente int foreign key(IdCliente) references Cliente(IdCliente),
)

CREATE TABLE AutoCliente(
	IdAutoCliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	IdAutomovil int foreign key(IdAutomovil) references Automovil(IdAutomovil),
	IdVentaAuto int foreign key(IdVentaAuto) references VentaAuto(IdVentaAuto)
)


-- __________________________________________________________________ Mantenimiento

/*
create table CategoriaAutoparte(
	IdCategoriaAutoparte int identity(1,1) primary key,
	Categoria varchar(70)
	)

create table Autopartes(
	IdAutopartes int identity(1,1) primary key,
	Nombre varchar(50),
	IdCategoriaAutoparte int foreign key(IdCategoriaAutoparte) references CategoriaAutoparte(IdCategoriaAutoparte),
	IdConcesinaria int foreign key(IdConcesinaria) references Concesinaria(IdConcesinaria)
	)

create table Mantenimiento(
	IdMantenimiento int identity(1,1) primary key,
	Descripcion varchar(70),	
	IdAutopartes int foreign key(IdAutopartes) references Autopartes(IdAutopartes),
	IdVentaAuto int foreign key(IdVentaAuto) references VentaAuto(IdVentaAuto)
	)
	*/