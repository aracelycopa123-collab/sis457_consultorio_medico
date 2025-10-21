// FrmMenu.Designer.cs
namespace CpConsultorioMedico
{
    partial class FrmMenu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuPacientes;
        private System.Windows.Forms.ToolStripMenuItem menuCitas;
        private System.Windows.Forms.ToolStripMenuItem menuDoctores;
        private System.Windows.Forms.ToolStripMenuItem menuEspecialidades;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuPacientes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCitas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDoctores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEspecialidades = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuPacientes,
                this.menuCitas,
                this.menuDoctores,
                this.menuEspecialidades
            });
            this.menuPacientes.Text = "Pacientes";
            this.menuCitas.Text = "Citas";
            this.menuDoctores.Text = "Doctores";
            this.menuEspecialidades.Text = "Especialidades";

            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel1
            });
            this.toolStripStatusLabel1.Text = "Listo";

            // FrmMenu
            this.ClientSize = new System.Drawing.Size(1024, 720);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMenu";
            this.Text = "Sistema Consultorio - Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
