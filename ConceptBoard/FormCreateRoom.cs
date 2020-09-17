using System;
using System.Windows.Forms;

namespace ConceptBoard
{
	public partial class FormCreateRoom : Form
	{
		public string RoomKey { get; set; }
		public string Nickname { get; set; }

		public FormCreateRoom()
		{
			InitializeComponent();
		}

		private void bCreateRoom_Click(object sender, EventArgs e)
		{
			RoomKey = tbRoomPassword.Text;
			Nickname = tbNickname.Text;

			Close();
		}
	}
}
