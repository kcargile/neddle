using System;
using Xunit;

namespace Neddle.Tests
{
    public class CourseFixture : NeddleObjectFixture<Course>
    {
        internal override Course MockEntity
        {
            get { throw new NotImplementedException(); }
        }

        [Fact]
        public override void Equals()
        {
            
        }

        [Fact]
        public override void Validate()
        {
            
        }
    }
}