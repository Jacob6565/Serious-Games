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
            // Testing parameters
            string testFirstName = "Foo";
            string testLastName = "Bar";

            StopStopwatch();
            _elapsedSec = ElapsedSeconds(); // Converts the time to seconds
            _secondsRound = unchecked(_elapsedSec); // Succesfully converts the long to float, ready for the database.

            _secondsTotal += _secondsRound;
            _clickedTotal += GameWindow.hexClickedRound;

            WinOrLose();

            P2SeriousGame.Persons person = new P2SeriousGame.Persons(testFirstName, testLastName);
            personList.Add(person);

            Round round = new Round(GameWindow.hexClickedRound, AverageClick(GameWindow.hexClickedRound, _secondsRound), WinOrLose(), _secondsRound);
            roundList.Add(round);

            AddPersonToDatabase();
            AddSessionToDatabase();
            AddRoundsToDatabase();
        }

        public List<Round> roundList = new List<Round>();
        public List<P2SeriousGame.Persons> personList = new List<P2SeriousGame.Persons>();

        public void ResetGameToDatabase(object sender, MouseEventArgs e)
        {
            // Testing parameters
            string testFirstName = "Poo";
            string testLastName = "The rapist";

            _elapsedSec = ElapsedSeconds(); 
            _secondsRound = unchecked(_elapsedSec);

            _secondsTotal += _secondsRound;
            _clickedTotal += GameWindow.hexClickedRound;

            _totalLoss += 1;

            P2SeriousGame.Persons person = new P2SeriousGame.Persons(testFirstName, testLastName);
            personList.Add(person);

            Round round = new Round(GameWindow.hexClickedRound, AverageClick(GameWindow.hexClickedRound, _secondsRound), WinOrLose(), _secondsRound);
            roundList.Add(round);

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

        public void AddPersonToDatabase()
        {
            using (var context = new Entities())
            {
                try
                {
                    foreach(var row in personList)
                    {
                        context.Person.Add(new Person
                        {
                            First_Name = row.firstname,
                            Last_Name = row.lastname
                        });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
                context.SaveChanges();
            }
        }

        public void AddSessionToDatabase()
        {
            using (var context = new Entities())
            {
                context.Session.Add(new Session // adds a row to the TestParameters table in the SQL database
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
