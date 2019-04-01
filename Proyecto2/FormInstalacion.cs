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

        private int idInstalacion;

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

                        if (!checkHorarios() && !checkHorariosCorrecto())
                        {
                            String error = BD.ORM_INSTALACION.InsertINSTALACION(textBoxNombreInstalacion.Text, gestExtern, textBoxDireccionInstalacion.Text);

                            inputHorarios();

                            if (!error.Equals(""))
                            {
                                MessageBox.Show(error);
                            }
                            else
                            {
                                MessageBox.Show("Instalacion creada satisfactoriamente");
                            }

                            this.Close();
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

        private bool checkHorariosCorrecto()
        {
            bool mal = false;
            if (comboBoxLunesIni.SelectedIndex > comboBoxLunesFin.SelectedIndex || (comboBoxLunesIni.SelectedIndex == 0 && comboBoxLunesFin.SelectedIndex != 0))
            {
                mal = true;
                MessageBox.Show("El horario final del lunes ha de ser mas tarde que el de inicio");
            }else
            {
                if(comboBoxMartesIni.SelectedIndex > comboBoxMartesFin.SelectedIndex || (comboBoxMartesIni.SelectedIndex == 0 && comboBoxMartesFin.SelectedIndex != 0))
                {
                    mal = true;
                    MessageBox.Show("El horario final del martes ha de ser mas tarde que el de inicio");
                }else
                {
                    if (comboBoxMiercolesIni.SelectedIndex > comboBoxMiercolesFin.SelectedIndex || (comboBoxMiercolesIni.SelectedIndex == 0 && comboBoxMiercolesFin.SelectedIndex != 0))
                    {
                        mal = true;
                        MessageBox.Show("El horario final del miercoles ha de ser mas tarde que el de inicio");
                    }else
                    {
                        if(comboBoxJuevesIni.SelectedIndex > comboBoxJuevesFin.SelectedIndex || (comboBoxJuevesIni.SelectedIndex == 0 && comboBoxJuevesFin.SelectedIndex != 0))
                        {
                            mal = true;
                            MessageBox.Show("El horario final del jueves ha de ser mas tarde que el de inicio");
                        }
                        else
                        {
                            if(comboBoxViernesIni.SelectedIndex > comboBoxViernesFin.SelectedIndex || (comboBoxViernesIni.SelectedIndex == 0 && comboBoxViernesFin.SelectedIndex != 0))
                            {
                                mal = true;
                                MessageBox.Show("El horario final del viernes ha de ser mas tarde que el de inicio");
                            }else
                            {
                                if(comboBoxSabadoIni.SelectedIndex > comboBoxSabadoFin.SelectedIndex || (comboBoxSabadoIni.SelectedIndex == 0 && comboBoxSabadoFin.SelectedIndex != 0))
                                {
                                    mal = true;
                                    MessageBox.Show("El horario final del sabado ha de ser mas tarde que el de inicio");
                                }else
                                {
                                    if(comboBoxDomingoIni.SelectedIndex > comboBoxDomingoFin.SelectedIndex || (comboBoxDomingoIni.SelectedIndex == 0 && comboBoxDomingoFin.SelectedIndex != 0))
                                    {
                                        mal = true;
                                        MessageBox.Show("El horario final del domingo ha de ser mas tarde que el de inicio");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return mal;
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

        private void inputHorarios()
        {
            List<INSTALACIONS> instalaciones = BD.ORM_INSTALACION.SelectAllINSTALACION(ref mensaje);

            foreach (var item in instalaciones)
            {
                if (item.nom == textBoxNombreInstalacion.Text)
                {
                    idInstalacion = item.id;
                }
                else
                {
                    idInstalacion = -1;
                }
            }

            List<DIES_SETMANA> a = BD.ORM_DIES_SETMANA.SelectAllDIES_SETMANA(ref mensaje);

            if (idInstalacion == -1)
            {
                MessageBox.Show("Error!");
            }
            else
            {
                foreach (var item in a)
                {
                    switch (item.nom.ToLower())
                    {
                        case "lunes":
                            if (!comboBoxLunesIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxLunesIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxLunesFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "martes":
                            if (!comboBoxMartesIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxMartesIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxMartesFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "miercoles":
                            if (!comboBoxMiercolesIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxMiercolesIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxMiercolesFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "jueves":
                            if (!comboBoxJuevesIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxJuevesIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxMiercolesFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "viernes":
                            if (!comboBoxViernesIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxViernesIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxViernesFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "sabado":
                            if (!comboBoxSabadoIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxSabadoIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxSabadoFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                        case "domingo":
                            if (!comboBoxDomingoIni.SelectedItem.ToString().Equals("CERRADO"))
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse(comboBoxDomingoIni.SelectedItem.ToString()), TimeSpan.Parse(comboBoxDomingoFin.SelectedItem.ToString()), idInstalacion, item.id);
                            }
                            else
                            {
                                BD.ORM_HORARIS_INSTALACIONS.InsertHORARIS_INSTALACIONS(TimeSpan.Parse("00:00"), TimeSpan.Parse("00:00"), idInstalacion, item.id);
                            }
                            break;
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
