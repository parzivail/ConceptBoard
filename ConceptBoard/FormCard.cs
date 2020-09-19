using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConceptBoard
{
	public partial class FormCard : Form
	{
		private readonly Random _random = new Random();
		private readonly string[] _easy;
		private readonly string[] _medium;
		private readonly string[] _hard;

		public FormCard()
		{
			InitializeComponent();
			_easy = PickN(Resources.words_easy, 3);
			_medium = PickN(Resources.words_medium, 3);
			_hard = PickN(Resources.words_hard, 3);
		}

		public static GraphicsPath RoundedRect(RectangleF bounds, int radius)
		{
			var diameter = radius * 2;
			var size = new Size(diameter, diameter);
			var arc = new RectangleF(bounds.Location, size);
			var path = new GraphicsPath();

			if (radius == 0)
			{
				path.AddRectangle(bounds);
				return path;
			}

			// top left arc  
			path.AddArc(arc, 180, 90);

			// top right arc  
			arc.X = bounds.Right - diameter;
			//path.AddArc(arc, 270, 90);
			path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top);

			// bottom right arc  
			arc.Y = bounds.Bottom - diameter;
			path.AddArc(arc, 0, 90);

			// bottom left arc 
			arc.X = bounds.Left;
			//path.AddArc(arc, 90, 90);
			path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom);

			path.CloseFigure();
			return path;
		}

		private void FormCard_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			var fgBrush = new SolidBrush(ForeColor);
			var redPen = new Pen(Color.Red, 3);
			var bluePen = new Pen(Color.Blue, 3);
			var grayPen = new Pen(Color.DarkGray, 3);

			var redPenDot = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dot };
			var bluePenDot = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dot };
			var grayPenDot = new Pen(Color.DarkGray, 2) { DashStyle = DashStyle.Dot };

			var y = 0;

			var size = new SizeF(Width - 40, Font.GetHeight() * 1.2f);
			var dy = size.Height;

			g.DrawPath(redPen, RoundedRect(new RectangleF(10, (y + 1) * dy, size.Width, (int)(size.Height * 3)), 20));
			DrawString(g, _easy[0], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(redPenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _easy[1], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(redPenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _easy[2], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));

			y++;

			g.DrawPath(bluePen, RoundedRect(new RectangleF(10, (y + 1) * dy, size.Width, (int)(size.Height * 3)), 20));
			DrawString(g, _medium[0], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(bluePenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _medium[1], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(bluePenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _medium[2], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));

			y++;

			g.DrawPath(grayPen, RoundedRect(new RectangleF(10, (y + 1) * dy, size.Width, (int)(size.Height * 3)), 20));
			DrawString(g, _hard[0], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(grayPenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _hard[1], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));
			g.DrawLine(grayPenDot, 20, (y + 1) * dy, Width - 40, (y + 1) * dy);
			DrawString(g, _hard[2], Font, fgBrush, new RectangleF(new PointF(20, ++y * dy), size));

			y++;
		}

		private void DrawString(System.Drawing.Graphics g, string s, Font font, Brush brush, RectangleF layoutRect)
		{
			g.DrawString(s, GetAdjustedFont(g, s, font, (int)layoutRect.Width, (int)font.Size, 4, true), brush, layoutRect);
		}

		public Font GetAdjustedFont(System.Drawing.Graphics g, string graphicString, Font originalFont, int containerWidth, int maxFontSize, int minFontSize, bool smallestOnFail)
		{
			Font testFont = null;
			// We utilize MeasureString which we get via a control instance           
			for (float adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize -= 0.5f)
			{
				testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

				// Test the string with the new size
				SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

				if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
				{
					// Good font, return it
					return testFont;
				}
			}

			// If you get here there was no fontsize that worked
			// return minimumSize or original?
			if (smallestOnFail)
			{
				return testFont;
			}
			else
			{
				return originalFont;
			}
		}

		private string[] PickN(string file, int n)
		{
			var lines = file.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			var indices = new int[n];
			for (var i = 0; i < n; i++)
			{
				int idx;
				do
				{
					idx = _random.Next(lines.Length);
				} while (indices.Contains(idx));

				indices[i] = idx;
			}

			var values = new string[n];
			for (var i = 0; i < n; i++) values[i] = lines[indices[i]];

			return values;
		}

		private void FormCard_Resize(object sender, EventArgs e)
		{
			Invalidate();
		}
	}
}
