<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ejemplo.aspx.cs" Inherits="GridViewEventos.Ejemplo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ejemplo con GridView</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div class="col-md-12">

                    <div class="form-group">
                        <asp:Label Text="Nombre Pastel:" runat="server" />
                        <span class="text-danger">*</span>
                        <asp:TextBox ID="txtNombrePastel" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <asp:Label Text="Tamaño Pastel:" runat="server" />
                        <span class="text-danger">*</span>
                        <asp:TextBox ID="txtTamanio" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" CssClass="btn btn-success btn-block" />
                    </div>

                </div>
            </div>

            <div class="row">

                <div class="col-md-12">

                    <div class="table-responsive">
                        <asp:GridView ID="gvPastelCrud" runat="server" AutoGenerateColumns="false" DataKeyNames="idPastel" CssClass="table table-hover" GridLines="None" PageSize="10" AllowSorting="true" OnSorting="gvPastelCrud_Sorting"
                            OnRowDataBound="gvPastelCrud_RowDataBound" OnRowEditing="gvPastelCrud_RowEditing" OnRowCancelingEdit="gvPastelCrud_RowCancelingEdit"
                            OnRowUpdating="gvPastelCrud_RowUpdating" OnRowDeleting="gvPastelCrud_RowDeleting" ShowHeaderWhenEmpty="true" OnPageIndexChanging="gvPastelCrud_PageIndexChanging">
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="Primera" LastPageText="Ultima"/>
                            <EmptyDataTemplate>

                                <div class="jumbotron">
                                    <h1>No tenemos datos para mostrar.</h1>
                                </div>

                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="idPastel" HeaderText="Código Pastel" ReadOnly="true" SortExpression="idPastel" />
                                <asp:TemplateField HeaderText="Nombre Pastel" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombrePastel") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNombrePastel" runat="server" Text='<%# Eval("nombrePastel") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tamaño" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTamanio" runat="server" Text='<%# Eval("tamanio") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTamanio" runat="server" Text='<%# Eval("tamanio") %>' TextMode="Number"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado" ItemStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("estado").ToString() == "True" ? "Activo" : "Inactivo" %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Eval("estado").ToString() == "True" ? true : false %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150" CancelText="Cancelar" DeleteText="Borrar" EditText="Editar" UpdateText="Actualizar" ControlStyle-CssClass="btn btn-default btn-block" HeaderText="Opciones" />
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

            </div>

        </div>
    </form>

    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

    <script type="text/javascript">
        function validar() {
            var nombrePastel = document.getElementById("<%# txtNombrePastel.ClientID %>").value;
            var tamanio = document.getElementById("<%# txtTamanio.ClientID %>").value;
            if (nombrePastel == "" || tamanio == "") {
                alert("Complete los campos por favor.");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</body>
</html>
