<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="autores.aspx.cs" Inherits="aspnet_Livros_ADO.autores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Gestão de Autores</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-3.7.1.min.js"></script>
</head>
<body class="ms-5">
    <form id="form1" runat="server">
        <div class="container-fluid mt-4">
            <div class="row">
                <div class="col-4">
                    <asp:GridView ID="gridAutores" runat="server" OnRowDataBound="gridAutores_RowDataBound"
                        AutoGenerateSelectButton="True"
                        CssClass="table-borderless table-hover"
                        Width="100%" OnSelectedIndexChanged="gridAutores_SelectedIndexChanged">
                    </asp:GridView>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-12">
                    <div style="margin-bottom: 10px;">
                        <asp:TextBox ID="textNome" runat="server" 
                            Width="350px" 
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>

                    <div>
                        <asp:TextBox ID="textBiografia" runat="server" 
                            Width="600px" 
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="3" ></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-12">
                    <asp:Button ID="buttonInsert" runat="server" 
                        Text="Inserir autor" 
                        CssClass="btn btn-outline-dark" OnClick="buttonInsert_Click" />
                        
                    <asp:Button ID="buttonUpdate" runat="server" 
                        Text="Atualizar autor" 
                        CssClass="btn btn-outline-dark"
                        style="margin-left: 20px;" OnClick="buttonUpdate_Click" />
                        
                    <asp:Button ID="buttonDelete" runat="server" 
                        Text="Eliminar autor"
                        CssClass="btn btn-outline-dark"
                        style="margin-left: 20px;" OnClick="buttonDelete_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>