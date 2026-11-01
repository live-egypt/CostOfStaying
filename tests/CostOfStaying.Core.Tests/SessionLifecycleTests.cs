namespace CostOfStaying.Core.Tests
{
    using CostOfStaying;
    using CostOfStaying.Core.Domain;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SessionLifecycleTests
    {
        [Test]
        public void Constructor_RejectsInvalidSessionId()
        {
            Assert.Throws<System.ArgumentException>(() => new SessionLifecycle(default));
        }

        [Test]
        public void NewSession_StartsPendingWithoutActor()
        {
            var session = new SessionLifecycle(new SessionId(1));
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
            Assert.That(session.ActorId, Is.Null);
        }

        [Test]
        public void Authenticate_AssociatesActorExactlyOnce()
        {
            var session = new SessionLifecycle(new SessionId(1));
            Result result = session.TryAuthenticate(new ActorId(42));

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Authenticated));
            Assert.That(session.ActorId, Is.EqualTo(new ActorId(42)));
        }

        [Test]
        public void Authenticate_RejectsInvalidActorWithoutMutation()
        {
            var session = new SessionLifecycle(new SessionId(1));
            Result result = session.TryAuthenticate(default);

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidArgument));
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
            Assert.That(session.ActorId, Is.Null);
        }

        [Test]
        public void Authenticate_RejectsSecondAssociationWithoutReplacingActor()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));

            Result result = session.TryAuthenticate(new ActorId(99));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.ActorId, Is.EqualTo(new ActorId(42)));
        }

        [Test]
        public void Disconnect_RequiresAuthenticatedSession()
        {
            var session = new SessionLifecycle(new SessionId(1));
            Assert.That(session.TryDisconnect().ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.State, Is.EqualTo(SessionState.Pending));
        }

        [Test]
        public void Disconnect_PreservesActorAssociation()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));

            Result result = session.TryDisconnect();

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Disconnected));
            Assert.That(session.ActorId, Is.EqualTo(new ActorId(42)));
        }

        [Test]
        public void DisconnectedSession_CannotBeReauthenticated()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));
            session.TryDisconnect();

            Result result = session.TryAuthenticate(new ActorId(42));

            Assert.That(result.ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.State, Is.EqualTo(SessionState.Disconnected));
        }

        [Test]
        public void Close_WorksFromPending()
        {
            var session = new SessionLifecycle(new SessionId(1));
            Assert.That(session.TryClose().IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Closed));
            Assert.That(session.ActorId, Is.Null);
        }

        [Test]
        public void Close_WorksFromAuthenticatedAndPreservesActor()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));

            Assert.That(session.TryClose().IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Closed));
            Assert.That(session.ActorId, Is.EqualTo(new ActorId(42)));
        }

        [Test]
        public void Close_WorksFromDisconnected()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));
            session.TryDisconnect();

            Assert.That(session.TryClose().IsSuccess, Is.True);
            Assert.That(session.State, Is.EqualTo(SessionState.Closed));
        }

        [Test]
        public void ClosedSession_IsTerminal()
        {
            var session = new SessionLifecycle(new SessionId(1));
            session.TryAuthenticate(new ActorId(42));
            session.TryClose();

            Assert.That(session.TryAuthenticate(new ActorId(42)).ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.TryDisconnect().ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.TryClose().ErrorCode, Is.EqualTo(CoreErrorCode.InvalidState));
            Assert.That(session.State, Is.EqualTo(SessionState.Closed));
        }

        [Test]
        public void SessionInstances_AreIndependent()
        {
            var first = new SessionLifecycle(new SessionId(1));
            var second = new SessionLifecycle(new SessionId(2));
            first.TryAuthenticate(new ActorId(42));

            Assert.That(first.State, Is.EqualTo(SessionState.Authenticated));
            Assert.That(second.State, Is.EqualTo(SessionState.Pending));
            Assert.That(second.ActorId, Is.Null);
        }
    }
}
