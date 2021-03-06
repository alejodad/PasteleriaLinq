USE [master]
GO
/****** Object:  Database [PasteleriaDB]    Script Date: 24/08/2017 20:58:18 ******/
CREATE DATABASE [PasteleriaDB]
GO
USE [PasteleriaDB]
GO
/****** Object:  Table [dbo].[Ingrediente]    Script Date: 24/08/2017 20:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ingrediente](
	[idIngrediente] [int] IDENTITY(1,1) NOT NULL,
	[nombreIngrediente] [varchar](50) NOT NULL,
	[cantidadExistente] [int] NOT NULL,
	[precioIngrediente] [decimal](8, 2) NOT NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_Ingrediente] PRIMARY KEY CLUSTERED 
(
	[idIngrediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IngredientePastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientePastel](
	[idIngredientePastel] [int] IDENTITY(1,1) NOT NULL,
	[idPastel] [int] NOT NULL,
	[idIngrediente] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precioIngrediente] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_IngredientePastel] PRIMARY KEY CLUSTERED 
(
	[idIngredientePastel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pastel](
	[idPastel] [int] IDENTITY(1,1) NOT NULL,
	[nombrePastel] [varchar](50) NOT NULL,
	[tamanio] [int] NOT NULL,
	[estado] [bit] NULL CONSTRAINT [DF_Pastel_estado]  DEFAULT ((1)),
 CONSTRAINT [PK_Pastel] PRIMARY KEY CLUSTERED 
(
	[idPastel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Ingrediente] ADD  CONSTRAINT [DF_Ingrediente_estado]  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[IngredientePastel]  WITH CHECK ADD  CONSTRAINT [FK_IngredientePastel_Ingrediente] FOREIGN KEY([idIngrediente])
REFERENCES [dbo].[Ingrediente] ([idIngrediente])
GO
ALTER TABLE [dbo].[IngredientePastel] CHECK CONSTRAINT [FK_IngredientePastel_Ingrediente]
GO
ALTER TABLE [dbo].[IngredientePastel]  WITH CHECK ADD  CONSTRAINT [FK_IngredientePastel_Pastel] FOREIGN KEY([idPastel])
REFERENCES [dbo].[Pastel] ([idPastel])
GO
ALTER TABLE [dbo].[IngredientePastel] CHECK CONSTRAINT [FK_IngredientePastel_Pastel]
GO
/****** Object:  StoredProcedure [dbo].[SP_Actualizar_Pastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Actualizar_Pastel]
@idPastel int,
@nombrePastel varchar(50),
@tamanio int
AS
BEGIN 

	UPDATE Pastel SET nombrePastel = @nombrePastel, tamanio = @tamanio WHERE idPastel = @idPastel

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CambiarEstado_Pastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CambiarEstado_Pastel]
@idPastel int,
@estado bit
AS
BEGIN

	UPDATE Pastel SET estado = @estado WHERE idPastel = @idPastel;

END
GO
/****** Object:  StoredProcedure [dbo].[SP_Insert_Pastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yhoan Galeano
-- Create date: 15/08/2017
-- Description: Realizara el insert en la tabla pastel
-- =============================================
CREATE PROCEDURE [dbo].[SP_Insert_Pastel] 
	@nombrePastel varchar(50), 
	@tamanio int
AS
BEGIN

	INSERT INTO Pastel (nombrePastel, tamanio) VALUES (@nombrePastel, @tamanio);

END

GO
/****** Object:  StoredProcedure [dbo].[SP_Listar_Pastel]    Script Date: 24/08/2017 20:58:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Listar_Pastel]
AS
BEGIN
	SELECT idPastel, nombrePastel, tamanio, estado 
	FROM Pastel
END
GO
USE [master]
GO
ALTER DATABASE [PasteleriaDB] SET  READ_WRITE 
GO
