using Battleships;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class GameTests
    {
        [Test]
        public void Game_OnInit_Generates2DestroyersForPlayer()
        {
            Game game = new Game();

            int destroyers = game.PlayerShips.Count(s => string.Equals(s.Type, "Destroyer"));

            Assert.AreEqual(2, destroyers);
        }

        [Test]
        public void Game_OnInit_Generates1BattleshipForPlayer()
        {
            Game game = new Game();

            int battleships = game.PlayerShips.Count(s => string.Equals(s.Type, "Battleship"));

            Assert.AreEqual(1, battleships);
        }

        [Test]
        public void Game_OnInit_Generates2DestroyersForNPC()
        {
            Game game = new Game();

            int destroyers = game.NPCShips.Count(s => string.Equals(s.Type, "Destroyer"));

            Assert.AreEqual(2, destroyers);
        }

        [Test]
        public void Game_OnInit_Generates1BattleshipForNPC()
        {
            Game game = new Game();

            int battleships = game.NPCShips.Count(s => string.Equals(s.Type, "Battleship"));

            Assert.AreEqual(1, battleships);
        }

        [Test]
        public void Game_OnInit_AllBattleshipsAre4Squares()
        {
            Game game = new Game();

            var playerBattleships = game.PlayerShips.Where(s => string.Equals(s.Type, "Battleship")).Select(bs => bs.Length);
            var npcBattleships = game.NPCShips.Where(s => string.Equals(s.Type, "Battleship")).Select(bs => bs.Length);

            var invalidShips = playerBattleships.Count(l => l != 4) + npcBattleships.Count(l => l != 4);

            Assert.AreEqual(0, invalidShips);
        }

        [Test]
        public void Game_OnInit_AllDestroyersAre5Squares()
        {
            Game game = new Game();

            var playerBattleships = game.PlayerShips.Where(s => string.Equals(s.Type, "Destroyer")).Select(bs => bs.Length);
            var npcBattleships = game.NPCShips.Where(s => string.Equals(s.Type, "Destroyer")).Select(bs => bs.Length);

            var invalidShips = playerBattleships.Count(l => l != 5) + npcBattleships.Count(l => l != 5);

            Assert.AreEqual(0, invalidShips);
        }
    }
}