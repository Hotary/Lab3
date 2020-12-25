using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class NeuralNetwork
    {
        public Layer[] Layers;
        public Layer Input => Layers.First();
        public Layer Output => Layers.Last();
        public double LearningRate = 0.01;
        public Func<double, double> Activation;
        public Func<double, double> Derivative;

        public NeuralNetwork(int[] size) 
        {
            Layers = new Layer[size.Length];
            int prev_size = size.Length;
            for(int i = 0; i < size.Length; i++) 
            {
                Layers[i] = new Layer();
                int next_size = 0;
                if (i + 1 < size.Length) next_size = size[i + 1]; 
                Layers[i].Init(size[i], next_size);
                prev_size = size[i];
            }
        }

        public void Work() 
        {
            for(int i = 1; i < Layers.Length; i++) 
            {
                var l = Layers[i];
                var pl = Layers[i - 1];

                for(int k = 0; k < l.Size; k++)
                {
                    var n = l.Neurons[k];
                    for (int j = 0; j < pl.Size; j++)
                    {
                        n.Output += pl.Weights[j][k] * pl.Neurons[j].Output;
                    }
                    n.Output += n.Base;
                    n.Output = Activation(n.Output);
                }
            }
        }

        public void Clear() 
        {
            foreach(var l in Layers) 
            {
                foreach(var n in l.Neurons) 
                {
                    n.Output = 0;
                }
            }
        }

        public void LoadConfig(double[][][] data) 
        {
            for (int i = 0; i < data.Length; i++)
            {
                Layers[i].LoadConfig(data[i]);
            }
        }

        public double[][][] SaveConfig() 
        {
            var data = new double[Layers.Length][][];
            for(int i = 0; i < data.Length; i++) 
            {
                data[i] = Layers[i].SaveConfig();
            }
            return data;
        }

        public void BackPropagation(double[] targets)
        {
            double[] errors = new double[Output.Size];
            for (int i = 0; i < Output.Size; i++)
            {
                errors[i] = targets[i] - Output.Neurons[i].Output;
            }
            for (int k = Layers.Length - 2; k >= 0; k--)
            {
                Layer l = Layers[k];
                Layer l1 = Layers[k + 1];
                double[] errorsNext = new double[l.Size];
                double[] gradients = new double[l1.Size];
                for (int i = 0; i < l1.Size; i++)
                {
                    gradients[i] = errors[i] * Derivative(l1.Neurons[i].Output);
                    gradients[i] *= LearningRate;
                }
                double[][] deltas = new double[l1.Size][];
                for (int i = 0; i < l1.Size; i++)
                {
                    deltas[i] = new double[l.Size]; 
                    for (int j = 0; j < l.Size; j++)
                    {
                        deltas[i][j] = gradients[i] * l.Neurons[j].Output;
                    }
                }
                for (int i = 0; i < l.Size; i++)
                {
                    errorsNext[i] = 0;
                    for (int j = 0; j < l1.Size; j++)
                    {
                        errorsNext[i] += l.Weights[i][j] * errors[j];
                    }
                }
                errors = new double[l.Size];
                errorsNext.CopyTo(errors, 0);
                double[][] weightsNew = new double[l.Size][];
                for (int i = 0; i < l.Size; i++)
                    weightsNew[i] = new double[l1.Size];
                for (int i = 0; i < l1.Size; i++)
                {
                    for (int j = 0; j < l.Size; j++)
                    {
                        weightsNew[j][i] = l.Weights[j][i] + deltas[i][j];
                    }
                }

                l.Weights = weightsNew;

                for (int i = 0; i < l1.Size; i++)
                {
                    l1.Neurons[i].Base += gradients[i];
                }
            }
        }

    }
}
