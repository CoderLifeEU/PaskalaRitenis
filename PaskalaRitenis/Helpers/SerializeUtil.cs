using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PaskalaRitenis.Helpers
{
    public static class SerializeUtils
    {
        /// <summary>
        /// Deserialize Object From XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializeFromXML<T>(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (FileStream fs = File.OpenRead(path))
            {
                return (T)xs.Deserialize(fs);
            }
        }
    }
}