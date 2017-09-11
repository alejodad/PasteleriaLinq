using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DAO;
using DTO;

namespace PasteleriaWeb
{
    public partial class Inicio : System.Web.UI.Page
    {

        private DAOPastel _DAOPastel;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListarPastel();
            }
            
        }

        public void InsertarPastel()
        {
            pnlAlertaError.Visible = false;
            pnlAlertaValida.Visible = false;

            DTOPastel _DTOPastel = new DTOPastel();
            _DTOPastel.Nombre = txtNombrePastel.Text;
            _DTOPastel.Tamanio = int.Parse(txtTamano.Text);

            _DAOPastel = new DAOPastel();
            bool resultado = _DAOPastel.InsertarPastel(_DTOPastel);

            if (resultado)
            {
                pnlAlertaValida.Visible = true;
                ListarPastel();
                LimpiarCampos();
            }
            else
            {
                pnlAlertaError.Visible = true;
            }
        }

        public void LimpiarCampos()
        {
            txtNombrePastel.Text = "";
            txtTamano.Text = "";

            btnGuardar.CssClass = "btn btn-success btn-block";
            btnGuardar.Text = "Guardar";
        }

        public void ListarPastel()
        {
            _DAOPastel = new DAOPastel();

            gvPasteles.DataSource = _DAOPastel.ListarPasteles();
            gvPasteles.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InsertarPastel();
        }

        protected void gvPasteles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "btnActualizar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = gvPasteles.Rows[index];

                txtNombrePastel.Text = fila.Cells[1].Text;
                txtTamano.Text = fila.Cells[2].Text;
                txtIdPastel.Text = fila.Cells[0].Text;

                btnGuardar.CssClass = "btn btn-primary btn-block";
                btnGuardar.Text = "Actualizar";
            }
            else if(e.CommandName == "btnCambiarEstado")
            {

            }
        }
    }
}