using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Linq;

namespace Raven.Tests 
{
    [TestClass]
    public class LazyTests 
    {
        [TestMethod]
        public void Passing_not_embedded_with_empty_convention() {
            var store = TestHelpers.Store(
                embedded: false, 
                conventions: new DocumentConvention { }
            );
            Populate(store);

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }
        }

        [TestMethod]
        public void Passing_embedded_disabled_profiling_false() {
            var store = TestHelpers.Store(
                embedded: true,
                conventions: new DocumentConvention {
                    DisableProfiling = false
                });
            Populate(store);

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }
        }

        [TestMethod]
        public void Passing_not_embedded_with_disabled_profiling_true() {
            var store = TestHelpers.Store(
                embedded: false,
                conventions: new DocumentConvention {
                    DisableProfiling = true
                });
            Populate(store);

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }
        }

        [TestMethod]
        public void Passing_when_not_using_lazy() {
            var store = TestHelpers.Store(
                embedded: false,
                conventions: new DocumentConvention {
                    DisableProfiling = false
                });
            Populate(store);

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().ToArray();
                Assert.IsTrue(dump.Length > 0);
            }

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().ToArray();
                Assert.IsTrue(dump.Length > 0);
            }
        }

        [TestMethod]
        public void Failing_when_not_embedded_with_disabled_profiling_false() {
            var store = TestHelpers.Store(
                embedded: false,
                conventions: new DocumentConvention {
                    DisableProfiling = false
                });
            Populate(store);

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }

            using (var session = store.OpenSession()) {
                var dump = session.Query<Simple, Simple_Index>().Lazily().Value.ToArray();
                Assert.IsTrue(dump.Length > 0);
            }
        }

        private static void Populate(IDocumentStore store) {
            using (var session = store.OpenSession()) {
                RavenQueryStatistics stats = null;
                session.Query<Simple, Simple_Index>().Statistics(out stats).Take(0).ToArray();
                if (stats.TotalResults > 0) return;
            }

            using (var session = store.OpenSession()) {
                for (int i = 0; i < 1000; i++)
                    session.Store(new Simple());
                session.SaveChanges();
            }

            using (var session = store.OpenSession()) {
                TestHelpers.WaitForIndex(session, "Simple/Index", TimeSpan.FromSeconds(20));
            }
        }
    }
}
