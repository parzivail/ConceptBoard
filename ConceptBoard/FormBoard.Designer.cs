namespace ConceptBoard
{
	partial class FormBoard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBoard));
			this.lRoomId = new System.Windows.Forms.Label();
			this.bLeave = new System.Windows.Forms.Button();
			this.bNextRound = new System.Windows.Forms.Button();
			this.glBoard = new OpenTK.GLControl();
			this.playerList = new ConceptBoard.Controls.PlayerList();
			this.SuspendLayout();
			// 
			// lRoomId
			// 
			this.lRoomId.AutoSize = true;
			this.lRoomId.Font = new System.Drawing.Font("Lucida Sans Typewriter", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lRoomId.Location = new System.Drawing.Point(43, 9);
			this.lRoomId.Name = "lRoomId";
			this.lRoomId.Size = new System.Drawing.Size(101, 39);
			this.lRoomId.TabIndex = 0;
			this.lRoomId.Text = "XXXX";
			// 
			// bLeave
			// 
			this.bLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.bLeave.Location = new System.Drawing.Point(12, 839);
			this.bLeave.Name = "bLeave";
			this.bLeave.Size = new System.Drawing.Size(168, 23);
			this.bLeave.TabIndex = 2;
			this.bLeave.Text = "Leave";
			this.bLeave.UseVisualStyleBackColor = true;
			this.bLeave.Click += new System.EventHandler(this.bLeave_Click);
			// 
			// bNextRound
			// 
			this.bNextRound.Location = new System.Drawing.Point(12, 51);
			this.bNextRound.Name = "bNextRound";
			this.bNextRound.Size = new System.Drawing.Size(168, 23);
			this.bNextRound.TabIndex = 3;
			this.bNextRound.Text = "Next Round";
			this.bNextRound.UseVisualStyleBackColor = true;
			this.bNextRound.Click += new System.EventHandler(this.bNextRound_Click);
			// 
			// glBoard
			// 
			this.glBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.glBoard.BackColor = System.Drawing.Color.Black;
			this.glBoard.Location = new System.Drawing.Point(186, 12);
			this.glBoard.Name = "glBoard";
			this.glBoard.Size = new System.Drawing.Size(950, 850);
			this.glBoard.TabIndex = 5;
			this.glBoard.VSync = false;
			this.glBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.GlBoardPaint);
			this.glBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glBoard_MouseDown);
			this.glBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glBoard_MouseMove);
			this.glBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glBoard_MouseUp);
			// 
			// playerList
			// 
			this.playerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.playerList.BackColor = System.Drawing.Color.White;
			this.playerList.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.playerList.Location = new System.Drawing.Point(12, 80);
			this.playerList.Name = "playerList";
			this.playerList.Size = new System.Drawing.Size(168, 753);
			this.playerList.TabIndex = 4;
			this.playerList.Text = "playerList1";
			// 
			// FormBoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1148, 874);
			this.Controls.Add(this.glBoard);
			this.Controls.Add(this.playerList);
			this.Controls.Add(this.bNextRound);
			this.Controls.Add(this.bLeave);
			this.Controls.Add(this.lRoomId);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormBoard";
			this.Text = "Concept";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBoard_FormClosing);
			this.Load += new System.EventHandler(this.FormBoard_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lRoomId;
		private System.Windows.Forms.Button bLeave;
		private System.Windows.Forms.Button bNextRound;
		private Controls.PlayerList playerList;
		private OpenTK.GLControl glBoard;
	}
}