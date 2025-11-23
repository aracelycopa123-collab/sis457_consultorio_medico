-- Crear BD
CREATE DATABASE FinalConsultorioMedico;
GO

-- Usar BD
USE FinalConsultorioMedico;
GO

-- Crear usuario de 
CREATE USER usrfinalconsultoriomedico FOR LOGIN usrfinalconsultoriomedico;
GO

-- Dar permisos db_owner
ALTER ROLE db_owner ADD MEMBER usrfinalconsultoriomedico;
GO

  -- TABLAS

-- Tabla Especialidad
CREATE TABLE Especialidad(
    id INT IDENTITY(1,1) PRIMARY KEY, 
    nombre VARCHAR(30) NOT NULL, 
    usuarioRegistro VARCHAR(50) NOT NULL, 
    fechaRegistro DATETIME NOT NULL, 
    estado SMALLINT NOT NULL 
);

-- Tabla Doctor
CREATE TABLE Doctor(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    idEspecialidad INT NOT NULL, -- FK especialidad
    cedulaIdentidad VARCHAR(12) NOT NULL, -- CI
    nombres VARCHAR(30) NOT NULL, -- nombre
    primerApellido VARCHAR(30) NULL, -- apellido 1
    segundoApellido VARCHAR(30) NULL, -- apellido 2
    direccion VARCHAR(250) NOT NULL, -- dirección
    celular BIGINT NOT NULL, -- celular
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha
    estado SMALLINT NOT NULL -- estado
);

-- Tabla Paciente
CREATE TABLE Paciente(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    cedulaIdentidad VARCHAR(12) NOT NULL, -- CI
    nombres VARCHAR(30) NOT NULL, -- nombre
    primerApellido VARCHAR(30) NULL, -- apellido 1
    segundoApellido VARCHAR(30) NULL, -- apellido 2
    direccion VARCHAR(200) NULL, -- dirección
    celular BIGINT NOT NULL, -- celular
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha
    estado SMALLINT NOT NULL, -- estado
    FechaNacimiento DATETIME2 NULL -- nacimiento
);

-- Tabla Cita
CREATE TABLE Cita(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    idDoctor INT NOT NULL, -- FK doctor
    idPaciente INT NOT NULL, -- FK paciente
    idEspecialidad INT NOT NULL, -- FK especialidad
    fecha DATE NOT NULL, -- fecha
    hora TIME(7) NOT NULL, -- hora
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha
    estado SMALLINT NOT NULL -- estado
);

-- Tabla Concepto
CREATE TABLE Concepto(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    idEspecialidad INT NOT NULL, -- FK especialidad
    descripcion VARCHAR(250) NOT NULL, -- concepto
    costo DECIMAL(10,2) NOT NULL, -- costo
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha
    estado SMALLINT NOT NULL -- estado
);

-- Tabla Pago
CREATE TABLE Pago(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    idCita INT NOT NULL, -- FK cita
    idConcepto INT NOT NULL, -- FK concepto
    fecha DATE NOT NULL, -- fecha
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha reg
    estado SMALLINT NOT NULL -- estado
);

-- Tabla Usuario
CREATE TABLE Usuario(
    id INT IDENTITY(1,1) PRIMARY KEY, -- PK
    idDoctor INT NOT NULL, -- FK doctor
    usuario VARCHAR(20) NOT NULL, -- usuario login
    clave VARCHAR(250) NOT NULL, -- clave
    usuarioRegistro VARCHAR(50) NOT NULL, -- usuario
    fechaRegistro DATETIME NOT NULL, -- fecha
    estado SMALLINT NOT NULL -- estado
);


/* ============================
   DEFAULTS
   ============================ */

ALTER TABLE Cita       ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Cita       ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Cita       ADD DEFAULT (1) FOR estado;

ALTER TABLE Concepto   ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Concepto   ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Concepto   ADD DEFAULT (1) FOR estado;

ALTER TABLE Doctor     ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Doctor     ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Doctor     ADD DEFAULT (1) FOR estado;

ALTER TABLE Especialidad ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Especialidad ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Especialidad ADD DEFAULT (1) FOR estado;

ALTER TABLE Paciente   ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Paciente   ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Paciente   ADD DEFAULT (1) FOR estado;

ALTER TABLE Pago       ADD DEFAULT (getdate()) FOR fecha;
ALTER TABLE Pago       ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Pago       ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Pago       ADD DEFAULT (1) FOR estado;

ALTER TABLE Usuario    ADD DEFAULT (suser_sname()) FOR usuarioRegistro;
ALTER TABLE Usuario    ADD DEFAULT (getdate()) FOR fechaRegistro;
ALTER TABLE Usuario    ADD DEFAULT (1) FOR estado;



ALTER TABLE Doctor     ADD FOREIGN KEY(idEspecialidad) REFERENCES Especialidad(id);
ALTER TABLE Concepto   ADD FOREIGN KEY(idEspecialidad) REFERENCES Especialidad(id);
ALTER TABLE Cita       ADD FOREIGN KEY(idEspecialidad) REFERENCES Especialidad(id);
ALTER TABLE Cita       ADD FOREIGN KEY(idDoctor) REFERENCES Doctor(id);
ALTER TABLE Cita       ADD FOREIGN KEY(idPaciente) REFERENCES Paciente(id);
ALTER TABLE Pago       ADD FOREIGN KEY(idCita) REFERENCES Cita(id);
ALTER TABLE Pago       ADD FOREIGN KEY(idConcepto) REFERENCES Concepto(id);
ALTER TABLE Usuario    ADD FOREIGN KEY(idDoctor) REFERENCES Doctor(id);

--USUARIO prueba
INSERT INTO Usuario(usuario, clave, idDoctor)
VALUES ('soe', 'i0hcoO/nssY6WOs9pOp5Xw==', 1);

GO



