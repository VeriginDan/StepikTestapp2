using System;
using System.Collections.Generic;


public class MainClass
{
    public static void Main()
    {
        var A = new Matrix();
        A.Read();

        Matrix.Transpose(A).Write();

    }
    public class Matrix
    {
        public int Rows, Columns;
        double[,] Data;

        public void Read()
        {
            int rows, columns;
            string[] input = Console.ReadLine().Split(" ");

            rows = int.Parse(input[0]);
            if (rows > -1) Rows = rows;

            columns = int.Parse(input[1]);
            if (columns > -1) Columns = columns;

            Data = new double[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                input = Console.ReadLine().Split(" ");
                for (int j = 0; j < Columns; j++)
                    Data[i, j] = double.Parse(input[j]);
            }

        }

        public void Write()
        {
            List<double> output = new(Columns);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    output.Add(Data[i, j]);
                Console.WriteLine(string.Join(" ", output));
                output.Clear();
            }
        }

        public static Matrix Multiply(Matrix A, double n)
        {
            Matrix result = new()
            {
                Rows = A.Rows,
                Columns = A.Columns
            };
            result.Data = new double[result.Rows, result.Columns];

            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Columns; j++)
                    result.Data[i, j] = A.Data[i, j] * n;

            return result;
        }

        public static Matrix Sum(Matrix A, Matrix B)
        {
            Matrix result = new()
            {
                Rows = A.Rows,
                Columns = A.Columns
            };
            result.Data = new double[result.Rows, result.Columns];

            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Columns; j++)
                    result.Data[i, j] += A.Data[i, j] + B.Data[i, j];

            return result;
        }
        public static Matrix Multiply(Matrix A, Matrix B)
        {
            Matrix result = new()
            {
                Rows = A.Rows,
                Columns = B.Columns
            };
            result.Data = new double[result.Rows, result.Columns];

            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Columns; j++)
                {
                    result.Data[i, j] = 0;
                    for (int k = 0; k < A.Columns; k++) result.Data[i, j] += A.Data[i, k] * B.Data[k, j];
                }

            return result;
        }

        public static Matrix Transpose(Matrix A)
        {
            Matrix result = new()
            {
                Rows = A.Columns,
                Columns = A.Rows
            };
            result.Data = new double[result.Rows, result.Columns];

            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Columns; j++)
                    result.Data[i, j] = A.Data[j, i];

            return result;
        }
    }
}


