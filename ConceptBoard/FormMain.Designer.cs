namespace ConceptBoard
{
	partial class FormMain
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
			System.Windows.Forms.GroupBox gbServer;
			System.Windows.Forms.Label lServerIp;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.gbRoom = new System.Windows.Forms.GroupBox();
			this.bCreateRoom = new System.Windows.Forms.Button();
			this.bJoinRoom = new System.Windows.Forms.Button();
			this.bAbout = new System.Windows.Forms.Button();
			this.lVersion = new System.Windows.Forms.Label();
			this.tbServerIp = new System.Windows.Forms.TextBox();
			this.bConnect = new System.Windows.Forms.Button();
			this.bDisconnect = new System.Windows.Forms.Button();
			gbServer = new System.Windows.Forms.GroupBox();
			lServerIp = new System.Windows.Forms.Label();
			this.gbRoom.SuspendLayout();
			gbServer.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbRoom
			// 
			this.gbRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbRoom.Controls.Add(this.bCreateRoom);
			this.gbRoom.Controls.Add(this.bJoinRoom);
			this.gbRoom.Enabled = false;
			this.gbRoom.Location = new System.Drawing.Point(12, 94);
			this.gbRoom.Name = "gbRoom";
			this.gbRoom.Size = new System.Drawing.Size(310, 114);
			this.gbRoom.TabIndex = 2;
			this.gbRoom.TabStop = false;
			this.gbRoom.Text = "Room";
			// 
			// bCreateRoom
			// 
			this.bCreateRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bCreateRoom.Location = new System.Drawing.Point(6, 19);
			this.bCreateRoom.Name = "bCreateRoom";
			this.bCreateRoom.Size = new System.Drawing.Size(298, 23);
			this.bCreateRoom.TabIndex = 1;
			this.bCreateRoom.Text = "Create Room";
			this.bCreateRoom.UseVisualStyleBackColor = true;
			this.bCreateRoom.Click += new System.EventHandler(this.bCreateRoom_Click);
			// 
			// bJoinRoom
			// 
			this.bJoinRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bJoinRoom.Location = new System.Drawing.Point(6, 48);
			this.bJoinRoom.Name = "bJoinRoom";
			this.bJoinRoom.Size = new System.Drawing.Size(298, 23);
			this.bJoinRoom.TabIndex = 2;
			this.bJoinRoom.Text = "Join Room";
			this.bJoinRoom.UseVisualStyleBackColor = true;
			this.bJoinRoom.Click += new System.EventHandler(this.bJoinRoom_Click);
			// 
			// bAbout
			// 
			this.bAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bAbout.Location = new System.Drawing.Point(241, 226);
			this.bAbout.Name = "bAbout";
			this.bAbout.Size = new System.Drawing.Size(75, 23);
			this.bAbout.TabIndex = 3;
			this.bAbout.Text = "About";
			this.bAbout.UseVisualStyleBackColor = true;
			// 
			// lVersion
			// 
			this.lVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lVersion.AutoSize = true;
			this.lVersion.Location = new System.Drawing.Point(15, 231);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(69, 13);
			this.lVersion.TabIndex = 4;
			this.lVersion.Text = "Version X.XX";
			// 
			// gbServer
			// 
			gbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			gbServer.Controls.Add(this.bDisconnect);
			gbServer.Controls.Add(this.bConnect);
			gbServer.Controls.Add(this.tbServerIp);
			gbServer.Controls.Add(lServerIp);
			gbServer.Location = new System.Drawing.Point(12, 12);
			gbServer.Name = "gbServer";
			gbServer.Size = new System.Drawing.Size(310, 76);
			gbServer.TabIndex = 1;
			gbServer.TabStop = false;
			gbServer.Text = "Server";
			// 
			// lServerIp
			// 
			lServerIp.AutoSize = true;
			lServerIp.Location = new System.Drawing.Point(6, 22);
			lServerIp.Name = "lServerIp";
			lServerIp.Size = new System.Drawing.Size(38, 13);
			lServerIp.TabIndex = 0;
			lServerIp.Text = "Server";
			// 
			// tbServerIp
			// 
			this.tbServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbServerIp.Location = new System.Drawing.Point(50, 19);
			this.tbServerIp.Name = "tbServerIp";
			this.tbServerIp.Size = new System.Drawing.Size(254, 20);
			this.tbServerIp.TabIndex = 1;
			// 
			// bConnect
			// 
			this.bConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bConnect.Location = new System.Drawing.Point(229, 45);
			this.bConnect.Name = "bConnect";
			this.bConnect.Size = new System.Drawing.Size(75, 23);
			this.bConnect.TabIndex = 2;
			this.bConnect.Text = "Connect";
			this.bConnect.UseVisualStyleBackColor = true;
			this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
			// 
			// bDisconnect
			// 
			this.bDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bDisconnect.Enabled = false;
			this.bDisconnect.Location = new System.Drawing.Point(148, 45);
			this.bDisconnect.Name = "bDisconnect";
			this.bDisconnect.Size = new System.Drawing.Size(75, 23);
			this.bDisconnect.TabIndex = 3;
			this.bDisconnect.Text = "Disconnect";
			this.bDisconnect.UseVisualStyleBackColor = true;
			this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 261);
			this.Controls.Add(gbServer);
			this.Controls.Add(this.lVersion);
			this.Controls.Add(this.bAbout);
			this.Controls.Add(this.gbRoom);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(300, 175);
			this.Name = "FormMain";
			this.Text = "Concept Launcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.gbRoom.ResumeLayout(false);
			gbServer.ResumeLayout(false);
			gbServer.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bJoinRoom;
		private System.Windows.Forms.Button bCreateRoom;
		private System.Windows.Forms.Button bAbout;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.Button bDisconnect;
		private System.Windows.Forms.Button bConnect;
		private System.Windows.Forms.TextBox tbServerIp;
		private System.Windows.Forms.GroupBox gbRoom;
	}
}