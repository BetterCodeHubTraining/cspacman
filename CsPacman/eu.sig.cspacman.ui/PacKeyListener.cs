using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eu.sig.cspacman.ui
{
	internal class PacKeyListener {

		private readonly IDictionary<Keys, Action> mappings;

		internal PacKeyListener(IDictionary<Keys, Action> keyMappings) {
			System.Diagnostics.Debug.Assert(keyMappings != null);
			this.mappings = keyMappings;
		}
			
		public void DoKeyDown(Keys k) {
			if (mappings.ContainsKey(k)) {
				Action action = mappings[k];
				if (action != null) {
					action();
				}
			}
		}       			
	}
}