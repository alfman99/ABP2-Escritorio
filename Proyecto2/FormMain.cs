using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Proyecto2
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            buttonMin.Visible = false;
            buttonMax.Visible = true;
        }

        //esto es para poder mover la ventana
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        //variables a necesitar
        private int idEntidad = 0;
        private int idActDem = 0;
        String mensaje = "";

        private void radioButtonHome_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            dataGridViewTelefonos.Hide();


            buttonAñadirTel.Hide();
            buttonEliminarTel.Hide();
            buttonGuardarEntidad.Enabled = false;
        }

        private void radioButtonEntidades_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            dataGridViewTelefonos.Hide();
            buttonAñadirTel.Hide();
            buttonEliminarTel.Hide();
            buttonGuardarEntidad.Enabled = false;
        }

        private void radioButtonActividades_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

            dataGridViewTelefonos.Hide();
            buttonAñadirTel.Hide();
            buttonEliminarTel.Hide();
            buttonGuardarEntidad.Enabled = false;



            /*
            if (WindowState == FormWindowState.Maximized)
            {
                label3.Font = new Font("Microsoft Sans Serif",24);
            }
            */
        }

        private void radioButtonInstalaciones_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            dataGridViewTelefonos.Hide();
            buttonAñadirTel.Hide();
            buttonEliminarTel.Hide();
            buttonGuardarEntidad.Enabled = false;
        }

        private void radioButtonCalendario_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            dataGridViewTelefonos.Hide();
            buttonAñadirTel.Hide();
            buttonEliminarTel.Hide();
            buttonGuardarEntidad.Enabled = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } //boton cerrar

        private void buttonMax_Click(object sender, EventArgs e)
        {

            buttonMax.Visible = false;
            buttonMin.Visible = true;
            this.WindowState = FormWindowState.Maximized;
        } //Hacer grande la pantalla con sus botones

        private void buttonMin_Click(object sender, EventArgs e)
        {

            buttonMin.Visible = false;
            buttonMax.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }//Hacer pequeña la pantalla con sus botones

        private void buttonMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        } //para minimizar la pantalla

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelFecha.Text = DateTime.Now.ToString("HH:mm:ss");
            labelHora.Text = DateTime.Now.ToLongDateString();
        } //para la fecha y hora

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }//Esto es para poder arrastrar la ventana

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void buttonAñadirTel_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewListaEntidad.CurrentRow.Cells[0].Value;
            FormTelefonos t = new FormTelefonos(id);
            t.ShowDialog();
            refrescarListaTelefonos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ENTITATS _entidad = (ENTITATS)dataGridViewListaEntidad.CurrentRow.DataBoundItem;
            List<EQUIPS> equipos = _entidad.EQUIPS.ToList();
            FormEquipos eq = new FormEquipos(equipos, _entidad.id);
            eq.ShowDialog();
        }

        private void buttonEditarEntidad_Click(object sender, EventArgs e)
        {
            if (dataGridViewListaEntidad.SelectedRows.Count != 0)
            {
                //deshabilitamos los botones no necesarios y habilitamos el guardar
                buttonAñadirEntidad.Enabled = false;
                buttonEliminarEntidad.Enabled = false;
                buttonGuardarEntidad.Enabled = true;

                //editamos
                ENTITATS _entidad = (ENTITATS)dataGridViewListaEntidad.CurrentRow.DataBoundItem;

                dataGridViewTelefonos.Show();
                bindingSourceListaTelefonos.DataSource = _entidad.TELEFONS.ToList();

                buttonAñadirTel.Show();
                buttonEliminarTel.Show();

                textBoxNombreEntidad.Text = _entidad.nom;
                comboBoxTemporadaEntidad.Text = _entidad.temporada;
                textBoxDireccionEntidad.Text = _entidad.adreca;
                textBoxNifEntidad.Text = _entidad.NIF;
                textBoxCorreoEntidad.Text = _entidad.correu;
                textBoxContraseñaEntidad.Text = _entidad.password;
                idEntidad = _entidad.id;

                buttonGuardarEntidad.Focus();

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormInstalacion I = new FormInstalacion(null);
            I.ShowDialog();

        }

        private void bindingSourceListaEntidades_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            bindingSourceListaEntidades.DataSource = BD.ORM_ENTITATS.SelectAllENTITATS(ref mensaje);
            bindingSourceListaInstalaciones.DataSource = BD.ORM_INSTALACION.SelectAllINSTALACION(ref mensaje);
            bindingSourceListaActividades.DataSource = BD.ORM_ACTIVITATS.SelectAllACTIVITATS(ref mensaje);
            bindingSourceActivitatsDemandades.DataSource = BD.ORM_ACTIVITATS_DEMANADES.SelectAllACTIVITATS(ref mensaje);

            comboBoxTiposActividad.DataSource = BD.ORM_TIPUS_ACTIVITAT.SelectAllTIPUS_ACTIVITAT(ref mensaje);
            comboBoxTiposActividad.DisplayMember = "nom";

            comboBoxEspacioActividad.DataSource = BD.ORM_ESPAIS.SelectAllESPAIS(ref mensaje);
            comboBoxEspacioActividad.DisplayMember = "nom";

            comboBoxEquipoActividad.DataSource = BD.ORM_EQUIPS.SelectAllEQUIPS(ref mensaje);
            comboBoxEquipoActividad.DisplayMember = "nom";

            if (!mensaje.Equals(""))
            {
                DialogResult a = MessageBox.Show(mensaje);
                if (a == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void buttonAñadirEntidad_Click(object sender, EventArgs e)
        {
            if (textBoxNombreEntidad.Text == "")
            {
                MessageBox.Show("Falta poner el nombre a la entidad.", "ATENCION", MessageBoxButtons.OK);
            }
            else
            {

                if (comboBoxTemporadaEntidad.Text == "")
                {
                    MessageBox.Show("Falta seleccionar la temporada de la entidad.", "ATENCION", MessageBoxButtons.OK);
                }
                else
                {

                    if (textBoxDireccionEntidad.Text == "")
                    {
                        MessageBox.Show("Falta poner la direccion de la entidad.", "ATENCION", MessageBoxButtons.OK);
                    }
                    else
                    {

                        if (textBoxNifEntidad.Text == "")
                        {
                            MessageBox.Show("Falta poner el NIF de la entidad.", "ATENCION", MessageBoxButtons.OK);
                        }
                        else
                        {

                            if (textBoxCorreoEntidad.Text == "")
                            {
                                MessageBox.Show("Falta poner el correo de la entidad.", "ATENCION", MessageBoxButtons.OK);
                            }
                            else
                            {

                                if (textBoxContraseñaEntidad.Text == "")
                                {
                                    MessageBox.Show("Falta poner la contraseña de la entidad.", "ATENCION", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    mensaje = "";
                                    mensaje = BD.ORM_ENTITATS.InsertENTITAT(textBoxNombreEntidad.Text,
                                                                   comboBoxTemporadaEntidad.Text,
                                                                   textBoxDireccionEntidad.Text,
                                                                   textBoxNifEntidad.Text,
                                                                   textBoxCorreoEntidad.Text,
                                                                   textBoxContraseñaEntidad.Text);

                                    if (mensaje.Equals(""))
                                    {
                                        MessageBox.Show("Operacion realizada satisfactoriamente");
                                        textBoxNombreEntidad.Text = "";
                                        comboBoxTemporadaEntidad.Text = "";
                                        textBoxDireccionEntidad.Text = "";
                                        textBoxNifEntidad.Text = "";
                                        textBoxCorreoEntidad.Text = "";
                                        textBoxContraseñaEntidad.Text = "";
                                        refrescarListaEntidades();
                                    }
                                    else
                                    {
                                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridViewListaEntidad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ENTITATS entidad = (ENTITATS)dataGridViewListaEntidad.SelectedRows[0].DataBoundItem;
            //textBoxNombreEntidad.Text = entidad.nom;
            //comboBoxTemporadaEntidad.Text = entidad.temporada;
            //textBoxDireccionEntidad.Text = entidad.adreca;
            //textBoxNifEntidad.Text = entidad.NIF;
            //textBoxCorreoEntidad.Text = entidad.correu;
        }

        private void buttonGuardarEntidad_Click(object sender, EventArgs e)
        {
            if (dataGridViewListaEntidad.CurrentRow.Selected == true)
            {
                //primero comprobaciones



                //despues guardamos
                BD.ORM_ENTITATS.UpdateENTITATS(idEntidad, textBoxNombreEntidad.Text, comboBoxTemporadaEntidad.Text, textBoxDireccionEntidad.Text,
                                               textBoxNifEntidad.Text, textBoxCorreoEntidad.Text, textBoxContraseñaEntidad.Text);

                refrescarListaEntidades();

                //finalmente limpiamos campos
                //despues limpiamos campos
                textBoxNombreEntidad.Clear();
                comboBoxTemporadaEntidad.SelectedIndex = -1;
                textBoxDireccionEntidad.Clear();
                textBoxNifEntidad.Clear();
                textBoxCorreoEntidad.Clear();
                textBoxContraseñaEntidad.Clear();

                //hide todo lo del telefono de la entidad
                dataGridViewTelefonos.Hide();
                buttonAñadirTel.Hide();
                buttonEliminarTel.Hide();

                //mensaje de guardado correcto
                MessageBox.Show("Datos guardados correctamente.", "CORRECTO", MessageBoxButtons.OK);

                //habilitamos botones
                buttonAñadirEntidad.Enabled = true;
                buttonEliminarEntidad.Enabled = true;
                buttonGuardarEntidad.Enabled = false;

            }
            else
            {
                MessageBox.Show("Para editar tienes que seleccionar una entidad.", "ATENCION", MessageBoxButtons.OK);
            }

        }

        private void refrescarListaEntidades()
        {
            dataGridViewListaEntidad.DataSource = null;
            dataGridViewListaEntidad.DataSource = BD.ORM_ENTITATS.SelectAllENTITATS(ref mensaje);
        }

        private void refrescarListaTelefonos()
        {
            ENTITATS _entidad = (ENTITATS)dataGridViewListaEntidad.CurrentRow.DataBoundItem;

            dataGridViewTelefonos.DataSource = null;
            dataGridViewTelefonos.DataSource = _entidad.TELEFONS.ToList();
        }

        private void refrescarActivitatsDemanades()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = BD.ORM_ACTIVITATS_DEMANADES.SelectAllACTIVITATS(ref mensaje);
        }

        private void dataGridViewInstalaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            INSTALACIONS instalacionClick = (INSTALACIONS)dataGridViewInstalaciones.SelectedRows[0].DataBoundItem;

            FormInstalacion formInstalacion = new FormInstalacion(instalacionClick);

            formInstalacion.ShowDialog();
        }

        private void buttonEliminarEntidad_Click(object sender, EventArgs e)
        {
            ENTITATS _entidad = (ENTITATS)dataGridViewListaEntidad.CurrentRow.DataBoundItem;

            DialogResult m = MessageBox.Show("Estas seguro de eliminar la entidad " + _entidad.nom + " ? ", "ATENCION!", MessageBoxButtons.YesNo);
            if (m == DialogResult.Yes)
            {
                mensaje = "";
                mensaje = BD.ORM_ENTITATS.DeleteENTITATS(_entidad);

                if (mensaje.Equals(""))
                {
                    refrescarListaEntidades();
                }
                else
                {
                    DialogResult result = MessageBox.Show(mensaje + " está seguro de que quiere borrarlo? Se borraran los datos relacionados", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (result == DialogResult.OK)
                    {
                        //ELIMINAR AUN QUE TENGA DATOS RELACIONADOS
                    }
                    else
                    {
                        MessageBox.Show("Operacion cancelada");
                    }
                }
            }
        }

        private void buttonEliminarTel_Click(object sender, EventArgs e)
        {
            TELEFONS _telefon = (TELEFONS)dataGridViewTelefonos.CurrentRow.DataBoundItem;

            DialogResult m = MessageBox.Show("Estas seguro de eliminar el telefono " + _telefon.rao + " ? ", "ATENCION!", MessageBoxButtons.YesNo);
            if (m == DialogResult.Yes)
            {
                BD.ORM_TELEFONS.DeleteTELEFON(_telefon);
                refrescarListaTelefonos();
            }
        }

        private void buttonAñadir_Click(object sender, EventArgs e)
        {
            bool asignada;

            try
            {
                if (comboBoxAsignadaActividad.SelectedItem.ToString() == "Yes")
                {
                    asignada = true;
                }
                else
                {
                    asignada = false;
                }

                mensaje = BD.ORM_ACTIVITATS_DEMANADES.InsertACTIVITATS_DEMANADES(textBoxNombreActividad.Text,
                     System.TimeSpan.Parse(textBoxDuracionActividad.Text),
                     ((EQUIPS)comboBoxEquipoActividad.SelectedItem).id,
                     ((ESPAIS)comboBoxEspacioActividad.SelectedItem).id,
                     ((TIPUS_ACTIVITAT)comboBoxTiposActividad.SelectedItem).id, int.Parse(textBoxDiasActividad.Text),
                     asignada);

                if (!mensaje.Equals(""))
                {
                    MessageBox.Show(mensaje);
                }
                else
                {
                    refrescarActivitatsDemanades();
                    textBoxNombreActividad.Text = "";
                    textBoxDuracionActividad.Text = "";
                    comboBoxEquipoActividad.SelectedIndex = 0;
                    comboBoxEspacioActividad.SelectedIndex = 0;
                    comboBoxTiposActividad.SelectedIndex = 0;
                    textBoxDiasActividad.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tienes que completar los datos para poder agregar una nueva actividad");
            }

        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ACTIVITATS_DEMANADES _activitatDemanada = (ACTIVITATS_DEMANADES)dataGridView1.SelectedRows[0].DataBoundItem;

                DialogResult m = MessageBox.Show("Estas seguro? ", "ATENCION!", MessageBoxButtons.YesNo);
                if (m == DialogResult.Yes)
                {
                    mensaje = "";
                    mensaje = BD.ORM_ACTIVITATS_DEMANADES.DeleteACTIVITAT(_activitatDemanada);

                    if (mensaje.Equals(""))
                    {
                        MessageBox.Show("Se ha eliminado satisfactoriamente");
                        refrescarActivitatsDemanades();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tienes que seleccionar alguna actividad para poder eliminarla");
            }

        }

        private void textBoxDiasActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            dataGridViewInstalaciones.DataSource = null;
            dataGridViewInstalaciones.DataSource = BD.ORM_INSTALACION.SelectAllINSTALACION(ref mensaje);
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            try
            {
                ACTIVITATS_DEMANADES _activitat = (ACTIVITATS_DEMANADES)dataGridView1.SelectedRows[0].DataBoundItem;

                textBoxNombreActividad.Text = _activitat.nom;
                comboBoxTiposActividad.SelectedItem = _activitat.TIPUS_ACTIVITAT;
                comboBoxEspacioActividad.SelectedItem = _activitat.ESPAIS;
                comboBoxEquipoActividad.SelectedItem = _activitat.ACTIVITATS;
                textBoxDuracionActividad.Text = _activitat.durada.ToString();
                textBoxDiasActividad.Text = _activitat.num_dies.ToString();
                if (_activitat.assignada.Equals(true))
                {
                    comboBoxAsignadaActividad.SelectedItem = "Si";
                }
                else
                {
                    comboBoxAsignadaActividad.SelectedItem = "No";
                }

                idActDem = _activitat.id;

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            ACTIVITATS_DEMANADES _actDemanades = (ACTIVITATS_DEMANADES)dataGridView1.CurrentRow.DataBoundItem;

            try
            {
                String nombre = textBoxNombreActividad.Text;
                TimeSpan duracion = TimeSpan.Parse(textBoxDuracionActividad.Text);
                int idEquipo = ((EQUIPS)comboBoxEquipoActividad.SelectedItem).id;
                int idEspacio = ((ESPAIS)comboBoxEspacioActividad.SelectedItem).id;
                int idTipoAct = ((TIPUS_ACTIVITAT)comboBoxTiposActividad.SelectedItem).id;
                int diasAct = int.Parse(textBoxDiasActividad.Text);

                bool asignada;

                if (comboBoxAsignadaActividad.SelectedItem.Equals("Si"))
                {
                    asignada = true;
                }
                else
                {
                    asignada = false;
                }


                mensaje = "";
                mensaje = BD.ORM_ACTIVITATS_DEMANADES.UpdateACTIVITATS_DEMANADES(idActDem, nombre, duracion, idEquipo, idEspacio, idTipoAct, diasAct, asignada);

                if (!mensaje.Equals(""))
                {
                    MessageBox.Show(mensaje);
                }
                else
                {
                    refrescarActivitatsDemanades();
                    idActDem = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
