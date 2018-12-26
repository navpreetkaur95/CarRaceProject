using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CarRaceProject
{
    public partial class CarRaceUI : Form
    {
        public CarRaceUI()
        {
            InitializeComponent();
        }

        private Bettor []_bettorList = null;
        private Car []_carList = null;
        private int _flag = 0;
        private bool _enableRaceBtn = false;
        
        public void FillArrays()
        {
            Random random = new Random();

            _bettorList = new Bettor[3]
            {
                new Bettor() 
                { 
                    Name = "Joe", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = lblBettor1, 
                    MyRadioButton = rdbBettor1
                },

                new Bettor()
                { 
                    Name = "Bob", 
                    Cash = 50,  
                    MyBet = new Bet(),  
                    MyLabel = lblBettor2, 
                    MyRadioButton = rdbBettor2
                },

                new Bettor() 
                { 
                    Name = "Al", 
                    Cash = 50,  
                    MyBet = new Bet(), 
                    MyLabel = lblBettor3, 
                    MyRadioButton = rdbBettor3
                }
            };

            _carList = new Car[4]
            {
                new Car() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70, 
                    StartPos = pBoxRaceTrack.Location.X, 
                    Rand = random, 
                    MyPictureBox = pbCar1
                },

                new Car()
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70,
                    StartPos = pBoxRaceTrack.Location.X,
                    Rand = random, 
                    MyPictureBox = pbCar2
                },

                new Car() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70,
                    StartPos = pBoxRaceTrack.Location.X,
                    Rand = random, 
                    MyPictureBox = pbCar3
                },

                new Car() 
                { 
                    RaceTrackLength = pBoxRaceTrack.Width - 70,
                    StartPos = pBoxRaceTrack.Location.X,
                    Rand = random, 
                    MyPictureBox = pbCar4
                }
            };

            for (int i = 0; i < _bettorList.Length; i++)
            {
                _bettorList[i].MyBet.Bettor = _bettorList[i];
                _bettorList[i].UpdateLabels();
            }

            setCarPictures();            
        }

        private void frmBetting_Load(object sender, EventArgs e)
        {
            try
            {

                FillArrays();
                
                if (!this._enableRaceBtn)
                    btnRace.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rdbBettor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBettor1.Checked)
            {
                this._flag = 1;
                lblBettorName.Text = this._bettorList[0].Name;
            }
        }

        private void rdbBettor2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBettor2.Checked)
            {
                this._flag = 2;
                lblBettorName.Text = this._bettorList[1].Name;
            }
        }

        private void rdbBettor3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBettor3.Checked)
            {
                this._flag = 3;
                lblBettorName.Text = this._bettorList[2].Name;
            }
        }

        public void BetsButtonWorking()
        {            
            int totBucks = 0;
            int carNumber = 0;

            if (!rdbBettor1.Checked && !rdbBettor2.Checked && !rdbBettor3.Checked)
            {
                MessageBox.Show("SELECT A BETTOR!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            totBucks = Convert.ToInt32(amountNum.Value);
            carNumber = Convert.ToInt32(carNum.Value);

            if (IsExceedBetLimit(totBucks))
            {
                MessageBox.Show("Bet Limit Exceeds Cash!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _enableRaceBtn = true;

            if (this._flag == 1)
            {
                this._bettorList[0].PlaceBet(totBucks, carNumber);
            }
            else if (this._flag == 2)
            {
                this._bettorList[1].PlaceBet(totBucks, carNumber);
            }
            else if (this._flag == 3)
            {
                this._bettorList[2].PlaceBet(totBucks, carNumber);
            }            
        }

        private void btnBets_Click(object sender, EventArgs e)
        {
            try
            {
                BetsButtonWorking();

                if (this._enableRaceBtn)
                    btnRace.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool IsExceedBetLimit(int amount)
        {
            return amount > Bettor.maxBet ? true : false;
        }

        public void RaceButtonWorking()
        {
            btnBets.Enabled = false;
            btnRace.Enabled = false;

            bool winningCarFlag = false;
            int winningCar = 0;            

            while (!winningCarFlag)
            {
                for (int i = 0; i < _carList.Length; i++)
                {
                    if (this._carList[i].Run())
                    {
                        winningCarFlag = true;
                        winningCar = i;
                    }
                  
                    pBoxRaceTrack.Refresh();                 
                }                
            }

            MessageBox.Show("The winning car is: #" + (winningCar + 1), "Finished!");

            for (int j = 0; j < _bettorList.Length; j++)
            {
                this._bettorList[j].Collect(winningCar + 1);
                this._bettorList[j].ClearBet();
            }

            setCarPictures();

            btnBets.Enabled = true;       
        }

        public void setCarPictures()
        {
            for (int k = 0; k < _carList.Length; k++)
                _carList[k].TakeStartPos();  
        }

        private void btnRace_Click(object sender, EventArgs e)
        {
            try
            {
                RaceButtonWorking();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pBoxRaceTrack_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblbettor2_Click(object sender, EventArgs e)
        {

        }

        private void lblBettor1_Click(object sender, EventArgs e)
        {

        }

        private void lblBettor3_Click(object sender, EventArgs e)
        {

        }

        private void lblMinimumBet_Click(object sender, EventArgs e)
        {

        }

        private void pbCar1_Click(object sender, EventArgs e)
        {

        }

        private void pbCar3_Click(object sender, EventArgs e)
        {

        }
    }
}
