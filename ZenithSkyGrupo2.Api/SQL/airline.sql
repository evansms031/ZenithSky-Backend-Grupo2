USE ZenithSky_Proyecto_BAEE;
GO

-- =============================================
-- TABLA AIRLINE
-- =============================================

IF OBJECT_ID('dbo.airline', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.airline
    (
        airline_id INT IDENTITY(1,1) PRIMARY KEY,
        iata CHAR(2) NOT NULL,
        airlinename VARCHAR(100) NULL,
        base_airport INT NOT NULL,

        CONSTRAINT FK_airline_airport
            FOREIGN KEY (base_airport)
            REFERENCES dbo.airport(airport_id),

        CONSTRAINT CK_airline_iata_length
            CHECK (LEN(RTRIM(iata)) = 2)
    );
END
GO


-- =============================================
-- GET ALL
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Airline_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.airline_id,
        a.iata,
        a.airlinename,
        a.base_airport,
        ap.iata AS base_airport_iata,
        ap.name AS base_airport_name
    FROM dbo.airline AS a
    INNER JOIN dbo.airport AS ap
        ON a.base_airport = ap.airport_id
    ORDER BY a.airline_id;
END
GO


-- =============================================
-- GET BY ID
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Airline_GetById
    @airline_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.airline_id,
        a.iata,
        a.airlinename,
        a.base_airport,
        ap.iata AS base_airport_iata,
        ap.name AS base_airport_name
    FROM dbo.airline AS a
    INNER JOIN dbo.airport AS ap
        ON a.base_airport = ap.airport_id
    WHERE a.airline_id = @airline_id;
END
GO


-- =============================================
-- INSERT
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Airline_Create
    @iata CHAR(2),
    @airlinename VARCHAR(100),
    @base_airport INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.airline
    (
        iata,
        airlinename,
        base_airport
    )
    VALUES
    (
        @iata,
        @airlinename,
        @base_airport
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS airline_id;
END
GO


-- =============================================
-- UPDATE
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Airline_Update
    @airline_id INT,
    @iata CHAR(2),
    @airlinename VARCHAR(100),
    @base_airport INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.airline
    SET
        iata = @iata,
        airlinename = @airlinename,
        base_airport = @base_airport
    WHERE airline_id = @airline_id;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO


-- =============================================
-- DELETE
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Airline_Delete
    @airline_id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- La aerolínea no existe
    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.airline
        WHERE airline_id = @airline_id
    )
    BEGIN
        SELECT -1 AS Result;
        RETURN;
    END;

    -- Tiene aviones asociados
    IF EXISTS
    (
        SELECT 1
        FROM dbo.airplane
        WHERE airline_id = @airline_id
    )
    BEGIN
        SELECT 0 AS Result;
        RETURN;
    END;

    -- Se puede eliminar
    DELETE FROM dbo.airline
    WHERE airline_id = @airline_id;

    SELECT 1 AS Result;
END
GO