using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PasteleriaWeb.Models;

namespace PasteleriaWeb
{
    public partial class GridViewLinq : System.Web.UI.Page
    {
        private PasteleriaDataContext db = new PasteleriaDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Listar();
            }
        }

        public void Listar()
        {
            var lista = db.Pastel.ToList();
            gvPasteles.DataSource = lista;
            gvPasteles.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            //Validaciones
            if (btnGuardar.Text == "Guardar")
            {
                Guardar();
            }
            else
            {
                Actualizar();
            }

            Limpiar();
            Listar();
        }

        private void Limpiar()
        {
            txtIdPastel.Text = "";
            txtNombrePastel.Text = String.Empty;
            txtTamanioPastel.Text = string.Empty;

            btnGuardar.Text = "Guardar";
        }

        private void Guardar()
        {
            string nombrePastel = txtNombrePastel.Text;
            int tamanioPastel = int.Parse(txtTamanioPastel.Text);

            Pastel pastel = new Pastel();
            pastel.nombrePastel = nombrePastel;
            pastel.tamanio = tamanioPastel;
            db.Pastel.InsertOnSubmit(pastel);
            db.SubmitChanges();
        }

        private void Actualizar()
        {
            int idPastel = int.Parse( txtIdPastel.Text );
            string nombrePastel = txtNombrePastel.Text;
            int tamanioPastel = int.Parse(txtTamanioPastel.Text);

            var pastel = db.Pastel.Where(x => x.idPastel == idPastel).FirstOrDefault();
            pastel.nombrePastel = nombrePastel;
            pastel.tamanio = tamanioPastel;

            db.SubmitChanges();
        }

        protected void gvPasteles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPasteles.EditIndex = e.NewEditIndex;
            this.Listar();
        }

        protected void gvPasteles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPasteles.EditIndex = -1;
            this.Listar();
        }

        protected void gvPasteles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var row = gvPasteles.Rows[e.RowIndex];
            int idPastel = Convert.ToInt32( gvPasteles.DataKeys[e.RowIndex].Values[0] );
            var pastel = db.Pastel.FirstOrDefault(x => x.idPastel == idPastel);
            pastel.nombrePastel = (row.FindControl("txtNombrePastel") as TextBox).Text;
            pastel.estado = (row.FindControl("chkEstado") as CheckBox).Checked;
            db.SubmitChanges();

            gvPasteles.EditIndex = -1;
            Listar();
        }
    }
}