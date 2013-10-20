using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Linq;

namespace Raven.Tests 
{
    [TestClass]
    public class ConcurrencyTests 
    {
        [TestMethod]
        public void Concurrency_Failing_Test() {
            var store = TestHelpers.Store(
                new DocumentConvention(),
                embedded: false
            );
            
            using (var session = store.OpenSession()) {
                session.Advanced.UseOptimisticConcurrency = true;
                Simple simple = new Simple { Id = 1, key = "New", stamp = (int)DateTime.UtcNow.Ticks };
                session.Store(simple);
                session.SaveChanges();
            }

            using (var session = store.OpenSession()) {
                session.Advanced.UseOptimisticConcurrency = false;
                Simple simple = new Simple { Id = 1, key = "Override", stamp = (int)DateTime.UtcNow.Ticks };
                session.Store(simple);
                session.SaveChanges();
            }
        }
    }
}
