using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace Raven.Tests 
{
    public class TestHelpers 
    {
        public static DocumentStore Store(DocumentConvention conventions, bool embedded = true) {
            DocumentStore store = null;

            if (embedded) {
                store = new EmbeddableDocumentStore {
                    RunInMemory = true,
                    UseEmbeddedHttpServer = false,
                    Conventions = conventions
                };
            }
            else 
                store = new DocumentStore { 
                    Url = "http://localhost:8080",
                    Conventions = conventions
                };

            store.Initialize();

            var indexes = (from type in typeof(LazyTests).Assembly.GetTypes()
                           where type.IsSubclassOf(typeof(AbstractIndexCreationTask))
                           select type).ToArray();

            IndexCreation.CreateIndexes(
                new CompositionContainer(new TypeCatalog(indexes)),
                store.DatabaseCommands,
                store.Conventions);

            return store;
        }

        public static void WaitForIndex(IDocumentSession session, string index, TimeSpan span) {
            session.Query<object>(index)
                .Customize(c => c.WaitForNonStaleResultsAsOf(DateTime.UtcNow, span))
                .Take(0).ToArray();
        }
    }
}
