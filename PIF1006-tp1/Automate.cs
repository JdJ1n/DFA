using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIF1006_tp1
{
    public class Automate
    {
        public State InitialState { get; set; }
        public State CurrentState { get; set; }

        public List<State> ListState { get; set; }

        public Automate(State initialState, List<State> allState)
        {
            InitialState = initialState;
            ListState = allState;
            Reset();
        }

        public Automate()
        {
            ListState = new List<State>();
            Reset();
        }

        public void LoadFromFile(string filePath)
        {
            string strline;
            string[] inputline;
            StreamReader sr = new StreamReader(filePath, Encoding.Default);
            //Balayer ligne par ligne et interprété en séparant chaque ligne en un tableau de strings
            while ((strline = sr.ReadLine()) != null)
            {
                inputline = strline.Split(" ");
                //Si le 1er terme est "state", on prend les arguments et on crée un état du même nom et on l'ajoute à une liste d'état
                if (inputline[0] == "state" && inputline.Length == 3 && (inputline[2] == "0" || inputline[2] == "1"))
                {
                    bool b = inputline[2].Equals("1");
                    ListState.Add(new State(inputline[1], b)); ;
                }
                //Si c'est "transition" on cherche dans la liste d'état l'état qui a le nom en 1er argument et on ajoute la transition avec les 2 autres
                //arguments à sa liste
                else if (inputline[0] == "transition" && inputline.Length == 4 && (inputline[2] == "0" || inputline[2] == "1") &&
                    ListState.Where(n => n.Name == inputline[1]).Any() && ListState.Where(n => n.Name == inputline[3]).Any())
                {
                    ListState.Where(n => n.Name == inputline[1]).First().Transitions.Add(
                        new Transition(inputline[2].Last(), ListState.Where(n => n.Name == inputline[3]).First()));
                }
                //S'il y a d'autres termes ou l'état n'est pas trouvé dans la liste, les lignes pourraient être ignorées
            }

            //Si toutes les lignes du fichier txt entier sont ignorées, on crée un automate de base
            if (ListState.Count == 0)
            {
                ListState.Add(new State("s0", true));
            }
            //Initailization
            InitialState = ListState.First();
            Reset();

        }

        public bool Validate(string input)
        {
            foreach (char ch in input)
            {
                //Vérifier s'il y a un transfert disponible dans l'état actuel, si oui, suivre le premier, sinon retourner false.
                if (CurrentState.Transitions.Where(n => n.Input == ch).Any())
                {
                    CurrentState = CurrentState.Transitions.Where(n => n.Input == ch).First().TransiteTo;
                }
                else
                {
                    return false;
                }
            }
            bool isValid = CurrentState.IsFinal;
            Reset();
            return isValid;
        }

        //Retourner un équivalent string qui décrit tous les états et la table de transitions de l'automate.
        public override string ToString()
        {
            String automatable = "";
            foreach (State s in ListState)
            {
                automatable += "\r\n" + s.ToString();
            }
            return automatable;
        }

        public void Reset() => CurrentState = InitialState;
    }
}
