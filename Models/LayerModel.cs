using System.Windows.Media;

namespace Models
{
    public class LayerModel
    {
        public string Name { get; set; }
        public Brush Color { get; set; }
        public double[] X { get; set; }
        public double[] Y { get; set; }
        public double[] Z { get; set; }
    }
}
