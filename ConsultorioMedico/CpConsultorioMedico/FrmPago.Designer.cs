// FrmPago.Designer.cs
namespace CpConsultorioMedico
{
    partial class FrmPago
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Button btnNuevoPago;
        private System.Windows.Forms.Button btnImprimir;
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
            this.btnNuevoPago = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Pagos";

            // dgvMain
            this.dgvMain.Location = new System.Drawing.Point(16, 45);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(700, 320);
            this.dgvMain.ReadOnly = true;
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;

            // btnNuevoPago
            this.btnNuevoPago.Location = new System.Drawing.Point(16, 375);
            this.btnNuevoPago.Name = "btnNuevoPago";
            this.btnNuevoPago.Size = new System.Drawing.Size(120, 30);
            this.btnNuevoPago.Text = "Nuevo Pago";

            // btnImprimir
            this.btnImprimir.Location = new System.Drawing.Point(152, 375);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(90, 30);
            this.btnImprimir.Text = "Imprimir";

            // btnCerrar
            this.btnCerrar.Location = new System.Drawing.Point(252, 375);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 30);
            this.btnCerrar.Text = "Cerrar";

            // FrmPago
            this.ClientSize = new System.Drawing.Size(740, 420);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.btnNuevoPago);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmPago";
            this.Text = "Pagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}
