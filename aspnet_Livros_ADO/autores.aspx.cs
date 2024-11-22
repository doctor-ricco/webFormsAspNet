using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspnet_Livros_ADO
{
    public partial class autores : System.Web.UI.Page
    {
        SqlConnection connection = new SqlConnection
                (@"Data Source=.\SqlExpress; " +
                @"Initial Catalog = Livros;" +
                "Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
                GetAutores();

        }

        void GetAutores()
        {
            //definir query para obter os dados
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText =
            "SELECT Id, Nome, Biografia FROM Autor ORDER BY Nome";
            SqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            //mostrar dados no controlo GridView
            gridAutores.DataSource = table;
            gridAutores.DataBind();
        }

        protected void gridAutores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > 1)
            {
                //ocultar cabeçalho da coluna Nome
                if (e.Row.RowType == DataControlRowType.Header)
                    e.Row.Cells[2].Visible = false;
                e.Row.Cells[0].Width = 80;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                //ocultar coluna referente ao Id (chave primária)
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Width = 300;
                //ocultar coluna referente à Biografia
                e.Row.Cells[3].Visible = false;
                //tratar caracteres especiais
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[2].Text = Server.HtmlDecode(e.Row.Cells[2].Text);
                    e.Row.Cells[3].Text = Server.HtmlDecode(e.Row.Cells[3].Text);
                }
            }
        }

        protected void buttonInsert_Click(object sender, EventArgs e)
        {
            // Verifica se os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(textNome.Text))
            {
                // Opcional: Mostrar mensagem de erro se o nome estiver em branco
                Response.Write("<script>alert('Por favor, preencha o nome do autor.');</script>");
                return;
            }

            try
            {
                // Definir query para criar registo
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT Autor(Nome, Biografia) VALUES(@nome, @biografia)";

                // Ler valores definidos nos campos Nome e Biografia
                command.Parameters.AddWithValue("@nome", textNome.Text);

                // Verificar se a biografia está vazia para evitar null
                command.Parameters.AddWithValue("@biografia",
                    string.IsNullOrWhiteSpace(textBiografia.Text) ? (object)DBNull.Value : textBiografia.Text);

                // Abrir conexão e executar query
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                // Limpar campos após inserção
                textNome.Text = string.Empty;
                textBiografia.Text = string.Empty;

                // Atualizar controlo gridAutores
                GetAutores();
            }
            catch (Exception ex)
            {
                // Tratar possíveis erros de banco de dados
                Response.Write($"<script>alert('Erro ao inserir autor: {ex.Message}');</script>");
            }
        }

        protected void buttonEliminar_Click(object sender, EventArgs e)
        {
            //definir query para criar registo
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE Autor where Id = @id";
            //obter Id referente ao autor selecionado
            command.Parameters.AddWithValue("@id", gridAutores.SelectedRow.Cells[1].Text);
            //executar query
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                string mensagem = "O autor " +
                gridAutores.SelectedRow.Cells[2].Text + " não pode ser eliminado.";
                Response.Write("<script>alert('" + mensagem + "')</script>");
            }
            //atualizar controlo gridAutores
            GetAutores();
            Limpar();
        }

        protected void gridAutores_SelectedIndexChanged(object sender, EventArgs e)
        {
            textNome.Text = gridAutores.SelectedRow.Cells[2].Text;
            textBiografia.Text = gridAutores.SelectedRow.Cells[3].Text;
        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            //definir query para criar registo
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE Autor where Id = @id";
            //obter Id referente ao autor selecionado
            command.Parameters.AddWithValue("@id", gridAutores.SelectedRow.Cells[1].Text);
            //executar query
            try
            {
                connection.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {
                string mensagem = "O autor " +
                gridAutores.SelectedRow.Cells[2].Text + " não pode ser eliminado.";
                Response.Write("<script>alert('" + mensagem + "')</script>");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            //atualizar controlo gridAutores
            GetAutores();
            Limpar();
        }

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            //definir query para criar registo
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Autor SET Nome = @nome, Biografia = @biografia "
            + "WHERE Id = @id";
            //ler valores definidos nos campos Nome e Biografia
            command.Parameters.AddWithValue("@nome", textNome.Text);
            command.Parameters.AddWithValue("@biografia", textBiografia.Text);
            //obter Id referente ao autor selecionado
            command.Parameters.AddWithValue("@id", gridAutores.SelectedRow.Cells[1].Text);
            //executar query
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            //atualizar controlo gridAutores
            GetAutores();
            Limpar();
        }

        void Limpar()
        {
            foreach (Control item in form1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox t = (TextBox)item;
                    t.Text = "";
                }
                if (item is DropDownList)
                {
                    DropDownList d = (DropDownList)item;
                    d.SelectedIndex = -1;
                }
                if (item is CheckBox)
                {
                    CheckBox c = (CheckBox)item;
                    c.Checked = false;
                }
                if (item is GridView)
                {
                    GridView c = (GridView)item;
                    c.SelectedIndex = -1;   
                }
            }

        }

    }
}