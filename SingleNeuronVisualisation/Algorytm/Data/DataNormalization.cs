using Algorithm.Marshalling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Data
{
    public static class DataNormalization
    {
        /// <summary>
        /// Mirrors points to make all of them land in 0-180deg quadrants.
        /// </summary>
        public static MLData MirrorPoints(MLData data)
        {
            MLData result = new(data.DatasetSize);

            foreach (var dataset in data.Datasets_train)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    if(normalized[i].Y < 0)
                        normalized[i] = normalized[i] * -1;
                result.AddDataset(dataset);
            }
            foreach (var dataset in data.Datasets_test)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    if(normalized[i].Y < 0)
                        normalized[i] = normalized[i] * -1;
                result.AddDataset(dataset, true);
            }

            return result;
        }

        /// <summary>
        /// Places all points on unit circle.
        /// </summary>
        public static MLData EqualizePointsLenght(MLData data)
        {
            MLData result = new(data.DatasetSize);

            foreach (var dataset in data.Datasets_train)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    normalized[i] = normalized[i].Normalized;
                result.AddDataset(dataset);
            }
            foreach (var dataset in data.Datasets_test)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    normalized[i] = normalized[i].Normalized;
                result.AddDataset(dataset, true);
            }

            return result;
        }

        public static MLData SortPoints(MLData data)
        {
            throw new NotImplementedException();
            MLData result = new(data.DatasetSize);

            foreach (var dataset in data.Datasets_train)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    normalized[i] = normalized[i].Normalized;
                result.AddDataset(dataset);
            }
            foreach (var dataset in data.Datasets_test)
            {
                Dataset normalized = dataset;
                for (int i = 0; i < normalized.Size; i++)
                    normalized[i] = normalized[i].Normalized;
                result.AddDataset(dataset, true);
            }

            return result;
        }
    }
}
