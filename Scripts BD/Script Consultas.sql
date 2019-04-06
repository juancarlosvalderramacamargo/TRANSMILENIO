--Script Consultas en la BD INFRAESTRUCTURA_TRANSMILENIO
--Integrantes:
--				- Marilyn Astrid Chaparro Hurtado
--				- Juan Carlos Valderrama Camargo

------------------------------------------------------------------
--Consultas realizadas en la BD
------------------------------------------------------------------
--Consulta que obtiene el nombre y posición de las paradas de la ruta tipo troncal 1
SELECT P.NOMBRE_PARADERO, RP.POSICION_RUTA 
FROM PARADEROS P
INNER JOIN RUTAS_PARADEROS RP ON RP.ID_PARADERO = P.ID_PARADERO
INNER JOIN RUTAS R ON R.ID_RUTA = RP.ID_RUTA
WHERE R.NOMBRE_RUTA = '1' AND R.ID_TIPO_RUTA = 1

--Consulta que obtiene el nombre de los conductores de buses tipo Articulado
SELECT B.CONDUTOR 
FROM BUSES B
INNER JOIN TIPOS_BUSES TB ON TB.ID_TIPO_BUS = B.ID_TIPO_BUS
WHERE TB.TIPO_BUS = 'Articulado'

--Consulta que obtiene las rutas que pasan por la Avenida El Dorado
SELECT R.NOMBRE_RUTA 
FROM RUTAS R
INNER JOIN VIAS_RUTAS VR ON VR.ID_RUTA = R.ID_RUTA
INNER JOIN VIAS V ON V.ID_VIA = VR.ID_VIA 
WHERE V.NOMBRE_VIA = 'Avenida El Dorado'

--Consulta que obtiene los conductores de buses y el tipo de bus del modelo 2015
SELECT B.CONDUTOR, TB.TIPO_BUS 
FROM BUSES B
INNER JOIN TIPOS_BUSES TB ON TB.ID_TIPO_BUS = B.ID_TIPO_BUS
WHERE B.MODELO = '2015'

--Consulta que permite obtener los tipos de buses que circulan por la Autopista Norte
SELECT DISTINCT TB.TIPO_BUS
FROM BUSES B
INNER JOIN TIPOS_BUSES TB ON TB.ID_TIPO_BUS = B.ID_TIPO_BUS
INNER JOIN BUSES_VIAS BV ON BV.ID_TIPO_BUS = TB.ID_TIPO_BUS
INNER JOIN VIAS V ON V.ID_VIA = BV.ID_VIA
WHERE V.NOMBRE_VIA = 'Autopista Norte'

--Consulta que obtiene las rutas del servicio complementario
SELECT R.NOMBRE_RUTA
FROM RUTAS R
INNER JOIN TIPOS_RUTAS TR ON TR.ID_TIPO_RUTA = R.ID_TIPO_RUTA
WHERE TR.TIPO_RUTA = 'Complementario'