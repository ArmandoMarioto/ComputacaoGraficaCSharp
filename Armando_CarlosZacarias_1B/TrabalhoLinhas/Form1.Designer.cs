namespace TrabalhoLinhas
{
    partial class Form1
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
            this.tela = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPM = new System.Windows.Forms.RadioButton();
            this.rbMD = new System.Windows.Forms.RadioButton();
            this.rbERR = new System.Windows.Forms.RadioButton();
            this.rbLinha = new System.Windows.Forms.RadioButton();
            this.rbCirc = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbElipse = new System.Windows.Forms.RadioButton();
            this.rbPMC = new System.Windows.Forms.RadioButton();
            this.rbEquaExpli = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPri = new System.Windows.Forms.TabPage();
            this.tabPolig = new System.Windows.Forms.TabPage();
            this.txtCoord = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.cbPolig = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConf = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbHorizontal = new System.Windows.Forms.CheckBox();
            this.cbVertical = new System.Windows.Forms.CheckBox();
            this.rbEspelhamento = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCisY = new System.Windows.Forms.TextBox();
            this.tbCisX = new System.Windows.Forms.TextBox();
            this.rbCisalhamento = new System.Windows.Forms.RadioButton();
            this.cbPolig1 = new System.Windows.Forms.ComboBox();
            this.btnDesenhar = new System.Windows.Forms.Button();
            this.tbRot = new System.Windows.Forms.TextBox();
            this.tbEscala = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTransY = new System.Windows.Forms.TextBox();
            this.tbTransX = new System.Windows.Forms.TextBox();
            this.rbRot = new System.Windows.Forms.RadioButton();
            this.rbEscala = new System.Windows.Forms.RadioButton();
            this.rbTrans = new System.Windows.Forms.RadioButton();
            this.tabCor = new System.Windows.Forms.TabPage();
            this.cores = new System.Windows.Forms.PictureBox();
            this.cbPollig3 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtErro = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tela)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPri.SuspendLayout();
            this.tabPolig.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabCor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cores)).BeginInit();
            this.SuspendLayout();
            // 
            // tela
            // 
            this.tela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tela.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tela.Location = new System.Drawing.Point(324, 12);
            this.tela.Name = "tela";
            this.tela.Size = new System.Drawing.Size(716, 742);
            this.tela.TabIndex = 0;
            this.tela.TabStop = false;
            this.tela.MouseDown += new System.Windows.Forms.MouseEventHandler(this.coordInicial);
            this.tela.MouseUp += new System.Windows.Forms.MouseEventHandler(this.coordFinal);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbPM);
            this.panel1.Controls.Add(this.rbMD);
            this.panel1.Controls.Add(this.rbERR);
            this.panel1.Location = new System.Drawing.Point(40, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 91);
            this.panel1.TabIndex = 1;
            // 
            // rbPM
            // 
            this.rbPM.AutoSize = true;
            this.rbPM.Location = new System.Drawing.Point(5, 60);
            this.rbPM.Name = "rbPM";
            this.rbPM.Size = new System.Drawing.Size(43, 17);
            this.rbPM.TabIndex = 2;
            this.rbPM.TabStop = true;
            this.rbPM.Text = "PM";
            this.rbPM.UseVisualStyleBackColor = true;
            // 
            // rbMD
            // 
            this.rbMD.AutoSize = true;
            this.rbMD.Location = new System.Drawing.Point(3, 31);
            this.rbMD.Name = "rbMD";
            this.rbMD.Size = new System.Drawing.Size(51, 17);
            this.rbMD.TabIndex = 1;
            this.rbMD.TabStop = true;
            this.rbMD.Text = "DDA";
            this.rbMD.UseVisualStyleBackColor = true;
            // 
            // rbERR
            // 
            this.rbERR.AutoSize = true;
            this.rbERR.Location = new System.Drawing.Point(5, 5);
            this.rbERR.Name = "rbERR";
            this.rbERR.Size = new System.Drawing.Size(105, 17);
            this.rbERR.TabIndex = 0;
            this.rbERR.TabStop = true;
            this.rbERR.Text = "Eq Geral Reta";
            this.rbERR.UseVisualStyleBackColor = true;
            // 
            // rbLinha
            // 
            this.rbLinha.AutoSize = true;
            this.rbLinha.Checked = true;
            this.rbLinha.Location = new System.Drawing.Point(45, 100);
            this.rbLinha.Name = "rbLinha";
            this.rbLinha.Size = new System.Drawing.Size(62, 17);
            this.rbLinha.TabIndex = 2;
            this.rbLinha.TabStop = true;
            this.rbLinha.Text = "Linhas";
            this.rbLinha.UseVisualStyleBackColor = true;
            this.rbLinha.CheckedChanged += new System.EventHandler(this.rbLinha_CheckedChanged);
            // 
            // rbCirc
            // 
            this.rbCirc.AutoSize = true;
            this.rbCirc.Location = new System.Drawing.Point(47, 254);
            this.rbCirc.Name = "rbCirc";
            this.rbCirc.Size = new System.Drawing.Size(107, 17);
            this.rbCirc.TabIndex = 3;
            this.rbCirc.Text = "Circunferência";
            this.rbCirc.UseVisualStyleBackColor = true;
            this.rbCirc.CheckedChanged += new System.EventHandler(this.rbCirc_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbElipse);
            this.panel2.Controls.Add(this.rbPMC);
            this.panel2.Controls.Add(this.rbEquaExpli);
            this.panel2.Location = new System.Drawing.Point(40, 280);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(176, 91);
            this.panel2.TabIndex = 4;
            // 
            // rbElipse
            // 
            this.rbElipse.AutoSize = true;
            this.rbElipse.Location = new System.Drawing.Point(6, 60);
            this.rbElipse.Name = "rbElipse";
            this.rbElipse.Size = new System.Drawing.Size(59, 17);
            this.rbElipse.TabIndex = 5;
            this.rbElipse.TabStop = true;
            this.rbElipse.Text = "Elipse";
            this.rbElipse.UseVisualStyleBackColor = true;
            // 
            // rbPMC
            // 
            this.rbPMC.AutoSize = true;
            this.rbPMC.Location = new System.Drawing.Point(5, 33);
            this.rbPMC.Name = "rbPMC";
            this.rbPMC.Size = new System.Drawing.Size(43, 17);
            this.rbPMC.TabIndex = 1;
            this.rbPMC.TabStop = true;
            this.rbPMC.Text = "PM";
            this.rbPMC.UseVisualStyleBackColor = true;
            // 
            // rbEquaExpli
            // 
            this.rbEquaExpli.AutoSize = true;
            this.rbEquaExpli.Location = new System.Drawing.Point(6, 7);
            this.rbEquaExpli.Name = "rbEquaExpli";
            this.rbEquaExpli.Size = new System.Drawing.Size(78, 17);
            this.rbEquaExpli.TabIndex = 0;
            this.rbEquaExpli.TabStop = true;
            this.rbEquaExpli.Text = "Eq Geral ";
            this.rbEquaExpli.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPri);
            this.tabControl1.Controls.Add(this.tabPolig);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabCor);
            this.tabControl1.Location = new System.Drawing.Point(4, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(296, 475);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPri
            // 
            this.tabPri.BackColor = System.Drawing.Color.LightGray;
            this.tabPri.Controls.Add(this.panel2);
            this.tabPri.Controls.Add(this.panel1);
            this.tabPri.Controls.Add(this.rbCirc);
            this.tabPri.Controls.Add(this.rbLinha);
            this.tabPri.Location = new System.Drawing.Point(4, 22);
            this.tabPri.Name = "tabPri";
            this.tabPri.Padding = new System.Windows.Forms.Padding(3);
            this.tabPri.Size = new System.Drawing.Size(288, 449);
            this.tabPri.TabIndex = 0;
            this.tabPri.Text = "Primitivas";
            // 
            // tabPolig
            // 
            this.tabPolig.BackColor = System.Drawing.Color.LightGray;
            this.tabPolig.Controls.Add(this.label8);
            this.tabPolig.Controls.Add(this.txtCoord);
            this.tabPolig.Controls.Add(this.txtNome);
            this.tabPolig.Controls.Add(this.cbPolig);
            this.tabPolig.Controls.Add(this.btnCancel);
            this.tabPolig.Controls.Add(this.btnConf);
            this.tabPolig.Controls.Add(this.btnExcluir);
            this.tabPolig.Controls.Add(this.btnAdd);
            this.tabPolig.Location = new System.Drawing.Point(4, 22);
            this.tabPolig.Name = "tabPolig";
            this.tabPolig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPolig.Size = new System.Drawing.Size(288, 449);
            this.tabPolig.TabIndex = 1;
            this.tabPolig.Text = "Polígonos";
            // 
            // txtCoord
            // 
            this.txtCoord.Location = new System.Drawing.Point(8, 138);
            this.txtCoord.Multiline = true;
            this.txtCoord.Name = "txtCoord";
            this.txtCoord.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCoord.Size = new System.Drawing.Size(249, 279);
            this.txtCoord.TabIndex = 6;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(8, 77);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(131, 20);
            this.txtNome.TabIndex = 5;
            // 
            // cbPolig
            // 
            this.cbPolig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPolig.FormattingEnabled = true;
            this.cbPolig.Location = new System.Drawing.Point(8, 103);
            this.cbPolig.Name = "cbPolig";
            this.cbPolig.Size = new System.Drawing.Size(131, 21);
            this.cbPolig.TabIndex = 4;
            this.cbPolig.SelectedIndexChanged += new System.EventHandler(this.mudarPoligono);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(164, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.cancelar);
            // 
            // btnConf
            // 
            this.btnConf.BackColor = System.Drawing.Color.Transparent;
            this.btnConf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConf.Location = new System.Drawing.Point(164, 73);
            this.btnConf.Name = "btnConf";
            this.btnConf.Size = new System.Drawing.Size(92, 27);
            this.btnConf.TabIndex = 2;
            this.btnConf.Text = "Confirmar";
            this.btnConf.UseVisualStyleBackColor = false;
            this.btnConf.MouseClick += new System.Windows.Forms.MouseEventHandler(this.confirmar);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Transparent;
            this.btnExcluir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExcluir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExcluir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Location = new System.Drawing.Point(164, 103);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(93, 27);
            this.btnExcluir.TabIndex = 1;
            this.btnExcluir.Text = "Remover";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.excluir);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAdd.Location = new System.Drawing.Point(8, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(131, 27);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Adicionar";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            this.btnAdd.MouseClick += new System.Windows.Forms.MouseEventHandler(this.novoPoligono);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightGray;
            this.tabPage3.Controls.Add(this.cbHorizontal);
            this.tabPage3.Controls.Add(this.cbVertical);
            this.tabPage3.Controls.Add(this.rbEspelhamento);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbCisY);
            this.tabPage3.Controls.Add(this.tbCisX);
            this.tabPage3.Controls.Add(this.rbCisalhamento);
            this.tabPage3.Controls.Add(this.cbPolig1);
            this.tabPage3.Controls.Add(this.btnDesenhar);
            this.tabPage3.Controls.Add(this.tbRot);
            this.tabPage3.Controls.Add(this.tbEscala);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.tbTransY);
            this.tabPage3.Controls.Add(this.tbTransX);
            this.tabPage3.Controls.Add(this.rbRot);
            this.tabPage3.Controls.Add(this.rbEscala);
            this.tabPage3.Controls.Add(this.rbTrans);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(288, 449);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Transformações";
            // 
            // cbHorizontal
            // 
            this.cbHorizontal.AutoSize = true;
            this.cbHorizontal.Location = new System.Drawing.Point(77, 387);
            this.cbHorizontal.Name = "cbHorizontal";
            this.cbHorizontal.Size = new System.Drawing.Size(83, 17);
            this.cbHorizontal.TabIndex = 19;
            this.cbHorizontal.Text = "Horizontal";
            this.cbHorizontal.UseVisualStyleBackColor = true;
            // 
            // cbVertical
            // 
            this.cbVertical.AutoSize = true;
            this.cbVertical.Location = new System.Drawing.Point(77, 359);
            this.cbVertical.Name = "cbVertical";
            this.cbVertical.Size = new System.Drawing.Size(69, 17);
            this.cbVertical.TabIndex = 18;
            this.cbVertical.Text = "Vertical";
            this.cbVertical.UseVisualStyleBackColor = true;
            // 
            // rbEspelhamento
            // 
            this.rbEspelhamento.AutoSize = true;
            this.rbEspelhamento.Location = new System.Drawing.Point(49, 332);
            this.rbEspelhamento.Name = "rbEspelhamento";
            this.rbEspelhamento.Size = new System.Drawing.Size(104, 17);
            this.rbEspelhamento.TabIndex = 16;
            this.rbEspelhamento.Text = "Espelhamento";
            this.rbEspelhamento.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "X:";
            // 
            // tbCisY
            // 
            this.tbCisY.Location = new System.Drawing.Point(160, 286);
            this.tbCisY.Name = "tbCisY";
            this.tbCisY.Size = new System.Drawing.Size(66, 20);
            this.tbCisY.TabIndex = 13;
            // 
            // tbCisX
            // 
            this.tbCisX.Location = new System.Drawing.Point(59, 286);
            this.tbCisX.Name = "tbCisX";
            this.tbCisX.Size = new System.Drawing.Size(66, 20);
            this.tbCisX.TabIndex = 12;
            // 
            // rbCisalhamento
            // 
            this.rbCisalhamento.AutoSize = true;
            this.rbCisalhamento.Location = new System.Drawing.Point(36, 260);
            this.rbCisalhamento.Name = "rbCisalhamento";
            this.rbCisalhamento.Size = new System.Drawing.Size(100, 17);
            this.rbCisalhamento.TabIndex = 11;
            this.rbCisalhamento.Text = "Cisalhamento";
            this.rbCisalhamento.UseVisualStyleBackColor = true;
            // 
            // cbPolig1
            // 
            this.cbPolig1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPolig1.FormattingEnabled = true;
            this.cbPolig1.Location = new System.Drawing.Point(36, 15);
            this.cbPolig1.Name = "cbPolig1";
            this.cbPolig1.Size = new System.Drawing.Size(189, 21);
            this.cbPolig1.TabIndex = 10;
            this.cbPolig1.SelectedIndexChanged += new System.EventHandler(this.cbPolig1_SelectedIndexChanged);
            // 
            // btnDesenhar
            // 
            this.btnDesenhar.Location = new System.Drawing.Point(36, 413);
            this.btnDesenhar.Name = "btnDesenhar";
            this.btnDesenhar.Size = new System.Drawing.Size(190, 27);
            this.btnDesenhar.TabIndex = 9;
            this.btnDesenhar.Text = "Desenhar";
            this.btnDesenhar.UseVisualStyleBackColor = true;
            this.btnDesenhar.Click += new System.EventHandler(this.btnDesenhar_Click);
            // 
            // tbRot
            // 
            this.tbRot.Location = new System.Drawing.Point(36, 216);
            this.tbRot.Name = "tbRot";
            this.tbRot.Size = new System.Drawing.Size(189, 20);
            this.tbRot.TabIndex = 8;
            // 
            // tbEscala
            // 
            this.tbEscala.Location = new System.Drawing.Point(36, 143);
            this.tbEscala.Name = "tbEscala";
            this.tbEscala.Size = new System.Drawing.Size(189, 20);
            this.tbEscala.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "X:";
            // 
            // tbTransY
            // 
            this.tbTransY.Location = new System.Drawing.Point(160, 75);
            this.tbTransY.Name = "tbTransY";
            this.tbTransY.Size = new System.Drawing.Size(66, 20);
            this.tbTransY.TabIndex = 4;
            // 
            // tbTransX
            // 
            this.tbTransX.Location = new System.Drawing.Point(59, 75);
            this.tbTransX.Name = "tbTransX";
            this.tbTransX.Size = new System.Drawing.Size(66, 20);
            this.tbTransX.TabIndex = 3;
            // 
            // rbRot
            // 
            this.rbRot.AutoSize = true;
            this.rbRot.Location = new System.Drawing.Point(36, 189);
            this.rbRot.Name = "rbRot";
            this.rbRot.Size = new System.Drawing.Size(73, 17);
            this.rbRot.TabIndex = 2;
            this.rbRot.Text = "Rotação";
            this.rbRot.UseVisualStyleBackColor = true;
            // 
            // rbEscala
            // 
            this.rbEscala.AutoSize = true;
            this.rbEscala.Location = new System.Drawing.Point(36, 117);
            this.rbEscala.Name = "rbEscala";
            this.rbEscala.Size = new System.Drawing.Size(63, 17);
            this.rbEscala.TabIndex = 1;
            this.rbEscala.Text = "Escala";
            this.rbEscala.UseVisualStyleBackColor = true;
            // 
            // rbTrans
            // 
            this.rbTrans.AutoSize = true;
            this.rbTrans.Checked = true;
            this.rbTrans.Location = new System.Drawing.Point(36, 48);
            this.rbTrans.Name = "rbTrans";
            this.rbTrans.Size = new System.Drawing.Size(88, 17);
            this.rbTrans.TabIndex = 0;
            this.rbTrans.TabStop = true;
            this.rbTrans.Text = "Translação";
            this.rbTrans.UseVisualStyleBackColor = true;
            // 
            // tabCor
            // 
            this.tabCor.BackColor = System.Drawing.Color.LightGray;
            this.tabCor.Controls.Add(this.label7);
            this.tabCor.Controls.Add(this.label9);
            this.tabCor.Controls.Add(this.cores);
            this.tabCor.Controls.Add(this.cbPollig3);
            this.tabCor.Controls.Add(this.label6);
            this.tabCor.Controls.Add(this.label5);
            this.tabCor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabCor.Location = new System.Drawing.Point(4, 22);
            this.tabCor.Name = "tabCor";
            this.tabCor.Size = new System.Drawing.Size(288, 449);
            this.tabCor.TabIndex = 3;
            this.tabCor.Text = "Outros";
            // 
            // cores
            // 
            this.cores.Location = new System.Drawing.Point(205, 44);
            this.cores.Name = "cores";
            this.cores.Size = new System.Drawing.Size(58, 15);
            this.cores.TabIndex = 17;
            this.cores.TabStop = false;
            this.cores.Click += new System.EventHandler(this.Cores_Click);
            // 
            // cbPollig3
            // 
            this.cbPollig3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPollig3.FormattingEnabled = true;
            this.cbPollig3.Location = new System.Drawing.Point(28, 175);
            this.cbPollig3.Name = "cbPollig3";
            this.cbPollig3.Size = new System.Drawing.Size(180, 21);
            this.cbPollig3.TabIndex = 16;
            this.cbPollig3.SelectedIndexChanged += new System.EventHandler(this.cbPollig3_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Balde de Tinta";
            this.label6.Click += new System.EventHandler(this.tbBalde_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Paleta de Cores";
            this.label5.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtErro
            // 
            this.txtErro.AutoSize = true;
            this.txtErro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErro.ForeColor = System.Drawing.Color.Red;
            this.txtErro.Location = new System.Drawing.Point(747, 21);
            this.txtErro.Name = "txtErro";
            this.txtErro.Size = new System.Drawing.Size(0, 20);
            this.txtErro.TabIndex = 6;
            this.txtErro.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(36, 527);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(203, 27);
            this.button2.TabIndex = 8;
            this.button2.Text = "Limpar tela";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(74, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "ScanLine";
            this.label9.Click += new System.EventHandler(this.btnPreencher_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Selecione o polígono";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Nome do Polígono";
            this.label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // Form1
            // 
            this.AccessibleDescription = "Armando_CarlosZacarias";
            this.AccessibleName = "Armando_CarlosZacarias";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1052, 770);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtErro);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tela);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Armando_CarlosZacarias";
            ((System.ComponentModel.ISupportInitialize)(this.tela)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPri.ResumeLayout(false);
            this.tabPri.PerformLayout();
            this.tabPolig.ResumeLayout(false);
            this.tabPolig.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabCor.ResumeLayout(false);
            this.tabCor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox tela;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPM;
        private System.Windows.Forms.RadioButton rbMD;
        private System.Windows.Forms.RadioButton rbERR;
        private System.Windows.Forms.RadioButton rbLinha;
        private System.Windows.Forms.RadioButton rbCirc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbPMC;
        private System.Windows.Forms.RadioButton rbEquaExpli;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPri;
        private System.Windows.Forms.TabPage tabPolig;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConf;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox cbPolig;
        private System.Windows.Forms.TextBox txtCoord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTransY;
        private System.Windows.Forms.TextBox tbTransX;
        private System.Windows.Forms.RadioButton rbRot;
        private System.Windows.Forms.RadioButton rbEscala;
        private System.Windows.Forms.RadioButton rbTrans;
        private System.Windows.Forms.TextBox tbEscala;
        private System.Windows.Forms.TextBox tbRot;
        private System.Windows.Forms.Button btnDesenhar;
        private System.Windows.Forms.ComboBox cbPolig1;
        private System.Windows.Forms.TabPage tabCor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.RadioButton rbEspelhamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCisY;
        private System.Windows.Forms.TextBox tbCisX;
        private System.Windows.Forms.RadioButton rbCisalhamento;
        private System.Windows.Forms.CheckBox cbHorizontal;
        private System.Windows.Forms.CheckBox cbVertical;
        private System.Windows.Forms.Label txtErro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox cores;
        private System.Windows.Forms.ComboBox cbPollig3;
        private System.Windows.Forms.RadioButton rbElipse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

