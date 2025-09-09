using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernLangToolsApp
{
    public delegate void CouncilChanged(string message);
    public class JediCollection
    {
        private List<Jedi> jediList = new List<Jedi>();
        public JediCollection() { }

        public event CouncilChanged change;
        public void Add(Jedi j)
        {
            jediList.Add(j);
            change?.Invoke($"A tanácshoz hozzá lett adva egy új tag: {j.Name}");
        }
        public void Remove()
        {
            jediList.RemoveAt(jediList.IndexOf(jediList.LastOrDefault()));
            change?.Invoke(jediList.Count() == 0 ? "A tanácsból el lett távolítva az utolsó tag is.": "A tanácsból el lett távolítva a legutolsó tag.");
        }
        private static bool LowerThan530(Jedi jedi)
        {
            return jedi.MidiChlorianCount < 530;
        }
        public List<Jedi> FindAll530_Delegate()
        {
            return jediList.FindAll(LowerThan530);
        }
        public List<Jedi> FindAll1000_Lambda()
        {
            return jediList.FindAll(j => j.MidiChlorianCount < 1000);
        }

    }
    
}
