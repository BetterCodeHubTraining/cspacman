using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using eu.sig.cspacman.level;

namespace eu.sig.cspacman.ui
{
	public class ScorePanel : TableLayoutPanel {

		private readonly IDictionary<Player, Label> scoreLabels;

		public delegate string ScoreFormatter(Player p);

		public static readonly ScoreFormatter DEFAULT_SCORE_FORMATTER =
			p => String.Format("Score: {0}", p.Score);

		private ScoreFormatter scoreFormatter = DEFAULT_SCORE_FORMATTER;

		public ScorePanel(IList<Player> players) : base() {
			System.Diagnostics.Debug.Assert(players != null);

			for (int i = 1; i <= players.Count; i++) {
				Label label = new Label();
				label.AutoSize = true;
				label.Text = $"Player: {i}";
				label.TextAlign = ContentAlignment.BottomRight;
				this.Controls.Add(label, i - 1, 0);
			}
			scoreLabels = new Dictionary<Player, Label>();
			foreach (Player p in players) {
				Label scoreLabel = new Label();
				scoreLabel.AutoSize = true;
				scoreLabel.Text = "0";
				scoreLabel.TextAlign = ContentAlignment.BottomRight;
				scoreLabels[p] = scoreLabel;
				this.Controls.Add(scoreLabel, -1, 1);
			}
			Size = new Size(60, 30);
		}

		internal void refresh() {
			foreach (Player p in scoreLabels.Keys) {
				string score = "";
				if (!p.IsAlive) {
					score = "You died. ";
				}
				score += scoreFormatter(p);
				scoreLabels[p].Text = score;
			}
		}

		public void setScoreFormatter(ScoreFormatter sf) {
			System.Diagnostics.Debug.Assert(sf != null);
			scoreFormatter = sf;
		}
	}
}
