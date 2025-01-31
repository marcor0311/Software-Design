using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFoodCommerce.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class register_admin_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_TiqueteDescuento_CodigoDescuento",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProducto_Producto_CodigoProducto",
                table: "PrecioProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProducto_TipoPrecio_CodigoTipoPrecio",
                table: "PrecioProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductoPedido_Pedido_CodigoPedido",
                table: "PrecioProductoPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductoPedido_PrecioProducto_CodigoPrecioProducto",
                table: "PrecioProductoPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_LineaComida_CodigoLineaComida",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaProcesadorPago_ProcesadorPago_CodigoProcesadorPago",
                table: "TarjetaProcesadorPago");

            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaProcesadorPago_Tarjeta_CodigoTarjeta",
                table: "TarjetaProcesadorPago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiqueteDescuento",
                table: "TiqueteDescuento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPrecio",
                table: "TipoPrecio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarjetaProcesadorPago",
                table: "TarjetaProcesadorPago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarjeta",
                table: "Tarjeta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcesadorPago",
                table: "ProcesadorPago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecioProductoPedido",
                table: "PrecioProductoPedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecioProducto",
                table: "PrecioProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineaComida",
                table: "LineaComida");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Error",
                table: "Error");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bitacora",
                table: "Bitacora");

            migrationBuilder.RenameTable(
                name: "TiqueteDescuento",
                newName: "TiqueteDescuentos");

            migrationBuilder.RenameTable(
                name: "TipoPrecio",
                newName: "TipoPrecios");

            migrationBuilder.RenameTable(
                name: "TarjetaProcesadorPago",
                newName: "TarjetaProcesadorPagos");

            migrationBuilder.RenameTable(
                name: "Tarjeta",
                newName: "Tarjetas");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Productos");

            migrationBuilder.RenameTable(
                name: "ProcesadorPago",
                newName: "ProcesadorPagos");

            migrationBuilder.RenameTable(
                name: "PrecioProductoPedido",
                newName: "PrecioProductoPedidos");

            migrationBuilder.RenameTable(
                name: "PrecioProducto",
                newName: "PrecioProductos");

            migrationBuilder.RenameTable(
                name: "Pedido",
                newName: "Pedidos");

            migrationBuilder.RenameTable(
                name: "LineaComida",
                newName: "LineaComidas");

            migrationBuilder.RenameTable(
                name: "Error",
                newName: "Errors");

            migrationBuilder.RenameTable(
                name: "Bitacora",
                newName: "Bitacoras");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaProcesadorPago_CodigoTarjeta",
                table: "TarjetaProcesadorPagos",
                newName: "IX_TarjetaProcesadorPagos_CodigoTarjeta");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaProcesadorPago_CodigoProcesadorPago",
                table: "TarjetaProcesadorPagos",
                newName: "IX_TarjetaProcesadorPagos_CodigoProcesadorPago");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_CodigoLineaComida",
                table: "Productos",
                newName: "IX_Productos_CodigoLineaComida");

            migrationBuilder.RenameIndex(
                name: "IX_ProcesadorPago_Tipo",
                table: "ProcesadorPagos",
                newName: "IX_ProcesadorPagos_Tipo");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedido_CodigoPrecioProducto",
                table: "PrecioProductoPedidos",
                newName: "IX_PrecioProductoPedidos_CodigoPrecioProducto");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedido_CodigoPedido",
                table: "PrecioProductoPedidos",
                newName: "IX_PrecioProductoPedidos_CodigoPedido");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedido_Codigo",
                table: "PrecioProductoPedidos",
                newName: "IX_PrecioProductoPedidos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProducto_CodigoTipoPrecio",
                table: "PrecioProductos",
                newName: "IX_PrecioProductos_CodigoTipoPrecio");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProducto_CodigoProducto",
                table: "PrecioProductos",
                newName: "IX_PrecioProductos_CodigoProducto");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProducto_Codigo",
                table: "PrecioProductos",
                newName: "IX_PrecioProductos_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_CodigoDescuento",
                table: "Pedidos",
                newName: "IX_Pedidos_CodigoDescuento");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_Codigo",
                table: "Pedidos",
                newName: "IX_Pedidos_Codigo");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pregunta_Seguridad",
                table: "AspNetUsers",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Respuesta_Seguridad",
                table: "AspNetUsers",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiqueteDescuentos",
                table: "TiqueteDescuentos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPrecios",
                table: "TipoPrecios",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarjetaProcesadorPagos",
                table: "TarjetaProcesadorPagos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarjetas",
                table: "Tarjetas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcesadorPagos",
                table: "ProcesadorPagos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecioProductoPedidos",
                table: "PrecioProductoPedidos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecioProductos",
                table: "PrecioProductos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineaComidas",
                table: "LineaComidas",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Errors",
                table: "Errors",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bitacoras",
                table: "Bitacoras",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_TiqueteDescuentos_CodigoDescuento",
                table: "Pedidos",
                column: "CodigoDescuento",
                principalTable: "TiqueteDescuentos",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductoPedidos_Pedidos_CodigoPedido",
                table: "PrecioProductoPedidos",
                column: "CodigoPedido",
                principalTable: "Pedidos",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductoPedidos_PrecioProductos_CodigoPrecioProducto",
                table: "PrecioProductoPedidos",
                column: "CodigoPrecioProducto",
                principalTable: "PrecioProductos",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductos_Productos_CodigoProducto",
                table: "PrecioProductos",
                column: "CodigoProducto",
                principalTable: "Productos",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductos_TipoPrecios_CodigoTipoPrecio",
                table: "PrecioProductos",
                column: "CodigoTipoPrecio",
                principalTable: "TipoPrecios",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_LineaComidas_CodigoLineaComida",
                table: "Productos",
                column: "CodigoLineaComida",
                principalTable: "LineaComidas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaProcesadorPagos_ProcesadorPagos_CodigoProcesadorPago",
                table: "TarjetaProcesadorPagos",
                column: "CodigoProcesadorPago",
                principalTable: "ProcesadorPagos",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaProcesadorPagos_Tarjetas_CodigoTarjeta",
                table: "TarjetaProcesadorPagos",
                column: "CodigoTarjeta",
                principalTable: "Tarjetas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_TiqueteDescuentos_CodigoDescuento",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductoPedidos_Pedidos_CodigoPedido",
                table: "PrecioProductoPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductoPedidos_PrecioProductos_CodigoPrecioProducto",
                table: "PrecioProductoPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductos_Productos_CodigoProducto",
                table: "PrecioProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecioProductos_TipoPrecios_CodigoTipoPrecio",
                table: "PrecioProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_LineaComidas_CodigoLineaComida",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaProcesadorPagos_ProcesadorPagos_CodigoProcesadorPago",
                table: "TarjetaProcesadorPagos");

            migrationBuilder.DropForeignKey(
                name: "FK_TarjetaProcesadorPagos_Tarjetas_CodigoTarjeta",
                table: "TarjetaProcesadorPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiqueteDescuentos",
                table: "TiqueteDescuentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPrecios",
                table: "TipoPrecios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarjetas",
                table: "Tarjetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TarjetaProcesadorPagos",
                table: "TarjetaProcesadorPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcesadorPagos",
                table: "ProcesadorPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecioProductos",
                table: "PrecioProductos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecioProductoPedidos",
                table: "PrecioProductoPedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LineaComidas",
                table: "LineaComidas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Errors",
                table: "Errors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bitacoras",
                table: "Bitacoras");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pregunta_Seguridad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Respuesta_Seguridad",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "TiqueteDescuentos",
                newName: "TiqueteDescuento");

            migrationBuilder.RenameTable(
                name: "TipoPrecios",
                newName: "TipoPrecio");

            migrationBuilder.RenameTable(
                name: "Tarjetas",
                newName: "Tarjeta");

            migrationBuilder.RenameTable(
                name: "TarjetaProcesadorPagos",
                newName: "TarjetaProcesadorPago");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "ProcesadorPagos",
                newName: "ProcesadorPago");

            migrationBuilder.RenameTable(
                name: "PrecioProductos",
                newName: "PrecioProducto");

            migrationBuilder.RenameTable(
                name: "PrecioProductoPedidos",
                newName: "PrecioProductoPedido");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido");

            migrationBuilder.RenameTable(
                name: "LineaComidas",
                newName: "LineaComida");

            migrationBuilder.RenameTable(
                name: "Errors",
                newName: "Error");

            migrationBuilder.RenameTable(
                name: "Bitacoras",
                newName: "Bitacora");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaProcesadorPagos_CodigoTarjeta",
                table: "TarjetaProcesadorPago",
                newName: "IX_TarjetaProcesadorPago_CodigoTarjeta");

            migrationBuilder.RenameIndex(
                name: "IX_TarjetaProcesadorPagos_CodigoProcesadorPago",
                table: "TarjetaProcesadorPago",
                newName: "IX_TarjetaProcesadorPago_CodigoProcesadorPago");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_CodigoLineaComida",
                table: "Producto",
                newName: "IX_Producto_CodigoLineaComida");

            migrationBuilder.RenameIndex(
                name: "IX_ProcesadorPagos_Tipo",
                table: "ProcesadorPago",
                newName: "IX_ProcesadorPago_Tipo");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductos_CodigoTipoPrecio",
                table: "PrecioProducto",
                newName: "IX_PrecioProducto_CodigoTipoPrecio");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductos_CodigoProducto",
                table: "PrecioProducto",
                newName: "IX_PrecioProducto_CodigoProducto");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductos_Codigo",
                table: "PrecioProducto",
                newName: "IX_PrecioProducto_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedidos_CodigoPrecioProducto",
                table: "PrecioProductoPedido",
                newName: "IX_PrecioProductoPedido_CodigoPrecioProducto");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedidos_CodigoPedido",
                table: "PrecioProductoPedido",
                newName: "IX_PrecioProductoPedido_CodigoPedido");

            migrationBuilder.RenameIndex(
                name: "IX_PrecioProductoPedidos_Codigo",
                table: "PrecioProductoPedido",
                newName: "IX_PrecioProductoPedido_Codigo");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_CodigoDescuento",
                table: "Pedido",
                newName: "IX_Pedido_CodigoDescuento");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_Codigo",
                table: "Pedido",
                newName: "IX_Pedido_Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiqueteDescuento",
                table: "TiqueteDescuento",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPrecio",
                table: "TipoPrecio",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarjeta",
                table: "Tarjeta",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TarjetaProcesadorPago",
                table: "TarjetaProcesadorPago",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcesadorPago",
                table: "ProcesadorPago",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecioProducto",
                table: "PrecioProducto",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecioProductoPedido",
                table: "PrecioProductoPedido",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                table: "Pedido",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineaComida",
                table: "LineaComida",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Error",
                table: "Error",
                column: "Codigo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bitacora",
                table: "Bitacora",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_TiqueteDescuento_CodigoDescuento",
                table: "Pedido",
                column: "CodigoDescuento",
                principalTable: "TiqueteDescuento",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProducto_Producto_CodigoProducto",
                table: "PrecioProducto",
                column: "CodigoProducto",
                principalTable: "Producto",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProducto_TipoPrecio_CodigoTipoPrecio",
                table: "PrecioProducto",
                column: "CodigoTipoPrecio",
                principalTable: "TipoPrecio",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductoPedido_Pedido_CodigoPedido",
                table: "PrecioProductoPedido",
                column: "CodigoPedido",
                principalTable: "Pedido",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecioProductoPedido_PrecioProducto_CodigoPrecioProducto",
                table: "PrecioProductoPedido",
                column: "CodigoPrecioProducto",
                principalTable: "PrecioProducto",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_LineaComida_CodigoLineaComida",
                table: "Producto",
                column: "CodigoLineaComida",
                principalTable: "LineaComida",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaProcesadorPago_ProcesadorPago_CodigoProcesadorPago",
                table: "TarjetaProcesadorPago",
                column: "CodigoProcesadorPago",
                principalTable: "ProcesadorPago",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarjetaProcesadorPago_Tarjeta_CodigoTarjeta",
                table: "TarjetaProcesadorPago",
                column: "CodigoTarjeta",
                principalTable: "Tarjeta",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
