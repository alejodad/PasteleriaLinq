<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewLinq.aspx.cs" Inherits="PasteleriaWeb.GridViewLinq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TextBox runat="server" Visible="false" ID="txtIdPastel" />

            <asp:Label Text="Nombre Pastel:" runat="server" />
            <asp:TextBox runat="server" ID="txtNombrePastel" />
            <br />

            <asp:Label Text="Tamaño Pastel:" runat="server" />
            <asp:TextBox runat="server" ID="txtTamanioPastel" TextMode="Number" />
            <br />

            <asp:Button Text="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"  />

            <asp:GridView runat="server" ID="gvPasteles" AutoGenerateColumns="false" OnRowEditing="gvPasteles_RowEditing" DataKeyNames="idPastel, estado" OnRowCancelingEdit="gvPasteles_RowCancelingEdit" OnRowUpdating="gvPasteles_RowUpdating" >
                <Columns>
                    <asp:BoundField DataField="idPastel" HeaderText="Código Pastel" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Nombre Pastel">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("nombrePastel") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" ID="txtNombrePastel" Text='<%# Eval("nombrePastel") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("estado").ToString() == "True" ? "Activo" : "Inactivo" %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Eval("estado").ToString() == "True" ? true: false %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" EditText="Editar" DeleteText="Borrar" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
