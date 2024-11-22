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
    public partial class areas : System.Web.UI.Page
    {

        SqlConnection connection = new SqlConnection
               (@"Data Source=.\SqlExpress; " +
               @"Initial Catalog = Livros;" +
               "Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack == false)
                GetAreas();
        }

        void GetAreas()
        {
            //definir query para obter os dados
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText =
            "SELECT ID, Nome FROM Area ORDER BY Nome";
            SqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            connection.Close();
            //mostrar dados no controlo GridView
            gridAreas.DataSource = table;
            gridAreas.DataBind();
        }


        protected void gridAreas_RowDataBound(object sender, GridViewRowEventArgs e)
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
               
                //tratar caracteres especiais
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[2].Text = Server.HtmlDecode(e.Row.Cells[2].Text);
                   
                }
            }
        }

        protected void gridAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            textArea.Text = gridAreas.SelectedRow.Cells[2].Text;
            
        }

        protected void buttonInsert_Click(object sender, EventArgs e)
        {
            // Verifica se os campos obrigatórios estão preenchidos
            if (string.IsNullOrWhiteSpace(textArea.Text))
            {
                // Opcional: Mostrar mensagem de erro se o nome estiver em branco
                Response.Write("<script>alert('Por favor, preencha o nome da área.');</script>");
                return;
            }

            try
            {
                // Definir query para criar registo
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT Area(Nome) VALUES(@nome)";

                // Ler valores definidos nos campos Nome 
                command.Parameters.AddWithValue("@nome", textArea.Text);
                

                // Abrir conexão e executar query
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                // Limpar campos após inserção
                textArea.Text = string.Empty;              

                // Atualizar controlo gridAutores
                GetAreas();
            }
            catch (Exception ex)
            {
                // Tratar possíveis erros de banco de dados
                Response.Write($"<script>alert('Erro ao inserir área: {ex.Message}');</script>");
            }

        }

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            //definir query para criar registo
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE Area where Id = @id";
            //obter Id referente a área selecionado
            command.Parameters.AddWithValue("@id", gridAreas.SelectedRow.Cells[1].Text);
            //executar query
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
                string mensagem = "O autor " +
                gridAreas.SelectedRow.Cells[2].Text + " não pode ser eliminado.";
                Response.Write("<script>alert('" + mensagem + "')</script>");
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                    connection.Close();
            }
            //atualizar controlo gridAreas
            GetAreas();
        }
    }
}