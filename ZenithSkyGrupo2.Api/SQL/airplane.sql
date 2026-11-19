
-- CREACIÓN DE TABLA --

CREATE TABLE airplane (
    airplane_id INT IDENTITY(1,1) PRIMARY KEY,
    capacity INT NOT NULL CHECK (capacity > 0),
    type_id INT NOT NULL,
    airline_id INT NOT NULL,

    FOREIGN KEY (type_id) REFERENCES airplane_type(type_id),
    FOREIGN KEY (airline_id) REFERENCES airline(airline_id)
);
GO

-- PROCEDIMIENTOS ALMACENADOS --

-- Listar y filtrar aviones --

CREATE PROCEDURE sp_GetAirplanes
    @type_id INT = NULL,
    @airline_id INT = NULL
AS
BEGIN
    SELECT
        a.airplane_id,
        a.capacity,
        a.type_id,
        a.airline_id,
        t.identifier,
        t.description,
        al.airlinename
    FROM airplane a
    INNER JOIN airplane_type t ON a.type_id = t.type_id
    INNER JOIN airline al ON a.airline_id = al.airline_id
    WHERE (@type_id IS NULL OR a.type_id = @type_id)
      AND (@airline_id IS NULL OR a.airline_id = @airline_id);
END;
GO


--EXEC sp_GetAirplanes; -- Listar todos los aviones
--EXEC sp_GetAirplanes @type_id = 1; -- Filtrar por tipo de avión
--EXEC sp_GetAirplanes @airline_id = 2; -- Filtrar por aerolínea

-- Consultar un avión por ID --

CREATE PROCEDURE sp_GetAirplaneById
    @airplane_id INT
AS
BEGIN
    SELECT
        a.airplane_id,
        a.capacity,
        a.type_id,
        a.airline_id,
        t.identifier,
        t.description,
        al.airlinename
    FROM airplane a
    INNER JOIN airplane_type t ON a.type_id = t.type_id
    INNER JOIN airline al ON a.airline_id = al.airline_id
    WHERE a.airplane_id = @airplane_id;
END;
GO

--EXEC sp_GetAirplaneById @airplane_id = 1;

-- Insertar aviones --

CREATE PROCEDURE sp_InsertAirplane
    @capacity INT,
    @type_id INT,
    @airline_id INT
AS
BEGIN
    INSERT INTO airplane (capacity, type_id, airline_id)
    VALUES (@capacity, @type_id, @airline_id);
END;
GO

-- Prueba de inserción
/*EXEC sp_InsertAirplane
    @capacity = 220,
    @type_id = 1,
    @airline_id = 2;
*/
--EXEC sp_GetAirplanes;

-- Actualizar un avión -- 

CREATE PROCEDURE sp_UpdateAirplane
    @airplane_id INT,
    @capacity INT,
    @type_id INT,
    @airline_id INT
AS
BEGIN
    UPDATE airplane
    SET capacity = @capacity,
        type_id = @type_id,
        airline_id = @airline_id
    WHERE airplane_id = @airplane_id;
END;
GO

-- Prueba de modificación --

/*EXEC sp_UpdateAirplane
    @airplane_id = 3,
    @capacity = 200,
    @type_id = 2,
    @airline_id = 1;
*/
--EXEC sp_GetAirplaneById @airplane_id = 3;

-- Eliminar avión --

CREATE PROCEDURE sp_DeleteAirplane
    @airplane_id INT
AS
BEGIN
    DELETE FROM airplane
    WHERE airplane_id = @airplane_id;
END;
GO

-- Prueba de eliminación --

--EXEC sp_DeleteAirplane @airplane_id = 3;

--EXEC sp_GetAirplanes;