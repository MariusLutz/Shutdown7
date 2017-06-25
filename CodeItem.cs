using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shutdown7
{
	public class CodeItem
	{
		#region " Properties/Constants... "

		public string Text { get; set; }
		public string Image { get; set; }
		#endregion

		#region " Constructors... "
		public CodeItem()
		{
		}

		public CodeItem(string text, string image)
		{
			Text = text;
			Image = image;
		}
		#endregion

		#region " Overrides... "
		public override string ToString()
		{
			return Text;
		}
		#endregion
	}
}
