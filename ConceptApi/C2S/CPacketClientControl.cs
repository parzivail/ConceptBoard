using System;
using System.Collections.Generic;
using System.Text;

namespace ConceptApi.C2S
{
	public readonly struct CPacketClientControl
	{
		public readonly ClientControlFunction Function;

		public CPacketClientControl(ClientControlFunction function)
		{
			Function = function;
		}
	}
}
