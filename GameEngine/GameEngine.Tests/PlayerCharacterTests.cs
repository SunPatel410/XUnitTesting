using System;
using System.Runtime.InteropServices.ComTypes;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace GameEngine.Tests
{
    [Trait("Category", "PlayerCharacter")]
    //use IDisposable to cleanup code and dispose once completed
    public class PlayerCharacterTests : IDisposable
    {

        private readonly PlayerCharacter _sut;
        private readonly ITestOutputHelper _output;

        public PlayerCharacterTests(ITestOutputHelper output)
        {
            _output = output;
            //Custom test output text 
            _output.WriteLine("Creating new PlayerCharacter");
            _sut = new PlayerCharacter();
        }


        #region Assertions With Strings

        [Fact]
        public void BeInexperiancedWhenNew()
        {
            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            _sut.FirstName = "Sunny";
            _sut.LastName = "Patel";
            

            Assert.Equal("Sunny Patel", _sut.FullName);

        }

        [Fact]
        public void HaveFullNameWithFirstName()
        {
            _sut.FirstName = "Sunny";
            _sut.LastName = "Patel";

            Assert.StartsWith("Sunny", _sut.FullName);
        }

        [Fact]
        public void HaveFullNameWithLastName()
        {
            _sut.FirstName = "Sunny";
            _sut.LastName = "Patel";

            Assert.EndsWith("Patel", _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnoreCaseAssertExample()
        {
            _sut.FirstName = "SUNNY";
            _sut.LastName = "PATEL";


            Assert.Equal("Sunny Patel", _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CaclulateFullName_SubstringAssertExample()
        {
            _sut.FirstName = "Sunny";
            _sut.LastName = "Patel";

            Assert.Contains("Su", _sut.FullName);
        }

        [Fact]
        public void CaclulateFullNameWithTitleCase()
        {
            _sut.FirstName = "Sunny";
            _sut.LastName = "Patel";

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
        }

        #endregion


        #region NumericValues Tests

        [Fact]
        public void StartWithDefaultHealth()
        {
            var sut = new PlayerCharacter();

            Assert.Equal(100, sut.Health);
        }

        [Fact]
        public void StartWithDefaultHealth_NotEqualExample()
        {
            var sut = new PlayerCharacter();

            Assert.NotEqual(10, sut.Health);
        }

        #endregion

        //Asserting Floating Points
        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            _sut.Sleep();

            //Assert.True(sut.Health >= 100 && sut.Health <= 200);
            Assert.InRange(_sut.Health, 101, 200);
        }

        //Asserting Null Values
        [Fact]
        public void NotHaveNickNameByDefault()
        {
            Assert.Null(_sut.Nickname);
        }


        #region Collection Tests
        [Fact]
        public void HaveALongBow()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
        }

        [Fact]
        public void NotHaveAStaffOfWonder()
        {
            Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
        }

        [Fact]
        public void HaveAtLeastOneKindOfSword()
        {
            Assert.Contains(_sut.Weapons, w => w.Contains("Sword"));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new string[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, _sut.Weapons);
        }

        [Fact]
        public void HaveNoEmptyDefaultWepons()
        {
            //goes through all of the collection and check if the collection is null or is just set to white space
            Assert.All(_sut.Weapons, w => Assert.False(string.IsNullOrWhiteSpace(w)));
        }
        #endregion


        #region EventTests

        [Fact]
        public void RaiseSleptEvent()
        {

            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(_sut, "Health", () => _sut.TakeDamage(10));
        }

        #endregion

        //Inline data similar to test case in nunit
        [Theory]
        //[InlineData(0, 100)]
        //[InlineData(1, 99)]
        //[InlineData(50, 50)]
        //[InlineData(101, 1)]
        [MemberData(nameof(InternalHealthDamageTestData.TestData),
            MemberType = typeof(InternalHealthDamageTestData))]
        public void TakeDamage(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, _sut.Health);
        }



        public void Dispose()
        {
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");
        }
    }
}
