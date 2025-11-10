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
using System.Text.RegularExpressions;
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
            if (dgvLista.Columns.Contains("estado"))
                dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombreCompletoPaciente"].HeaderText = "Nombre Completo";
            dgvLista.Columns["direccion"].HeaderText = "Dirección";
            dgvLista.Columns["celular"].HeaderText = "Celular";

            if (dgvLista.Columns.Contains("usuarioRegistro"))
                dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario";
            if (dgvLista.Columns.Contains("fechaRegistro"))
                dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";

            // Seleccionar primera celda segura
            if (lista.Count > 0)
            {
                if (dgvLista.Columns.Contains("cedulaIdentidad"))
                {
                    int colIndex = dgvLista.Columns["cedulaIdentidad"].Index;
                    if (dgvLista.Rows[0].Cells.Count > colIndex)
                        dgvLista.CurrentCell = dgvLista.Rows[0].Cells[colIndex];
                }
                else if (dgvLista.Rows[0].Cells.Count > 0)
                {
                    dgvLista.CurrentCell = dgvLista.Rows[0].Cells[0];
                }
            }

            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;

            // Seleccionar y cargar datos si el usuario buscó por cédula
            seleccionarYMostrarPacientePorParametro(lista, txtParametro.Text.Trim());
        }

        /// <summary>
        /// Normaliza una cédula eliminando todo lo que no sean dígitos.
        /// </summary>
        private string NormalizarCedula(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            return Regex.Replace(s, @"\D", "");
        }

        /// <summary>
        /// Intenta seleccionar en el grid y rellenar los controles inferiores según el parámetro.
        /// Busca primero en la lista mostrada (comparación normalizada), luego en la entidad (BD).
        /// </summary>
        private void seleccionarYMostrarPacientePorParametro(IEnumerable<paPacienteListar_Result> lista, string parametro)
        {
            if (string.IsNullOrWhiteSpace(parametro))
                return;

            var listaFiltrada = lista?.ToList() ?? new List<paPacienteListar_Result>();
            var paramNorm = NormalizarCedula(parametro);

            // 1) Buscar coincidencia en la lista visible por cédula (normalizada)
            paPacienteListar_Result match = null;
            if (!string.IsNullOrEmpty(paramNorm))
            {
                match = listaFiltrada.FirstOrDefault(x =>
                    !string.IsNullOrEmpty(x.cedulaIdentidad) &&
                    NormalizarCedula(x.cedulaIdentidad).Equals(paramNorm, StringComparison.OrdinalIgnoreCase));
            }

            // 2) Si no encontró exacta, intentar contains (normalizado)
            if (match == null && !string.IsNullOrEmpty(paramNorm))
            {
                match = listaFiltrada.FirstOrDefault(x =>
                    !string.IsNullOrEmpty(x.cedulaIdentidad) &&
                    NormalizarCedula(x.cedulaIdentidad).IndexOf(paramNorm, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (match != null)
            {
                // Seleccionar fila en el grid si existe la columna
                SelectGridRowByCedula(match.cedulaIdentidad);
                // Rellenar controles inferiores
                txtCedulaIdentidad.Text = match.cedulaIdentidad;
                txtPaciente.Text = match.nombreCompletoPaciente;
                txtDireccion.Text = match.direccion;
                txtCelular.Text = match.celular.ToString();
                return;
            }

            // 3) Fallback: buscar en la base de datos por cédula exacta (método existente)
            var pacienteExacto = PacienteCln.buscarPorCedula(parametro);
            if (pacienteExacto != null && pacienteExacto.estado != -1)
            {
                // Intentar seleccionar por id o por cédula en el grid
                SelectGridRowByCedula(pacienteExacto.cedulaIdentidad);
                txtCedulaIdentidad.Text = pacienteExacto.cedulaIdentidad;
                txtPaciente.Text = pacienteExacto.nombreCompletoPaciente;
                txtDireccion.Text = pacienteExacto.direccion;
                txtCelular.Text = pacienteExacto.celular.ToString();
                return;
            }

            // 4) Si no hay coincidencias y la lista tiene un solo elemento, usarlo
            if (listaFiltrada.Count == 1)
            {
                var item = listaFiltrada[0];
                SelectGridRowByCedula(item.cedulaIdentidad);
                txtCedulaIdentidad.Text = item.cedulaIdentidad;
                txtPaciente.Text = item.nombreCompletoPaciente;
                txtDireccion.Text = item.direccion;
                txtCelular.Text = item.celular.ToString();
                return;
            }

            // 5) No encontrado: limpiar campos inferiores
            txtCedulaIdentidad.Clear();
            txtPaciente.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
        }

        /// <summary>
        /// Selecciona en el DataGridView la fila que tenga la cédula dada (comparación normalizada).
        /// </summary>
        private void SelectGridRowByCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula) || dgvLista.Rows.Count == 0) return;
            var target = NormalizarCedula(cedula);
            for (int i = 0; i < dgvLista.Rows.Count; i++)
            {
                var row = dgvLista.Rows[i];
                if (!row.IsNewRow && row.Cells.Cast<DataGridViewCell>().Any(c => c.OwningColumn.Name == "cedulaIdentidad"))
                {
                    var cell = row.Cells["cedulaIdentidad"];
                    var cellVal = cell?.Value?.ToString() ?? string.Empty;
                    if (NormalizarCedula(cellVal).Equals(target, StringComparison.OrdinalIgnoreCase))
                    {
                        int colIndex = cell.ColumnIndex;
                        if (row.Cells.Count > colIndex)
                        {
                            dgvLista.CurrentCell = row.Cells[colIndex];
                        }
                        return;
                    }
                }
            }
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
                paciente.nombreCompletoPaciente = txtPaciente.Text.Trim();
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