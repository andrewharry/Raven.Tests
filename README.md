Raven.Tests
===========

Documented a Failing test for Raven 2.5.2700

System.InvalidOperationException was unhandled by user code
  HResult=-2146233079
  Message=Sequence contains no elements
  Source=System.Core
  StackTrace:
       at System.Linq.Enumerable.Last[TSource](IEnumerable`1 source)
       at Raven.Client.Connection.MultiGetOperation.HandleCachingResponse(GetResponse[] responses, HttpJsonRequestFactory jsonRequestFactory) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\MultiGetOperation.cs:line 131
       at Raven.Client.Connection.ServerClient.<>c__DisplayClassb8.<MultiGet>b__b6(String operationUrl) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\ServerClient.cs:line 1876
       at Raven.Client.Connection.ReplicationInformer.TryOperation[T](Func`2 operation, String operationUrl, Boolean avoidThrowing, T& result, Boolean& wasTimeout) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\ReplicationInformer.cs:line 494
       at Raven.Client.Connection.ReplicationInformer.ExecuteWithReplication[T](String method, String primaryUrl, Int32 currentRequest, Int32 currentReadStripingBase, Func`2 operation) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\ReplicationInformer.cs:line 455
       at Raven.Client.Connection.ServerClient.ExecuteWithReplication[T](String method, Func`2 operation) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\ServerClient.cs:line 174
       at Raven.Client.Connection.ServerClient.MultiGet(GetRequest[] requests) in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Connection\ServerClient.cs:line 1843
       at Raven.Client.Document.DocumentSession.ExecuteLazyOperationsSingleStep() in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Document\DocumentSession.cs:line 860
       at Raven.Client.Document.DocumentSession.ExecuteAllPendingLazyOperations() in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Document\DocumentSession.cs:line 834
       at Raven.Client.Document.DocumentSession.<>c__DisplayClass26`1.<AddLazyOperation>b__24() in c:\Builds\RavenDB-Stable\Raven.Client.Lightweight\Document\DocumentSession.cs:line 806
       at System.Lazy`1.CreateValue()
  InnerException: 
