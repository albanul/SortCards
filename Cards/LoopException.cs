using System;

namespace Cards
{
	public class LoopException : Exception
	{
		public LoopException(string message) : base(message)
		{}
	}
}
