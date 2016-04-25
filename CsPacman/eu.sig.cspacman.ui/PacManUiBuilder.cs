using System;
using System.Collections.Generic;
using System.Windows.Forms;

using eu.sig.cspacman.game;

namespace eu.sig.cspacman.ui
{
	public class PacManUiBuilder {

		private const string STOP_CAPTION = "Stop";

		private const string START_CAPTION = "Start";

		private readonly IDictionary<string, Action> buttons;

		private readonly IDictionary<Keys, Action> keyMappings;

		private bool defaultButtons;

		private ScorePanel.ScoreFormatter scoreFormatter = null;

		public PacManUiBuilder() {
			this.defaultButtons = false;
			this.buttons = new Dictionary<string, Action>();
			this.keyMappings = new Dictionary<Keys, Action>();
		}

		public PacManUI build(Game game) {
			System.Diagnostics.Debug.Assert(game != null);

			if (defaultButtons) {
				addStartButton(game);
				addStopButton(game);
			}
			return new PacManUI(game, buttons, keyMappings, scoreFormatter);
		}

		private void addStopButton(Game game) {
			System.Diagnostics.Debug.Assert(game != null);

			buttons[STOP_CAPTION] = delegate() {
					game.Stop();
			};
		}

		private void addStartButton(Game game) {
			System.Diagnostics.Debug.Assert(game != null);

			buttons[START_CAPTION] = delegate() {
					game.Start();
			};
		}

		public PacManUiBuilder addKey(Keys keyCode, Action action) {
			System.Diagnostics.Debug.Assert(action != null);

			keyMappings[keyCode] = action;
			return this;
		}
		
		public PacManUiBuilder addButton(String caption, Action action) {
			System.Diagnostics.Debug.Assert(caption != null);
			System.Diagnostics.Debug.Assert(caption.Length != 0);
			System.Diagnostics.Debug.Assert(action != null);

			buttons[caption] = action;
			return this;
		}

		public PacManUiBuilder withDefaultButtons() {
			defaultButtons = true;
			buttons[START_CAPTION] = null;
			buttons[STOP_CAPTION] = null;
			return this;
		}

		public PacManUiBuilder withScoreFormatter(ScorePanel.ScoreFormatter sf) {
			scoreFormatter = sf;
			return this;
		}
	}
}
