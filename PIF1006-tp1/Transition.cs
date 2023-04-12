using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PIF1006_tp1
{
    public class Transition
    {
        public char Input { get; set; }
        public State TransiteTo { get; set; }

        public Transition(char input, State transiteTo)
        {
            Input = input;
            TransiteTo = transiteTo;
        }

        //Redéfinir ToString() pour afficher dans Automate.ToString()
        public override string ToString()
        {
            return Input.ToString() + TransiteTo.Name;
        }

        //Redéfinir Equals() et GetHashCode() pour supprimer les lignes redondantes sur l'entrée
        public override bool Equals(object obj)
        {
            return obj is Transition trans && trans.Input == Input && trans.TransiteTo.Equals(TransiteTo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Input, TransiteTo);
            throw new NotImplementedException();
        }


    }
}
