<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="areas.aspx.cs" Inherits="aspnet_Livros_ADO.areas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="Content/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.min.js"></script>
<script src="Scripts/jquery-3.7.1.min.js"></script>

<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-4">
            <div class="row">
                <div class="col-4">
                    <asp:GridView ID="gridAreas" runat="server" OnRowDataBound="gridAreas_RowDataBound"
                        AutoGenerateSelectButton="True"
                        CssClass="table table-borderless table-hover"
                        Width="100%" OnSelectedIndexChanged="gridAreas_SelectedIndexChanged">
                    </asp:GridView>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-12">
                    <div style="margin-bottom: 10px;">
                        <asp:TextBox ID="textArea" runat="server"
                            Width="350px"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-12">
                    <asp:Button ID="buttonInsert" runat="server"
                        Text="Inserir Área"
                        CssClass="btn btn-outline-dark" OnClick="buttonInsert_Click" />

                    <asp:Button ID="buttonUpdate" runat="server"
                        Text="Atualizar Área"
                        CssClass="btn btn-outline-dark"
                        Style="margin-left: 20px;" OnClick="buttonUpdate_Click" />

                    <asp:Button ID="buttonDelete" runat="server"
                        Text="Eliminar Área"
                        CssClass="btn btn-outline-dark"
                        Style="margin-left: 20px;" OnClick="buttonDelete_Click" />
                </div>
            </div>

        </div>
    </form>
</body>
</html>
