using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModernLangToolsApp
{
  
   
    [XmlRoot("Jedi")]
    public class Jedi
    {
       
        private string name;
        
        private int midiChlorianCount;
        [XmlAttribute("Név")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [XmlAttribute("MidiChlorianSzám")]
        public int MidiChlorianCount
        {
            get { return midiChlorianCount; }
            set
            {
                if (value <= 35)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "A MidiChlorianCount értéke nem lehet 35 vagy annál kisebb.");
                }
                midiChlorianCount = value;
            }
        }

        public Jedi(string name, int midiChlorianCount)
        {
            Name = name;
            MidiChlorianCount = midiChlorianCount;
        }
        public Jedi()
        {
        }
        static public Jedi JediClone(Jedi j)
        {
            var serializer = new XmlSerializer(typeof(Jedi));
            using(var stream = new FileStream("jedi.txt", FileMode.Create))
            {
                serializer.Serialize(stream, j);
            }
            using (var stream2 = new FileStream("jedi.txt", FileMode.Open))
            {
                return (Jedi)serializer.Deserialize(stream2);
            }
        }
    }
}
