using System;
using System.Windows.Forms;
using ConceptApi;
using ConceptApi.S2C;

namespace ConceptBoard
{
	public partial class FormMain : Form
	{
		readonly FormCreateRoom _createDialog = new FormCreateRoom();
		readonly FormMatchmake _joinDialog = new FormMatchmake();

		public FormMain()
		{
			InitializeComponent();

			GameClient.Connected += OnConnected;
			GameClient.Disconnected += OnDisconnected;
		}

		private void OnConnected(object sender, EventArgs e)
		{
			gbRoom.Enabled = true;
			bConnect.Enabled = false;
			bDisconnect.Enabled = true;
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			gbRoom.Enabled = false;
			bConnect.Enabled = true;
			bDisconnect.Enabled = false;
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			GameClient.Disconnect();
		}

		private void bCreateRoom_Click(object sender, EventArgs e)
		{
			if (_createDialog.ShowDialog(this) != DialogResult.OK)
				return;
			
			bCreateRoom.Enabled = false;

			GameClient.CreateRoomResponse += OnCreateRoomResponse;
			GameClient.BeginCreateRoom(_createDialog.RoomKey, _createDialog.Nickname);
		}

		private void OnCreateRoomResponse(object o, SPacketCreateRoom packet)
		{
			Invoke(new Action(() =>
			{
				GameClient.CreateRoomResponse -= OnCreateRoomResponse;

				switch (packet.Status)
				{
					case CreateRoomStatus.Success:
						ShowBoard(packet.RoomId);
						break;
					case CreateRoomStatus.MatchmakerFull:
						MessageBox.Show(this, "The matchmaker is currently full", "Concept Matchmaker", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				bCreateRoom.Enabled = true;
			}));
		}

		private void ShowBoard(string roomId)
		{
			var boardForm = new FormBoard(roomId);
			boardForm.FormClosing += (sender, args) => Show();

			boardForm.Show();
			Hide();
		}

		private void bJoinRoom_Click(object sender, EventArgs e)
		{
			if (_joinDialog.ShowDialog(this) != DialogResult.OK)
				return;
			
			bJoinRoom.Enabled = false;

			GameClient.JoinRoomResponse += OnJoinRoomResponse;
			GameClient.BeginJoinRoom(_joinDialog.RoomId, _joinDialog.RoomPassword, _joinDialog.Nickname);
		}

		private void OnJoinRoomResponse(object o, SPacketJoinRoom packet)
		{
			Invoke(new Action(() =>
			{
				GameClient.JoinRoomResponse -= OnJoinRoomResponse;

				switch (packet.Status)
				{
					case JoinRoomStatus.Success:
						ShowBoard(_joinDialog.RoomId);
						break;
					case JoinRoomStatus.RoomNotFound:
						MessageBox.Show(this, "Unable to find room", "Concept Matchmaker", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					case JoinRoomStatus.RoomKeyIncorrect:
						MessageBox.Show(this, "Incorrect room key", "Concept Matchmaker", MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				bJoinRoom.Enabled = true;
			}));
		}

		private void bConnect_Click(object sender, EventArgs e)
		{
			GameClient.Connect(tbServerIp.Text);
		}

		private void bDisconnect_Click(object sender, EventArgs e)
		{
			GameClient.Disconnect();
		}
	}
}
