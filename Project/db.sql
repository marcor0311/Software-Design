
USE [e-food-commerce];

CREATE TABLE [dbo].[BITACORA] (
    [codigo]          INT           NOT NULL	IDENTITY(1,1),
    [nombre_usuario]  VARCHAR (50)  NULL,
    [fecha_hora]      DATETIME      NOT NULL,
    [codigo_registro] INT           NOT NULL,
    [descripcion]     VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_BITACORA] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE TABLE [dbo].[ERRORES] (
    [codigo]     INT           NOT NULL,
    [fecha_hora] DATETIME      NOT NULL,
    [mensaje]    VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ERRORES] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE TABLE [dbo].[LINEA_COMIDA] (
    [codigo] INT          NOT NULL,
    [nombre] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_LINEA_COMIDA] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE UNIQUE NONCLUSTERED INDEX [Index_LINEA_COMIDA_1]
    ON [dbo].[LINEA_COMIDA]([nombre] ASC);


CREATE TABLE [dbo].[PEDIDO] (
    [codigo]           INT          NOT NULL,
    [monto]            INT          NOT NULL,
    [fecha_hora]       DATETIME     NOT NULL,
    [estado]           VARCHAR (50) NOT NULL,
    [codigo_descuento] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PEDIDO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE TABLE [dbo].[PRECIO_PRODUCTO] (
    [codigo]               INT NOT NULL,
    [codigo_producto]      INT NOT NULL,
    [codigo_tipos_precios] INT NOT NULL,
    [precio]               INT NOT NULL,
    CONSTRAINT [PK_PRECIO_PRODUCTO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);

CREATE TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] (
    [codigo_precio_producto] INT NOT NULL,
    [codigo_pedido]          INT NOT NULL,
    [cantidad]               INT NOT NULL,
    [codigo]                 INT NOT NULL,
    CONSTRAINT [PK_PRECIO_PRODUCTO_PEDIDO] PRIMARY KEY CLUSTERED ([codigo_precio_producto] ASC, [codigo_pedido] ASC, [codigo] ASC)
);

CREATE TABLE [dbo].[TIPOS_PRECIOS] (
    [codigo]      INT          NOT NULL,
    [nombre]      VARCHAR (50) NOT NULL,
    [descripcion] VARCHAR (70) NOT NULL,
    CONSTRAINT [PK_TIPOS_PRECIOS] PRIMARY KEY CLUSTERED ([codigo] ASC)
);

CREATE TABLE [dbo].[PROCESADORES_PAGO] (
    [codigo]             INT          NOT NULL,
    [nombre_procesador]  VARCHAR (50) NOT NULL,
    [nombre_opcion_pago] VARCHAR (50) NOT NULL,
    [tipo]               VARCHAR (50) NOT NULL,
    [estado]             BIT          NOT NULL,
    [verificacion]       BIT          NOT NULL,
    [metodo]             VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PROCESADORES_PAGO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE UNIQUE NONCLUSTERED INDEX [Index_PROCESADORES_PAGO_1]
    ON [dbo].[PROCESADORES_PAGO]([nombre_procesador] ASC);


CREATE TABLE [dbo].[PRODUCTO] (
    [codigo]              INT           NOT NULL,
    [nombre]              VARCHAR (50)  NOT NULL,
    [codigo_linea_comida] INT           NOT NULL,
    [contenido]           VARCHAR (MAX) NULL,
    [ruta_imagen]         VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_PRODUCTO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE UNIQUE NONCLUSTERED INDEX [Index_PRODUCTO_1]
    ON [dbo].[PRODUCTO]([nombre] ASC);


CREATE TABLE [dbo].[ROL] (
    [codigo] INT          NOT NULL,
    [nombre] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ROL] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE UNIQUE NONCLUSTERED INDEX [Index_ROL_1]
    ON [dbo].[ROL]([nombre] ASC);


CREATE TABLE [dbo].[TARJETAS] (
    [codigo]      INT           NOT NULL,
    [nombre]      VARCHAR (50)  NOT NULL,
    [descripcion] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_TARJETAS] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE UNIQUE NONCLUSTERED INDEX [Index_TARJETAS_1]
    ON [dbo].[TARJETAS]([nombre] ASC);


CREATE TABLE [dbo].[TARJETAS_PROCESADORES_PAGO] (
    [codigo_tarjetas]          INT NOT NULL,
    [codigo_procesadores_pago] INT NOT NULL,
    [codigo]                   INT NOT NULL,
    CONSTRAINT [PK_TARJETAS_PROCESADORES_PAGO] PRIMARY KEY CLUSTERED ([codigo_tarjetas] ASC, [codigo_procesadores_pago] ASC, [codigo] ASC)
);


CREATE TABLE [dbo].[TIQUETES_DESCUENTO] (
    [codigo]      VARCHAR (50)  NOT NULL,
    [nombre]      VARCHAR (MAX) NOT NULL,
    [disponibles] INT           NOT NULL,
    [descuento]   INT           NOT NULL,
    CONSTRAINT [PK_TIQUETES_DESCUENTO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE TABLE [dbo].[USUARIO] (
    [codigo]              INT           NOT NULL,
    [nombre]              VARCHAR (50)  NOT NULL,
    [email]               VARCHAR (MAX) NOT NULL,
    [pregunta_seguridad]  VARCHAR (MAX) NOT NULL,
    [respuesta_seguridad] VARCHAR (MAX) NOT NULL,
    [hash]                VARCHAR (MAX) NOT NULL,
    [salt]                VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_USUARIO] PRIMARY KEY CLUSTERED ([codigo] ASC)
);


CREATE TABLE [dbo].[USUARIO_ROL] (
    [codigo_usuario] INT NOT NULL,
    [rol]            INT NOT NULL,
    [codigo]         INT NOT NULL,
    CONSTRAINT [PK_USUARIO_ROL] PRIMARY KEY CLUSTERED ([codigo_usuario] ASC, [rol] ASC, [codigo] ASC)
);


ALTER TABLE [dbo].[PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [FK_PEDIDO_TIQUETES_DESCUENTO] FOREIGN KEY ([codigo_descuento]) REFERENCES [dbo].[TIQUETES_DESCUENTO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PRECIO_PRODUCTO] WITH NOCHECK
    ADD CONSTRAINT [FK_PRECIO_PRODUCTO_PRODUCTO] FOREIGN KEY ([codigo_producto]) REFERENCES [dbo].[PRODUCTO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PRECIO_PRODUCTO] WITH NOCHECK
    ADD CONSTRAINT [FK_PRECIO_PRODUCTO_TIPOS_PRECIOS] FOREIGN KEY ([codigo_tipos_precios]) REFERENCES [dbo].[TIPOS_PRECIOS] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [FK_PRECIO_PRODUCTO_PEDIDO_PEDIDO] FOREIGN KEY ([codigo_pedido]) REFERENCES [dbo].[PEDIDO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [FK_PRECIO_PRODUCTO_PEDIDO_PRECIO_PRODUCTO] FOREIGN KEY ([codigo_precio_producto]) REFERENCES [dbo].[PRECIO_PRODUCTO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PRODUCTO] WITH NOCHECK
    ADD CONSTRAINT [FK_PRODUCTO_LINEA_COMIDA] FOREIGN KEY ([codigo_linea_comida]) REFERENCES [dbo].[LINEA_COMIDA] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[TARJETAS_PROCESADORES_PAGO] WITH NOCHECK
    ADD CONSTRAINT [FK_TARJETAS_PROCESADORES_PAGO_PROCESADORES_PAGO] FOREIGN KEY ([codigo_procesadores_pago]) REFERENCES [dbo].[PROCESADORES_PAGO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[TARJETAS_PROCESADORES_PAGO] WITH NOCHECK
    ADD CONSTRAINT [FK_TARJETAS_PROCESADORES_PAGO_TARJETAS] FOREIGN KEY ([codigo_tarjetas]) REFERENCES [dbo].[TARJETAS] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[USUARIO_ROL] WITH NOCHECK
    ADD CONSTRAINT [FK_USUARIO_ROL_USUARIO] FOREIGN KEY ([codigo_usuario]) REFERENCES [dbo].[USUARIO] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[USUARIO_ROL] WITH NOCHECK
    ADD CONSTRAINT [FK_USUARIO_ROL_ROL] FOREIGN KEY ([rol]) REFERENCES [dbo].[ROL] ([codigo]) ON DELETE CASCADE;


ALTER TABLE [dbo].[PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [CK_PEDIDO_2] CHECK ([estado]='en concurso' OR [estado]='cancelado' OR [estado]='procesado');


ALTER TABLE [dbo].[PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [CK_PEDIDO_1] CHECK ([monto]>=(0));


ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH NOCHECK
    ADD CONSTRAINT [CK_PRECIO_PRODUCTO_PEDIDO_1] CHECK ([cantidad]>=(1));


ALTER TABLE [dbo].[PROCESADORES_PAGO] WITH NOCHECK
    ADD CONSTRAINT [CK_PROCESADORES_PAGO_1] CHECK ([tipo]='cheque' OR [tipo]='efectivo' OR [tipo]='debito' OR [tipo]='credito');


ALTER TABLE [dbo].[TIQUETES_DESCUENTO] WITH NOCHECK
    ADD CONSTRAINT [CK_TIQUETES_DESCUENTO_2] CHECK ([descuento]>=(0) AND [descuento]<=(100));


ALTER TABLE [dbo].[TIQUETES_DESCUENTO] WITH NOCHECK
    ADD CONSTRAINT [CK_TIQUETES_DESCUENTO_1] CHECK ([disponibles]>=(0));



ALTER TABLE [dbo].[PEDIDO] WITH CHECK CHECK CONSTRAINT [FK_PEDIDO_TIQUETES_DESCUENTO];

ALTER TABLE [dbo].[PRECIO_PRODUCTO] WITH CHECK CHECK CONSTRAINT [FK_PRECIO_PRODUCTO_PRODUCTO];

ALTER TABLE [dbo].[PRECIO_PRODUCTO] WITH CHECK CHECK CONSTRAINT [FK_PRECIO_PRODUCTO_TIPOS_PRECIOS];

ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH CHECK CHECK CONSTRAINT [FK_PRECIO_PRODUCTO_PEDIDO_PEDIDO];

ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH CHECK CHECK CONSTRAINT [FK_PRECIO_PRODUCTO_PEDIDO_PRECIO_PRODUCTO];

ALTER TABLE [dbo].[PRODUCTO] WITH CHECK CHECK CONSTRAINT [FK_PRODUCTO_LINEA_COMIDA];

ALTER TABLE [dbo].[TARJETAS_PROCESADORES_PAGO] WITH CHECK CHECK CONSTRAINT [FK_TARJETAS_PROCESADORES_PAGO_PROCESADORES_PAGO];

ALTER TABLE [dbo].[TARJETAS_PROCESADORES_PAGO] WITH CHECK CHECK CONSTRAINT [FK_TARJETAS_PROCESADORES_PAGO_TARJETAS];

ALTER TABLE [dbo].[USUARIO_ROL] WITH CHECK CHECK CONSTRAINT [FK_USUARIO_ROL_USUARIO];

ALTER TABLE [dbo].[USUARIO_ROL] WITH CHECK CHECK CONSTRAINT [FK_USUARIO_ROL_ROL];

ALTER TABLE [dbo].[PEDIDO] WITH CHECK CHECK CONSTRAINT [CK_PEDIDO_2];

ALTER TABLE [dbo].[PEDIDO] WITH CHECK CHECK CONSTRAINT [CK_PEDIDO_1];

ALTER TABLE [dbo].[PRECIO_PRODUCTO_PEDIDO] WITH CHECK CHECK CONSTRAINT [CK_PRECIO_PRODUCTO_PEDIDO_1];

ALTER TABLE [dbo].[PROCESADORES_PAGO] WITH CHECK CHECK CONSTRAINT [CK_PROCESADORES_PAGO_1];

ALTER TABLE [dbo].[TIQUETES_DESCUENTO] WITH CHECK CHECK CONSTRAINT [CK_TIQUETES_DESCUENTO_2];

ALTER TABLE [dbo].[TIQUETES_DESCUENTO] WITH CHECK CHECK CONSTRAINT [CK_TIQUETES_DESCUENTO_1];

GO

---------------------------------------------------------------------
-- LINEA_COMIDA


CREATE TRIGGER trg_LINEA_COMIDA_Insert
ON LINEA_COMIDA
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla LINEA_COMIDA'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_LINEA_COMIDA_Update
ON LINEA_COMIDA
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla LINEA_COMIDA'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_LINEA_COMIDA_Delete
ON LINEA_COMIDA
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla LINEA_COMIDA'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- PEDIDO


CREATE TRIGGER trg_PEDIDO_Insert
ON PEDIDO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla PEDIDO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PEDIDO_Update
ON PEDIDO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla PEDIDO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PEDIDO_Delete
ON PEDIDO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla PEDIDO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- PRECIO_PRODUCTO


CREATE TRIGGER trg_PRECIO_PRODUCTO_Insert
ON PRECIO_PRODUCTO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla PRECIO_PRODUCTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRECIO_PRODUCTO_Update
ON PRECIO_PRODUCTO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla PRECIO_PRODUCTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRECIO_PRODUCTO_Delete
ON PRECIO_PRODUCTO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla PRECIO_PRODUCTO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- PRECIO_PRODUCTO_PEDIDO


CREATE TRIGGER trg_PRECIO_PRODUCTO_PEDIDO_Insert
ON PRECIO_PRODUCTO_PEDIDO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla PRECIO_PRODUCTO_PEDIDO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRECIO_PRODUCTO_PEDIDO_Update
ON PRECIO_PRODUCTO_PEDIDO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla PRECIO_PRODUCTO_PEDIDO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRECIO_PRODUCTO_PEDIDO_Delete
ON PRECIO_PRODUCTO_PEDIDO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla PRECIO_PRODUCTO_PEDIDO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- PROCESADORES_PAGO


CREATE TRIGGER trg_PROCESADORES_PAGO_Insert
ON PROCESADORES_PAGO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla PROCESADORES_PAGO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PROCESADORES_PAGO_Update
ON PROCESADORES_PAGO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla PROCESADORES_PAGO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PROCESADORES_PAGO_Delete
ON PROCESADORES_PAGO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla PROCESADORES_PAGO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- PRODUCTO


CREATE TRIGGER trg_PRODUCTO_Insert
ON PRODUCTO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla PRODUCTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRODUCTO_Update
ON PRODUCTO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla PRODUCTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_PRODUCTO_Delete
ON PRODUCTO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla PRODUCTO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- ROL


CREATE TRIGGER trg_ROL_Insert
ON ROL
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla ROL'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_ROL_Update
ON ROL
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla ROL'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_ROL_Delete
ON ROL
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla ROL'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- TARJETAS


CREATE TRIGGER trg_TARJETAS_Insert
ON TARJETAS
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla TARJETAS'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TARJETAS_Update
ON TARJETAS
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla TARJETAS'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TARJETAS_Delete
ON TARJETAS
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla TARJETAS'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- TARJETAS_PROCESADORES_PAGO


CREATE TRIGGER trg_TARJETAS_PROCESADORES_PAGO_Insert
ON TARJETAS_PROCESADORES_PAGO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla TARJETAS_PROCESADORES_PAGO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TARJETAS_PROCESADORES_PAGO_Update
ON TARJETAS_PROCESADORES_PAGO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla TARJETAS_PROCESADORES_PAGO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TARJETAS_PROCESADORES_PAGO_Delete
ON TARJETAS_PROCESADORES_PAGO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla TARJETAS_PROCESADORES_PAGO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- TIPOS_PRECIOS


CREATE TRIGGER trg_TIPOS_PRECIOS_Insert
ON TIPOS_PRECIOS
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla TIPOS_PRECIOS'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TIPOS_PRECIOS_Update
ON TIPOS_PRECIOS
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla TIPOS_PRECIOS'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TIPOS_PRECIOS_Delete
ON TIPOS_PRECIOS
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla TIPOS_PRECIOS'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- TIQUETES_DESCUENTO


CREATE TRIGGER trg_TIQUETES_DESCUENTO_Insert
ON TIQUETES_DESCUENTO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla TIQUETES_DESCUENTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TIQUETES_DESCUENTO_Update
ON TIQUETES_DESCUENTO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla TIQUETES_DESCUENTO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_TIQUETES_DESCUENTO_Delete
ON TIQUETES_DESCUENTO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla TIQUETES_DESCUENTO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- USUARIO


CREATE TRIGGER trg_USUARIO_Insert
ON USUARIO
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla USUARIO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_USUARIO_Update
ON USUARIO
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla USUARIO'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_USUARIO_Delete
ON USUARIO
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla USUARIO'
    FROM deleted;
END;
GO


---------------------------------------------------------------------
-- USUARIO_ROL


CREATE TRIGGER trg_USUARIO_ROL_Insert
ON USUARIO_ROL
AFTER INSERT
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro insertado en la tabla USUARIO_ROL'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_USUARIO_ROL_Update
ON USUARIO_ROL
AFTER UPDATE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        inserted.codigo,
        'Registro actualizado en la tabla USUARIO_ROL'
    FROM inserted;
END;
GO
CREATE TRIGGER trg_USUARIO_ROL_Delete
ON USUARIO_ROL
AFTER DELETE
AS
BEGIN
    INSERT INTO BITACORA (nombre_usuario, fecha_hora, codigo_registro, descripcion)
    SELECT
        SYSTEM_USER,
        GETDATE(),
        deleted.codigo,
        'Registro eliminado de la tabla USUARIO_ROL'
    FROM deleted;
END;
GO

