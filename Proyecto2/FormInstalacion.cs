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
        private INSTALACIONS instalacionModif = null;

        private String mensaje = "";

        public FormInstalacion(INSTALACIONS instalacion)
        {
            InitializeComponent();
            instalacionModif = instalacion;
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
            if (instalacionModif != null)
            {
                textBoxNombreInstalacion.Text = instalacionModif.nom;
                textBoxDireccionInstalacion.Text = instalacionModif.adreca;
                if (instalacionModif.gestioExterna == true)
                {
                    comboBoxGestionExterna.SelectedItem = "Si";
                }
                else
                {
                    comboBoxGestionExterna.SelectedItem = "No";
                }

                bindingSourceEspaciosInstalacion.DataSource = instalacionModif.ESPAIS.ToList();
                buttonAñadirInstalacion.Enabled = false;
            }
            else
            {
                buttonGuardarModificacion.Enabled = false;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewEspaciosInstalacion_SelectionChanged(object sender, EventArgs e)
        {
            ESPAIS _espai = (ESPAIS)dataGridViewEspaciosInstalacion.Rows[0].DataBoundItem;

            textBoxNombreEspacio.Text = _espai.nom;
            textBoxPrecioEspacio.Text = _espai.preu.ToString();

            if (_espai.exterior == true)
            {
                comboBoxExteriorEspacio.SelectedItem = "Si";
            }
            else
            {
                comboBoxExteriorEspacio.SelectedItem = "No";
            }
        }

        private void buttonAñadirEspacio_Click(object sender, EventArgs e)
        {

            bool exterior;

            if (!textBoxNombreEspacio.Text.Equals(""))
            {
                if (!textBoxPrecioEspacio.Text.Equals(""))
                {
                    if (comboBoxExteriorEspacio.SelectedIndex != -1)
                    {
                        if (comboBoxExteriorEspacio.SelectedItem.Equals("Si"))
                        {
                            exterior = true;
                        }
                        else
                        {
                            exterior = false;
                        }

                        BD.ORM_ESPAIS.InsertESPAI(textBoxNombreEspacio.Text, Double.Parse(textBoxPrecioEspacio.Text), exterior, instalacionModif.id);
                        refresGridEspais();
                    }
                    else
                    {
                        MessageBox.Show("Tienes que seleccionar si es exterior o no");
                    }
                }
                else
                {
                    MessageBox.Show("El precio no puede estar vacio");
                }
            }
            else
            {
                MessageBox.Show("El nombre no puede estar vacio");
            }
        }

        private void buttonEliminarEspacio_Click(object sender, EventArgs e)
        {
            ESPAIS _espai = (ESPAIS)dataGridViewEspaciosInstalacion.Rows[0].DataBoundItem;

            if (MessageBox.Show("Está seguro de que quiere borrar ese espacio?", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                BD.ORM_ESPAIS.DeleteESPAI(_espai);
                refresGridEspais();
            }
        }

        private void refresGridEspais()
        {
            bindingSourceEspaciosInstalacion.DataSource = instalacionModif.ESPAIS.ToList();

            if (!mensaje.Equals(""))
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAñadirInstalacion_Click(object sender, EventArgs e)
        {
            bool gestExtern;

            if (!textBoxNombreInstalacion.Text.Equals(""))
            {
                if (!textBoxDireccionInstalacion.Text.Equals(""))
                {
                    if (comboBoxGestionExterna.SelectedIndex != -1)
                    {
                        if (comboBoxGestionExterna.SelectedItem.Equals("Si"))
                        {
                            gestExtern = true;
                        }
                        else
                        {
                            gestExtern = false;
                        }

                        if(!checkHorarios())
                        {
                            Console.WriteLine("purrfect");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona si es de gestion externa o no");
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona una direccion de instalacion");
                }
            }
            else
            {
                MessageBox.Show("Selecciona el nombre de la instalacion");
            }
        }

        private bool checkHorarios()
        {
            bool vacio = false;
            foreach (var item in panel2.Controls.OfType<ComboBox>())
            {
                if (!vacio && item.SelectedIndex == -1)
                {
                    vacio = true;
                    MessageBox.Show("Tienes que completar todos horarios");
                    item.Focus();
                }
            }

            return vacio;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
