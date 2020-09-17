namespace ConceptBoard
{
	partial class FormCreateRoom
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
			System.Windows.Forms.Label lRoomPassword;
			System.Windows.Forms.Label lNickname;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateRoom));
			this.tbRoomPassword = new System.Windows.Forms.TextBox();
			this.bCreateRoom = new System.Windows.Forms.Button();
			this.tbNickname = new System.Windows.Forms.TextBox();
			lRoomPassword = new System.Windows.Forms.Label();
			lNickname = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lRoomPassword
			// 
			lRoomPassword.AutoSize = true;
			lRoomPassword.Location = new System.Drawing.Point(10, 15);
			lRoomPassword.Name = "lRoomPassword";
			lRoomPassword.Size = new System.Drawing.Size(84, 13);
			lRoomPassword.TabIndex = 990;
			lRoomPassword.Text = "Room Password";
			// 
			// lNickname
			// 
			lNickname.AutoSize = true;
			lNickname.Location = new System.Drawing.Point(10, 41);
			lNickname.Name = "lNickname";
			lNickname.Size = new System.Drawing.Size(55, 13);
			lNickname.TabIndex = 991;
			lNickname.Text = "Nickname";
			// 
			// tbRoomPassword
			// 
			this.tbRoomPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRoomPassword.Location = new System.Drawing.Point(98, 12);
			this.tbRoomPassword.MaxLength = 100;
			this.tbRoomPassword.Name = "tbRoomPassword";
			this.tbRoomPassword.Size = new System.Drawing.Size(224, 20);
			this.tbRoomPassword.TabIndex = 1;
			// 
			// bCreateRoom
			// 
			this.bCreateRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCreateRoom.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bCreateRoom.Location = new System.Drawing.Point(240, 76);
			this.bCreateRoom.Name = "bCreateRoom";
			this.bCreateRoom.Size = new System.Drawing.Size(82, 23);
			this.bCreateRoom.TabIndex = 3;
			this.bCreateRoom.Text = "Create Room";
			this.bCreateRoom.UseVisualStyleBackColor = true;
			this.bCreateRoom.Click += new System.EventHandler(this.bCreateRoom_Click);
			// 
			// tbNickname
			// 
			this.tbNickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbNickname.Location = new System.Drawing.Point(98, 38);
			this.tbNickname.MaxLength = 100;
			this.tbNickname.Name = "tbNickname";
			this.tbNickname.Size = new System.Drawing.Size(224, 20);
			this.tbNickname.TabIndex = 2;
			// 
			// FormCreateRoom
			// 
			this.AcceptButton = this.bCreateRoom;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 111);
			this.Controls.Add(this.tbNickname);
			this.Controls.Add(lNickname);
			this.Controls.Add(this.tbRoomPassword);
			this.Controls.Add(lRoomPassword);
			this.Controls.Add(this.bCreateRoom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(250, 150);
			this.Name = "FormCreateRoom";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create Room";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbRoomPassword;
		private System.Windows.Forms.Button bCreateRoom;
		private System.Windows.Forms.TextBox tbNickname;
	}
}