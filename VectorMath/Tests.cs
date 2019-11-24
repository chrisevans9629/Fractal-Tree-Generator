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
    public class PenToolTests
    {
        private PenTool penTool;
        [SetUp]
        public void Setup()
        {
            penTool = new PenTool();
        }

        [Test]
        public void DrawSquare()
        {
            for (int i = 0; i < 4; i++)
            {
                penTool.Draw(10);
                penTool.Rotate(new DegreeAngle(90));
            }

            Assert.AreEqual(4, penTool.Lines.Count);

            var last = penTool.Lines.Last();
            
            Assert.AreEqual(0, last.X2);
            Assert.AreEqual(0, last.Y2);
            Assert.AreEqual(0, last.X1);
            Assert.AreEqual(10, last.Y1);
        }

        [Test]
        public void DrawStraightLine()
        {
           var l = penTool.Draw(10);

           Assert.AreEqual(10, l.X2);
        }
    }


    [TestFixture]
    public class Tests
    {
        [Test]
        public void ConvertAngle()
        {
            var pen = new PenTool();
            Assert.AreEqual( Math.Round(Math.PI,2), Math.Round(pen.ToRadian(new DegreeAngle(180)).Value,2));
        }

    

        [Test]
        public void SubtractAndRotate()
        {
            var vect = new Vector(4, 0);
            var upvect = new Vector(4, 4);
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
