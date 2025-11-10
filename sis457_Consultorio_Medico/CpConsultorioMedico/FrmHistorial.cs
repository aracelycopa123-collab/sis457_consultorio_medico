using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity; 
using CpConsultorioMedico;
using CadConsultorioMedico; // entidades generadas (LabConsultorioMedicoEntities, Cita, Paciente, HistorialClinico)
using CpMinerva; // <- Añadido para acceder a Util
using ClnConsultorioMedico; // <- Añadido para usar PacienteCln, EspecialidadCln, DoctorCln

namespace CpConsultorioMedico
{
    public partial class FrmHistorial : Form
    {
        // Estado para edición/creación
        private int? currentCitaId = null;
        private bool isEditing = false;

        public FrmHistorial()
        {
            InitializeComponent();

            // Inicia con el panel de datos deshabilitado
            gbxDatos.Enabled = false;

            // Enlazo eventos de guardado/cancelar y otros
            btnGuardar.Click += btnGuardar_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnNuevo.Click += btnNuevo_Click;
            btnEditar.Click += btnEditar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnCerrar.Click += btnCerrar_Click;
            btnBuscar.Click += btnBuscar_Click;
            dgvLista.SelectionChanged += dgvLista_SelectionChanged;
            dgvLista.DoubleClick += dgvLista_DoubleClick;
            dtpFFecha.ValueChanged += dtpFFecha_ValueChanged;
            txtParametro.KeyPress += txtParametro_KeyPress;

            // Enlazar cambio de especialidad para cargar doctores
            cbxEspecialidad.SelectedIndexChanged += cbxEspecialidad_SelectedIndexChanged;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Preparar formulario para nueva entrada
            isEditing = false;
            currentCitaId = null;
            LimpiarCampos();
            gbxDatos.Enabled = true;

            // cargar combos para nueva entrada
            cargarEspecialidades();
            cbxDoctor.DataSource = null;
            cbxDoctor.Items.Clear();

            txtPaciente.Focus();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargar combos y listado al iniciar
                cargarEspecialidades();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carga las especialidades en el combo
        private void cargarEspecialidades()
        {
            try
            {
                var lista = EspecialidadCln.listar();
                cbxEspecialidad.DataSource = lista;
                cbxEspecialidad.DisplayMember = "nombre";
                cbxEspecialidad.ValueMember = "id";
                cbxEspecialidad.SelectedIndex = -1;
            }
            catch
            {
                cbxEspecialidad.DataSource = null;
                cbxEspecialidad.Items.Clear();
            }
        }

        // Carga los doctores para una especialidad
        private void cargarDoctores(int idEspecialidad)
        {
            try
            {
                var doctores = DoctorCln.listarPorEspecialidad(idEspecialidad);
                cbxDoctor.DataSource = doctores;
                cbxDoctor.DisplayMember = "nombreCompletoDoctor";
                cbxDoctor.ValueMember = "id";
                cbxDoctor.SelectedIndex = -1;
            }
            catch
            {
                cbxDoctor.DataSource = null;
                cbxDoctor.Items.Clear();
            }
        }

        // Handler del cambio de especialidad
        private void cbxEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEspecialidad.SelectedValue == null) { cbxDoctor.DataSource = null; cbxDoctor.Items.Clear(); return; }

            int id;
            if (int.TryParse(cbxEspecialidad.SelectedValue.ToString(), out id))
            {
                cargarDoctores(id);
            }
            else
            {
                cbxDoctor.DataSource = null;
                cbxDoctor.Items.Clear();
            }
        }

        // Ahora acepta filtros opcionales
        private void CargarHistorial(int? idPaciente = null, DateTime? fecha = null)
        {
            using (var ctx = new LabConsultorioMedicoEntities())
            {
                var q = from h in ctx.HistorialClinico
                        join p in ctx.Paciente on h.idPaciente equals p.id into gj
                        from p in gj.DefaultIfEmpty()
                        select new { h, p };

                if (idPaciente.HasValue)
                    q = q.Where(x => x.h.idPaciente == idPaciente.Value);

                if (fecha.HasValue)
                {
                    var d = fecha.Value.Date;
                    q = q.Where(x => DbFunctions.TruncateTime(x.h.fecha) == d);
                }

                var lista = q.OrderByDescending(x => x.h.fecha)
                             .Select(x => new
                             {
                                 x.h.id,
                                 Fecha = x.h.fecha,
                                 Paciente = x.p != null ? x.p.nombreCompletoPaciente : string.Empty,
                                 Descripcion = x.h.diagnostico,
                                 Tratamiento = x.h.tratamiento,
                                 Estado = x.h.estado
                             })
                             .ToList();

                dgvLista.DataSource = lista;
            }

            // Ajustar botones según selección
            dgvLista_SelectionChanged(dgvLista, EventArgs.Empty);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var ci = txtParametro.Text.Trim();
            if (!string.IsNullOrEmpty(ci))
            {
                var paciente = PacienteCln.buscarPorCedula(ci);
                if (paciente != null)
                {
                    txtFPaciente.Text = paciente.nombreCompletoPaciente;
                    txtPaciente.Text = paciente.nombreCompletoPaciente;
                    CargarHistorial(paciente.id, null);
                    return;
                }
                else
                {
                    MessageBox.Show("Paciente no encontrado para la cédula indicada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFPaciente.Clear();
                    txtPaciente.Clear();
                    CargarHistorial();
                    return;
                }
            }

            // Si no hay CI, intentar filtrar por nombre en txtFPaciente
            if (!string.IsNullOrWhiteSpace(txtFPaciente.Text))
            {
                var paciente = PacienteCln.buscar(txtFPaciente.Text.Trim());
                if (paciente != null) CargarHistorial(paciente.id, null);
                else CargarHistorial();
            }
            else
            {
                CargarHistorial();
            }
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) btnBuscar.PerformClick();
        }

        private void dtpFFecha_ValueChanged(object sender, EventArgs e)
        {
            // Filtrar por la fecha seleccionada
            var fecha = dtpFFecha.Value.Date;
            CargarHistorial(null, fecha);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones mínimas
            if (string.IsNullOrWhiteSpace(txtPaciente.Text))
            {
                MessageBox.Show("Ingrese el nombre del paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaciente.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
            {
                MessageBox.Show("Ingrese el diagnóstico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiagnostico.Focus();
                return;
            }

            int savedId = 0;
            try
            {
                using (var ctx = new LabConsultorioMedicoEntities())
                {
                    // Primero tratar de obtener paciente por cédula (txtParametro) o por nombre (txtPaciente)
                    Paciente paciente = null;
                    var ci = txtParametro.Text.Trim();
                    if (!string.IsNullOrEmpty(ci))
                        paciente = ctx.Paciente.FirstOrDefault(p => p.cedulaIdentidad == ci);

                    if (paciente == null)
                        paciente = ctx.Paciente.FirstOrDefault(p => p.nombreCompletoPaciente == txtPaciente.Text.Trim());

                    if (paciente == null)
                    {
                        MessageBox.Show("Paciente no encontrado. Busque por cédula o ingrese nombre exacto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!isEditing || currentCitaId == null)
                    {
                        // Crear nueva entrada de historial clínico
                        var nueva = new HistorialClinico
                        {
                            fecha = DateTime.Now,
                            fechaRegistro = DateTime.Now,
                            diagnostico = txtDiagnostico.Text.Trim(),
                            tratamiento = string.IsNullOrWhiteSpace(txtTratamiento.Text) ? null : txtTratamiento.Text.Trim(),
                            estado = 1, // valor habitual activo
                            usuarioRegistro = Util.usuario?.usuario,
                            idPaciente = paciente.id
                        };

                        // Nota: si quieres guardar doctor/especialidad debes añadir idDoctor/idEspecialidad a la entidad HistorialClinico en el modelo/BD
                        ctx.HistorialClinico.Add(nueva);
                        ctx.SaveChanges();
                        savedId = nueva.id;
                    }
                    else
                    {
                        // Editar existente
                        var existente = ctx.HistorialClinico.FirstOrDefault(h => h.id == currentCitaId.Value);
                        if (existente != null)
                        {
                            existente.diagnostico = txtDiagnostico.Text.Trim();
                            existente.tratamiento = string.IsNullOrWhiteSpace(txtTratamiento.Text) ? null : txtTratamiento.Text.Trim();
                            existente.usuarioRegistro = Util.usuario?.usuario;
                            ctx.SaveChanges();
                            savedId = existente.id;
                        }
                        else
                        {
                            MessageBox.Show("Registro a editar no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Entrada guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar y seleccionar el registro guardado
                CargarHistorial();
                if (savedId > 0) SelectRowById(savedId);

                gbxDatos.Enabled = false;
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error guardando entrada: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // selecciona la fila del DataGridView cuyo campo 'id' coincide con id
        private void SelectRowById(int id)
        {
            if (dgvLista.DataSource == null) return;
            for (int i = 0; i < dgvLista.Rows.Count; i++)
            {
                var row = dgvLista.Rows[i];
                object val = null;
                var idCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c =>
                    string.Equals(dgvLista.Columns[c.ColumnIndex].Name, "id", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(dgvLista.Columns[c.ColumnIndex].HeaderText, "id", StringComparison.OrdinalIgnoreCase));
                if (idCell != null) val = idCell.Value;
                else val = row.Cells[0].Value;

                if (val != null && int.TryParse(val.ToString(), out int rowId) && rowId == id)
                {
                    dgvLista.CurrentCell = row.Cells[0];
                    dgvLista.Rows[i].Selected = true;
                    return;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object idObj = dgvLista.CurrentRow.Cells.Cast<DataGridViewCell>().FirstOrDefault(c =>
                string.Equals(dgvLista.Columns[c.ColumnIndex].Name, "id", StringComparison.OrdinalIgnoreCase))?.Value ?? dgvLista.CurrentRow.Cells[0].Value;

            if (idObj == null || !int.TryParse(idObj.ToString(), out int id))
            {
                MessageBox.Show("No se pudo determinar el identificador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dialog = MessageBox.Show("¿Está seguro de eliminar la entrada seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;

            using (var ctx = new LabConsultorioMedicoEntities())
            {
                var reg = ctx.HistorialClinico.Find(id);
                if (reg != null)
                {
                    reg.estado = -1;
                    reg.usuarioRegistro = Util.usuario?.usuario;
                    ctx.SaveChanges();
                }
            }

            CargarHistorial();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Maneja selección de fila: habilita/deshabilita botones
        private void dgvLista_SelectionChanged(object sender, EventArgs e)
        {
            bool has = dgvLista.CurrentRow != null && dgvLista.Rows.Count > 0;
            btnEditar.Enabled = has;
            btnEliminar.Enabled = has;
        }

        // Doble clic en fila abre edición (comodidad)
        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            btnEditar.PerformClick();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            gbxDatos.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtPaciente.Text = string.Empty;
            txtDiagnostico.Text = string.Empty;
            txtTratamiento.Text = string.Empty;
            if (cbxDoctor.Items.Count > 0) cbxDoctor.SelectedIndex = -1;
            if (cbxEspecialidad.Items.Count > 0) cbxEspecialidad.SelectedIndex = -1;
            currentCitaId = null;
            isEditing = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object idObj = dgvLista.CurrentRow.Cells.Cast<DataGridViewCell>().FirstOrDefault(c =>
                string.Equals(dgvLista.Columns[c.ColumnIndex].Name, "id", StringComparison.OrdinalIgnoreCase))?.Value ?? dgvLista.CurrentRow.Cells[0].Value;

            if (idObj == null || !int.TryParse(idObj.ToString(), out int id))
            {
                MessageBox.Show("No se pudo determinar el identificador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var ctx = new LabConsultorioMedicoEntities())
            {
                var reg = ctx.HistorialClinico.Find(id);
                if (reg != null)
                {
                    isEditing = true;
                    currentCitaId = reg.id;
                    gbxDatos.Enabled = true;

                    // Cargar datos en los campos
                    var paciente = ctx.Paciente.Find(reg.idPaciente);
                    txtPaciente.Text = paciente != null ? paciente.nombreCompletoPaciente : string.Empty;
                    txtDiagnostico.Text = reg.diagnostico;
                    txtTratamiento.Text = reg.tratamiento;

                    // Si tienes especialidad/doctor en el modelo, aquí deberías cargar los combos
                    // cargarEspecialidades();
                    // cbxEspecialidad.SelectedValue = reg.idEspecialidad;
                    // cargarDoctores(reg.idEspecialidad);
                    // cbxDoctor.SelectedValue = reg.idDoctor;
                }
                else
                {
                    MessageBox.Show("Registro no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
