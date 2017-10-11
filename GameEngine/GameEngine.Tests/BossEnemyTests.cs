using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class BossEnemyTests
    {
        //outputing texts to the console test window
        private readonly ITestOutputHelper _output;

        public BossEnemyTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        [Trait("Category", "Boss")]
        public void HaveCorrectPower()
        {
            _output.WriteLine("Creating Boss Enemy");
            var bossEnemy = new BossEnemy();

            Assert.Equal(166.667, bossEnemy.TotalSpecialAttackPower, 3);
        }
    }
}
