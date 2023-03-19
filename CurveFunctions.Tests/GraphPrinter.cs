using System.Text;

namespace CurveFunctions.Tests
{
    internal class GraphPrinter
    {
        public Func<IInterpolatorInput, float> Function { get; set; }

        private IInterpolatorInput input;
        public IInterpolatorInput Input {
            get => input;
            set
            {
                input = value;
                Integrate();
            }
        }

        private int size { get; }
        private int chartSize
        {
            get => size + 1;
        }

        private string[,] graph;
        
        private ITestOutputHelper output { get; }

        public GraphPrinter(ITestOutputHelper output, int size) 
        {
            this.size = size;
            this.output = output;
            graph = Initialize();

            //initialize to empty but valid values to avoid talking about nulls all the time.
            input = new Interpolator.EmptyInput();
            Function = (IInterpolatorInput i) => 0f;
        }


        private string[,] Initialize()
        {
            string[,] emptyGraph = new string[chartSize, chartSize];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    emptyGraph[x, y] = "--";
                }
            }

            //axis labels
            for (int x = 0; x < chartSize; x++)
            {
                emptyGraph[x, chartSize - 1] = x.ToString("00");
            }
            for (int y = 0; y < chartSize; y++)
            {
                emptyGraph[chartSize - 1, y] = y.ToString("00");
            }

            return emptyGraph;
        }

        private void Integrate()
        {
            //draw the curve
            for (int t = 0; t < size; t++)
            {
                Input.Time = (float) t / size;
                int xIndex = t;
                int yIndex = Convert.ToInt32(Function.Invoke(Input));
                graph[xIndex, yIndex] = "()";
            }
        }

        public void Print()
        {
            var graphBuilder = new StringBuilder();
            for (int y = chartSize - 1; y > -1; y--)
            {
                for (int x = 0; x < chartSize; x++)
                {
                    graphBuilder.Append(graph[x, y]);
                }
                graphBuilder.AppendLine();
            }
            output.WriteLine(graphBuilder.ToString());
        }
    }
}
