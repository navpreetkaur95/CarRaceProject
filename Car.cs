using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarRaceProject
{
    public class Car
    {   
        private int _startPos;

        public int StartPos
        {
            get { return _startPos; }
            set { _startPos = value; }
        }

        private int _raceTrackLength;

        public int RaceTrackLength
        {
            get { return _raceTrackLength; }
            set { _raceTrackLength = value; }
        }

        private int _location;

        public int Location
        {
            get { return _location; }
            set { _location = value; }
        }    

        private PictureBox _myPictureBox;       

        public PictureBox MyPictureBox
        {
            get { return _myPictureBox; }
            set { _myPictureBox = value; }
        }

        private Random _random;

        public Random Rand
        {
            get { return _random; }
            set { _random = value; }
        }
       
        public bool Run()
        {
            int randomDistance = this._random.Next(1, 4);
            this._location += randomDistance;

            Point point = this._myPictureBox.Location;

            if (point.X > this._raceTrackLength)
            {
                return true;
            }
            else
            {
                point.X += randomDistance;
                this._myPictureBox.Location = point;

                return false;
            }
        }
  
        public void TakeStartPos()
        {             
            this._location = this._startPos;

            Point point = this._myPictureBox.Location;
            point.X = Location;
            this._myPictureBox.Location = point;
        }
    }
}
