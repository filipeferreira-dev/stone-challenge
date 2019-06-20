using System;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Domain.Tests.Entities
{
    [TestFixture]
    public class EntityTest
    {
        [Test(Description = "On delete entity should set deleteAt date")]
        public void OnDelete()
        {
            var entity = Substitute.ForPartsOf<Entity>();
            entity.Delete().Should().BeTrue();
            entity.DeletedAt.Should().BeBefore(DateTime.UtcNow);
        }

        [Test(Description = "Should return false on try delete a entity twice")]
        public void OnDeleteTwice()
        {
            var entity = Substitute.ForPartsOf<Entity>();
            entity.Delete();
            entity.Delete().Should().BeFalse();
        }
    }
}
