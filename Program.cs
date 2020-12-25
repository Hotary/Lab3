using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static NeuralNetwork nn;
        static Random rnd = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {
            var size = new int[] { 3, 5, 5, 3 };
            nn = new NeuralNetwork(size);
            nn.Activation = (x) => { return 1 / (1 + Math.Exp(-x)); };
            nn.Derivative = (x) => { return x * (1 - x); };
            foreach(var l in nn.Layers)
            {
                foreach (var n in l.Neurons)
                {
                    n.Base = rnd.NextDouble() * 2 - 1;
                }
            }
            for (int i = 1; i < nn.Layers.Length; i++) 
            {
                for (int j = 0; j < nn.Layers[i].Weights.Length; j++) 
                {
                    for (int k = 0; k < nn.Layers[i].Weights[j].Length; k++)
                        nn.Layers[i].Weights[j][k] = rnd.NextDouble() * 2 - 1;
                }
            }
            for (int i = 0; i < 100000; i++)
                Learning();
            while (true)
            {
                Console.WriteLine("Input parametrs: ");
                for (int i = 0; i < nn.Input.Size; i++)
                {
                    nn.Input.Neurons[i].Output = int.Parse(Console.ReadLine());
                }
                nn.Work();
                Console.WriteLine("Results: ");
                for (int i = 0; i < nn.Output.Size; i++)
                {
                    Console.WriteLine("Class {0} = {1}", i, nn.Output.Neurons[i].Output);
                }
                nn.Clear();
            }
        }

        static void Learning()
        {
            nn.Input.Neurons[0].Output = 1;
            nn.Input.Neurons[1].Output = 1;
            nn.Input.Neurons[2].Output = 0;
            var target = new double[] { 1, 0, 0 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 1;
            nn.Input.Neurons[1].Output = 0;
            nn.Input.Neurons[2].Output = 1;
            target = new double[] { 1, 0, 0 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 0;
            nn.Input.Neurons[1].Output = 1;
            nn.Input.Neurons[2].Output = 1;
            target = new double[] { 0, 1, 0 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 0;
            nn.Input.Neurons[1].Output = 1;
            nn.Input.Neurons[2].Output = 0;
            target = new double[] { 0, 0, 1 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 0;
            nn.Input.Neurons[1].Output = 0;
            nn.Input.Neurons[2].Output = 1;
            target = new double[] { 0, 0, 1 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 0;
            nn.Input.Neurons[1].Output = 1;
            nn.Input.Neurons[2].Output = 0;
            target = new double[] { 0, 0, 1 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 1;
            nn.Input.Neurons[1].Output = 1;
            nn.Input.Neurons[2].Output = 1;
            target = new double[] { 1, 0, 0 };
            nn.Work();
            nn.BackPropagation(target);
            nn.Input.Neurons[0].Output = 0;
            nn.Input.Neurons[1].Output = 0;
            nn.Input.Neurons[2].Output = 0;
            target = new double[] { 0, 1, 0 };
            nn.Work();
            nn.BackPropagation(target);
        }
    }
}
