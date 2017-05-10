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

            WinOrLose();

            AddSessionToDatabase();

            // AddRoundsToDatabase();
            Round round = new Round(GameWindow.hexClickedRound, AverageClick(GameWindow.hexClickedRound, _secondsRound), WinOrLose(), _secondsRound);
            roundList.Add(round);
        }

        public List<Round> roundList = new List<Round>();
        public List<P2SeriousGame.Person> personList = new List<P2SeriousGame.Person>();

        public void ResetGameToDatabase(object sender, MouseEventArgs e)
        {
            // Testing parameters
            string testFirstName = "Foo";
            string testLastName = "Bar";

            _elapsedSec = ElapsedSeconds(); 
            _secondsRound = unchecked(_elapsedSec);

            _secondsTotal += _secondsRound;
            _clickedTotal += GameWindow.hexClickedRound;

            _totalLoss += 1;

            P2SeriousGame.Person person = new P2SeriousGame.Person(testFirstName, testLastName);
            personList.Add(person);
            // AddPersonToDatabase(); old shit

            Round round = new Round(GameWindow.hexClickedRound, AverageClick(GameWindow.hexClickedRound, _secondsRound), WinOrLose(), _secondsRound);
            roundList.Add(round);
            // AddRoundsToDatabase(); old shit

            GameWindow.hexClickedRound = 0;
            RestartStopwatch(); // Starts the stopwatch from 0
            ResetCounter(); // Increments the reset counter
        }

        // Unique to WinMethod
        private int _roundWin;
        private int _roundLoss;

        public int WinOrLose()
        {
            if (Pathfinding.gameRoundWin)
            {
                _roundLoss = 0;
                _roundWin = 1;
                return 1;
            }
            else
            {
                _roundLoss = 1;
                _roundWin = 0;
                return 0;
            }
        }

        //public void WinOrLose()
        //{
        //    if (Pathfinding.gameRoundWin)
        //    {
        //        _roundWin = 1;
        //        _roundLoss = 0;
        //    }
        //    else if (!Pathfinding.gameRoundWin)
        //    {
        //        _roundWin = 0;
        //        _roundLoss = 1;
        //    }
        //}

        public void AddPersonToDatabase()
        {
            using (var context = new Entities())
            {
                context.Person.Add(new P2SeriousGame.SQL.Person // adds a row to the Person table in the SQL database
                {
                    First_Name = "Poo",
                    Last_Name = "The rapist"
                });

                context.SaveChanges();
            }
        }

        public void AddRoundsToDatabase()
        {
            using (var context = new Entities())
            {
                try
                {
                    foreach(var row in roundList)
                    {
                        context.Rounds.Add(new Rounds
                        {
                            Clicks = row.NumberOfClicks,
                            AVG_Clicks = row.ClicksPerMinute,
                            Win = row.Win,
                            Loss = row.Loss,
                            Time_Used = row.TimeUsed
                        });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //context.Rounds.Add(new Rounds // adds a row to the Rounds table in the SQL database
                //{
                //    Clicks = GameWindow.hexClickedRound,
                //    AVG_Clicks = AverageClick(GameWindow.hexClickedRound, _secondsRound),
                //    Win = _roundWin,
                //    Loss = _roundLoss,
                //    Time_Used = _secondsRound
                //});

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

        public void RestartStopwatch()
        {
            stopwatchRound.Restart();
        }

        public void StopStopwatch()
        {
            stopwatchRound.Stop();
        }

        public long ElapsedSeconds()
        {
            return stopwatchRound.ElapsedMilliseconds / 1000;
        }
    }
}
