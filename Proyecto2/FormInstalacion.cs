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
                bool i = instalacionDelForm.gestioExterna;
                if (i== true)
                {
                    comboBoxGestionExterna.SelectedIndex = 0;
                }else
                {
                    comboBoxGestionExterna.SelectedIndex = 1;
                }
                buttonAñadirInstalacion.Enabled = false;
                

            }
            else
            {
                textBoxNombreEspacio.Enabled = false;
                textBoxPrecioEspacio.Enabled = false;
                comboBoxExteriorEspacio.Enabled = false;
                buttonAñadirEspacio.Enabled = false;
                buttonEliminarEspacio.Enabled = false;

                comboBoxLunesIni.Enabled = false;
                comboBoxLunesFin.Enabled = false;
                comboBoxMartesIni.Enabled = false;
                comboBoxMartesFin.Enabled = false;
                comboBoxMiercolesIni.Enabled = false;
                comboBoxMiercolesFin.Enabled = false;
                comboBoxJuevesIni.Enabled = false;
                comboBoxJuevesFin.Enabled = false;
                comboBoxViernesIni.Enabled = false;
                comboBoxViernesFin.Enabled = false;
                comboBoxSabadoFin.Enabled = false;
                comboBoxSabadoIni.Enabled = false;
                comboBoxDomingoIni.Enabled = false;
                comboBoxDomingoFin.Enabled = false;
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

        private void buttonAñadirEspacio_Click(object sender, EventArgs e)
        {
            int id = instalacionDelForm.id;
            bool exterior;

            if(comboBoxExteriorEspacio.SelectedIndex == 1){exterior = true;}
            else { exterior = false; }

            BD.ORM_ESPAIS.InsertESPAI(textBoxNombreEspacio.Text, double.Parse(textBoxPrecioEspacio.Text),exterior,id);

            MessageBox.Show("Se ha añadido el espacio satisfactoriamente", "ATENCION", MessageBoxButtons.OK);

            dataGridViewEspaciosInstalacion.DataSource = null;
            dataGridViewEspaciosInstalacion.DataSource = instalacionDelForm.ESPAIS.ToList();
        }

        private void buttonAñadirInstalacion_Click(object sender, EventArgs e)
        {
            bool externo;

            if(comboBoxGestionExterna.SelectedIndex == 0)
            {
                externo = true;
            }else { externo = false; }

            BD.ORM_INSTALACION.InsertINSTALACION(textBoxNombreInstalacion.Text,externo,textBoxDireccionInstalacion.Text);

            MessageBox.Show("Se ha añadido la instalacion satisfactoriamente. \n Recuerda agregar los horarios y los espacios de esta entidad.", "CORRECTO", MessageBoxButtons.OK);

            this.Close(); 
        }

        private void buttonEliminarEspacio_Click(object sender, EventArgs e)
        {
            ESPAIS _espais = (ESPAIS)dataGridViewEspaciosInstalacion.CurrentRow.DataBoundItem;

            BD.ORM_ESPAIS.DeleteESPAI(_espais);
            MessageBox.Show("Se ha eliminado el espacio satisfactoriamente", "ATENCION", MessageBoxButtons.OK);

            dataGridViewEspaciosInstalacion.DataSource = null;
            dataGridViewEspaciosInstalacion.DataSource = instalacionDelForm.ESPAIS.ToList();
        }

        private void buttonGuardarModificacion_Click(object sender, EventArgs e)
        {
            INSTALACIONS _instalacion = (INSTALACIONS)dataGridViewEspaciosInstalacion.CurrentRow.DataBoundItem;

            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxLunesIni.Text), TimeSpan.Parse(comboBoxLunesFin.Text), _instalacion.id,0);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxMartesIni.Text), TimeSpan.Parse(comboBoxMartesFin.Text), _instalacion.id, 1);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxMiercolesIni.Text), TimeSpan.Parse(comboBoxMiercolesFin.Text), _instalacion.id, 2);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxJuevesIni.Text), TimeSpan.Parse(comboBoxJuevesFin.Text), _instalacion.id, 3);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxViernesIni.Text), TimeSpan.Parse(comboBoxViernesFin.Text), _instalacion.id, 4);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxSabadoIni.Text), TimeSpan.Parse(comboBoxSabadoFin.Text), _instalacion.id, 5);
            BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxDomingoIni.Text), TimeSpan.Parse(comboBoxDomingoFin.Text), _instalacion.id, 6);
        }
    }
}
