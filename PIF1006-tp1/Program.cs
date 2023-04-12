//PIF1006-TP1-Program.cs
//MEMBRES DE L'ÉQUIPE NO 3
//Jiadong Jin JINJ86100000
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PIF1006_tp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"..\..\..\Data\test1.txt";
            bool menu_continue = true;
            while (menu_continue)
            {
                Automate automate = new();
                automate.LoadFromFile(filePath);
                Console.WriteLine("Le fichier d'automates actuellement chargé est: " + filePath);
                Console.WriteLine("\r\nMenu");
                Console.WriteLine("Veuillez entrer un numéro pour sélectionner la fonction que vous voulez utiliser:");
                Console.WriteLine("(1) Charger un fichier en spécifiant le chemin (relatif) du fichier.");
                Console.WriteLine("(2) Afficher la liste des états et la liste des transitions.");
                Console.WriteLine("(3) Soumettre un input en tant que chaine de 0 ou de 1 et valider.");
                Console.WriteLine("(4) Quitter l'application.");
                switch ((char)Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.WriteLine("\r\nEntrer en spécifiant le chemin (relatif) du fichier:");
                        string pathGet = Console.ReadLine();
                        //Vérifier le chemin relatif de l'entrée
                        if (File.Exists(pathGet))
                        {
                            filePath = pathGet;
                            Console.WriteLine("Chargé avec succès\r\n");
                        }
                        else { Console.WriteLine("Chemin relatif incorrect.\r\n"); }
                        break;
                    case '2':
                        //Afficher proprement la liste des états et la liste des transitions
                        Console.Write("\r\n" + "State" + "\t" + "IsFinal" + "\t" + "Input0" + "\t" + "Input1");
                        Console.WriteLine(automate.ToString() + "\r\n");
                        break;
                    case '3':
                        //Soumettre un input en tant que chaîne de 0 ou de 1
                        Console.WriteLine("\r\nVeuillez entrer une chaine de 0 ou de 1:");
                        bool varchar_validated = false;
                        do
                        {
                            string varchar_input = Console.ReadLine();
                            //Assurez-vous que la chaine passée ne contient que ces caractères avant d'envoyer
                            if (varchar_input.Length == varchar_input.Where(n => n == '1' || n == '0').Count())
                            {
                                switch (automate.Validate(varchar_input))
                                {
                                    case true:
                                        Console.WriteLine("Valid");
                                        break;
                                    case false:
                                        Console.WriteLine("NotValid");
                                        break;
                                }
                                varchar_validated = true;
                            }
                            else
                            {
                                //Indiquer un message si c'est rejeté et demander une nouvelle entrée
                                Console.WriteLine("La chaine saisie ne correspond pas à la spécification, veuillez la saisir à nouveau:");
                            }
                        } while (!varchar_validated);
                        Console.WriteLine();
                        break;
                    case '4':
                        //Quitter l'application
                        menu_continue = false;
                        break;
                    default:
                        Console.WriteLine("\r\n" + "Caractère non valide.\r\n");
                        break;
                }
            }
        }
    }
}
