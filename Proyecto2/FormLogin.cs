using System;
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
    public partial class FormLogin : Form
    {

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private String mensaje = "";


        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cogemos la lista de usuarios que tenemos en la BD
            List<USUARIS> listaUsuarios = BD.ORM_USUARIS.SelectAllUSUARIS(ref mensaje);

            //guardamos los campos escritos en los textbox
            String nombre = textBoxUsuario.Text;
            String pass = textBoxContraseña.Text;
            bool encontrado = false;

            //recorremos la lista en busca del usuario pedido
            foreach(USUARIS u in listaUsuarios)
            {
                if(u.nom == nombre && u.contrasenya == pass)
                {
                    //si está, cambiamos la condicion
                    encontrado = true;
                }
            }

            //si está, abrimos el siguiente form
            if (encontrado)
            {
                FormMain f = new Proyecto2.FormMain();
                this.Hide();
                f.ShowDialog();
                this.Close();

            }
            else
            {
                //sino, hacemos un mensaje de error
                MessageBox.Show("Error al introducir el usuario o contraseña.", "ATENCION", MessageBoxButtons.OK);
                textBoxUsuario.Text = "";
                textBoxContraseña.Text = "";
                textBoxUsuario.Focus();
            }
            

        }

        //boton de cancelar
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //estos metodos son para poder mover la ventada desde casi todos los lados
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        
    }
}
