using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameEngine.Tests
{
    public class NonPlayerCharacterTests
    {
        //using memberdata to share data accross different test classes. Thus not having
        //no duplicated data
        //[MemberData(nameof(InternalHealthDamageTestData.TestData), 
        //    MemberType = typeof(InternalHealthDamageTestData))]

        [Theory]
        //using custom attribute to store data
        [HealthDamageData]
        public void TakeDamage(int damage, int expectedHealth)
        {
            var sut = new NonPlayerCharacter();

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);
        }
    }
}
