namespace CpConsultorioMedico
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblAgregarEditar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.ErrorProvider erpUsuario;
        private System.Windows.Forms.ErrorProvider erpClave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAgregarEditar = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.erpUsuario = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogin.Controls.Add(this.pictureBox1);
            this.pnlLogin.Controls.Add(this.lblAgregarEditar);
            this.pnlLogin.Controls.Add(this.lblUsuario);
            this.pnlLogin.Controls.Add(this.txtUsuario);
            this.pnlLogin.Controls.Add(this.lblClave);
            this.pnlLogin.Controls.Add(this.txtClave);
            this.pnlLogin.Controls.Add(this.btnIngresar);
            this.pnlLogin.Controls.Add(this.btnCerrar);
            this.pnlLogin.Location = new System.Drawing.Point(327, 125);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(380, 310);
            this.pnlLogin.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(155, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblAgregarEditar
            // 
            this.lblAgregarEditar.BackColor = System.Drawing.Color.Transparent;
            this.lblAgregarEditar.Font = new System.Drawing.Font("Wide Latin", 14.25F);
            this.lblAgregarEditar.Location = new System.Drawing.Point(-1, 83);
            this.lblAgregarEditar.Name = "lblAgregarEditar";
            this.lblAgregarEditar.Size = new System.Drawing.Size(380, 40);
            this.lblAgregarEditar.TabIndex = 1;
            this.lblAgregarEditar.Text = "Iniciar Sesión";
            this.lblAgregarEditar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F);
            this.lblUsuario.Location = new System.Drawing.Point(40, 135);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(58, 21);
            this.lblUsuario.TabIndex = 2;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9.75F);
            this.txtUsuario.Location = new System.Drawing.Point(120, 135);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(200, 22);
            this.txtUsuario.TabIndex = 3;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F);
            this.lblClave.Location = new System.Drawing.Point(40, 170);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(78, 21);
            this.lblClave.TabIndex = 4;
            this.lblClave.Text = "Contraseña:";
            // 
            // txtClave
            // 
            this.txtClave.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9.75F);
            this.txtClave.Location = new System.Drawing.Point(120, 170);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(200, 22);
            this.txtClave.TabIndex = 5;
            // 
            // btnIngresar
            // 
            this.btnIngresar.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11.25F);
            this.btnIngresar.Image = global::CpConsultorioMedico.Properties.Resources.Aceptar;
            this.btnIngresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIngresar.Location = new System.Drawing.Point(60, 220);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(110, 45);
            this.btnIngresar.TabIndex = 6;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11.25F);
            this.btnCerrar.Image = global::CpConsultorioMedico.Properties.Resources.Cerrar;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(200, 220);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 45);
            this.btnCerrar.TabIndex = 7;
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // erpUsuario
            // 
            this.erpUsuario.ContainerControl = this;
            // 
            // erpClave
            // 
            this.erpClave.ContainerControl = this;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(965, 499);
            this.Controls.Add(this.pnlLogin);
            this.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.Text = "::: Consultorio Médico - Login :::";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
