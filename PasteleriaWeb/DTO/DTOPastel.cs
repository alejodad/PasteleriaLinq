using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPastel
    {
        private int idPastel;

        public int IdPastel
        {
            get { return idPastel; }
            set { idPastel = value; }
        }


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int tamanio;

        public int Tamanio
        {
            get { return tamanio; }
            set { tamanio = value; }
        }

        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

    }
}
