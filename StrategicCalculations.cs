﻿using ElfKingdom;

namespace SkillzProject
{
    abstract class StrategicCalculations
    {
        public int manaWasted { get; set; }
        protected int defendRadius { get; private set; }
        protected int maxPotentialMana { get; private set; }
        protected int buildRange { get; private set; }
        protected int enemyAggressivePortalRangeFromCastle { get; private set; }
        public abstract void DoTurn(Game game);
        public void CalculateAll(Game game)
        {
            CalculateDefendRadius(game);
            CalculateMaxPotentialMana(game);
            CalculateBuildRange(game);
        }
        protected Location Cis(double radius, double degree, Location baseLocation = null)
        {
            if (baseLocation == null)
            {
                return new Location((int)(radius * System.Math.Sin(degree)), (int)(radius * System.Math.Cos(degree)));
            }
            else
            {
                return new Location(baseLocation.Row + (int)(radius * System.Math.Sin(degree)), baseLocation.Col + (int)(radius * System.Math.Cos(degree)));
            }
        }
        protected double DegreeBetween(Location a, Location b)
        {
            double a1 = System.Math.Sqrt(System.Math.Pow(a.Col - b.Col, 2) + System.Math.Pow(a.Row - b.Row, 2));
            double b1 = System.Math.Abs(a.Col - b.Col);
            return (a.Col > b.Col ? 3 * System.Math.PI / 2 : System.Math.PI / 2) + System.Math.Asin(b1 / a1);
        }
        private void CalculateDefendRadius(Game game)
        {
            defendRadius = 1300;
        }
        private void CalculateMaxPotentialMana(Game game)
        {
            maxPotentialMana = (game.MaxTurns - 150) * game.GetMyself().ManaPerTurn;
        }
        private void CalculateBuildRange(Game game)
        {
            if (game.Turn % 100 == 0)
            {
                buildRange = 2800;
                if (game.Turn < 400)
                {
                    buildRange = 2300;
                }
                if (game.Turn < 200)
                {
                    buildRange = 1800;
                }
            }
            if (game.GetMyCastle().CurrentHealth < 125 && buildRange > 2300)
            {
                buildRange = 2300;
            }
            if (game.GetMyCastle().CurrentHealth < 100 && buildRange > 1800)
            {
                buildRange = 1800;
            }
            if (game.GetMyCastle().CurrentHealth < 50 && buildRange > 1300)
            {
                buildRange = 1300;
            }
        }
        private void CalculateEnemyAggressivePortalRangeFromCastle(Game game)
        {
            enemyAggressivePortalRangeFromCastle = 3500;
        }
    }
}
