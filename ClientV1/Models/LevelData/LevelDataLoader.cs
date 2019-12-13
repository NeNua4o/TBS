using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientV1.Models.LevelData
{
    public class LevelDataLoader
    {
        public LevelData LevelData;

        public List<Volume> Objs = new List<Volume>();

        public LevelDataLoader(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                var xmlS = new XmlSerializer(typeof(LevelData));
                LevelData = (LevelData)xmlS.Deserialize(fs);
                fs.Close();
            }



        }
    }
}
