using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2SeriousGame.SQL;
using P2SeriousGame;

namespace P2SeriosuGame
{
    class Database
    {
        public Database() { }

        private long _elapsedSec;
        private float _secondsRound;
        private float _secondsTotal;
        private float _clickedTotal;

        // Unique to ResetGameToDatabase
        private static int _totalLoss;

        public void ExitGameToDatabase() // Former SendToDatabase()
        {
            StopStopwatch();
            _elapsedSec = ElapsedSeconds(); // Converts the time to seconds
            _secondsRound = unchecked(_elapsedSec); // Succesfully converts the long to float, ready for the database.

            _secondsTotal += _secondsRound;
            _clickedTotal += GameWindow.hexClickedRound;

            WinMethod();

            AddSessionToDatabase();
            AddRoundsToDatabase();
        }

        public void ResetGameToDatabase(object sender, MouseEventArgs e)
        {
            _elapsedSec = ElapsedSeconds(); 
            _secondsRound = unchecked(_elapsedSec);
            StopStopwatch();
            StartStopwatch(); // make restart function

            _secondsTotal += _secondsRound;
            _clickedTotal += GameWindow.hexClickedRound;

            _totalLoss += 1;

            WinMethod();

            AddPersonToDatabase();
            AddRoundsToDatabase();

            ResetCounter();
        }

        // Unique to WinMethod
        private int _roundWin;
        private int _roundLoss;

        public void WinMethod() //Name in progress...
        {
            if (Pathfinding.gameRoundWin)
            {
                _roundWin = 1;
                _roundLoss = 0;
            }
            else if (!Pathfinding.gameRoundWin)
            {
                _roundWin = 0;
                _roundLoss = 1;
            }
        }

        public void AddPersonToDatabase()
        {
            // Testing parameters
            string testFirstName = "Foo";
            string testLastName = "Bar";

            using (var context = new Entities())
            {
                context.Person.Add(new Person // adds a row to the Person table in the SQL database
                {
                    First_Name = testFirstName,
                    Last_Name = testLastName
                });

                context.SaveChanges();
            }
        }

        public void AddRoundsToDatabase()
        {
            _secondsRound = ElapsedSeconds();
            using (var context = new Entities())
            {
                context.Rounds.Add(new Rounds // adds a row to the Rounds table in the SQL database
                {
                    Clicks = GameWindow.hexClickedRound,
                    AVG_Clicks = AverageClick(GameWindow.hexClickedRound, _secondsRound),
                    Win = _roundWin,
                    Loss = _roundLoss,
                    Time_Used = _secondsRound
                });

                context.SaveChanges();
            }
        }

        public void AddSessionToDatabase()
        {
            using (var context = new Entities())
            {
                context.Session.Add(new P2SeriousGame.SQL.Session // adds a row to the TestParameters table in the SQL database
                {
                    Clicks = _clickedTotal,
                    AVG_Clicks = AverageClick(_clickedTotal, _secondsTotal),
                    Rounds = _resetCounter + 1,
                    Wins = Pathfinding.gameTotalWins,
                    Losses = _totalLoss,
                    Time_Used = _secondsTotal
                });

                context.SaveChanges();
            }
        }

        private float AverageClick(float hexClicked, float seconds)
        {
            return hexClicked / seconds;
        }

        private int _resetCounter;

        private void ResetCounter()
        {
            _resetCounter += 1;
        }

        Stopwatch stopwatchRound = new Stopwatch();

        public void StartStopwatch()
        {
            stopwatchRound.Start();
        }

        public void StopStopwatch()
        {
            stopwatchRound.Stop();
        }

        public void RestartStopWatch()
        {
            stopwatchRound.Restart();
        }

        public long ElapsedSeconds()
        {
            return stopwatchRound.ElapsedMilliseconds / 1000;
        }
    }
}
