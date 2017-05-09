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

namespace P2SeriousGame
{
    class Database
    {
        public Database() { }

        public Stopwatch watchRound;
        private float _hexClickedRound;

        // trygetcollector...
        private long elapsedSec;
        private float _secondsRound;
        private static int _totalLoss;

        // Send to database
        private float _secondsTotal;
        private float _clickedTotal;
        private int _roundWin;
        private int _roundLoss;

        public void SendToDatabase()
        {
            var elapsedSec = watchRound.ElapsedMilliseconds / 1000; // Converts the time to seconds
            float secondsRound = unchecked(elapsedSec);

            _secondsTotal += secondsRound;
            _clickedTotal += _hexClickedRound;

            WinMethod();

            AddTestParametersToDatabase();
            AddRoundsToDatabase();
        }

        public void RoundDataCollector(object sender, MouseEventArgs e)
        {
            watchRound.Stop(); // Stops the time for the round
            elapsedSec = watchRound.ElapsedMilliseconds / 1000; // Converts the time to seconds
            _secondsRound = unchecked(elapsedSec);

            _secondsTotal += _secondsRound;
            _clickedTotal += _hexClickedRound;

            _totalLoss += 1;

            WinMethod();

            AddPersonToDatabase();
            AddRoundsToDatabase();

            ResetCounter();
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
            using (var context = new Entities())
            {
                context.Rounds.Add(new Rounds // adds a row to the Rounds table in the SQL database
                {
                    Clicks = _hexClickedRound,
                    AVG_Clicks = AverageClick(_hexClickedRound, _secondsRound),
                    Win = _roundWin,
                    Loss = _roundLoss,
                    Time_Used = _secondsRound
                });

                context.SaveChanges();
            }
        }

        public void AddTestParametersToDatabase()
        {
            using (var context = new Entities())
            {
                context.TestParameters.Add(new TestParameters // adds a row to the TestParameters table in the SQL database
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

    }
}
