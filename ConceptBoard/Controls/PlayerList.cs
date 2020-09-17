using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConceptApi;
using ConceptApi.Board;

namespace ConceptBoard.Controls
{
	internal partial class PlayerList : Control
	{
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<Player> Players { get; set; }

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid CurrentPlayer { get; set; }

		public PlayerList()
		{
			Players = new List<Player>();
		}

		/// <inheritdoc />
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);

			var g = pevent.Graphics;

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.Clear(BackColor);

			var fgBrush = new SolidBrush(ForeColor);

			var x = 2;
			var y = 2f;

			var lineSpacing = Font.GetHeight();

			foreach (var player in Players)
			{
				var playerFont = new Font(Font, CurrentPlayer == player.Id ? FontStyle.Bold : FontStyle.Regular);

				g.DrawString(player.Nickname, playerFont, fgBrush, x, y);

				y += lineSpacing;
			}
		}
	}
}
