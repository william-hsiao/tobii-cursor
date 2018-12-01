using System;
using Tobii.Interaction;
using Tobii.Interaction.Framework;
using System.Runtime.InteropServices;

namespace Interaction_Streams_101 {
 
    public class Program {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public static double Lerp(double firstFloat, double secondFloat, double by) {
            return firstFloat * by + secondFloat * (1 - by);
        }

        public static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, double by) {
            double retX = Lerp(firstVector.X, secondVector.X, by);
            double retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }

        public static void Main(string[] args) {
           
            var host = new Host();
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();
            Vector2 prevPoint = new Vector2(0,0), currentPoint;

            gazePointDataStream.GazePoint((x, y, ts) => {
                Console.WriteLine("Timestamp: {0}\t X: {1} Y:{2}", ts, x, y);
                currentPoint = Lerp(new Vector2(x, y), prevPoint, 0.08);
                SetCursorPos((int)currentPoint.X, (int)currentPoint.Y);
                prevPoint = currentPoint;
            });
            
            Console.ReadKey();
            host.DisableConnection();


        }
    }
}

