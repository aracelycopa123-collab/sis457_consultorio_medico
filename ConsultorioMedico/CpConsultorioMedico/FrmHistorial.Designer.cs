// FrmHistorial.Designer.cs
namespace CpConsultorioMedico
{
    partial class FrmHistorial
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Historial";

            // dgvMain
            this.dgvMain.Location = new System.Drawing.Point(16, 45);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(800, 320);
            this.dgvMain.ReadOnly = true;
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;

            // btnExportar
            this.btnExportar.Location = new System.Drawing.Point(16, 375);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(120, 30);
            this.btnExportar.Text = "Exportar";

            // btnCerrar
            this.btnCerrar.Location = new System.Drawing.Point(152, 375);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 30);
            this.btnCerrar.Text = "Cerrar";

            // FrmHistorial
            this.ClientSize = new System.Drawing.Size(840, 420);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmHistorial";
            this.Text = "Historial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
