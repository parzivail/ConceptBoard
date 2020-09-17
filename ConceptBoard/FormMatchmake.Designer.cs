namespace ConceptBoard
{
	partial class FormMatchmake
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
			System.Windows.Forms.Label lNickname;
			System.Windows.Forms.Label lRoomId;
			System.Windows.Forms.Label lRoomPassword;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMatchmake));
			this.tbRoomId = new System.Windows.Forms.TextBox();
			this.tbNickname = new System.Windows.Forms.TextBox();
			this.bJoinRoom = new System.Windows.Forms.Button();
			this.tbRoomPassword = new System.Windows.Forms.TextBox();
			lNickname = new System.Windows.Forms.Label();
			lRoomId = new System.Windows.Forms.Label();
			lRoomPassword = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lNickname
			// 
			lNickname.AutoSize = true;
			lNickname.Location = new System.Drawing.Point(10, 67);
			lNickname.Name = "lNickname";
			lNickname.Size = new System.Drawing.Size(55, 13);
			lNickname.TabIndex = 1;
			lNickname.Text = "Nickname";
			// 
			// lRoomId
			// 
			lRoomId.AutoSize = true;
			lRoomId.Location = new System.Drawing.Point(10, 15);
			lRoomId.Name = "lRoomId";
			lRoomId.Size = new System.Drawing.Size(49, 13);
			lRoomId.TabIndex = 2;
			lRoomId.Text = "Room ID";
			// 
			// lRoomPassword
			// 
			lRoomPassword.AutoSize = true;
			lRoomPassword.Location = new System.Drawing.Point(10, 41);
			lRoomPassword.Name = "lRoomPassword";
			lRoomPassword.Size = new System.Drawing.Size(84, 13);
			lRoomPassword.TabIndex = 7;
			lRoomPassword.Text = "Room Password";
			// 
			// tbRoomId
			// 
			this.tbRoomId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRoomId.Location = new System.Drawing.Point(98, 12);
			this.tbRoomId.MaxLength = 100;
			this.tbRoomId.Name = "tbRoomId";
			this.tbRoomId.Size = new System.Drawing.Size(224, 20);
			this.tbRoomId.TabIndex = 1;
			// 
			// tbNickname
			// 
			this.tbNickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbNickname.Location = new System.Drawing.Point(98, 64);
			this.tbNickname.MaxLength = 100;
			this.tbNickname.Name = "tbNickname";
			this.tbNickname.Size = new System.Drawing.Size(224, 20);
			this.tbNickname.TabIndex = 3;
			// 
			// bJoinRoom
			// 
			this.bJoinRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bJoinRoom.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bJoinRoom.Location = new System.Drawing.Point(247, 101);
			this.bJoinRoom.Name = "bJoinRoom";
			this.bJoinRoom.Size = new System.Drawing.Size(75, 23);
			this.bJoinRoom.TabIndex = 4;
			this.bJoinRoom.Text = "Join Room";
			this.bJoinRoom.UseVisualStyleBackColor = true;
			this.bJoinRoom.Click += new System.EventHandler(this.bJoinRoom_Click);
			// 
			// tbRoomPassword
			// 
			this.tbRoomPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbRoomPassword.Location = new System.Drawing.Point(98, 38);
			this.tbRoomPassword.MaxLength = 100;
			this.tbRoomPassword.Name = "tbRoomPassword";
			this.tbRoomPassword.Size = new System.Drawing.Size(224, 20);
			this.tbRoomPassword.TabIndex = 2;
			// 
			// FormMatchmake
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 136);
			this.Controls.Add(this.tbRoomPassword);
			this.Controls.Add(lRoomPassword);
			this.Controls.Add(this.bJoinRoom);
			this.Controls.Add(this.tbNickname);
			this.Controls.Add(this.tbRoomId);
			this.Controls.Add(lRoomId);
			this.Controls.Add(lNickname);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(250, 175);
			this.Name = "FormMatchmake";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Join Room";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox tbRoomId;
		private System.Windows.Forms.TextBox tbNickname;
		private System.Windows.Forms.Button bJoinRoom;
		private System.Windows.Forms.TextBox tbRoomPassword;
	}
}