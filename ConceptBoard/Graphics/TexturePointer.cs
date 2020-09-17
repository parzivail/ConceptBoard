using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace ConceptBoard.Graphics
{
	public class TexturePointer : IDisposable
    {
        public int Id { get; }

        public TexturePointer(int textureId)
        {
	        Id = textureId;
        }

        public static TexturePointer Create(Bitmap bitmap)
        {
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out int tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int) TextureWrapMode.MirroredRepeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int) TextureWrapMode.MirroredRepeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new TexturePointer(tex);
        }

        public static TexturePointer Create()
        {
	        var tex = GL.GenTexture();
			return new TexturePointer(tex);
        }

        private void ReleaseUnmanagedResources()
        {
			GL.DeleteTexture(Id);
        }

        public void Dispose()
        {
	        ReleaseUnmanagedResources();
	        GC.SuppressFinalize(this);
        }
    }
}
