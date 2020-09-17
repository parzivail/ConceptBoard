namespace ConceptServer
{
	/// <summary>
	/// Provides functions to translate arbitrary data to a unique, reversible sequence
	/// of alpha-only characters
	/// </summary>
	internal class AlphaOnlyDataEncoder
	{
		private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static string Encode(byte[] data)
		{
			var pos = 0;
			var buf = new char[data.Length << 1];

			int i;
			while ((i = pos) < data.Length)
			{
				buf[i << 1] = Alphabet[data[pos] >> 4];
				buf[(i << 1) + 1] = Alphabet[(Alphabet.Length - 1) - (data[pos] & 0x0F)];
				pos++;
			}

			return new string(buf);
		}

		public static byte[] Decode(string text)
		{
			if (text.Length % 2 != 0)
				return null;

			var nPos = new int[2];
			var buf = new byte[text.Length >> 1];

			for (var i = 0; i < text.Length >> 1; i++)
			{
				nPos[0] = Alphabet.IndexOf(text[i << 1]);
				nPos[1] = (Alphabet.Length - 1) - Alphabet.IndexOf(text[(i << 1) + 1]);
				if (nPos[0] < 0 || nPos[1] < 0)
					return null;

				buf[i] = (byte)((nPos[0] << 4) | nPos[1]);
			}
			return buf;
		}
	}
}