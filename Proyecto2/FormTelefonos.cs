using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    public partial class FormTelefonos : Form
    {
        int id;

        public FormTelefonos(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void buttonAñadir_Click(object sender, EventArgs e)
        {
            if(textBoxnombreTelefono.Text == "")
            {
                MessageBox.Show("Falta el nombre del telefono.", "ATENCION", MessageBoxButtons.OK);
            }
            else
            {
                if(textBoxNumeroTelefono.Text == "")
                {
                    MessageBox.Show("Falta el numero de telefono.", "ATENCION", MessageBoxButtons.OK);
                }
                else
                {
                    BD.ORM_TELEFONS.InsertTELEFON(textBoxnombreTelefono.Text, textBoxNumeroTelefono.Text, id);
                    MessageBox.Show("Telefono añadido correctamente", "Correcto", MessageBoxButtons.OK);
                    this.Close();
                }
            }
        }
    }
}
