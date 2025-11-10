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
    public partial class FrmEspecialidad : Form
    {
        public FrmEspecialidad()
        {
            InitializeComponent();
        }
        private bool esNuevo = false;
        public void listar()
        {
            var lista = EspecialidadCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["nombre"].HeaderText = "Especialidad";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["nombre"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private bool validar()
        {
            bool esValido = true;
            erpEspecialidad.SetError(txtEspecialidad, "");

            if (string.IsNullOrEmpty(txtEspecialidad.Text))
            {
                erpEspecialidad.SetError(txtEspecialidad, "El campo Nombre de la Especialidad es obligatorio");
                esValido = false;
            }
            return esValido;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmEspecialidad_Load(object sender, EventArgs e)
        {
            Size = new Size(528, 475);
            listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(528, 600);
            txtEspecialidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(528, 600);
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var especialidad = EspecialidadCln.obtenerUno(id);
            txtEspecialidad.Text = especialidad.nombre;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string especialidad = dgvLista.Rows[index].Cells["nombre"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar la especialidad {especialidad}?",
                "::: Consultorio Médico - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                EspecialidadCln.eliminar(id, "");
                listar();
                MessageBox.Show("especialidad dado de baja correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                listar();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(528, 475);
            txtEspecialidad.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var especialidad = new Especialidad();
                especialidad.nombre = txtEspecialidad.Text.Trim();
                especialidad.usuarioRegistro = Util.usuario.usuario;
                if (esNuevo)
                {
                    especialidad.fechaRegistro = DateTime.Now;
                    especialidad.estado = 1;
                    EspecialidadCln.insertar(especialidad);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    especialidad.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    EspecialidadCln.actualizar(especialidad);
                }

                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Especialidad guardada correctamente", "::: Consultorio Médico - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
