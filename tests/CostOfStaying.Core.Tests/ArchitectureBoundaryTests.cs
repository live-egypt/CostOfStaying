using NUnit.Framework;
using System.Reflection;
using System.Linq;

namespace CostOfStaying.Core.Tests
{
    /// <summary>
    /// Gate-3.1 — NetArchTest lite without external package: Core must not reference Unity/FishNet.
    /// Full NetArchTest enforcment in CI when package available; this reflection check is deterministic offline.
    /// </summary>
    [TestFixture]
    public class ArchitectureBoundaryTests
    {
        [Test]
        public void Core_Does_Not_Reference_Unity_Or_FishNet()
        {
            var core = typeof(CostOfStaying.Core.BetrayalLedger).Assembly;
            var refs = core.GetReferencedAssemblies().Select(a => a.Name).ToList();
            Assert.That(refs, Does.Not.Contain("UnityEngine"), "Core must not reference UnityEngine");
            Assert.That(refs, Does.Not.Contain("FishNet"), "Core must not reference FishNet");
        }

        [Test]
        public void Domain_Does_Not_Reference_Unity_Or_FishNet()
        {
            var domain = typeof(CostOfStaying.Domain.Match).Assembly;
            var refs = domain.GetReferencedAssemblies().Select(a => a.Name).ToList();
            Assert.That(refs, Does.Not.Contain("UnityEngine"));
            Assert.That(refs, Does.Not.Contain("FishNet"));
        }
    }
}
