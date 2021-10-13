using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace Laba1
{
    [Serializable]
    class ParamSerialized
    {
        private int A;
        private int B;
        private int m;
        private int Y0;
        private string path = "test.dat";
       public ParamSerialized()
        {
            A = 15;
            B = 17;
            m = 4096;
            Y0 = 4003;

        }
        public void setA(int a)
        {
            A = a;
        }
        public void setB(int b)
        {
            B = b;
        }
        public void setM(int M)
        {
            m = M;
        }
        public void setY0(int y0)
        {
            Y0 = y0;
        }

        public int getA()
        {
            return A;
        }
        public int getB()
        {
            return B;
        }
        public int getM()
        {
            return m;
        }
        public int getY0()
        {
            return Y0;
        }

        public void serialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);

                MessageBox.Show("Параметры сохранены!");
            }
        }
        public object deserialize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileInfo file = new FileInfo(path);
            if (file.Exists && file.Length != 0)
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (ParamSerialized)formatter.Deserialize(fs);
                }
            }
            else
            {
                return this;
            }
        }
     
    }
}
