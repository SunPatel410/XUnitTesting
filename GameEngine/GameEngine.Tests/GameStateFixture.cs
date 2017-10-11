using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Tests
{
    public class GameStateFixture : IDisposable
    {
        public GameState State { get; private set; }

        public GameStateFixture(GameState state)
        {
            State = state;
        }

        public void Dispose()
        {
            //CleanUp
        }
    }
}
