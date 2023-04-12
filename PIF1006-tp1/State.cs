using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIF1006_tp1
{
    public class State
    {
        public bool IsFinal { get; set; }
        public string Name { get; private set; }
        public List<Transition> Transitions { get; private set; }

        public State(string name, bool isFinal)
        {
            Name = name;
            IsFinal = isFinal;
            Transitions = new List<Transition>();
        }

        //Redéfinir ToString() pour afficher dans Automate.ToString() et Transition.ToString()
        public override string ToString()
        {
            string trans0 = "";
            string trans1 = "";
            foreach (Transition t in Transitions)
            {
                if (t.Input == '0')
                {
                    trans0 += t.TransiteTo.Name;
                }
                else if (t.Input == '1')
                {
                    trans1 += t.TransiteTo.Name;
                }
            }
            return Name + "\t" +IsFinal.ToString()+ "\t" + trans0 + "\t" + trans1;
        }

        //Redéfinir Equals() et GetHashCode() pour supprimer les lignes redondantes sur l'entrée
        public override bool Equals(object obj)
        {
            return obj is State state && state.Name == Name && state.IsFinal == IsFinal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsFinal, Name);
            throw new NotImplementedException();
        }
    }
}
