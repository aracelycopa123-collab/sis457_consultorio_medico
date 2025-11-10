namespace CpConsultorioMedico
{
    partial class FrmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.c1Ribbon1 = new C1.Win.Ribbon.C1Ribbon();
            this.ribbonApplicationMenu1 = new C1.Win.Ribbon.RibbonApplicationMenu();
            this.ribbonBottomToolBar1 = new C1.Win.Ribbon.RibbonBottomToolBar();
            this.ribbonConfigToolBar1 = new C1.Win.Ribbon.RibbonConfigToolBar();
            this.ribbonQat1 = new C1.Win.Ribbon.RibbonQat();
            this.ribbonTab1 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup1 = new C1.Win.Ribbon.RibbonGroup();
            this.btnPaPacientes = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab2 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup2 = new C1.Win.Ribbon.RibbonGroup();
            this.btnDoDoctores = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab5 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup5 = new C1.Win.Ribbon.RibbonGroup();
            this.btnEsEspecialidades = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab7 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup7 = new C1.Win.Ribbon.RibbonGroup();
            this.btnHCHistoriales = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab3 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup3 = new C1.Win.Ribbon.RibbonGroup();
            this.btnCiCitas = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab8 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup6 = new C1.Win.Ribbon.RibbonGroup();
            this.btnCoConcepto = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab4 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup4 = new C1.Win.Ribbon.RibbonGroup();
            this.btnReGanancias = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTab6 = new C1.Win.Ribbon.RibbonTab();
            this.ribbonGroup8 = new C1.Win.Ribbon.RibbonGroup();
            this.ribbonButton1 = new C1.Win.Ribbon.RibbonButton();
            this.ribbonTopToolBar1 = new C1.Win.Ribbon.RibbonTopToolBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Ribbon1
            // 
            this.c1Ribbon1.ApplicationMenuHolder = this.ribbonApplicationMenu1;
            this.c1Ribbon1.AutoSizeElement = C1.Framework.AutoSizeElement.Width;
            this.c1Ribbon1.BottomToolBarHolder = this.ribbonBottomToolBar1;
            this.c1Ribbon1.ConfigToolBarHolder = this.ribbonConfigToolBar1;
            this.c1Ribbon1.Location = new System.Drawing.Point(0, 0);
            this.c1Ribbon1.Name = "c1Ribbon1";
            this.c1Ribbon1.QatHolder = this.ribbonQat1;
            this.c1Ribbon1.Size = new System.Drawing.Size(804, 161);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab1);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab2);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab5);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab7);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab3);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab8);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab4);
            this.c1Ribbon1.Tabs.Add(this.ribbonTab6);
            this.c1Ribbon1.TopToolBarHolder = this.ribbonTopToolBar1;
            // 
            // ribbonApplicationMenu1
            // 
            this.ribbonApplicationMenu1.Name = "ribbonApplicationMenu1";
            // 
            // ribbonBottomToolBar1
            // 
            this.ribbonBottomToolBar1.Name = "ribbonBottomToolBar1";
            // 
            // ribbonConfigToolBar1
            // 
            this.ribbonConfigToolBar1.Name = "ribbonConfigToolBar1";
            // 
            // ribbonQat1
            // 
            this.ribbonQat1.Name = "ribbonQat1";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Groups.Add(this.ribbonGroup1);
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Text = "Pacientes";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Items.Add(this.btnPaPacientes);
            this.ribbonGroup1.Name = "ribbonGroup1";
            this.ribbonGroup1.Text = "Administración de Pacientes";
            // 
            // btnPaPacientes
            // 
            this.btnPaPacientes.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnPaPacientes.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnPaPacientes.IconSet")))));
            this.btnPaPacientes.Name = "btnPaPacientes";
            this.btnPaPacientes.Text = "Pacientes";
            this.btnPaPacientes.Click += new System.EventHandler(this.btnPaPacientes_Click);
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Groups.Add(this.ribbonGroup2);
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Text = "Doctores";
            // 
            // ribbonGroup2
            // 
            this.ribbonGroup2.Items.Add(this.btnDoDoctores);
            this.ribbonGroup2.Name = "ribbonGroup2";
            this.ribbonGroup2.Text = "Administración del Personal Médico";
            // 
            // btnDoDoctores
            // 
            this.btnDoDoctores.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnDoDoctores.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnDoDoctores.IconSet")))));
            this.btnDoDoctores.Name = "btnDoDoctores";
            this.btnDoDoctores.Text = "Doctores";
            this.btnDoDoctores.Click += new System.EventHandler(this.btnDoDoctores_Click);
            // 
            // ribbonTab5
            // 
            this.ribbonTab5.Groups.Add(this.ribbonGroup5);
            this.ribbonTab5.Name = "ribbonTab5";
            this.ribbonTab5.Text = "Especialidades";
            // 
            // ribbonGroup5
            // 
            this.ribbonGroup5.Items.Add(this.btnEsEspecialidades);
            this.ribbonGroup5.Name = "ribbonGroup5";
            this.ribbonGroup5.Text = "Administración de especialidades";
            // 
            // btnEsEspecialidades
            // 
            this.btnEsEspecialidades.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnEsEspecialidades.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnEsEspecialidades.IconSet")))));
            this.btnEsEspecialidades.Name = "btnEsEspecialidades";
            this.btnEsEspecialidades.Text = "Especilidades";
            this.btnEsEspecialidades.Click += new System.EventHandler(this.btnEsEspecialidades_Click);
            // 
            // ribbonTab7
            // 
            this.ribbonTab7.Groups.Add(this.ribbonGroup7);
            this.ribbonTab7.Name = "ribbonTab7";
            this.ribbonTab7.Text = "Historial Clínico";
            // 
            // ribbonGroup7
            // 
            this.ribbonGroup7.Items.Add(this.btnHCHistoriales);
            this.ribbonGroup7.Name = "ribbonGroup7";
            this.ribbonGroup7.Text = "Administración de historiales";
            // 
            // btnHCHistoriales
            // 
            this.btnHCHistoriales.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnHCHistoriales.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnHCHistoriales.IconSet")))));
            this.btnHCHistoriales.Name = "btnHCHistoriales";
            this.btnHCHistoriales.Text = "Historiales Clínicos";
            this.btnHCHistoriales.Click += new System.EventHandler(this.btnHCHistoriales_Click);
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Groups.Add(this.ribbonGroup3);
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Text = "Cita";
            // 
            // ribbonGroup3
            // 
            this.ribbonGroup3.Items.Add(this.btnCiCitas);
            this.ribbonGroup3.Name = "ribbonGroup3";
            this.ribbonGroup3.Text = "Administración de Citas Médicas";
            // 
            // btnCiCitas
            // 
            this.btnCiCitas.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnCiCitas.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnCiCitas.IconSet")))));
            this.btnCiCitas.Name = "btnCiCitas";
            this.btnCiCitas.Text = "Citas";
            this.btnCiCitas.Click += new System.EventHandler(this.btnCiCitas_Click);
            // 
            // ribbonTab8
            // 
            this.ribbonTab8.Groups.Add(this.ribbonGroup6);
            this.ribbonTab8.Name = "ribbonTab8";
            this.ribbonTab8.Text = "Concepto";
            // 
            // ribbonGroup6
            // 
            this.ribbonGroup6.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("ribbonGroup6.IconSet")))));
            this.ribbonGroup6.Items.Add(this.btnCoConcepto);
            this.ribbonGroup6.Name = "ribbonGroup6";
            this.ribbonGroup6.Text = "Agregar Concepto de Pago";
            // 
            // btnCoConcepto
            // 
            this.btnCoConcepto.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnCoConcepto.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnCoConcepto.IconSet")))));
            this.btnCoConcepto.Name = "btnCoConcepto";
            this.btnCoConcepto.Text = "Agregar Concepto";
            this.btnCoConcepto.Click += new System.EventHandler(this.btnCoConcepto_Click);
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Groups.Add(this.ribbonGroup4);
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Text = "Reportes";
            // 
            // ribbonGroup4
            // 
            this.ribbonGroup4.Items.Add(this.btnReGanancias);
            this.ribbonGroup4.Name = "ribbonGroup4";
            this.ribbonGroup4.Text = "Reporte de ganacias";
            // 
            // btnReGanancias
            // 
            this.btnReGanancias.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.btnReGanancias.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("btnReGanancias.IconSet")))));
            this.btnReGanancias.Name = "btnReGanancias";
            this.btnReGanancias.Text = "Ganancias";
            // 
            // ribbonTab6
            // 
            this.ribbonTab6.Groups.Add(this.ribbonGroup8);
            this.ribbonTab6.Name = "ribbonTab6";
            this.ribbonTab6.Text = "Ayuda";
            // 
            // ribbonGroup8
            // 
            this.ribbonGroup8.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("ribbonGroup8.IconSet")))));
            this.ribbonGroup8.Items.Add(this.ribbonButton1);
            this.ribbonGroup8.Name = "ribbonGroup8";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.IconSet.Add(new C1.Framework.C1BitmapIcon("DefaultImage", new System.Drawing.Size(16, 16), System.Drawing.Color.Transparent, "DefaultImage", -1));
            this.ribbonButton1.IconSet.Add(new C1.Framework.C1BitmapIcon(null, new System.Drawing.Size(32, 32), System.Drawing.Color.Transparent, ((System.Drawing.Image)(resources.GetObject("ribbonButton1.IconSet")))));
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.Text = "Si Tiene algun problema con el programa comuníquese con el Técnico al: 75769274 o" +
    " al correo sopoetetecnico@gmail.com";
            // 
            // ribbonTopToolBar1
            // 
            this.ribbonTopToolBar1.Name = "ribbonTopToolBar1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 161);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(804, 520);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(804, 681);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.c1Ribbon1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Menú Principal - Consultorio Médico :::";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipal_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.c1Ribbon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.Ribbon.C1Ribbon c1Ribbon1;
        private C1.Win.Ribbon.RibbonApplicationMenu ribbonApplicationMenu1;
        private C1.Win.Ribbon.RibbonBottomToolBar ribbonBottomToolBar1;
        private C1.Win.Ribbon.RibbonConfigToolBar ribbonConfigToolBar1;
        private C1.Win.Ribbon.RibbonQat ribbonQat1;
        private C1.Win.Ribbon.RibbonTab ribbonTab1;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup1;
        private C1.Win.Ribbon.RibbonTopToolBar ribbonTopToolBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private C1.Win.Ribbon.RibbonButton btnPaPacientes;
        private C1.Win.Ribbon.RibbonTab ribbonTab2;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup2;
        private C1.Win.Ribbon.RibbonButton btnDoDoctores;
        private C1.Win.Ribbon.RibbonTab ribbonTab3;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup3;
        private C1.Win.Ribbon.RibbonButton btnCiCitas;
        private C1.Win.Ribbon.RibbonTab ribbonTab4;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup4;
        private C1.Win.Ribbon.RibbonButton btnReGanancias;
        private C1.Win.Ribbon.RibbonTab ribbonTab5;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup5;
        private C1.Win.Ribbon.RibbonButton btnEsEspecialidades;
        private C1.Win.Ribbon.RibbonTab ribbonTab6;
        private C1.Win.Ribbon.RibbonTab ribbonTab7;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup7;
        private C1.Win.Ribbon.RibbonButton btnHCHistoriales;
        private C1.Win.Ribbon.RibbonTab ribbonTab8;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup6;
        private C1.Win.Ribbon.RibbonButton btnCoConcepto;
        private C1.Win.Ribbon.RibbonGroup ribbonGroup8;
        private C1.Win.Ribbon.RibbonButton ribbonButton1;
    }
}