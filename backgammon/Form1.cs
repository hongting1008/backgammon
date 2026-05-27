using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
namespace backgammon
{
    public partial class Form1 : Form
    {
      
        private const int BoardSize = 15;
        private const int CellSize = 30;

      
        private const int BoardMargin = 20;

        private int[,] board = new int[BoardSize, BoardSize];
        private bool isBlackTurn = true;
        private bool isGameOver = false;

      
        private SoundPlayer placeSound;
        private SoundPlayer winSound;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

       
            this.ClientSize = new Size(
                BoardMargin * 2 + (BoardSize - 1) * CellSize,
                BoardMargin * 2 + (BoardSize - 1) * CellSize
            );

       
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);

            try
            {
                placeSound = new SoundPlayer("place.wav");
                winSound = new SoundPlayer("win.wav");
            }
            catch (Exception)
            {
                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            
            g.FillRectangle(Brushes.BurlyWood, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

           
            Pen boardPen = new Pen(Color.Black, 1);
            for (int i = 0; i < BoardSize; i++)
            {
                g.DrawLine(boardPen, BoardMargin, BoardMargin + i * CellSize, BoardMargin + (BoardSize - 1) * CellSize, BoardMargin + i * CellSize);
                g.DrawLine(boardPen, BoardMargin + i * CellSize, BoardMargin, BoardMargin + i * CellSize, BoardMargin + (BoardSize - 1) * CellSize);
            }

            
            Pen thickPen = new Pen(Color.Black, 3);
            g.DrawRectangle(thickPen, BoardMargin, BoardMargin, (BoardSize - 1) * CellSize, (BoardSize - 1) * CellSize);

          
            Brush starBrush = Brushes.Black;
            int starRadius = 4; 
            int[] starPositions = { 3, 11 }; 
            foreach (int i in starPositions)
            {
                foreach (int j in starPositions)
                {
                    g.FillEllipse(starBrush, BoardMargin + i * CellSize - starRadius, BoardMargin + j * CellSize - starRadius, starRadius * 2, starRadius * 2);
                }
            }
          
            g.FillEllipse(starBrush, BoardMargin + 7 * CellSize - starRadius, BoardMargin + 7 * CellSize - starRadius, starRadius * 2, starRadius * 2);

            
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    if (board[x, y] != 0)
                    {
                        int pieceSize = CellSize - 4;

                      
                        int px = BoardMargin + x * CellSize - (pieceSize / 2);
                        int py = BoardMargin + y * CellSize - (pieceSize / 2);

                        if (board[x, y] == 1)
                        {
                           
                            g.FillEllipse(Brushes.Black, px, py, pieceSize, pieceSize);
                        }
                        else if (board[x, y] == 2)
                        {
                            g.FillEllipse(Brushes.White, px, py, pieceSize, pieceSize);
                            g.DrawEllipse(Pens.Black, px, py, pieceSize, pieceSize);
                        }
                    }
                }
            }
        }
      
        private void ShowCustomGameOver(string message)
        {
            Form gameOverForm = new Form();
            gameOverForm.Text = "遊戲結束";
            gameOverForm.Size = new Size(400, 250);
            gameOverForm.StartPosition = FormStartPosition.CenterParent;
            gameOverForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            gameOverForm.MaximizeBox = false;
            gameOverForm.MinimizeBox = false;

          
            try
            {
                gameOverForm.BackgroundImage = Image.FromFile("winner.jpg");
                gameOverForm.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception)
            {
                gameOverForm.BackColor = Color.BurlyWood;
            }

           
            Label lblWinner = new Label();
            lblWinner.Text = message; 
            lblWinner.Font = new Font("微軟正黑體", 16, FontStyle.Bold);
            lblWinner.ForeColor = Color.Gold; 
            lblWinner.BackColor = Color.Transparent; 
            lblWinner.AutoSize = false;
            lblWinner.TextAlign = ContentAlignment.MiddleCenter;
            lblWinner.Size = new Size(400, 40);
            lblWinner.Location = new Point(-10, 130); 

            Button btnReplay = new Button();
            btnReplay.Text = "再來一局";
            btnReplay.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            btnReplay.Size = new Size(100, 35);
            btnReplay.Location = new Point(75, 170);
            btnReplay.BackColor = Color.WhiteSmoke;
            btnReplay.FlatStyle = FlatStyle.Flat;
            btnReplay.FlatAppearance.BorderColor = Color.SaddleBrown;
            btnReplay.FlatAppearance.BorderSize = 2;
            btnReplay.TabStop = false;
            btnReplay.DialogResult = DialogResult.OK;

        
            Button btnExit = new Button();
            btnExit.Text = "離開";
            btnExit.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
            btnExit.Size = new Size(100, 35);
            btnExit.Location = new Point(207, 170);
            btnExit.BackColor = Color.WhiteSmoke;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderColor = Color.SaddleBrown;
            btnExit.FlatAppearance.BorderSize = 2;
            btnExit.TabStop = false;
            btnExit.Click += (sender, e) => {
                Application.Exit();
            };

        
            gameOverForm.Controls.Add(lblWinner);
            gameOverForm.Controls.Add(btnReplay);
            gameOverForm.Controls.Add(btnExit);

         
            gameOverForm.ShowDialog(this);
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isGameOver) return;

          
            int x = (e.X - BoardMargin + CellSize / 2) / CellSize;
            int y = (e.Y - BoardMargin + CellSize / 2) / CellSize;

            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize && board[x, y] == 0)
            {
                board[x, y] = isBlackTurn ? 1 : 2;

                if (placeSound != null) placeSound.Play();

                this.Invalidate();

                if (CheckWin(x, y))
                {
                    isGameOver = true;
                    if (winSound != null) winSound.Play();
                    string winner = isBlackTurn ? " 黑方" : " 白方";
                    ShowCustomGameOver(winner + " 獲勝"); 
                    ResetGame();
                }
                else
                {
                    isBlackTurn = !isBlackTurn;
                }
            }
        }

        private bool CheckWin(int x, int y)
        {
            int currentPlayer = board[x, y];
            int[,] directions = { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, -1 } };

            for (int d = 0; d < 4; d++)
            {
                int count = 1;
                int dx = directions[d, 0];
                int dy = directions[d, 1];

                for (int i = 1; i <= 4; i++)
                {
                    int nx = x + dx * i, ny = y + dy * i;
                    if (nx >= 0 && nx < BoardSize && ny >= 0 && ny < BoardSize && board[nx, ny] == currentPlayer)
                        count++;
                    else
                        break;
                }
                for (int i = 1; i <= 4; i++)
                {
                    int nx = x - dx * i, ny = y - dy * i;
                    if (nx >= 0 && nx < BoardSize && ny >= 0 && ny < BoardSize && board[nx, ny] == currentPlayer)
                        count++;
                    else
                        break;
                }

                if (count >= 5) return true;
            }
            return false;
        }

        private void ResetGame()
        {
            board = new int[BoardSize, BoardSize];
            isBlackTurn = true;
            isGameOver = false;
            this.Invalidate();
        }
    }
}