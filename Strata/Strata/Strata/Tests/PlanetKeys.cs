using Makai.Input;
using Microsoft.Xna.Framework.Input;
namespace Strata.Tests
{
    class PlanetKeys :InputKeys
    {
        private Keys vUp = Keys.OemPlus;
        private Keys vDown = Keys.OemMinus;
        private Keys rowUp = Keys.Up;
        private Keys rowDown = Keys.Down;
        private Keys columnLeft = Keys.Left;
        private Keys columnRight = Keys.Right;

        public bool VUp()
        {
            return Press(vUp);
        }   // Increases frequency of Geodesic Sphere   
        public bool VDown()
        {
            return Press(vDown);
        }   // Decreases frequency of Geodesic Sphere
        public bool RowUp()
        {
            return Press(rowUp);
        }
        public bool RowDown()
        {
            return Press(rowDown);
        }
        public bool ColumnLeft()
        {
            return Press(columnLeft);
        }
        public bool ColumnRight()
        {
            return Press(columnRight);
        }
    }
}