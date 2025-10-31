using CadConsultorioMedico;
using ClnConsultorioMedico;
using CpMinerva;
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
    public partial class FrmPaciente : Form
    {
        public FrmPaciente()
        {
            InitializeComponent();
        }
        private bool esNuevo = false;

        private void listar()
        {
            var lista = PacienteCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombreCompletoPaciente"].HeaderText = "Nombre Completo";
            dgvLista.Columns["direccion"].HeaderText = "Dirección";
            dgvLista.Columns["celular"].HeaderText = "Celular";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["cedulaIdentidad"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtPaciente.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            Size = new Size(715, 450);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(715, 695);
            txtCedulaIdentidad.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                listar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nombre = dgvLista.Rows[index].Cells["nombreCompletoPaciente"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar al paciente {nombre}?",
                "::: Consultorio Médico - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                PacienteCln.eliminar(id, "");
                listar();
                MessageBox.Show("Paciente dado de baja correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(715, 695);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var paciente = PacienteCln.obtenerUno(id);

            txtCedulaIdentidad.Text = paciente.cedulaIdentidad;
            txtPaciente.Text = paciente.nombreCompletoPaciente;
            txtDireccion.Text = paciente.direccion;
            txtCelular.Text = paciente.celular.ToString();
            txtCedulaIdentidad.Focus();
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(715, 450);
            limpiar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpPaciente.SetError(txtPaciente, "");
            erpDireccion.SetError(txtDireccion, "");
            erpCelular.SetError(txtCelular, "");
            
            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo Cédula de Identidad es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtPaciente.Text))
            {
                erpPaciente.SetError(txtPaciente, "El campo Nombre Completo es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                erpDireccion.SetError(txtDireccion, "El campo Dirección es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtCelular.Text) || txtCelular.Text.Length != 8)
            {
                erpCelular.SetError(txtCelular, "El campo Celular es obligatorio o no tiene la longitud correcta");
                esValido = false;
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var paciente = new Paciente();
                paciente.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                paciente.nombreCompletoPaciente= txtPaciente.Text.Trim();
                paciente.direccion = txtDireccion.Text.Trim();
                paciente.celular = Convert.ToInt32(txtCelular.Text);
                paciente.usuarioRegistro = Util.usuario.usuario;
                if (esNuevo)
                {
                    paciente.fechaRegistro = DateTime.Now;
                    paciente.estado = 1;
                    PacienteCln.insertar(paciente);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    paciente.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    PacienteCln.actualizar(paciente);
                }

                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Paciente guardado correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
