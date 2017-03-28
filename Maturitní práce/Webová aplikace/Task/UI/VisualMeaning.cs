using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.UI
{
    public class VisualMeaning : Attribute
    {
        public string Meaning { get; private set; }

        public VisualMeaning(string meaning)
        {
            Meaning = meaning;
        }
    }
}
