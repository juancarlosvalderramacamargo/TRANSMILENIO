--Script Creación de la BD INFRAESTRUCTURA_TRANSMILENIO
--Integrantes:
--				- Marilyn Astrid Chaparro Hurtado
--				- Juan Carlos Valderrama Camargo

------------------------------------------------------------------
--Creación de la BD INFRAESTRUCTURA_TRANSMILENIO
------------------------------------------------------------------
CREATE DATABASE INFRAESTRUCTURA_TRANSMILENIO

------------------------------------------------------------------
--Creación de las tablas de la BD
------------------------------------------------------------------
USE INFRAESTRUCTURA_TRANSMILENIO

CREATE TABLE ZONAS
( 	
	ID_ZONA				NUMERIC(11)		NOT NULL,
	NOMBRE_ZONA			VARCHAR(20)		NOT NULL
);

CREATE TABLE VIAS
( 	
	ID_VIA				NUMERIC(11)		NOT NULL,
	ID_ZONA				NUMERIC(11)		NOT NULL,
	NOMBRE_VIA			VARCHAR(20)		NOT NULL
);

CREATE TABLE TIPOS_BUSES
( 	
	ID_TIPO_BUS			NUMERIC(1)		NOT NULL,
	TIPO_BUS			VARCHAR(20)		NOT NULL
);

CREATE TABLE BUSES
( 	
	ID_BUS				NUMERIC(11)		NOT NULL,
	ID_TIPO_BUS			NUMERIC(1)		NOT NULL,
	MARCA				VARCHAR(20)		NOT NULL,
	MODELO				VARCHAR(20)		NOT NULL,
	CONDUTOR			VARCHAR(50)		NOT NULL
);

CREATE TABLE BUSES_VIAS
( 	
	ID_TIPO_BUS			NUMERIC(1)		NOT NULL,
	ID_VIA				NUMERIC(11)		NOT NULL
);

CREATE TABLE TIPOS_RUTAS
( 	
	ID_TIPO_RUTA		NUMERIC(1)		NOT NULL,
	TIPO_RUTA			VARCHAR(20)		NOT NULL
);

CREATE TABLE RUTAS
( 	
	ID_RUTA				NUMERIC(11)		NOT NULL,
	ID_BUS				NUMERIC(11)		NOT NULL,
	ID_TIPO_RUTA		NUMERIC(1)		NOT NULL,
	NOMBRE_RUTA			VARCHAR(20)		NOT NULL
);

CREATE TABLE VIAS_RUTAS
( 	
	ID_VIA				NUMERIC(11)		NOT NULL,
	ID_RUTA				NUMERIC(11)		NOT NULL
);

CREATE TABLE TIPOS_PARADEROS
( 	
	ID_TIPO_PAR			NUMERIC(1)		NOT NULL,
	TIPO_PARADERO		VARCHAR(10)		NOT NULL
);

CREATE TABLE PARADEROS
( 	
	ID_PARADERO			NUMERIC(11)		NOT NULL,
	ID_TIPO_PAR			NUMERIC(1)		NOT NULL,
	ID_VIA				NUMERIC(11)		NOT NULL,
	NOMBRE_PARADERO		VARCHAR(20)		NOT NULL,
	UBICACION_PARADERO	VARCHAR(20)		NOT NULL,
	POSICION_VIA		NUMERIC(3)		NOT NULL
);

CREATE TABLE RUTAS_PARADEROS
( 	
	ID_PARADERO			NUMERIC(11)		NOT NULL,
	ID_RUTA				NUMERIC(11)		NOT NULL,
	POSICION_RUTA		NUMERIC(2)		NOT NULL
);