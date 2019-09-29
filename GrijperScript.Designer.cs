namespace GrijperScript
{
    partial class GrijperScript
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
            this.SelectHendel = new Sulakore.Components.SKoreButton();
            this.statusmessage = new System.Windows.Forms.Label();
            this.selectHoek = new Sulakore.Components.SKoreButton();
            this.StartGrijpAuto = new Sulakore.Components.SKoreButton();
            this.setMehod = new Sulakore.Components.SKoreButton();
            this.LoadSave = new Sulakore.Components.SKoreButton();
            this.SaveSettings = new Sulakore.Components.SKoreButton();
            this.selectTegel1 = new Sulakore.Components.SKoreButton();
            this.selectTegel0 = new Sulakore.Components.SKoreButton();
            this.SuspendLayout();
            // 
            // SelectHendel
            // 
            this.SelectHendel.Location = new System.Drawing.Point(6, 262);
            this.SelectHendel.Name = "SelectHendel";
            this.SelectHendel.Size = new System.Drawing.Size(100, 19);
            this.SelectHendel.TabIndex = 2;
            this.SelectHendel.Text = "Selecteer hendel";
            this.SelectHendel.Click += new System.EventHandler(this.SelectHendel_Click);
            // 
            // statusmessage
            // 
            this.statusmessage.AutoSize = true;
            this.statusmessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusmessage.Location = new System.Drawing.Point(3, 12);
            this.statusmessage.Name = "statusmessage";
            this.statusmessage.Size = new System.Drawing.Size(48, 16);
            this.statusmessage.TabIndex = 5;
            this.statusmessage.Text = "Status:";
            // 
            // selectHoek
            // 
            this.selectHoek.Location = new System.Drawing.Point(6, 235);
            this.selectHoek.Name = "selectHoek";
            this.selectHoek.Size = new System.Drawing.Size(100, 21);
            this.selectHoek.TabIndex = 6;
            this.selectHoek.Text = "Selecteer hoek";
            this.selectHoek.Click += new System.EventHandler(this.SelectHoek_Click);
            // 
            // StartGrijpAuto
            // 
            this.StartGrijpAuto.Location = new System.Drawing.Point(6, 143);
            this.StartGrijpAuto.Name = "StartGrijpAuto";
            this.StartGrijpAuto.Size = new System.Drawing.Size(78, 35);
            this.StartGrijpAuto.TabIndex = 12;
            this.StartGrijpAuto.Text = "Grijp";
            this.StartGrijpAuto.Click += new System.EventHandler(this.StartGrijpAuto_Click);
            // 
            // setMehod
            // 
            this.setMehod.Cursor = System.Windows.Forms.Cursors.Default;
            this.setMehod.Location = new System.Drawing.Point(6, 184);
            this.setMehod.Name = "setMehod";
            this.setMehod.Size = new System.Drawing.Size(78, 35);
            this.setMehod.TabIndex = 15;
            this.setMehod.Text = "Raak/mis";
            this.setMehod.Click += new System.EventHandler(this.SetMehod_Click);
            // 
            // LoadSave
            // 
            this.LoadSave.Location = new System.Drawing.Point(182, 12);
            this.LoadSave.Name = "LoadSave";
            this.LoadSave.Size = new System.Drawing.Size(70, 34);
            this.LoadSave.TabIndex = 16;
            this.LoadSave.Text = "Laad";
            this.LoadSave.Click += new System.EventHandler(this.LoadSave_Click);
            // 
            // SaveSettings
            // 
            this.SaveSettings.Location = new System.Drawing.Point(182, 51);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(70, 34);
            this.SaveSettings.TabIndex = 17;
            this.SaveSettings.Text = "Opslaan";
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // selectTegel1
            // 
            this.selectTegel1.Location = new System.Drawing.Point(112, 262);
            this.selectTegel1.Name = "selectTegel1";
            this.selectTegel1.Size = new System.Drawing.Size(89, 19);
            this.selectTegel1.TabIndex = 14;
            this.selectTegel1.Text = "tegel1";
            this.selectTegel1.Click += new System.EventHandler(this.SelectTegel1_Click);
            // 
            // selectTegel0
            // 
            this.selectTegel0.Location = new System.Drawing.Point(112, 237);
            this.selectTegel0.Name = "selectTegel0";
            this.selectTegel0.Size = new System.Drawing.Size(89, 19);
            this.selectTegel0.TabIndex = 13;
            this.selectTegel0.Text = "tegel 0";
            this.selectTegel0.Click += new System.EventHandler(this.SelectTegel0_Click);
            // 
            // GrijperScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(262, 303);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.LoadSave);
            this.Controls.Add(this.setMehod);
            this.Controls.Add(this.selectTegel1);
            this.Controls.Add(this.selectTegel0);
            this.Controls.Add(this.StartGrijpAuto);
            this.Controls.Add(this.selectHoek);
            this.Controls.Add(this.statusmessage);
            this.Controls.Add(this.SelectHendel);
            this.MaximizeBox = false;
            this.Name = "GrijperScript";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grijper script";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Sulakore.Components.SKoreButton SelectHendel;
        private System.Windows.Forms.Label statusmessage;
        private Sulakore.Components.SKoreButton selectHoek;
        private Sulakore.Components.SKoreButton StartGrijpAuto;
        private Sulakore.Components.SKoreButton setMehod;
        private Sulakore.Components.SKoreButton LoadSave;
        private Sulakore.Components.SKoreButton SaveSettings;
        private Sulakore.Components.SKoreButton selectTegel1;
        private Sulakore.Components.SKoreButton selectTegel0;
    }
}

