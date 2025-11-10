using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CadConsultorioMedico;
using ClnConsultorioMedico;
using CpMinerva;

namespace CpConsultorioMedico
{
    public partial class FrmConcepto : Form
    {
        public FrmConcepto()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool validar()
        {
            bool esValido = true;
            erpEspecialidad.SetError(cbxEspecialidad, "");
            erpConcepto.SetError(txtConcepto, "");
            erpCosto.SetError(nudCosto, "");
            if (string.IsNullOrEmpty(cbxEspecialidad.Text))
            {
                erpEspecialidad.SetError(cbxEspecialidad, "El campo Especilalidad es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtConcepto.Text))
            {
                erpConcepto.SetError(txtConcepto, "El campo Concepto es obligatorio");
                esValido = false;
            }
            if (nudCosto.Value <= 0)
            {
                erpCosto.SetError(nudCosto, "El campo Costo es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var concepto = new Concepto();
                concepto.idEspecialidad = Convert.ToInt32(cbxEspecialidad.SelectedValue);
                concepto.descripcion = txtConcepto.Text.Trim();
                concepto.costo = nudCosto.Value;
                concepto.fechaRegistro = DateTime.Now;
                concepto.usuarioRegistro = Util.usuario.usuario;
                concepto.estado = 1;
                ConceptoCln.insertar(concepto);
                MessageBox.Show("Concepto de pago guardado correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxEspecialidad.SelectedIndex = -1;
                txtConcepto.Clear();
                nudCosto.Value = 0;
            }
        }
        private void cargarEspecialidades()
        {
            var especialidades = EspecialidadCln.listar();
            cbxEspecialidad.DataSource = especialidades;
            cbxEspecialidad.DisplayMember = "nombre";
            cbxEspecialidad.ValueMember = "id";
            cbxEspecialidad.SelectedIndex = -1;
        }
        private void FrmConcepto_Load(object sender, EventArgs e)
        {
            nudCosto.Maximum = 1000000;
            nudCosto.DecimalPlaces = 2;
            cargarEspecialidades();
        }
    }
}
