using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void btnPaPacientes_Click(object sender, EventArgs e)
        {
            new FrmPaciente().ShowDialog();
        }

        private void btnDoDoctores_Click(object sender, EventArgs e)
        {
            new FrmDoctor().ShowDialog();
        }

        private void btnEsEspecialidades_Click(object sender, EventArgs e)
        {
            new FrmEspecialidad().ShowDialog();
        }

        private void btnHCHistoriales_Click(object sender, EventArgs e)
        {
            new FrmHistorial().ShowDialog();
        }

        private void btnCiCitas_Click(object sender, EventArgs e)
        {
            new FrmCita().ShowDialog();
        }
        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogin.Show();
        }

        private void btnCoConcepto_Click(object sender, EventArgs e)
        {
            new FrmConcepto().ShowDialog();
        }
    }
}
