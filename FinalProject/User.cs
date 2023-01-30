using System;

namespace FinalProject
{
    class User
    {
        public string userName;
        public int wins, losses, draws, total, winloss;

        public User(string userName)
        {
            this.userName = userName;
            this.wins = 0;
            this.losses = 0;
            this.draws = 0;
        }
        public User(string userName, int wins, int losses, int draws)
        {
            this.userName = userName;
            this.wins = wins;
            this.losses = losses;
            this.draws = draws;
            this.total = wins+losses+draws;
            winLoss();
        }
        public void addWins()
        {
            this.wins++;
            this.total++;
        }
        public void addLosses()
        {
            this.losses++;
            this.total++;
        }
        public void addDraws()
        {
            this.draws++;
            this.total++;
        }
        public double winLoss()
        {
            int l = 1;
            if(this.losses != 0)
            {
                l = this.losses;
            }
            this.winloss = this.wins/l;
            return this.wins / l;
        }
        public string saveProfile()
        {
            return this.userName +"," + this.wins.ToString() +","+ this.losses.ToString() +","+ this.draws.ToString();
        }
        public string getuserName()
        {
            return this.userName;
        }
        public int getWins()
        {
            return this.wins;
        }
        public int getDraws()
        {
            return this.draws;
        }
        public int getLosses()
        {
            return this.losses;
        }
    }
}