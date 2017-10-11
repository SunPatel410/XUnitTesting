using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class GameStateTests : IClassFixture<GameStateFixture>
    {

        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;

        public GameStateTests(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            _gameStateFixture = gameStateFixture;
            _output = output;
        }

        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
           _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var p1 = new PlayerCharacter();
            var p2 = new PlayerCharacter();

           _gameStateFixture.State.Players.Add(p1);
           _gameStateFixture.State.Players.Add(p2);

            var expectedHealthAfterEarthquake = p1.Health - GameState.EarthquakeDamage;

            _gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, p1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, p2.Health);
        }

        [Fact]
        public void Reset()
        {
            _output.WriteLine($"GameState ID={_gameStateFixture.State.Id}");

            var p1 = new PlayerCharacter();
            var p2 = new PlayerCharacter();

            _gameStateFixture.State.Players.Add(p1);
            _gameStateFixture.State.Players.Add(p2);

            _gameStateFixture.State.Reset();

            Assert.Empty(_gameStateFixture.State.Players);
        }

    }
}
