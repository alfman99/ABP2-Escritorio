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
    public partial class FormEquipos : Form
    {
        private List<EQUIPS> equiposForm;
        private List<ESPORTS> esportsList;
        private List<COMPETICIO> competicioList;
        private List<CATEGORIES_EDAT> categEdatList;
        private List<CATEGORIES> categList;
        private List<SEXES> sexesList;
        private int idEntSelec;

        private String mensaje = "";

        public FormEquipos(List<EQUIPS> equiposForm, int id)
        {
            this.equiposForm = equiposForm;
            idEntSelec = id;
            InitializeComponent();
        }

        public FormEquipos()
        {
            InitializeComponent();
        }

        private void FormEquipos_Load(object sender, EventArgs e)
        {
            mensaje = "";
            esportsList = BD.ORM_ESPORTS.SelectAllESPORTS(ref mensaje);
            competicioList = BD.ORM_COMPETICIO.SelectAllCOMPETICIO(ref mensaje);
            categEdatList = BD.ORM_CATEGORIES_EDAT.SelectAllCATEGORIES_EDAT(ref mensaje);
            categList = BD.ORM_CATEGORIES.SelectAllCATEGORIAS(ref mensaje);
            sexesList = BD.ORM_SEXES.SelectAllSEXES(ref mensaje);

            if (mensaje.Equals(""))
            {
                textBoxNombreEquipo.Text = "";

                comboBoxEquipos.DataSource = equiposForm;
                comboBoxEquipos.DisplayMember = "nom";

                comboBoxDeporteEquipo.DataSource = esportsList.ToList();
                comboBoxDeporteEquipo.DisplayMember = "nom";
                comboBoxDeporteEquipo.SelectedIndex = -1;

                comboBoxCompeticionEquipo.DataSource = competicioList.ToList();
                comboBoxCompeticionEquipo.DisplayMember = "nom";
                comboBoxCompeticionEquipo.SelectedIndex = -1;

                comboBoxCategoriaPorEdad.DataSource = categEdatList.ToList();
                comboBoxCategoriaPorEdad.DisplayMember = "nom";
                comboBoxCategoriaPorEdad.SelectedIndex = -1;

                comboBoxCategoria.DataSource = categList.ToList();
                comboBoxCategoria.DisplayMember = "nom";
                comboBoxCategoria.SelectedIndex = -1;

                comboBoxSexoEquipo.DataSource = sexesList.ToList();
                comboBoxSexoEquipo.DisplayMember = "nom";
                comboBoxSexoEquipo.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(mensaje);
            }
        }

        private void comboBoxEquipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EQUIPS equipoSeleccionado = (EQUIPS) comboBoxEquipos.SelectedItem;

            textBoxNombreEquipo.Text = "";
            textBoxNombreEquipo.Text = equipoSeleccionado.nom;
            comboBoxDeporteEquipo.DataSource = esportsList;
            comboBoxDeporteEquipo.DisplayMember = "nom";

            comboBoxDeporteEquipo.SelectedItem = equipoSeleccionado.ESPORTS;

            comboBoxCompeticionEquipo.SelectedItem = equipoSeleccionado.COMPETICIO;

            comboBoxCategoriaPorEdad.SelectedItem = equipoSeleccionado.CATEGORIES_EDAT;

            comboBoxCategoria.SelectedItem = equipoSeleccionado.CATEGORIES;

            comboBoxSexoEquipo.SelectedItem = equipoSeleccionado.SEXES;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EQUIPS equipoSeleccionado = (EQUIPS)comboBoxEquipos.SelectedItem;

            mensaje = BD.ORM_EQUIPS.DeleteEQUIP(equipoSeleccionado);

            DialogResult dialogResult = MessageBox.Show("Está seguro que quiere borrar " + equipoSeleccionado.nom, "Atención!" ,MessageBoxButtons.OKCancel);
            
            if(dialogResult == DialogResult.OK)
            {
                if (mensaje.Equals(""))
                {
                    List<ENTITATS> entitatTemp = BD.ORM_ENTITATS.SelectEntitatByID(idEntSelec, ref mensaje);

                    MessageBox.Show("La operacion se ha realizado satisfactoriamente");
                    comboBoxEquipos.DataSource = entitatTemp[0].EQUIPS.ToList();
                }
                else
                {
                    MessageBox.Show(mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Operacion cancelada");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ESPORTS esportSelec = (ESPORTS)comboBoxEquipos.SelectedItem;
            SEXES sexeSelec = (SEXES)comboBoxSexoEquipo.SelectedItem;
            CATEGORIES categSelec = (CATEGORIES)comboBoxCategoria.SelectedItem;
            CATEGORIES_EDAT categEdatSelec = (CATEGORIES_EDAT)comboBoxCategoriaPorEdad.SelectedItem;
            COMPETICIO competicioSelec = (COMPETICIO)comboBoxCompeticionEquipo.SelectedItem;

            mensaje = "";



            mensaje = BD.ORM_EQUIPS.InsertEQUIP(textBoxNombreEquipo.Text, idEntSelec, /*esportSelec.id*/1, sexeSelec.id, categSelec.id, categEdatSelec.id, competicioSelec.id);

            if (mensaje.Equals(""))
            {
                List<ENTITATS> entitatTemp = BD.ORM_ENTITATS.SelectEntitatByID(idEntSelec, ref mensaje);

                MessageBox.Show("Se ha agregado satisfactoriamente");
                comboBoxEquipos.DataSource = entitatTemp[0].EQUIPS.ToList();
            }
            else
            {
                MessageBox.Show(mensaje, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
