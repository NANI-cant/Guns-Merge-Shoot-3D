using Architecture.Services.AssetProviding.Impl;
using Gameplay.Utils;
using Metric;
using NUnit.Framework;

namespace TestsEditMode.AssetProviding {
    public class ResourcesMetricProviderTests {
        [Test]
        public void Player_Is_Not_Null() {
            //Arrage
            var provider = new ResourcesMetricProvider();

            //Act
            var player = provider.PlayerMetric;

            //Assert
            Assert.IsNotNull(player);
        }
        
        [Test]
        public void Big_Is_Not_Null() {
            //Arrage
            var provider = new ResourcesMetricProvider();

            //Act
            var big = provider.EnemyMetric(EnemyId.Big);

            //Assert
            Assert.IsNotNull(big);
        }
        
        [Test]
        public void Small_Is_Not_Null() {
            //Arrage
            var provider = new ResourcesMetricProvider();

            //Act
            var small = provider.EnemyMetric(EnemyId.Small);

            //Assert
            Assert.IsNotNull(small);
        }
        
        [Test]
        public void Medium_Is_Not_Null() {
            //Arrage
            var provider = new ResourcesMetricProvider();

            //Act
            var medium = provider.EnemyMetric(EnemyId.Medium);

            //Assert
            Assert.IsNotNull(medium);
        }
    }
}
