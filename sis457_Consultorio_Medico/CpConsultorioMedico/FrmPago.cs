using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Framework;
using CadConsultorioMedico;
using ClnConsultorioMedico;
using CpMinerva;

namespace CpConsultorioMedico
{
    public partial class FrmPago : Form
    {
        private int idCita;
        private int idEspecialidad;
        private bool esNuevo = true;
        private FrmCita formCita;
        public FrmPago(FrmCita frmCita, int id ,string paciente, string especialidad, int idE)
        {
            InitializeComponent();
            txtPaciente.Text = paciente;
            txtEspecialidad.Text = especialidad;
            formCita = frmCita;
            idCita = id;
            idEspecialidad = idE;
        }
        private void cargarPorEspecialidad(int idEspecialidad)
        {
            var conceptos = ConceptoCln.listarPorEspecialidad(idEspecialidad);
            cbxConcepto.DataSource = conceptos;
            cbxConcepto.DisplayMember = "descripcion";
            cbxConcepto.ValueMember = "id";
            cbxConcepto.SelectedIndex = -1;
        }
        private void FrmPago_Load(object sender, EventArgs e)
        {
            nudCosto.Maximum = 1000000;
            nudCosto.DecimalPlaces = 2;
            nudEfectivo.Maximum = 1000000;
            nudEfectivo.DecimalPlaces = 2;
            nudCambio.Maximum = 1000000;
            nudCambio.DecimalPlaces = 2;
            txtPaciente.ReadOnly = true;
            txtEspecialidad.ReadOnly = true;
            cargarPorEspecialidad(idEspecialidad);
            cbxConcepto.Focus();
        }
        private void cbxConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxConcepto.SelectedIndex != -1 && cbxConcepto.SelectedValue is int idConcepto)
            {
                var concepto = ConceptoCln.obtenerUno(idConcepto); ;

                if (concepto != null)
                {
                    nudCosto.Value = concepto.costo;
                }
                else
                {
                    nudEfectivo.Value = 0;
                }
            }
        }
        private void calcularCambio()
        {
            decimal cambio = nudEfectivo.Value - nudCosto.Value;
            if (nudEfectivo.Value < nudCosto.Value)
            {
                erpEfectivo.SetError(nudEfectivo, "El campo Efectivo debe ser igual o mayor al Costo del servicio");
            }
            else
            {
                nudCambio.Value = cambio;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudMonto_ValueChanged(object sender, EventArgs e)
        {
            calcularCambio();
        }
        private bool validar()
        {
            bool esValido = true;
            erpConcepto.SetError(cbxConcepto, "");
            erpEfectivo.SetError(nudEfectivo, "");
            if (string.IsNullOrEmpty(cbxConcepto.Text))
            {
                erpConcepto.SetError(cbxConcepto, "El campo Concepto es obligatorio");
                esValido = false;
            }
            if (nudEfectivo.Value <= 0)
            {
                erpEfectivo.SetError(nudEfectivo, "El campo Efectivo es obligatorio o debe ser igual o mayor al costo");
                esValido = false;
            }
            return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var pago = new Pago();
                pago.idCita = idCita;
                pago.idConcepto = Convert.ToInt32(cbxConcepto.SelectedValue);
                pago.fecha = DateTime.Now;
                pago.usuarioRegistro = Util.usuario.usuario;

                if (esNuevo)
                {
                    pago.fechaRegistro = DateTime.Now;
                    pago.estado = 1;
                    PagoCln.insertar(pago);
                }
                formCita.refrescar();
                btnCancelar.PerformClick();
                MessageBox.Show("Pago guardado correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lblAgregarEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
