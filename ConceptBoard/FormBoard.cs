using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConceptApi.Board;
using ConceptApi.C2S;
using ConceptApi.S2C;
using ConceptBoard.Graphics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Console = System.Console;

namespace ConceptBoard
{
	public partial class FormBoard : Form
	{
		private static readonly SizeF _bulbSize = new SizeF(0.6585f * 45, 45);
		private static readonly SizeF _exclamationSize = new SizeF(0.5956f * 45, 45);
		private static readonly SizeF _questionSize = new SizeF(0.5548f * 45, 45);
		private static readonly SizeF _cubeSize = new SizeF(0.4f * 45, 0.4f * 45);

		private TexturePointer _texBoard;

		private TexturePointer _texExclamationPoint;
		private TexturePointer _texQuestionMark;
		private TexturePointer _texCube;
		private TexturePointer _texBulb;

		private static Rectangle _boardRect = new Rectangle(100, 0, 850, 850);

		private PointF _mouseLocation = PointF.Empty;
		private PointF _mouseGrabOffset = PointF.Empty;
		private GamePieceType _selectedGamePiece;

		private GutterPiece[] _gutterPieces =
		{
			new GutterPiece(new Point(_boardRect.X / 2, 40), _bulbSize, GamePieceType.LightBulb),
			new GutterPiece(new Point(_boardRect.X / 2, 90), _cubeSize, GamePieceType.Cube | GamePieceType.Green),
			new GutterPiece(new Point(_boardRect.X / 2, 140), _questionSize, GamePieceType.QuestionMark | GamePieceType.Green),
			new GutterPiece(new Point(_boardRect.X / 2, 190), _cubeSize, GamePieceType.Cube),
			new GutterPiece(new Point(_boardRect.X / 2, 240), _exclamationSize, GamePieceType.Exclamation),
			new GutterPiece(new Point(_boardRect.X / 2, 290), _cubeSize,GamePieceType.Cube | GamePieceType.Blue),
			new GutterPiece(new Point(_boardRect.X / 2, 340), _exclamationSize, GamePieceType.Exclamation | GamePieceType.Blue),
			new GutterPiece(new Point(_boardRect.X / 2, 390), _cubeSize,GamePieceType.Cube | GamePieceType.Red),
			new GutterPiece(new Point(_boardRect.X / 2, 440), _exclamationSize, GamePieceType.Exclamation | GamePieceType.Red),
			new GutterPiece(new Point(_boardRect.X / 2, 490), _cubeSize,GamePieceType.Cube | GamePieceType.Yellow),
			new GutterPiece(new Point(_boardRect.X / 2, 540), _exclamationSize, GamePieceType.Exclamation | GamePieceType.Yellow),
		};

		public FormBoard(string roomId)
		{
			InitializeComponent();

			lRoomId.Text = roomId;
			GameClient.SyncRoom += GameClientOnSyncRoom;
			GameClient.FloatingPiece += GameClientOnFloatingPiece;
			GameClient.TurnStarted += GameClientOnTurnStarted;
		}

		private void GameClientOnTurnStarted(object sender, EventArgs e)
		{
			new FormCard().ShowDialog();
		}

		private void FormBoard_Load(object sender, EventArgs e)
		{
			glBoard.MakeCurrent();

			GL.ClearColor(Color4.White);

			_texBoard = TexturePointer.Create(Resources.concept_board);
			_texExclamationPoint = TexturePointer.Create(Resources.concept_exclamationpoint);
			_texQuestionMark = TexturePointer.Create(Resources.concept_questionmark);
			_texCube = TexturePointer.Create(Resources.concept_cube);
			_texBulb = TexturePointer.Create(Resources.concept_bulb);
		}

		private void GameClientOnSyncRoom(object sender, SPacketSyncRoom e)
		{
			lRoomId.Text = GameClient.Room.Id;

			playerList.CurrentPlayer = e.CurrentPlayer;
			playerList.Players = e.Players.ToList();

			playerList.Invalidate();
			glBoard.Invalidate();

			bNextRound.Enabled = GameClient.Room.CurrentPlayer == GameClient.Player.Id;
		}

		private void GameClientOnFloatingPiece(object sender, SPacketFloatingPiece e)
		{
			_selectedGamePiece = e.Type;

			_mouseGrabOffset = PointF.Empty;
			_mouseLocation = new PointF(e.X, e.Y);

			glBoard.Invalidate();
		}

		private void bNextRound_Click(object sender, EventArgs e)
		{
			GameClient.NextRound();
		}

		private void bLeave_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FormBoard_FormClosing(object sender, FormClosingEventArgs e)
		{
			GameClient.LeaveRoom();
		}

		private void GlBoardPaint(object sender, PaintEventArgs e)
		{
			var width = glBoard.ClientSize.Width;
			var height = glBoard.ClientSize.Height;

			glBoard.MakeCurrent();
			GL.Viewport(0, 0, width, height);

			GL.Clear(ClearBufferMask.ColorBufferBit |
					 ClearBufferMask.DepthBufferBit |
					 ClearBufferMask.StencilBufferBit);

			GL.PushMatrix();

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, width, height, 0, -100, 100);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

			GL.PushMatrix();

			GL.Translate(_boardRect.X, _boardRect.Y, 0);

			GL.Color3(Color.White);
			GL.BindTexture(TextureTarget.Texture2D, _texBoard.Id);
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord2(1, 0);

			GL.Vertex3(_boardRect.Width, 0, 0);
			GL.TexCoord2(1, 1);

			GL.Vertex3(_boardRect.Width, _boardRect.Height, 0);
			GL.TexCoord2(0, 1);

			GL.Vertex3(0, _boardRect.Height, 0);
			GL.TexCoord2(0, 0);
			GL.End();

			GL.PopMatrix();

			GL.PushMatrix();

			foreach (var gutterPiece in _gutterPieces)
				DrawPiece(gutterPiece.Piece, gutterPiece.Position.X, gutterPiece.Position.Y);

			if (GameClient.Room != null)
				foreach (var piece in GameClient.Room.Board)
					DrawPiece(piece.Type, piece.X, piece.Y);

			if (_selectedGamePiece != 0)
				DrawPiece(_selectedGamePiece, _mouseLocation.X + _mouseGrabOffset.X,
					_mouseLocation.Y + _mouseGrabOffset.Y);

			GL.PopMatrix();

			GL.PopMatrix();

			glBoard.SwapBuffers();
		}

		public GutterPiece GetGutterPieceAt(PointF location)
		{
			return _gutterPieces.FirstOrDefault(piece => piece.Bounds.Contains(location));
		}

		public GamePiece GetBoardPieceAt(PointF location)
		{
			foreach (var piece in GameClient.Room.Board)
			{
				var size = GetPieceSize(piece.Type);
				var bounds = new RectangleF(piece.X - size.Width / 2f, piece.Y - size.Height / 2f, size.Width, size.Height);
				if (bounds.Contains(location))
					return piece;
			}

			return null;
		}

		private SizeF GetPieceSize(GamePieceType piece)
		{
			if (piece.HasFlag(GamePieceType.QuestionMark))
				return _questionSize;

			if (piece.HasFlag(GamePieceType.Exclamation))
				return _exclamationSize;

			if (piece.HasFlag(GamePieceType.Cube))
				return _cubeSize;

			return _bulbSize;
		}

		private void DrawPiece(GamePieceType piece, float x, float y)
		{
			var tint = Color.White;
			if (piece.HasFlag(GamePieceType.Blue))
				tint = Color.DeepSkyBlue;
			else if (piece.HasFlag(GamePieceType.Green))
				tint = Color.LawnGreen;
			else if (piece.HasFlag(GamePieceType.Red))
				tint = Color.OrangeRed;
			else if (piece.HasFlag(GamePieceType.Yellow))
				tint = Color.Yellow;

			if (piece.HasFlag(GamePieceType.QuestionMark))
				DrawQuestion(x, y, tint);
			else if (piece.HasFlag(GamePieceType.Exclamation))
				DrawExclamation(x, y, tint);
			else if (piece.HasFlag(GamePieceType.Cube))
				DrawCube(x, y, tint);
			else if (piece.HasFlag(GamePieceType.LightBulb))
				DrawBulb(x, y, tint);
		}

		private void DrawBulb(float boardX, float boardY, Color tint)
		{
			GL.Color3(tint);

			GL.PushMatrix();
			GL.Translate(boardX - _bulbSize.Width / 2f, boardY - _bulbSize.Height / 2f, 0);
			GL.BindTexture(TextureTarget.Texture2D, _texBulb.Id);
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord2(1, 0);

			GL.Vertex3(_bulbSize.Width, 0, 0);
			GL.TexCoord2(1, 1);

			GL.Vertex3(_bulbSize.Width, _bulbSize.Height, 0);
			GL.TexCoord2(0, 1);

			GL.Vertex3(0, _bulbSize.Height, 0);
			GL.TexCoord2(0, 0);
			GL.End();
			GL.PopMatrix();
		}

		private void DrawExclamation(float boardX, float boardY, Color tint)
		{
			GL.Color3(tint);

			GL.PushMatrix();
			GL.Translate(boardX - _exclamationSize.Width / 2f, boardY - _exclamationSize.Height / 2f, 0);
			GL.BindTexture(TextureTarget.Texture2D, _texExclamationPoint.Id);
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord2(1, 0);

			GL.Vertex3(_exclamationSize.Width, 0, 0);
			GL.TexCoord2(1, 1);

			GL.Vertex3(_exclamationSize.Width, _exclamationSize.Height, 0);
			GL.TexCoord2(0, 1);

			GL.Vertex3(0, _exclamationSize.Height, 0);
			GL.TexCoord2(0, 0);
			GL.End();
			GL.PopMatrix();
		}

		private void DrawQuestion(float boardX, float boardY, Color tint)
		{
			GL.Color3(tint);

			GL.PushMatrix();
			GL.Translate(boardX - _questionSize.Width / 2f, boardY - _questionSize.Height / 2f, 0);
			GL.BindTexture(TextureTarget.Texture2D, _texQuestionMark.Id);
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord2(1, 0);

			GL.Vertex3(_questionSize.Width, 0, 0);
			GL.TexCoord2(1, 1);

			GL.Vertex3(_questionSize.Width, _questionSize.Height, 0);
			GL.TexCoord2(0, 1);

			GL.Vertex3(0, _questionSize.Height, 0);
			GL.TexCoord2(0, 0);
			GL.End();
			GL.PopMatrix();
		}

		private void DrawCube(float boardX, float boardY, Color tint)
		{
			GL.Color3(tint);

			GL.PushMatrix();
			GL.Translate(boardX - _cubeSize.Width / 2f, boardY - _cubeSize.Height / 2f, 0);
			GL.BindTexture(TextureTarget.Texture2D, _texCube.Id);
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord2(1, 0);

			GL.Vertex3(_cubeSize.Width, 0, 0);
			GL.TexCoord2(1, 1);

			GL.Vertex3(_cubeSize.Width, _cubeSize.Height, 0);
			GL.TexCoord2(0, 1);

			GL.Vertex3(0, _cubeSize.Height, 0);
			GL.TexCoord2(0, 0);
			GL.End();
			GL.PopMatrix();
		}

		private void glBoard_MouseMove(object sender, MouseEventArgs e)
		{
			if (!GameClient.MyTurn)
				return;

			_mouseLocation = new PointF(e.X, e.Y);

			if (_selectedGamePiece != 0)
				GameClient.UpdateFloatingPiece(_selectedGamePiece, e.X + _mouseGrabOffset.X, e.Y + _mouseGrabOffset.Y);

			glBoard.Invalidate();
		}

		private void glBoard_MouseDown(object sender, MouseEventArgs e)
		{
			if (!GameClient.MyTurn)
				return;

			if (_boardRect.Contains((int)_mouseLocation.X, (int)_mouseLocation.Y))
			{
				var piece = GetBoardPieceAt(_mouseLocation);
				if (piece == null) return;

				_mouseGrabOffset = new PointF(piece.X - _mouseLocation.X, piece.Y - _mouseLocation.Y);
				_selectedGamePiece = piece.Type;

				GameClient.DestroyPiece(piece.Id);
			}
			else
			{
				var pieceOver = GetGutterPieceAt(_mouseLocation);
				if (pieceOver == null) return;

				_mouseGrabOffset = new PointF(pieceOver.Bounds.X + pieceOver.Bounds.Width / 2 - _mouseLocation.X,
					pieceOver.Bounds.Y + pieceOver.Bounds.Height / 2 - _mouseLocation.Y);
				_selectedGamePiece = pieceOver.Piece;
			}
		}

		private void glBoard_MouseUp(object sender, MouseEventArgs e)
		{
			if (!GameClient.MyTurn) return;

			if (_selectedGamePiece != 0 && _boardRect.Contains((int)_mouseLocation.X, (int)_mouseLocation.Y))
				GameClient.CreatePiece(_selectedGamePiece, e.X + _mouseGrabOffset.X, e.Y + _mouseGrabOffset.Y);

			_selectedGamePiece = 0;
			GameClient.UpdateFloatingPiece(0, 0, 0);
		}
	}

	public class GutterPiece
	{
		public Point Position { get; }
		public SizeF Size { get; }
		public GamePieceType Piece { get; }
		public RectangleF Bounds { get; }

		public GutterPiece(Point position, SizeF size, GamePieceType piece)
		{
			Position = position;
			Size = size;
			Piece = piece;

			Bounds = new RectangleF(new PointF(Position.X - Size.Width / 2f, Position.Y - Size.Height / 2f), Size);
		}
	}
}
