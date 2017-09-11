using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace GridViewEventos
{
    public partial class Ejemplo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            //  You can cache the DataTable for improving performance
            DataTable dt = Listar();

            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + direction;

            gvPastelCrud.DataSource = dv;
            gvPastelCrud.DataBind();
        }

        private DataTable Listar()
        {
            string constr = ConfigurationManager.ConnectionStrings["PasteleriaDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Pasteles_CRUD"))
                {
                    cmd.Parameters.AddWithValue("@Action", "SELECT");
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        private void BindGrid()
        {
            gvPastelCrud.DataSource = this.Listar();
            gvPastelCrud.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombrePastel = txtNombrePastel.Text;
            string tamanio = txtTamanio.Text;
            string constr = ConfigurationManager.ConnectionStrings["PasteleriaDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Pasteles_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "INSERT");
                    cmd.Parameters.AddWithValue("@nombrePastel", nombrePastel);
                    cmd.Parameters.AddWithValue("@tamanio", tamanio);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void gvPastelCrud_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvPastelCrud.EditIndex)
            {
                (e.Row.Cells[4].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('¿Desea eliminar este registro?');";
            }
        }

        protected void gvPastelCrud_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPastelCrud.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void gvPastelCrud_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPastelCrud.EditIndex = -1;
            this.BindGrid();
        }

        protected void gvPastelCrud_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvPastelCrud.Rows[e.RowIndex];
            int idPastel = Convert.ToInt32(gvPastelCrud.DataKeys[e.RowIndex].Values[0]);
            string nombrePastel = (row.FindControl("txtNombrePastel") as TextBox).Text;
            string tamanio = (row.FindControl("txtTamanio") as TextBox).Text;
            bool estado = (row.FindControl("chkEstado") as CheckBox).Checked;
            string constr = ConfigurationManager.ConnectionStrings["PasteleriaDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Pasteles_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@idPastel", idPastel);
                    cmd.Parameters.AddWithValue("@nombrePastel", nombrePastel);
                    cmd.Parameters.AddWithValue("@tamanio", tamanio);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            gvPastelCrud.EditIndex = -1;
            this.BindGrid();
        }

        protected void gvPastelCrud_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idPastel = Convert.ToInt32(gvPastelCrud.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["PasteleriaDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Pasteles_CRUD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@idPastel", idPastel);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void gvPastelCrud_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPastelCrud.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvPastelCrud_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }

    }
}