using System.Linq;
using Ecowitt.Liasons;
using Ecowitt.Models.Options;
using Ecowitt.Models.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwoMQTT.Utils;

namespace EcowittTest.Liasons;

[TestClass]
public class MQTTLiasonTest
{
    [TestMethod]
    public void MapDataTest()
    {
        var tests = new[] {
                new {
                    Q = new SlugMapping { MAC = BasicMAC, Slug = BasicSlug },
                    Resource = new Resource { PASSKEY = BasicPasskey },
                    Expected = new { Passkey = BasicPasskey, Slug = BasicSlug, Found = true }
                },
                new {
                    Q = new SlugMapping { MAC = BasicMAC, Slug = BasicSlug },
                    Resource = new Resource { PASSKEY = $"{BasicMAC}-fake" },
                    Expected = new { Passkey = BasicPasskey, Slug = string.Empty, Found = false }
                },
            };

        foreach (var test in tests)
        {
            var logger = new Mock<ILogger<MQTTLiason>>();
            var generator = new Mock<IMQTTGenerator>();
            var sharedOpts = Options.Create(new SharedOpts
            {
                Resources = test.Expected.Found ? new[] { test.Q }.ToList() : new(),
            });

            generator.Setup(x => x.BuildDiscovery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<System.Reflection.AssemblyName>(), false))
                .Returns(new TwoMQTT.Models.MQTTDiscovery());
            generator.Setup(x => x.StateTopic(test.Q.Slug, nameof(Resource.PASSKEY)))
                .Returns($"totes/{test.Q.Slug}/topic/{nameof(Resource.PASSKEY)}");

            var mqttLiason = new MQTTLiason(logger.Object, generator.Object, sharedOpts);
            var results = mqttLiason.MapData(test.Resource);
            var actual = results.FirstOrDefault();

            Assert.AreEqual(test.Expected.Found, results.Any(), "The mapping should exist if found.");
            if (test.Expected.Found)
            {
                Assert.IsTrue(actual.topic.Contains(test.Expected.Slug), "The topic should contain the expected mac.");
                Assert.AreEqual(test.Expected.Passkey, actual.payload, "The payload be the expected passkey.");
            }
        }
    }

    [TestMethod]
    public void DiscoveriesTest()
    {
        var tests = new[] {
                new {
                    Q = new SlugMapping { MAC = BasicMAC, Slug = BasicSlug },
                    Resource = new Resource { PASSKEY = BasicPasskey },
                    Expected = new { Passkey = BasicPasskey, Slug = BasicSlug }
                },
            };

        foreach (var test in tests)
        {
            var logger = new Mock<ILogger<MQTTLiason>>();
            var generator = new Mock<IMQTTGenerator>();
            var sharedOpts = Options.Create(new SharedOpts
            {
                Resources = new[] { test.Q }.ToList(),
            });

            generator.Setup(x => x.BuildDiscovery(test.Q.Slug, It.IsAny<string>(), It.IsAny<System.Reflection.AssemblyName>(), false))
                .Returns(new TwoMQTT.Models.MQTTDiscovery());

            var mqttLiason = new MQTTLiason(logger.Object, generator.Object, sharedOpts);
            var results = mqttLiason.Discoveries();
            var result = results.FirstOrDefault();

            Assert.IsNotNull(result, "A discovery should exist.");
        }
    }

    private static string BasicSlug = "totallyaslug";
    private static string BasicPasskey = "ABCDEFG";
    private static string BasicMAC = "11:22:33:44:55:66";
}
