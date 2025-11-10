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
    public partial class FrmDoctor : Form
    {
        private bool esNuevo = false;
        public FrmDoctor()
        {
            InitializeComponent();
        }

        public void listar()
        {
            var lista = DoctorCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;

            // Comprobar existencia de columnas antes de acceder a ellas para evitar NullReferenceException
            DataGridViewColumn col;
            col = dgvLista.Columns["id"]; if (col != null) col.Visible = false;
            col = dgvLista.Columns["idEspecialidad"]; if (col != null) col.Visible = false;
            col = dgvLista.Columns["estado"]; if (col != null) col.Visible = false;
            col = dgvLista.Columns["estadoE"]; if (col != null) col.Visible = false;

            col = dgvLista.Columns["usuario"]; if (col != null) col.HeaderText = "Usuario";
            col = dgvLista.Columns["cedulaIdentidad"]; if (col != null) col.HeaderText = "Cédula de Identidad";
            col = dgvLista.Columns["nombreCompletoDoctor"]; if (col != null) col.HeaderText = "Nombre Completo";
            col = dgvLista.Columns["direccion"]; if (col != null) col.HeaderText = "Dirección";
            col = dgvLista.Columns["celular"]; if (col != null) col.HeaderText = "Celular";
            col = dgvLista.Columns["nombre"]; if (col != null) col.HeaderText = "Especialidad";
            col = dgvLista.Columns["usuarioRegistro"]; if (col != null) col.HeaderText = "Usuario Registro";
            col = dgvLista.Columns["fechaRegistro"]; if (col != null) col.HeaderText = "Fecha Registro";

            // Asignar CurrentCell solo si hay filas y la columna existe
            if (lista.Count > 0 && dgvLista.Columns["cedulaIdentidad"] != null && dgvLista.Rows.Count > 0)
            {
                dgvLista.CurrentCell = dgvLista.Rows[0].Cells["cedulaIdentidad"];
            }

            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtDoctor.Clear();
            cbxEspecialidad.SelectedIndex = -1;
            txtDireccion.Clear();
            txtCelular.Clear();
            txtUsuario.Clear();
        }
        private void FrmDoctor_Load(object sender, EventArgs e)
        {
            Size= new Size(840, 462);
            listar();
            cargarEspecialidades();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(840, 687);
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
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nombre = dgvLista.Rows[index].Cells["nombreCompletoDoctor"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar al doctor {nombre}?",
                "::: Consultorio Médico - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DoctorCln.eliminar(id, "");
                listar();
                MessageBox.Show("Doctor dado de baja correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(840, 687);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var doctor = DoctorCln.obtenerUno(id);
            txtCedulaIdentidad.Text = doctor.cedulaIdentidad; 
            txtDoctor.Text = doctor.nombreCompletoDoctor;
            cbxEspecialidad.SelectedValue = doctor.idEspecialidad;
            txtDireccion.Text = doctor.direccion;
            txtCelular.Text = doctor.celular.ToString();
            txtCedulaIdentidad.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(840, 462);
            limpiar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpDoctor.SetError(txtDoctor, "");
            erpDireccion.SetError(txtDireccion, "");
            erpCelular.SetError(txtCelular, "");
            erpEspecialidad.SetError(cbxEspecialidad, "");

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo Cédula de Identidad es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDoctor.Text))
            {
                erpDoctor.SetError(txtDoctor, "El campo Nombre Completo es obligatorio");
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
            if (string.IsNullOrEmpty(cbxEspecialidad.Text))
            {
                erpEspecialidad.SetError(cbxEspecialidad, "El campo Especilalidad es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                erpEspecialidad.SetError(txtUsuario, "El campo Usuario es obligatorio");
                esValido = false;
            }

            return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var doctor = new Doctor();
                doctor.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                doctor.nombreCompletoDoctor = txtDoctor.Text.Trim();
                doctor.idEspecialidad = Convert.ToInt32(cbxEspecialidad.SelectedValue);
                doctor.direccion = txtDireccion.Text.Trim();
                doctor.celular = Convert.ToInt64(txtCelular.Text);
                doctor.usuarioRegistro = Util.usuario.usuario;
                Usuario usuario = null;
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    usuario = new Usuario();
                    usuario.usuario = txtUsuario.Text.Trim();
                    // Asignar una clave por defecto (encriptada) para evitar violaciones de validación.
                    // Preferible: pedir al usuario la clave o generar una y mostrarla/forzar cambio.
                    usuario.clave = Util.Encrypt("hola123"); // <-- ahora no queda null
                }

                if (esNuevo)
                {
                    doctor.fechaRegistro = DateTime.Now;
                    doctor.estado = 1;
                    DoctorCln.insertar(doctor, usuario);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    doctor.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    DoctorCln.actualizar(doctor, txtUsuario.Text.Trim(), Util.Encrypt("hola123"));
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Doctor guardado correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
