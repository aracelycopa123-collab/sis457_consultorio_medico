using System;
using System.Windows.Forms;

namespace CpConsultorioMedico
{
    public partial class FrmMenu : Form
    {
        private FrmLogin frmLogin;

        public FrmMenu(FrmLogin frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
        }

        private void AbrirFormularioEnPanel(Form formulario)
        {
            // Cierra el contenido previo
            panelContenido.Controls.Clear();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(formulario);
            panelContenido.Tag = formulario;
            formulario.Show();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmPaciente());
        }

        private void btnDoctores_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmDoctor());
        }

        private void btnEspecialidades_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmEspecialidad());
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmHistorial());
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCita());
        }

        private void btnConcepto_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmConcepto());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            // Aquí podrías crear un FrmReportes y mostrarlo
            MessageBox.Show("Función de Reportes");
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Si tiene algún problema con el programa comuníquese al: 75769274 o al correo soporte@tecnico.com");
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogin.Show();
        }
    }
}
