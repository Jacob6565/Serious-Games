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

        private long _elapsedSec;
        private float _secondsRound;
        private float _secondsTotal;
        private float _clickedTotal;
        
        private static int _totalLoss;

        public List<Round> roundList = new List<Round>();
        public List<Persons> personList = new List<Persons>();

        public string testFirstName = "Poo";
        public string testLastName = "The rapist";

        public void ResetGameToList(object sender, MouseEventArgs e)
        {
            //ConvertSeconds();
            //AddToTotal();

            _elapsedSec = ElapsedSeconds(); // Converts the time to seconds
            _secondsRound = unchecked(_elapsedSec); // Succesfully converts the long to float, ready for the database.
            _secondsTotal += _secondsRound;
            _clickedTotal += GameForm.hexClickedRound;

            RoundVariables();

            _totalLoss += 1;

            Persons person = new Persons(testFirstName, testLastName);
            personList.Add(person);

            Round round = new Round(GameForm.hexClickedRound, roundAverage, roundResult, _secondsRound);
            roundList.Add(round);

            GameForm.hexClickedRound = 0; // Resets the amount of hex clicked
            stopwatchRound.Restart(); // Starts the stopwatch from 0
            ResetCounter(); // Increments the reset counter
        }

        public void ExitGameToDatabase()
        {
            //stopwatchRound.Stop();
            //ConvertSeconds();
            //AddToTotal();
            _elapsedSec = ElapsedSeconds(); // Converts the time to seconds
            _secondsRound = unchecked(_elapsedSec); // Succesfully converts the long to float, ready for the database.

            _secondsTotal += _secondsRound;
            _clickedTotal += GameForm.hexClickedRound;

            RoundVariables();

            Persons person = new Persons(testFirstName, testLastName);
            personList.Add(person);

            Round round = new Round(GameForm.hexClickedRound, roundAverage, roundResult, _secondsRound);
            roundList.Add(round);

            AddPersonToDatabase();
            AddSessionToDatabase();
            AddRoundsToDatabase();
        }

        public void ConvertSeconds()
        {
            _elapsedSec = ElapsedSeconds(); // Converts the time to seconds
            _secondsRound = unchecked(_elapsedSec); // Succesfully converts the long to float, ready for the database.
        }

        public void AddToTotal()
        {
            _secondsTotal += _secondsRound;
            _clickedTotal += GameForm.hexClickedRound;
        }

        // Unique to WinOrLose
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

        public int roundResult;
        public float roundAverage;

        public void RoundVariables()
        {
            roundResult = WinOrLose();
            roundAverage = AverageClick(GameForm.hexClickedRound, _secondsRound);
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
            Console.WriteLine(_clickedTotal);
            Console.WriteLine(AverageClick(_clickedTotal, _secondsTotal));
            Console.WriteLine(_resetCounter + 1);
            Console.WriteLine(Pathfinding.gameTotalWins);
            Console.WriteLine(_totalLoss);
            Console.WriteLine(_secondsTotal);
            using (var context = new Entities())
            {
                try
                {
                    context.Session.Add(new Session // adds a row to the Session table in the SQL database
                    {
                        Clicks = _clickedTotal,
                        AVG_Clicks = AverageClick(_clickedTotal, _secondsTotal),
                        Rounds = _resetCounter + 1,
                        Wins = Pathfinding.gameTotalWins,
                        Losses = _totalLoss,
                        Time_Used = _secondsTotal
                    });
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                context.SaveChanges();
            }
        }

        public void PrintData()
        {
            using (var context = new Entities())
            {
                foreach (var item in context.Rounds)
                {
                    Console.WriteLine(item.Id);
                    Console.WriteLine(item.Clicks);
                    Console.WriteLine(item.AVG_Clicks);
                    Console.WriteLine(item.Win);
                    Console.WriteLine(item.Loss);
                    Console.WriteLine(item.Time_Used);
                }
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

        public long ElapsedSeconds()
        {
            return stopwatchRound.ElapsedMilliseconds / 1000;
        }
    }
}
