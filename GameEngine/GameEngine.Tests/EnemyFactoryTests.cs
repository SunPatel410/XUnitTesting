using System;
using Xunit;
using Xunit.Sdk;

namespace GameEngine.Tests
{
    //Trait is a attribute that helps group and find unti tests within vs text explorer
    [Trait("Category", "Enemy")]
    public class EnemyFactoryTests
    {
        #region Checking Object Types
        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            var sut = new EnemyFactory();
            var enemy = sut.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        //Ignoring this test by using Skip Attribute and reson for why it is skiped
        [Fact(Skip = "Dont need to run this")]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            var sut = new EnemyFactory();
            var enemy = sut.Create("Zombie");

            Assert.IsNotType<DateTime>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            var sut = new EnemyFactory();

            var enemy = sut.Create("Zombie King", isBoss: true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CanReturnedTypeExample()
        {
            var sut = new EnemyFactory();

            var enemy = sut.Create("Zombie King", isBoss: true);

            //Assert andf cast result
            var boss = Assert.IsType<BossEnemy>(enemy);

            //Additional asserts on typed object
            Assert.Equal("Zombie King", boss.Name);
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            var sut = new EnemyFactory();

            var enemy = sut.Create("Zombie King", isBoss: true);

            //will take into account any inheratance
            Assert.IsAssignableFrom<Enemy>(enemy);
        }
        #endregion

        #region Asserting On Object Instances

        [Fact]
        public void CreateSeprateInstances_NotSame()
        {
            var sut = new EnemyFactory();

            var e1 = sut.Create("Zombie");
            var e2 = sut.Create("Zombie");

            Assert.NotSame(e1, e2);
        }

        [Fact]
        public void CreateSeprateInstances_Same()
        {
            var sut = new EnemyFactory();

            var e1 = sut.Create("Zombie");
            var e2 = e1;

            Assert.Same(e1, e2);
        }

        #endregion

        #region ThrowingExceptions

        [Fact]
        public void NotAllowNullName()
        {
            var sut = new EnemyFactory();

            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            var sut = new EnemyFactory();

            var ex = Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", isBoss: true));

            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }


        #endregion
    }
}
