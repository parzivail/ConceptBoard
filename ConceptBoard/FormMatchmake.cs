using System;
using System.Windows.Forms;

namespace ConceptBoard
{
	public partial class FormMatchmake : Form
	{
		public string RoomId { get; set; }
		public string RoomPassword { get; set; }
		public string Nickname { get; set; }

		public FormMatchmake()
		{
			InitializeComponent();
		}

		private void bJoinRoom_Click(object sender, EventArgs e)
		{
			RoomId = tbRoomId.Text;
			RoomPassword = tbRoomPassword.Text;
			Nickname = tbNickname.Text;

			Close();
		}
	}
}
