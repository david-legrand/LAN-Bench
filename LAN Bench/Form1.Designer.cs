namespace LANBench
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbNetworkInfos = new System.Windows.Forms.TextBox();
            this.bTest1 = new System.Windows.Forms.Button();
            this.bTest2 = new System.Windows.Forms.Button();
            this.tbResults = new System.Windows.Forms.TextBox();
            this.bTest3 = new System.Windows.Forms.Button();
            this.bTest4 = new System.Windows.Forms.Button();
            this.stripBench = new System.Windows.Forms.StatusStrip();
            this.benchStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbBench = new System.Windows.Forms.ProgressBar();
            this.stripBench.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNetworkInfos
            // 
            this.tbNetworkInfos.Location = new System.Drawing.Point(12, 12);
            this.tbNetworkInfos.Multiline = true;
            this.tbNetworkInfos.Name = "tbNetworkInfos";
            this.tbNetworkInfos.ReadOnly = true;
            this.tbNetworkInfos.Size = new System.Drawing.Size(294, 59);
            this.tbNetworkInfos.TabIndex = 0;
            this.tbNetworkInfos.DoubleClick += new System.EventHandler(this.tbWlanInfos_DoubleClick);
            // 
            // bTest1
            // 
            this.bTest1.Location = new System.Drawing.Point(12, 77);
            this.bTest1.Name = "bTest1";
            this.bTest1.Size = new System.Drawing.Size(145, 23);
            this.bTest1.TabIndex = 1;
            this.bTest1.UseVisualStyleBackColor = true;
            this.bTest1.Click += new System.EventHandler(this.bTest1_Click);
            // 
            // bTest2
            // 
            this.bTest2.Location = new System.Drawing.Point(163, 77);
            this.bTest2.Name = "bTest2";
            this.bTest2.Size = new System.Drawing.Size(143, 23);
            this.bTest2.TabIndex = 2;
            this.bTest2.UseVisualStyleBackColor = true;
            this.bTest2.Click += new System.EventHandler(this.bTest2_Click);
            // 
            // tbResults
            // 
            this.tbResults.Location = new System.Drawing.Point(312, 12);
            this.tbResults.Multiline = true;
            this.tbResults.Name = "tbResults";
            this.tbResults.ReadOnly = true;
            this.tbResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResults.Size = new System.Drawing.Size(427, 146);
            this.tbResults.TabIndex = 4;
            this.tbResults.TextChanged += new System.EventHandler(this.tbResults_TextChanged);
            this.tbResults.DoubleClick += new System.EventHandler(this.tbResults_DoubleClick);
            // 
            // bTest3
            // 
            this.bTest3.Location = new System.Drawing.Point(12, 106);
            this.bTest3.Name = "bTest3";
            this.bTest3.Size = new System.Drawing.Size(145, 23);
            this.bTest3.TabIndex = 5;
            this.bTest3.UseVisualStyleBackColor = true;
            this.bTest3.Click += new System.EventHandler(this.bTest3_Click);
            // 
            // bTest4
            // 
            this.bTest4.Location = new System.Drawing.Point(163, 106);
            this.bTest4.Name = "bTest4";
            this.bTest4.Size = new System.Drawing.Size(143, 23);
            this.bTest4.TabIndex = 6;
            this.bTest4.UseVisualStyleBackColor = true;
            this.bTest4.Click += new System.EventHandler(this.bTest4_Click);
            // 
            // stripBench
            // 
            this.stripBench.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.benchStatus});
            this.stripBench.Location = new System.Drawing.Point(0, 168);
            this.stripBench.Name = "stripBench";
            this.stripBench.Size = new System.Drawing.Size(750, 22);
            this.stripBench.SizingGrip = false;
            this.stripBench.TabIndex = 7;
            this.stripBench.Text = "statusStrip1";
            this.stripBench.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseDoubleClick);
            // 
            // benchStatus
            // 
            this.benchStatus.Name = "benchStatus";
            this.benchStatus.Size = new System.Drawing.Size(72, 17);
            this.benchStatus.Text = "benchStatus";
            // 
            // pbBench
            // 
            this.pbBench.Location = new System.Drawing.Point(12, 135);
            this.pbBench.Name = "pbBench";
            this.pbBench.Size = new System.Drawing.Size(294, 23);
            this.pbBench.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 190);
            this.Controls.Add(this.pbBench);
            this.Controls.Add(this.stripBench);
            this.Controls.Add(this.bTest4);
            this.Controls.Add(this.bTest3);
            this.Controls.Add(this.tbResults);
            this.Controls.Add(this.bTest2);
            this.Controls.Add(this.bTest1);
            this.Controls.Add(this.tbNetworkInfos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.stripBench.ResumeLayout(false);
            this.stripBench.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNetworkInfos;
        private System.Windows.Forms.Button bTest1;
        private System.Windows.Forms.Button bTest2;
        private System.Windows.Forms.TextBox tbResults;
        private System.Windows.Forms.Button bTest3;
        private System.Windows.Forms.Button bTest4;
        private System.Windows.Forms.StatusStrip stripBench;
        private System.Windows.Forms.ToolStripStatusLabel benchStatus;
        private System.Windows.Forms.ProgressBar pbBench;
    }
}

