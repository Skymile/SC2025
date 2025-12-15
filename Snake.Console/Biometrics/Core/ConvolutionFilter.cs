namespace Biometrics.Core;

public class ConvolutionFilter(
        params double[][] matrices
    ) : Algorithm
{
    public double Sensitivity { get; set; } = 1.0;

    public override unsafe void Apply(
        byte* read, byte* write, int length, int stride, int width, int height)
    {
        int windowSize = 3;
        const int bpp = 3;
        int border = windowSize / 2;
        int offset = bpp + stride;

        int* operations = stackalloc int[windowSize * windowSize];
        OperationMatrix.GetStandard3x3(operations, bpp, stride);

        for (int i = offset; i < length - offset; i++)
        {
            double val = 0;

            for (int m = 0; m < matrices.Length; m++)
            {
                var matrix = matrices[m];   
                double sum = matrix.Sum();
                if ((int)(sum * 100) == 0) 
                    sum = 1;
            
                for (int j = 0; j < 9; j++)
                    val += read[i + operations[j]] * matrix[j];
                val /= sum;
            }

            write[i] = (byte)double.Clamp(val * Sensitivity, 0, 255);
        }
    }
}
