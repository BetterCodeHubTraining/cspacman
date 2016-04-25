using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace eu.sig.cspacman.ui
{
	internal class ButtonPanel : Panel {

		internal ButtonPanel(IDictionary<string, Action> buttons, Form parent) : base() {
			System.Diagnostics.Debug.Assert(buttons != null);
			System.Diagnostics.Debug.Assert(parent != null);

			foreach (string caption in buttons.Keys) {
				Button button = new Button();
				button.Text = caption;
				button.Click += (s, e) => {
					buttons[caption]();
					parent.Activate();
				};
				Controls.Add(button);
			}
			DoHorizontalLayout();
		}

		private void DoHorizontalLayout() {
			int x = 0;
			int maxHeight = 0;
			foreach (Control c in Controls) {
				c.Location = new Point(x, 0);
				x += c.Size.Width;
				maxHeight = Math.Max(c.Size.Height, maxHeight);
			}
			Size = new Size(x, maxHeight);
		}
	}


	// TODO: move this extension method to a more logical place.
	internal static class ExtensionMethods {
		internal static T Mapreduce<T>(this Control.ControlCollection list, Func<Control, T, T> f) where T : new() {
			T result = new T();
			foreach (Control c in list) {
				result = f(c, result);
			}
			return result;
		}
	}
}
