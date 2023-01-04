CREATE DATABASE PEDALEA;
GO

USE PEDALEA;
GO

CREATE TABLE Producto (
  p_id INTEGER IDENTITY,
  p_nombre VARCHAR(255) NOT NULL,
  p_descripcion VARCHAR(255),
  p_precio DECIMAL(10,2) NOT NULL,
  p_isActivo BIT NOT NULL DEFAULT 1,
  CONSTRAINT PK_P_ID PRIMARY KEY(p_id)
);
GO

CREATE TABLE Pedido (
  pd_id INTEGER IDENTITY,
  pd_numeroPedido INTEGER NOT NULL,
  pd_fecha DATE NOT NULL,
  pd_direccionEnvio VARCHAR(255) NOT NULL,
  pd_estado VARCHAR(255) NOT NULL,
  pd_isActive BIT NOT NULL DEFAULT 1,
  CONSTRAINT PK_PD_ID PRIMARY KEY(pd_id),
);
GO

CREATE TABLE ProductoPedido (
   id_producto INT NOT NULL,
   id_pedido INT NOT NULL,
   Cantidad INT NOT NULL,
   PRIMARY KEY (id_producto, id_pedido),
   FOREIGN KEY (id_producto) REFERENCES Producto(p_id),
   FOREIGN KEY (id_pedido) REFERENCES Pedido(pd_id)
);
GO

--PROCEDURES
CREATE PROCEDURE sp_crearProducto
  @pNombre VARCHAR(255),
  @pDescripcion VARCHAR(255),
  @pPrecio DECIMAL(10,2)
AS
  INSERT INTO Producto(p_nombre,p_descripcion,p_precio,p_isActivo)
    VALUES(@pNombre,@pDescripcion,@pPrecio,1)
GO

CREATE PROCEDURE sp_listarProductos
AS
  select * from Producto
GO

CREATE PROCEDURE sp_buscarProducto
  @pId Integer
AS
  select * from Producto
  where p_id = @pId
GO

CREATE PROCEDURE sp_actualizarProducto
  @pId INTEGER,
  @pNombre VARCHAR(255),
  @pDescripcion VARCHAR(255),
  @pPrecio DECIMAL(10,2)
AS
  UPDATE Producto
  SET p_nombre = @pNombre, p_descripcion = @pDescripcion, p_precio = @pPrecio
  WHERE p_id = @pId
GO

CREATE PROCEDURE sp_desactivarProducto
  @pId INTEGER
AS
  UPDATE Producto
  SET p_isActivo = 0
  WHERE p_id = @pId
GO

CREATE PROCEDURE sp_crearPedido
  @pNumeroPedido INTEGER,
  @pFecha Date,
  @pDireccionEnvio VARCHAR(255),
  @pEstado VARCHAR(255)
AS
  INSERT INTO Pedido(pd_numeroPedido,pd_fecha,pd_direccionEnvio,pd_estado,pd_isActive)
    VALUES(@pNumeroPedido,@pFecha,@pDireccionEnvio,@pEstado,1)
GO

CREATE PROCEDURE sp_listarPedidos
AS
  select * from Pedido
GO

CREATE PROCEDURE sp_buscarPedido
  @pId Integer
AS
  select * from Pedido
  where pd_id = @pId
GO

CREATE PROCEDURE sp_actualizarPedido
  @pdId INTEGER,
  @pNumeroPedido INTEGER,
  @pFecha Date,
  @pDireccionEnvio VARCHAR(255),
  @pEstado VARCHAR(255)
AS
  UPDATE Pedido
  SET pd_numeroPedido = @pNumeroPedido, pd_fecha = @pFecha,
    pd_direccionEnvio = @pDireccionEnvio, pd_estado = @pEstado
  WHERE pd_id = @pdId
GO

CREATE PROCEDURE sp_desactivarPedido
  @pId INTEGER
AS
  UPDATE Pedido
  SET pd_isActive = 0
  WHERE pd_id = @pId
GO

CREATE PROCEDURE sp_crearPedidoProducto
  @pid_producto INTEGER,
  @pid_pedido INTEGER,
  @pcantidad INTEGER
AS
  INSERT INTO ProductoPedido(id_pedido,id_producto,Cantidad)
    VALUES(@pid_pedido,@pid_producto,@pcantidad)
GO

exec dbo.sp_crearProducto 'Camiseta Deportiva 55', 'Camiseta echa en algodon',50;
exec dbo.sp_crearProducto 'Casco para bicicle', 'Talla m color negro relleno de polimero',140;
exec dbo.sp_crearProducto 'Guantes volbo', 'Guantes para manejar bicicleta o moto',56;
exec dbo.sp_crearProducto 'Balon de futbol', 'Marca golty color azu',54;
exec dbo.sp_crearProducto 'Gafas protectoras', 'Hechas con material anti rayones',35;
GO

exec dbo.sp_crearPedido 2332,'2-01-2023','Calle luna calle sol','Se encuentra pendiente'
exec dbo.sp_crearPedido 2332,'2-01-2023','Carrera montaña','Se encuentra pendiente'
exec dbo.sp_crearPedido 2332,'2-01-2023','Avenida miranda','Se encuentra pendiente'
exec dbo.sp_crearPedido 2332,'2-01-2023','Luces del prado','Se encuentra pendiente'
exec dbo.sp_crearPedido 2332,'2-01-2023','Dos pisos #55','Entregado'
GO