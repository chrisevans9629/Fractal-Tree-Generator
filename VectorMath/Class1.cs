using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicVector;
using NUnit.Framework;

namespace VectorMath
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void SubtractAndRotate()
        {
            var vect = new Vector(4,0);
            var upvect = new Vector(4,4);
            var ed = upvect - vect;
            Assert.IsTrue(ed.X == 0 && ed.Y == 4);
            var rot = VectorUtil.Rotate(ed, -Math.PI / 4);
            var fin = upvect + rot;
            Assert.IsTrue(fin.X > 6 && fin.X < 7);
        }
        [Test]
        public void Distance()
        {
            var vect = new Vector(4, 0);
            var upvect = new Vector(4, 4);
            var ed = upvect - vect;
            Assert.IsTrue(ed.X == 0 && ed.Y == 4);
            
            Assert.IsTrue(ed.Length == 4);
        }
        [SetUp]
        public void Init()
        {
            
        }
    }
}
