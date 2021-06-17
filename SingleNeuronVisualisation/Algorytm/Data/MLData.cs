using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// Dependancy to read and write to .arff files.
using ArffTools;


namespace Algorithm.Data
{
    public class MLData
    {
        // Nie potrzebne...
        //public string DecissionAttributeName { get; set; }
        //public List<string> attributesNames { }
        public List<Dataset> Datasets_train { get; }
        public List<Dataset> Datasets_test { get; }
        public int DatasetSize { get; }

        /// <summary>
        /// Private constructor for initializing lists.
        /// </summary>
        MLData()
        {
            Datasets_train = new List<Dataset>();
            Datasets_test = new List<Dataset>();
        }

        /// <summary>
        /// Initializes empty data object with specified datasetSize.
        /// </summary>
        /// <param name="datasetSize"></param>
        public MLData(int datasetSize)
            : this()
        {
            DatasetSize = datasetSize;
        }

        /// <summary>
        /// Loads data from filename specified as only argument
        /// </summary>
        /// <param name="filename"> Data source. </param>
        public MLData(string filename)
            : this()
        {
            using (ArffReader reader = new ArffReader(filename))
            {
                ArffHeader header = reader.ReadHeader();
                //DecissionAttributeName = header.RelationName;
                int attributesCount = header.Attributes.Count;
                // If is even.
                if (attributesCount % 2 is 0)
                    throw new ArgumentException("Attribute count is even, programm is unable to parse points.");
                DatasetSize = attributesCount / 2;

                IEnumerable<object> rawDataset;
                while ((rawDataset = reader.ReadInstance()) is not null)
                {
                    // if (rawDataset.Count() != DatasetSize * 2 + 1)
                    //     throw new ArgumentException();

                    Datasets_train.Add(new Dataset(rawDataset.Cast<double>().ToArray()));
                }
            }
        }
        public void SaveToFile(string filename)
        {
            using (ArffWriter writer = new ArffWriter(filename))
            {
                writer.WriteRelationName("angle");

                for (int i = 0; i < DatasetSize; i++)
                {
                    writer.WriteAttribute(new ArffAttribute($"point{i}X", ArffAttributeType.Numeric));
                    writer.WriteAttribute(new ArffAttribute($"point{i}Y", ArffAttributeType.Numeric));
                }
                writer.WriteAttribute(new ArffAttribute("angle", ArffAttributeType.Numeric));

                foreach (var dataset in Datasets_train)
                    writer.WriteInstance(dataset.RawData.Cast<object>().ToArray());
                foreach (var dataset in Datasets_test)
                    writer.WriteInstance(dataset.RawData.Cast<object>().ToArray());
            }
        }

        public void AddDataset(Dataset dataset, bool asTestDataset = false)
            => (asTestDataset ? Datasets_test : Datasets_train).Add(dataset);
        /// <summary> Moves dataset of specified index to other dataset. </summary>
        public void MoveDataset(int index, bool toTestDataset = false)
        {
            (!toTestDataset ? Datasets_test : Datasets_train)
                .Add((toTestDataset ? Datasets_test : Datasets_train)[index]);
            RemoveDataset(index, toTestDataset);
        }
        /// <summary> Remove dataset at specified index. </summary>
        public void RemoveDataset(int index, bool fromTestDataset = false)
            => (fromTestDataset ? Datasets_test : Datasets_train).RemoveAt(index);
    }
}
