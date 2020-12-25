using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Layer
    {
        public int Size;
        public Neuron[] Neurons;
        public double[][] Weights;

        public Layer() { }

        public void Init(int size, int next) 
        {
            Size = size;
            Neurons = new Neuron[size];
            Weights = new double[size][];

            for ( int i = 0; i < size; i++) 
            {
                Neurons[i] = new Neuron();
                Weights[i] = new double[next];
            }
        }

        public void LoadConfig(double[][] data) 
        {
            Size = data.Length;
            Neurons = new Neuron[Size];
            //for (int i = 0; i < Neurons.Length; i++)
            //{
            //    Weights = data[i];
            //}
        }

        public double[][] SaveConfig() 
        {
            var data = new double[Size][];
            //for (int i = 0; i < Neurons.Length; i++)
            //{
            //    data[i] = Neurons[i].Weights;
            //}
            return data;
        }
    }
}
