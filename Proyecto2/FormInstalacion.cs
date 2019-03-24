using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2
{
    public partial class FormInstalacion : Form
    {
        private INSTALACIONS instalacionDelForm;

        public FormInstalacion(INSTALACIONS instalacion)
        {
            InitializeComponent();
            instalacionDelForm = instalacion;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInstalacion_Load(object sender, EventArgs e)
        {
            if (!(instalacionDelForm == null))
            {
                textBoxNombreInstalacion.Text = instalacionDelForm.nom;
                textBoxDireccionInstalacion.Text = instalacionDelForm.adreca;
                bindingSourceEspaciosInstalacion.DataSource = instalacionDelForm.ESPAIS;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
