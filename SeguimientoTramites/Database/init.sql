-- Crear la base de datos
CREATE DATABASE SeguimientoTramitesDB;
GO

USE SeguimientoTramitesDB;
GO

-- Tabla CARRERA
CREATE TABLE CARRERA (
    IdCarrera INT IDENTITY(1,1) PRIMARY KEY,
    Descrip VARCHAR(100) NOT NULL
);

-- Tabla ALUMNO
CREATE TABLE ALUMNO (
    Matricula INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL,
    Contra VARCHAR(200) NOT NULL,
    IdCarrera INT NOT NULL,
    IsActivo BIT DEFAULT 1,
    CONSTRAINT FK_Alumno_Carrera FOREIGN KEY (IdCarrera) REFERENCES CARRERA(IdCarrera)
);

-- Datos de prueba
INSERT INTO CARRERA (Descrip) VALUES ('Contador Publico');
INSERT INTO CARRERA (Descrip) VALUES ('Licenciado en Administracion');
INSERT INTO CARRERA (Descrip) VALUES ('Licenciado en Tecnologias de Informacion');
INSERT INTO CARRERA (Descrip) VALUES ('Licenciado en Negocios Internacionales');

INSERT INTO ALUMNO (Nombre, Correo, Contra, IdCarrera, IsActivo)
VALUES ('Juan Perez', 'juan.perez@uanl.edu.mx', '123456', 1, 1);

INSERT INTO ALUMNO (Nombre, Correo, Contra, IdCarrera, IsActivo)
VALUES ('Maria Garcia', 'maria.garcia@uanl.edu.mx', '123456', 3, 1);
