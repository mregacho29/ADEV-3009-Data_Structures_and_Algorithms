using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TestLibrary
{
    /// <summary>
    /// PointTest - A class for testing the Point class
    /// Point - A class that describes a point in a maze.
    ///         Holds the row and column information.
    /// Assignment:     #2
    /// Course:         ADEV-3001
    /// Date Created:   Sept. 18th, 2019
    /// 
    /// Revision Log
    /// Who         When        Reason
    /// ----------- ----------- ---------------
    /// 
    /// @author: Scott Wachal
    /// @version 1.0
    /// </summary>
    [TestFixture]
    public class PointTests
    {
        #region Constructor Tests
        /// <summary>
        /// Test the parameter constructor to ensure the values are being set properly in the created instance.
        /// </summary>
        [Test]
        public void Point_Constructor_Test()
        {
            int row = 3;
            int column = 2;
            Point point = new Point(row, column);

            ClassicAssert.That(point, Is.Not.Null);
            ClassicAssert.That(point.Column, Is.EqualTo(column));
            ClassicAssert.That(point.Row, Is.EqualTo(row));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// A method used to print row and column information from the Point
        /// </summary>
        [Test]
        public void ToString_Test()
        {
            int row = 3;
            int column = 2;
            Point point = new Point(row, column);

            string expectedString = $"[{row}, {column}]";
            ClassicAssert.That(point.ToString, Is.EqualTo(expectedString));
        }
        #endregion
    }
}