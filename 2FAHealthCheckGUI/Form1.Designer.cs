using System.Windows.Forms;

namespace _2FAHealthCheckGUI
{
    partial class Form
    {
        ToolTip ttButton;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.tbPC = new MetroFramework.Controls.MetroTextBox();
            this.tbComputerDescription = new MetroFramework.Controls.MetroTextBox();
            this.tbDriverDescription = new MetroFramework.Controls.MetroTextBox();
            this.tbDriver = new MetroFramework.Controls.MetroTextBox();
            this.bApply = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.lExport = new MetroFramework.Drawing.Html.HtmlLabel();
            this.tbPath = new MetroFramework.Controls.MetroTextBox();
            this.lInfo = new System.Windows.Forms.Label();
            this.lPC = new System.Windows.Forms.Label();
            this.lDriver = new System.Windows.Forms.Label();
            this.lmessage = new System.Windows.Forms.Label();
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.loadGif = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadGif)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPC
            // 
            // 
            // 
            // 
            this.tbPC.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.tbPC.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode")));
            this.tbPC.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.tbPC.CustomButton.Name = "";
            this.tbPC.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.tbPC.CustomButton.Style = MetroFramework.MetroColorStyle.Orange;
            this.tbPC.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.tbPC.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPC.CustomButton.UseSelectable = true;
            this.tbPC.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.tbPC.Lines = new string[0];
            resources.ApplyResources(this.tbPC, "tbPC");
            this.tbPC.MaxLength = 32767;
            this.tbPC.Multiline = true;
            this.tbPC.Name = "tbPC";
            this.tbPC.PasswordChar = '\0';
            this.tbPC.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPC.SelectedText = "";
            this.tbPC.SelectionLength = 0;
            this.tbPC.SelectionStart = 0;
            this.tbPC.UseSelectable = true;
            this.tbPC.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPC.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbPC.TextChanged += new System.EventHandler(this.tbPC_TextChanged);
            // 
            // tbComputerDescription
            // 
            // 
            // 
            // 
            this.tbComputerDescription.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.tbComputerDescription.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode1")));
            this.tbComputerDescription.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.tbComputerDescription.CustomButton.Name = "";
            this.tbComputerDescription.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.tbComputerDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbComputerDescription.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.tbComputerDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbComputerDescription.CustomButton.UseSelectable = true;
            this.tbComputerDescription.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.tbComputerDescription.Lines = new string[0];
            resources.ApplyResources(this.tbComputerDescription, "tbComputerDescription");
            this.tbComputerDescription.MaxLength = 32767;
            this.tbComputerDescription.Multiline = true;
            this.tbComputerDescription.Name = "tbComputerDescription";
            this.tbComputerDescription.PasswordChar = '\0';
            this.tbComputerDescription.ReadOnly = true;
            this.tbComputerDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbComputerDescription.SelectedText = "";
            this.tbComputerDescription.SelectionLength = 0;
            this.tbComputerDescription.SelectionStart = 0;
            this.tbComputerDescription.UseSelectable = true;
            this.tbComputerDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbComputerDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbDriverDescription
            // 
            // 
            // 
            // 
            this.tbDriverDescription.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.tbDriverDescription.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode2")));
            this.tbDriverDescription.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.tbDriverDescription.CustomButton.Name = "";
            this.tbDriverDescription.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.tbDriverDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDriverDescription.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.tbDriverDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDriverDescription.CustomButton.UseSelectable = true;
            this.tbDriverDescription.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible2")));
            this.tbDriverDescription.Lines = new string[0];
            resources.ApplyResources(this.tbDriverDescription, "tbDriverDescription");
            this.tbDriverDescription.MaxLength = 32767;
            this.tbDriverDescription.Multiline = true;
            this.tbDriverDescription.Name = "tbDriverDescription";
            this.tbDriverDescription.PasswordChar = '\0';
            this.tbDriverDescription.ReadOnly = true;
            this.tbDriverDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDriverDescription.SelectedText = "";
            this.tbDriverDescription.SelectionLength = 0;
            this.tbDriverDescription.SelectionStart = 0;
            this.tbDriverDescription.UseSelectable = true;
            this.tbDriverDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDriverDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tbDriver
            // 
            // 
            // 
            // 
            this.tbDriver.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.tbDriver.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode3")));
            this.tbDriver.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location3")));
            this.tbDriver.CustomButton.Name = "";
            this.tbDriver.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size3")));
            this.tbDriver.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbDriver.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex3")));
            this.tbDriver.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbDriver.CustomButton.UseSelectable = true;
            this.tbDriver.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible3")));
            this.tbDriver.Lines = new string[0];
            resources.ApplyResources(this.tbDriver, "tbDriver");
            this.tbDriver.MaxLength = 32767;
            this.tbDriver.Multiline = true;
            this.tbDriver.Name = "tbDriver";
            this.tbDriver.PasswordChar = '\0';
            this.tbDriver.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbDriver.SelectedText = "";
            this.tbDriver.SelectionLength = 0;
            this.tbDriver.SelectionStart = 0;
            this.tbDriver.UseSelectable = true;
            this.tbDriver.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbDriver.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbDriver.TextChanged += new System.EventHandler(this.tbDriver_TextChanged);
            // 
            // bApply
            // 
            this.bApply.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.bApply, "bApply");
            this.bApply.Name = "bApply";
            this.ttButton.SetToolTip(this.bApply, resources.GetString("bApply.ToolTip"));
            this.bApply.UseSelectable = true;
            this.bApply.UseVisualStyleBackColor = false;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // lExport
            // 
            resources.ApplyResources(this.lExport, "lExport");
            this.lExport.BackColor = System.Drawing.SystemColors.Window;
            this.lExport.Name = "lExport";
            // 
            // tbPath
            // 
            // 
            // 
            // 
            this.tbPath.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.tbPath.CustomButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("resource.ImeMode4")));
            this.tbPath.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location4")));
            this.tbPath.CustomButton.Name = "";
            this.tbPath.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size4")));
            this.tbPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbPath.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex4")));
            this.tbPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbPath.CustomButton.UseSelectable = true;
            this.tbPath.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible4")));
            this.tbPath.Lines = new string[0];
            resources.ApplyResources(this.tbPath, "tbPath");
            this.tbPath.MaxLength = 32767;
            this.tbPath.Name = "tbPath";
            this.tbPath.PasswordChar = '\0';
            this.tbPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbPath.SelectedText = "";
            this.tbPath.SelectionLength = 0;
            this.tbPath.SelectionStart = 0;
            this.tbPath.UseSelectable = true;
            this.tbPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // lInfo
            // 
            this.lInfo.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lInfo, "lInfo");
            this.lInfo.Name = "lInfo";
            // 
            // lPC
            // 
            this.lPC.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lPC, "lPC");
            this.lPC.Name = "lPC";
            // 
            // lDriver
            // 
            this.lDriver.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lDriver, "lDriver");
            this.lDriver.Name = "lDriver";
            // 
            // lmessage
            // 
            resources.ApplyResources(this.lmessage, "lmessage");
            this.lmessage.ForeColor = System.Drawing.Color.Red;
            this.lmessage.Name = "lmessage";
            // 
            // ttButton
            // 
            this.ttButton.AutoPopDelay = 5000;
            this.ttButton.InitialDelay = 1000;
            this.ttButton.ReshowDelay = 100;
            this.ttButton.ShowAlways = true;
            // 
            // loadGif
            // 
            resources.ApplyResources(this.loadGif, "loadGif");
            this.loadGif.Name = "loadGif";
            this.loadGif.TabStop = false;
            // 
            // Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lmessage);
            this.Controls.Add(this.lDriver);
            this.Controls.Add(this.lPC);
            this.Controls.Add(this.loadGif);
            this.Controls.Add(this.lInfo);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lExport);
            this.Controls.Add(this.bApply);
            this.Controls.Add(this.tbDriverDescription);
            this.Controls.Add(this.tbDriver);
            this.Controls.Add(this.tbComputerDescription);
            this.Controls.Add(this.tbPC);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Name = "Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loadGif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox tbPC;
        private MetroFramework.Controls.MetroTextBox tbComputerDescription;
        private MetroFramework.Controls.MetroTextBox tbDriverDescription;
        private MetroFramework.Controls.MetroTextBox tbDriver;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton bApply;
        private MetroFramework.Drawing.Html.HtmlLabel lExport;
        private MetroFramework.Controls.MetroTextBox tbPath;
        private Label lInfo;
        private PictureBox loadGif;
        private Label lPC;
        private Label lDriver;
        private Label lmessage;
    }
}

