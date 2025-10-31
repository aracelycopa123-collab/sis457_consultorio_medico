using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Framework;
using CadConsultorioMedico;
using ClnConsultorioMedico;
using CpMinerva;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CpConsultorioMedico
{
    public partial class FrmCita : Form
    {
        private bool esNuevo = false;
        private bool formularioCargado = false;
        private string parametroValido = "";
        public FrmCita()
        {
            InitializeComponent();
        }
        public void listar()
        {
            var lista = CitaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["idEspecialidad"].Visible = false;
            dgvLista.Columns["fecha"].HeaderText = "Fecha Programada";
            dgvLista.Columns["Hora"].HeaderText = "Hora Programada";
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombreCompletoPaciente"].HeaderText = "Paciente";
            dgvLista.Columns["nombre"].HeaderText = "Especialidad";
            dgvLista.Columns["nombreCompletoDoctor"].HeaderText = "Doctor";
            dgvLista.Columns["fecha1"].HeaderText = "Fecha de Pago";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["fecha"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void limpiar()
        {
            txtPaciente.Clear();
            cbxEspecialidad.SelectedIndex = -1;
            cbxDoctor.SelectedIndex = -1;
            dtpFecha.Value = DateTime.Now;
            cbxHora.SelectedIndex = -1;
        }
        private void FrmCita_Load(object sender, EventArgs e)
        {
            Size = new Size(862, 539);
            listar();
            cargarEspecialidades();
            cargarHoras();
            txtFPaciente.Enabled = false;
            txtPaciente.Enabled = false;
            formularioCargado = true;
            dgvLista_SelectionChanged(dgvLista, EventArgs.Empty);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void obtenerPaciente()
        {
            if (string.IsNullOrWhiteSpace(txtParametro.Text.Trim()))
            {
                txtFPaciente.Text = string.Empty;
                return;
            }

            string nombrePaciente = PacienteCln.obtenerNombrePaciente(txtParametro.Text.Trim());

            if (string.IsNullOrEmpty(nombrePaciente))
            {
                txtFPaciente.Text = string.Empty;
                MessageBox.Show("El paciente no existe", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtFPaciente.Text = nombrePaciente;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
            obtenerPaciente();
        }
        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
            {
                listar();
                obtenerPaciente(); 
            }
        }
        public void listarFecha()
        {
            var lista = CitaCln.listarFecha(dtpFFecha.Value);
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["idEspecialidad"].Visible = false;
            dgvLista.Columns["fecha"].HeaderText = "Fecha Programada";
            dgvLista.Columns["Hora"].HeaderText = "Hora Programada";
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombreCompletoPaciente"].HeaderText = "Paciente";
            dgvLista.Columns["nombre"].HeaderText = "Especialidad";
            dgvLista.Columns["nombreCompletoDoctor"].HeaderText = "Doctor";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["fecha"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void dtpFFecha_ValueChanged(object sender, EventArgs e)
        {
            listarFecha();
            if (dtpFFecha.Value != DateTime.Now)
            {
                txtParametro.Clear();
                txtFPaciente.Clear();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(862, 713);
            cbxEspecialidad.Focus();
            cbxDoctor.DataSource = null;
            cbxDoctor.Items.Clear();
            cbxDoctor.SelectedIndex = -1;
        }
        private void cargarEspecialidades()
        {
            var especialidades = EspecialidadCln.listar();
            cbxEspecialidad.DataSource = especialidades;
            cbxEspecialidad.DisplayMember = "nombre";
            cbxEspecialidad.ValueMember = "id";
            cbxEspecialidad.SelectedIndex = -1;
        }
        private void cargarDoctores(int idEspecialidad)
        {
            var doctores = DoctorCln.listarPorEspecialidad(idEspecialidad);
            cbxDoctor.DataSource = null;
            cbxDoctor.Items.Clear();
            cbxDoctor.DataSource = doctores;
            cbxDoctor.DisplayMember = "nombreCompletoDoctor";
            cbxDoctor.ValueMember = "id";
            cbxDoctor.SelectedIndex = -1;
        }
        private void cbxEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formularioCargado)
            {
                int idEspecialidad = Convert.ToInt32(cbxEspecialidad.SelectedValue);
                cargarDoctores(idEspecialidad);
            }
        }
        private void cargarHoras()
        {
            cbxHora.Items.Clear();

            for (int h = 9; h <= 17; h++)
            {
                cbxHora.Items.Add(new TimeSpan(h, 0, 0));
                if (h != 17)
                    cbxHora.Items.Add(new TimeSpan(h, 30, 0));
            }

            cbxHora.SelectedIndex = -1;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(862, 713);
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var cita = CitaCln.obtenerUno(id);
            var paciente = PacienteCln.obtenerUno(cita.idPaciente);
            txtPaciente.Text = paciente.nombreCompletoPaciente;
            cbxEspecialidad.SelectedValue = cita.idEspecialidad;
            cbxDoctor.SelectedValue = cita.idDoctor;
            dtpFecha.Value = cita.fecha;
            cbxHora.SelectedItem = cita.hora;
            cbxEspecialidad.Focus();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtFPaciente.Clear();
            txtParametro.Clear();
            Size = new Size(862, 539);
            limpiar();
        }
        private void txtFPaciente_TextChanged(object sender, EventArgs e)
        {
            txtPaciente.Text = txtFPaciente.Text;
        }
        private bool validar()
        {
            bool esValido = true;
            erpEspecialidad.SetError(cbxEspecialidad, "");
            erpDoctor.SetError(cbxDoctor, "");
            erpFecha.SetError(dtpFecha, "");
            erpHora.SetError(cbxHora, "");
            if (string.IsNullOrEmpty(cbxEspecialidad.Text))
            {
                erpEspecialidad.SetError(cbxEspecialidad, "El campo Especilalidad es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbxDoctor.Text))
            {
                erpDoctor.SetError(cbxDoctor, "El campo Doctor es obligatorio");
                esValido = false;
            }
            if (dtpFecha.Value.Date < DateTime.Today)
            {
                erpFecha.SetError(dtpFecha, "El campo Fecha no puede ser una fecha pasada");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbxHora.Text))
            {
                erpHora.SetError(cbxHora, "El campo Doctor es obligatorio");
                esValido = false;
            }

            return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cita = new Cita();
                cita.idEspecialidad = Convert.ToInt32(cbxEspecialidad.SelectedValue);
                cita.idDoctor = Convert.ToInt32(cbxDoctor.SelectedValue);
                cita.fecha = dtpFecha.Value;
                cita.hora = (TimeSpan)cbxHora.SelectedItem;
                cita.usuarioRegistro = Util.usuario.usuario;

                if (esNuevo)
                {
                    txtPaciente.Text = txtFPaciente.Text;
                    var cedulaIdentidad = txtParametro.Text.Trim();
                    var paciente = PacienteCln.buscarPorCedula(cedulaIdentidad);
                    if (CitaCln.existeCita(paciente.id, Convert.ToInt32(cbxEspecialidad.SelectedValue), dtpFecha.Value))
                    {
                        MessageBox.Show("Este paciente ya tiene una cita registrada para esta especialidad en la misma fecha.",
                            "::: Consultorio Médico - Mensaje :::",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    cita.idPaciente = paciente.id;
                    cita.fechaRegistro = DateTime.Now;
                    cita.estado = 1;
                    CitaCln.insertar(cita);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    cita.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    var paciente = PacienteCln.buscar(txtPaciente.Text.Trim());
                    cita.idPaciente = paciente.id;
                    CitaCln.actualizar(cita);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Cita guardada correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFPaciente.Clear();
                txtParametro.Clear();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string paciente = dgvLista.Rows[index].Cells["nombreCompletoPaciente"].Value.ToString();
            DateTime fecha = Convert.ToDateTime(dgvLista.Rows[index].Cells["fecha"].Value);
            string fechaSolo = fecha.ToString("yyyy/MM/dd");
            string hora = dgvLista.Rows[index].Cells["hora"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro que desea eliminar la cita del paciente {paciente} para la fecha {fechaSolo} a horas {hora}?",
                "::: Consultorio Médico - Mensaje ::: ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                CitaCln.eliminar(id, "vivi");
                listar();
                MessageBox.Show("Cita dada de baja correctamente", "::: Consutorio Médico - Mensaje ::: ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtFPaciente.Clear();
            txtParametro.Clear();
        }
        private void btnPagar_Click(object sender, EventArgs e)
        {
            txtFPaciente.Clear();
            txtParametro.Clear();
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string paciente=dgvLista.Rows[index].Cells["nombreCompletoPaciente"].Value.ToString();
            string especialidad = dgvLista.Rows[index].Cells["nombre"].Value.ToString();
            int idEspecialidad = Convert.ToInt32(dgvLista.Rows[index].Cells["idEspecialidad"].Value);
            new FrmPago(this,id,paciente,especialidad,idEspecialidad).ShowDialog();
        }

        private void txtParametro_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtParametro.Text))
            {
                parametroValido = txtParametro.Text.Trim();
            }
        }

        public void refrescar()
        {
            var parametro = parametroValido;
            if (dtpFFecha.Value > DateTime.Now)
            {
               var lista = CitaCln.listarFecha(dtpFFecha.Value);
               dgvLista.DataSource = lista;
                }
            else
            {
                var lista = CitaCln.listarPa(parametro);
                dgvLista.DataSource = lista;
            }
        }
        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow != null)
            {
                var pagada = dgvLista.CurrentRow.Cells["Pagada"].Value.ToString();

                if (pagada == "Sí")
                {
                    btnPagar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
                else
                {
                    btnPagar.Enabled = true;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
