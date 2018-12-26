using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRaceProject
{
    public class Bet
    {
        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private int _carNumber;

        public int CarNumber
        {
            get { return _carNumber; }
            set { _carNumber = value; }
        }

        private Bettor _bettor;

        public Bettor Bettor
        {
            get { return _bettor; }
            set { _bettor = value; }
        }

        public string GetDescription()
        {
            if (this._amount == 0)
                return this._bettor.Name + " hasn't placed any bet";
                return this._bettor.Name + " placed " + this._bettor.MyBet._amount.ToString() + " bucks on car # " + this._bettor.MyBet.CarNumber.ToString();
        }

        public int Payout(int winningCarNumber)
        {
            if (this._bettor.MyBet.CarNumber == winningCarNumber)
                return this._amount;
            else
                return -this._amount;
        }
    }
}
