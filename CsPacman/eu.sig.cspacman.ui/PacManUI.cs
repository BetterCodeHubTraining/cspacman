using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

using eu.sig.cspacman.game;
using System.Threading;

namespace eu.sig.cspacman.ui
{
    public class PacManUI : Form
    {

        private const int FRAME_INTERVAL = 40;

        private readonly ScorePanel scorePanel;

        private readonly BoardPanel boardPanel;

        private CancellationTokenSource ts = new CancellationTokenSource();

        private Task task;

        private PacKeyListener keys;

        public PacManUI(Game game, IDictionary<string, Action> buttons,
            IDictionary<Keys, Action> keyMappings, ScorePanel.ScoreFormatter sf) : base()
        {
            System.Diagnostics.Debug.Assert(game != null);
            System.Diagnostics.Debug.Assert(buttons != null);
            System.Diagnostics.Debug.Assert(keyMappings != null);

            keys = new PacKeyListener(keyMappings);
            KeyPreview = true;

            scorePanel = new ScorePanel(game.Players);
            if (sf != null)
            {
                scorePanel.setScoreFormatter(sf);
            }
            boardPanel = new BoardPanel(game);
            Panel buttonPanel = new ButtonPanel(buttons, this);

            Controls.Add(scorePanel);
            Controls.Add(boardPanel);
            Controls.Add(buttonPanel);
            DoVerticalLayout();
        }

        private void DoVerticalLayout()
        {
            int y = 0;
            int maxWidth = Controls.Mapreduce<int>((c, agg) => Math.Max(c.Size.Height, agg));
            foreach (Control c in Controls)
            {
                c.Location = new Point(maxWidth / 2 - c.Size.Width / 2, y);
                y += c.Size.Height;
            }
            // Allow for some padding (apparently, borders count)
            Size = new Size(maxWidth + 10, y + 26);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
            {
                keys.DoKeyDown(keyData);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void start()
        {
            Visible = true;
            task = Task.Delay(FRAME_INTERVAL, ts.Token).ContinueWith(x => this.nextFrame());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!ts.IsCancellationRequested)
            {
                ts.Cancel();

                lock (ts)
                {
                    Invoke(new Action(() => Close()));
                }
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private void nextFrame()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(updateFrame));
            }
            else {
                updateFrame();
            }
            lock (ts)
            {
                Task.Delay(FRAME_INTERVAL, ts.Token).ContinueWith(x => this.nextFrame());
            }
        }

        private void updateFrame()
        {
            lock (ts)
            {
                this.Invalidate();
                boardPanel.Refresh();
                scorePanel.refresh();
            }
        }
    }
}
